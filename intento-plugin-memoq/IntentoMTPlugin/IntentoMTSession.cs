using System;
using System.Linq;
using System.Collections.Generic;
using MemoQ.Addins.Common.DataStructures;
using MemoQ.MTInterfaces;
using MemoQ.Addins.Common.Utils;
using Intento.MT.Plugin.PropertiesForm;
using System.Reflection;

namespace IntentoMTPlugin
{
	/// <summary>
	/// Session that perform actual translation or storing translations. Created on a segment-by-segment basis, or once for batch operations.
	/// </summary>
	public class IntentoMTSession : ISession, ISessionForStoringTranslations
	{
		/// <summary>
		/// Access to Intento API
		/// </summary>
		IntentoMTServiceHelper serviceHelper;

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

		public IntentoMTSession(IntentoMTServiceHelper serviceHelper, string srcLangCode, string trgLangCode, IntentoMTOptions options)
		{
			// using (new Logs.Pair("IntentoMTSession.ctor"))
			{
				this.options = options;
				this.srcLangCode = srcLangCode;
				this.trgLangCode = trgLangCode;
				this.options = options;
				this.serviceHelper = serviceHelper;
				serviceHelper.format = options.GeneralSettings.ProviderFormats;
				Logs.Write2(string.Format("IsTrace: {0}", IntentoTranslationProviderOptionsForm.IsTrace()), null);
			}
		}

		#region ISession Members

		private string GetTaggedFormat()
		{   // Return format for non-smart routing calls
			string format = serviceHelper.format;
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
			// using (new Logs.Pair("IntentoMTSession.TranslateCorrectSegment-1"))
			{
				TranslationResult[] results = TranslateCorrectSegment(new Segment[] { segm }, new Segment[] { tmSource }, new Segment[] { tmTarget });
				TranslationResult result = results[0];
				return result;
			}
		}

		private void LogSegmentData(Segment[] segs)
		{
			foreach (Segment seg in segs)
			{
				var str = new List<string>();
				str.Add(string.Format("Length {0}, NumberOfInlineTags {1}, NumberOfStructuralTags {2}, NumberOfTags {3}\r\n",
					seg.Length, seg.NumberOfInlineTags, seg.NumberOfStructuralTags, seg.NumberOfTags));
				str.Add(string.Format("PlainText: {0}", seg.PlainText));
				str.Add(string.Format("PlainTextWithSpacesForTags: {0}", seg.PlainTextWithSpacesForTags));

				Logs.Write2("segs data", string.Join("\r\n", str));
			}
		}

		private bool IsImplicitTagged(Segment[] segs)
		{
			return segs.Any(i => i.PlainText != ConvertSegmentToString(i, true));
		}

		private bool IsTagged(Segment[] segs)
		{
			bool tagged = segs.Any(i => i.ITags.Length != 0);
			return tagged;
		}

