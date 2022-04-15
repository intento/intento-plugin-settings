using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Intento.MT.Plugin.PropertiesForm.Services;
using Intento.SDK.DI;
using IntentoMTPlugin;
using MemoQ.Addins.Common.DataStructures;
using MemoQ.MTInterfaces;

namespace IntentoMemoQMTPlugin
{
	/// <summary>
	/// Session that perform actual translation or storing translations. Created on a segment-by-segment basis, or once for batch operations.
	/// </summary>
	public partial class IntentoMTSession : ISession, ISessionForStoringTranslations
	{
		private static readonly Regex AttributeRegex = new("(\\S+)\\s*=\\s*([']|[\"])\\s*([\\W\\w]*?)\\s*\\2");
		
		/// <summary>
		/// Access to Intento API
		/// </summary>
		private readonly IntentoMTServiceHelper serviceHelper;

		/// <summary>
		/// The source language.
		/// </summary>
		private readonly string srcLangCode;

		/// <summary>
		/// The target language.
		/// </summary>
		private readonly string trgLangCode;

		/// <summary>
		/// Options of the plugin.
		/// </summary>
		private readonly IntentoMTOptions options;
		
		private IRemoteLogService RemoteLogService => serviceHelper.Locator?.Resolve<IRemoteLogService>();

		public IntentoMTSession(IntentoMTServiceHelper serviceHelper, string srcLangCode, string trgLangCode,
			IntentoMTOptions options)
		{
			this.options = options;
			this.srcLangCode = srcLangCode;
			this.trgLangCode = trgLangCode;
			this.options = options;
			this.serviceHelper = serviceHelper;
			serviceHelper.Format = options.GeneralSettings.ProviderFormats;
		}

		#region ISession Members

		private string GetTaggedFormat()
		{   // Return format for non-smart routing calls
			var format = serviceHelper.Format;
			// Check information from InentoAPI
			if (format == null)
				return "html";
			format = format.ToLower();
			if (format.Contains("html"))
				return "html";
			if (format.Contains("xml"))
				return "xml";
			return "html";
		}

		/// <summary>
		/// Translates a single segment, possibly using a fuzzy TM hit for improvement
		/// </summary>
		public TranslationResult TranslateCorrectSegment(Segment segm, Segment tmSource, Segment tmTarget)
		{
			var results = TranslateCorrectSegment(new[] { segm }, new[] { tmSource }, new[] { tmTarget });
			var result = results[0];
			return result;
		}

		private void LogSegmentData(IEnumerable<Segment> segs)
		{
			foreach (var seg in segs)
			{
				var str = new List<string>
				{
					$"Length {seg.Length}, NumberOfInlineTags {seg.NumberOfInlineTags}, NumberOfStructuralTags {seg.NumberOfStructuralTags}, NumberOfTags {seg.NumberOfTags}\r\n",
					$"PlainText: {seg.PlainText}",
					$"PlainTextWithSpacesForTags: {seg.PlainTextWithSpacesForTags}"
				};

				RemoteLogService.Write(PluginInfo.LogIdentificator, "segs data", string.Join("\r\n", str));
			}
		}

		private bool IsImplicitTagged(IEnumerable<Segment> segs)
		{
			return segs.Any(i => i.PlainText != ConvertSegmentToString(i, true));
		}

		private bool IsTagged(IEnumerable<Segment> segs)
		{
			var tagged = segs.Any(i => i.ITags.Length != 0);			
			return tagged;
		}