		/// <summary>
		/// Translates multiple segments, possibly using a fuzzy TM hit for improvement
		/// </summary>
		public TranslationResult[] TranslateCorrectSegment(Segment[] segs, Segment[] tmSources, Segment[] tmTargets)
		{
			// using (new Logs.Pair("IntentoMTSession.TranslateCorrectSegment-2"))
			{
				LogSegmentData(segs);

				if (segs.Length > 0)
				{
					WrapperMemoQAddinsCommon.Get(segs[0]);
				}

				TranslationResult[] results = new TranslationResult[segs.Length];
				Exception ex = null;
				bool intentoTagReplacement = options.GeneralSettings.IntentoTagReplacement;
				try
				{
					// Check for necessity to use tagged translation
					bool tagged = IsTagged(segs);

					IList<string> data = new List<string>();
					IntentoMTServiceHelper.ResultBatchTranslate resultBatchTranslate;
					string format = tagged ? GetTaggedFormat() : null;
					string routing = null; //provider_id available
					if (string.IsNullOrEmpty(options.GeneralSettings.ProviderId) && tagged)
						routing = "intento-tagged"; // smart routing. Format is provided by special smart routing table


					Logs.Write2("2: intentoTagReplacement", "{0}, format: {1}, tagged: {2}", intentoTagReplacement, format, tagged);
					data = segs.Select(i => ConvertSegmentToString(i, tagged)).ToList();
					data = data.Select(i => PluginHelper.PrepareText(format, i)).ToList();
					data = data.Select(i => IntentoMTTagHelper.CustomPrepareText(i, intentoTagReplacement)).ToList();
					resultBatchTranslate = serviceHelper.BatchTranslate(options, data, this.srcLangCode, this.trgLangCode, format: format, routing: routing);
					resultBatchTranslate.resultTranslate = resultBatchTranslate.resultTranslate
						.Select(i => IntentoMTTagHelper.CustomPrepareResult(i, intentoTagReplacement))
						.Select(i => PluginHelper.PrepareResult(format, i)).ToList();

					int resultLength = resultBatchTranslate.resultTranslate.Count;
					if (segs.Length != resultLength)
						throw new Exception(string.Format("Invalid result from Intento API: segments count is different {0}/{1}", segs.Length, resultLength));
					for (int i = 0; i < resultLength; i++)
					{
						//  if the segment returned null, then an error occurred while translating
						if (resultBatchTranslate.resultTranslate[i] != null)
						{
							results[i] = ConverStringToSegment(resultBatchTranslate.resultTranslate[i], segs[i].ITags, tagged);
							results[i].Info = string.IsNullOrWhiteSpace(resultBatchTranslate.viaProvider) ?
								null : string.Format("translated using {0}", resultBatchTranslate.viaProvider);
						}
					}
				}
				catch (Exception e)
				{
					ex = e;
					Logs.Write2("IntentoMTSession.TranslateCorrectSegment-2 exception", string.Format("{0}\r\n{1}", e.Message, e.StackTrace));
				}
				finally
				{
					if (ex == null)
						ex = new Exception("Error during translation");

					for (int i = 0; i < segs.Length; i++)
					{
						if (results[i] == null)
						{
							results[i] = new TranslationResult();
							// try-catch for callStack info
							try
							{
								throw ex;
							}
							catch (Exception e)
							{
								results[i].Exception = new MTException(e.Message, e.Message, e);
							}
						}
					}
				}
				return results;
			}
		}

		public static string ConvertSegmentToString(Segment seg, bool tagged)
		{
			if (tagged)
				return WrapperMemoQAddinsCommon.Get().ConvertSegment2Html(seg, tagged);
			return seg.PlainText;
		}

		public static TranslationResult ConverStringToSegment(string translation, IList<InlineTag> tags, bool tagged)
		{
			TranslationResult result = new TranslationResult();
			if (translation != null)
			{
				if (tagged)
					result.Translation = WrapperMemoQAddinsCommon.Get().ConvertHtml2Segment(translation, tags);
				else
					result.Translation = WrapperMemoQAddinsCommon.Get().CreateFromTrimmedStringAndITags(translation, tags);
			}
			else
				result.Translation = WrapperMemoQAddinsCommon.Get().CreateFromTrimmedStringAndITags("", null);
			return result;
		}

#endregion

#region ISessionForStoringTranslations

		public void StoreTranslation(TranslationUnit transunit)
		{
			try
			{
				serviceHelper.StoreTranslation(options, transunit.Source.PlainText, transunit.Target.PlainText, this.srcLangCode, this.trgLangCode);
			}
			catch (Exception e)
			{
				string localizedMessage = LocalizationHelper.Instance.GetResourceString("NetworkError");
				throw new MTException(string.Format(localizedMessage, e.Message), string.Format("A network error occured ({0}).", e.Message), e);
			}
		}

		public int[] StoreTranslation(TranslationUnit[] transunits)
		{

			try
			{
				return serviceHelper.BatchStoreTranslation(options,
										transunits.Select(s => s.Source.PlainText).ToList(), transunits.Select(s => s.Target.PlainText).ToList(),
										this.srcLangCode, this.trgLangCode);
			}
			catch (Exception e)
			{
				string localizedMessage = LocalizationHelper.Instance.GetResourceString("NetworkError");
				throw new MTException(string.Format(localizedMessage, e.Message), string.Format("A network error occured ({0}).", e.Message), e);
			}
		}

#endregion

#region IDisposable Members

		public void Dispose()
		{
			// dispose your resources if needed
		}

#endregion


		public class WrapperMemoQAddinsCommon
		{
			static WrapperMemoQAddinsCommon singletone = null;
			Assembly assembly;
			public MethodInfo methodConvertSegment2Html;
			public MethodInfo methodConvertHtml2Segment;
			public MethodInfo methodCreateFromTrimmedStringAndITags;
			public bool useNew2Html = true;
			public bool advancedSdk = false;

			private WrapperMemoQAddinsCommon(Assembly assembly)
			{
				this.assembly = assembly;

				System.Type[] types = assembly.GetExportedTypes();
				System.Type typedll;
#if !VARIANT_PUBLIC
				typedll = types.Where(i => i.Name == "SegmentXMLConverter").FirstOrDefault();
				if (typedll != null)
				{
					advancedSdk = true;
					methodConvertSegment2Html = typedll.GetMethod("ConvertSegment2Xml");
					methodConvertHtml2Segment = typedll.GetMethod("ConvertXML2Segment");
				}
				else
#endif
				{
					typedll = types.Where(i => i.Name == "SegmentHtmlConverter").FirstOrDefault();
					methodConvertSegment2Html = typedll.GetMethod("ConvertSegment2Html");
					methodConvertHtml2Segment = typedll.GetMethod("ConvertHtml2Segment");
				}
				typedll = types.Where(i => i.Name == "SegmentBuilder").FirstOrDefault();
				methodCreateFromTrimmedStringAndITags = typedll.GetMethod("CreateFromTrimmedStringAndITags");
			}

			public static WrapperMemoQAddinsCommon Get(Segment seg)
			{
				if (singletone != null)
					return singletone;

				singletone = new WrapperMemoQAddinsCommon(seg.GetType().Assembly);
				return singletone;
			}

			public static WrapperMemoQAddinsCommon Get()
			{
				if (singletone != null)
					return singletone;

				// We do not put it to singletone
				WrapperMemoQAddinsCommon wrapper = new WrapperMemoQAddinsCommon(typeof(Segment).Assembly);
				return wrapper;
			}

			public string ConvertSegment2Html(Segment seg, bool tagged)
			{
				object res;

				if (useNew2Html)
				{
					try
					{
						res = methodConvertSegment2Html.Invoke(null, new object[] { seg, tagged, false });
					}
					catch
					{
						useNew2Html = false;
						res = methodConvertSegment2Html.Invoke(null, new object[] { seg, tagged });
					}
				}
				else
				{
					try
					{
						res = methodConvertSegment2Html.Invoke(null, new object[] { seg, tagged });
					}
					catch
					{
						useNew2Html = true;
						res = methodConvertSegment2Html.Invoke(null, new object[] { seg, tagged, false });
					}
				}

				return (string)res;
			}
			public Segment ConvertHtml2Segment(string html, IList<InlineTag> tags)
			{
				object res = methodConvertHtml2Segment.Invoke(null, new object[] { html, tags });
				return (Segment)res;
			}
			public Segment CreateFromTrimmedStringAndITags(string txt, IList<InlineTag> tags)
			{
				object res = methodCreateFromTrimmedStringAndITags.Invoke(null, new object[] { txt, tags });
				return (Segment)res;
			}

		}
	}
}