		/// <summary>
		/// Translates multiple segments, possibly using a fuzzy TM hit for improvement
		/// </summary>
		public TranslationResult[] TranslateCorrectSegment(Segment[] segs, Segment[] tmSources, Segment[] tmTargets)
		{
			LogSegmentData(segs);

			var results = new TranslationResult[segs.Length];
			Exception ex = null;
			
			try
			{
				// Check for necessity to use tagged translation
				var tagged = IsTagged(segs) || IsImplicitTagged(segs);

				var format = GetTaggedFormat();
				//  ConvertSegment2Xml or ConvertSegment2Html
				string routing = null;
				//provider_id available
				if (string.IsNullOrEmpty(options.GeneralSettings.ProviderId))
				{
					routing = string.IsNullOrWhiteSpace(options.GeneralSettings.Routing)
						? PluginInfo.DefaultRoutingName
						: options.GeneralSettings
							.Routing; // smart routing. Format is provided by special smart routing table
				}

				RemoteLogService.Write(PluginInfo.LogIdentificator, "2: intentoTagReplacement",
					$"{options.GeneralSettings.IntentoTagReplacement}, format: {format}, tagged: {tagged}");

				var data = segs.Select(i => ConvertSegmentToString(i, tagged)).ToList();

				var resultBatchTranslate =
					serviceHelper.BatchTranslate(options, data, srcLangCode, trgLangCode, format, routing);

				var resultLength = resultBatchTranslate.ResultTranslate.Count;
				if (segs.Length != resultLength)
				{
					throw new Exception(
						$"Invalid result from Intento API: segments count is different {segs.Length}/{resultLength}");
				}

				for (var i = 0; i < resultLength; i++)
				{
					try
					{
						var res = resultBatchTranslate.ResultTranslate[i];
						//  if the segment returned null, then an error occurred while translating
						if (res == null)
						{
							continue;
						}

						try
						{
							results[i] = ConvertStringToSegment(res, segs[i].ITags, tagged);
						}
						catch (Exception)
						{
							res = AttributeRegex.Replace(res, (m) =>
							{
								if (m.Groups.Count < 4)
								{
									return m.Value;
								}

								var attrName = m.Groups[1].Value;
								var attrValue = System.Web.HttpUtility.HtmlEncode(m.Groups[3].Value);
								return $"{attrName}=\"{attrValue}\"";
							});
							results[i] = ConvertStringToSegment(res, segs[i].ITags, tagged);
						}

						results[i].Info = string.IsNullOrWhiteSpace(resultBatchTranslate.ViaProvider)
							? null
							: $"translated using {resultBatchTranslate.ViaProvider}";
					}
					catch (Exception e)
					{
						results[i] ??= new TranslationResult();
						results[i].Exception = new MTException(e.Message, e.Message, e);
						RemoteLogService.Write(PluginInfo.LogIdentificator,
							"IntentoMTSession.TranslateCorrectSegment exception",
							$"{e.Message}\r\n{e.StackTrace}");
					}
				}
			}
			catch (Exception e)
			{
				ex = e;
				RemoteLogService.Write(PluginInfo.LogIdentificator,
					"IntentoMTSession.TranslateCorrectSegment-global exception",
					$"{e.Message}\r\n{e.StackTrace}");
			}
			finally
			{
				for (var i = 0; i < segs.Length; i++)
				{
					if (results[i] != null)
					{
						continue;
					}
					results[i] = new TranslationResult();
					// try-catch for callStack info
					try
					{
						ex ??= new Exception("Error during translation");
						throw ex;
					}
					catch (Exception e)
					{
						results[i].Exception = new MTException(e.Message, e.Message, e);
					}
				}
			}

			return results;
		}

		private static string ConvertSegmentToString(Segment seg, bool tagged)
		{
			return tagged
				? WrapperMemoQAddinsCommonFactory.Current.Wrapper.ConvertSegment2Html(seg, true)
				: seg.PlainText;
		}

		private static TranslationResult ConvertStringToSegment(string translation, IList<InlineTag> tags, bool tagged)
		{
			var result = new TranslationResult();
			if (translation != null)
			{
				result.Translation = tagged
					? WrapperMemoQAddinsCommonFactory.Current.Wrapper.ConvertHtml2Segment(translation, tags)
					: WrapperMemoQAddinsCommonFactory.Current.Wrapper
						.CreateFromTrimmedStringAndITags(translation, tags);
			}
			else
				result.Translation = WrapperMemoQAddinsCommonFactory.Current.Wrapper.CreateFromTrimmedStringAndITags("", null);
			return result;
		}

		#endregion

		#region ISessionForStoringTranslations

		public void StoreTranslation(TranslationUnit transunit)
		{
			try
			{
				serviceHelper.StoreTranslation(options, transunit.Source.PlainText, transunit.Target.PlainText,
					srcLangCode, trgLangCode);
			}
			catch (Exception e)
			{
				string localizedMessage = LocalizationHelper.Instance.GetResourceString("NetworkError");
				throw new MTException(string.Format(localizedMessage, e.Message),
					$"A network error occured ({e.Message}).", e);
			}
		}

		public int[] StoreTranslation(TranslationUnit[] transunits)
		{
			try
			{
				return serviceHelper.BatchStoreTranslation(options,
					transunits.Select(s => s.Source.PlainText).ToList(),
					transunits.Select(s => s.Target.PlainText).ToList(),
					srcLangCode, trgLangCode);
			}
			catch (Exception e)
			{
				var localizedMessage = LocalizationHelper.Instance.GetResourceString("NetworkError");
				throw new MTException(string.Format(localizedMessage, e.Message),
					$"A network error occured ({e.Message}).", e);
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			// dispose your resources if needed
		}

		#endregion
	}
}
