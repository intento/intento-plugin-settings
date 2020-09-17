using System;
using System.Linq;
using System.Collections.Generic;
using MemoQ.Addins.Common.DataStructures;
using MemoQ.MTInterfaces;
using MemoQ.Addins.Common.Utils;
using Intento.MT.Plugin.PropertiesForm;
using static IntentoMTPlugin.ConverterHtml;

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
            return "xml";
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
    
            /*
            // Check for necessity to use tagged translation
            bool tagged = false;
            int major = 0;
            int minor = 0;
            string ver_raw = null;
            string[] ver; 
            try
            {   // Only memoQ 8.7 and higher support tagged docs
                ver_raw = IntentoMTServiceHelper.memoqVersion;
                ver = ver_raw.Split('.');
                major = int.Parse(ver[0]);
                minor = int.Parse(ver[1]);
                if (major > 8 || (major == 8 && minor >= 7))
                    tagged = segs.Any(i => i.ITags.Length != 0);
                Logs.Write2(string.Format("tagged: {0}, ver {1}.{2}", tagged, major, minor), null);
            }
            catch (Exception ex){
                Logs.Write2(string.Format("catch tagged: {0}, ver_raw: {1}", tagged, ver_raw), ex.Message);
            }
            */
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

                TranslationResult[] results = new TranslationResult[segs.Length];
                Exception ex = null;
				bool intentoTagReplacement = options.GeneralSettings.IntentoTagReplacement;
                try
                {
                    // Check for necessity to use tagged translation
                    bool tagged = IsTagged(segs);
					bool implicitTagged = IsImplicitTagged(segs);

					IList<string> data = new List<string>();
					IList<IntentoSegment> iData = new List<IntentoSegment>();
					IntentoMTServiceHelper.ResultBatchTranslate resultBatchTranslate;

					string format = tagged || implicitTagged ? GetTaggedFormat() : null;
                    string routing = null; //provider_id available
                    if (string.IsNullOrEmpty(options.GeneralSettings.ProviderId) && tagged)
                        routing = "intento-tagged"; // smart routing. Format is provided by special smart routing table

                    // if there are structural tags, we do not use intento tag replacement 
                    if (intentoTagReplacement)
                        intentoTagReplacement = !segs.Any(i => i.NumberOfStructuralTags != 0);

                    if (intentoTagReplacement && tagged)
                    {
                        Logs.Write2("1: intentoTagReplacement", "{0}, format: {1}, tagged: {2}", intentoTagReplacement, format, tagged);
						//iData = segs.Select(i => new IntentoSegment(i)).ToList();
						//data = iData.Select(i => i.Encode()).ToList();
						for (int i = 0; i < segs.Length; i++)
						{
							Segment seg = segs[i];
							iData.Add(new IntentoSegment(seg) );
							data.Add(IsTagged(new Segment[] { seg }) ?
								iData[i].Encode() :
								PluginHelper.PrepareText(format, ConvertSegmentToString(seg, true))
							);
						}
						resultBatchTranslate = serviceHelper.BatchTranslate(options, data, this.srcLangCode, this.trgLangCode, 
                            format: format, routing: routing);
                    }
                    else
                    {
                        Logs.Write2("2: intentoTagReplacement", "{0}, format: {1}, tagged: {2}", intentoTagReplacement, format, tagged);
                        data = segs.Select(i => ConvertSegmentToString(i, tagged || implicitTagged)).ToList();
                        data = data.Select(i => PluginHelper.PrepareText(format, i)).ToList();
						resultBatchTranslate = serviceHelper.BatchTranslate(options, data, this.srcLangCode, this.trgLangCode,  format: format, routing: routing);
						resultBatchTranslate.resultTranslate = resultBatchTranslate.resultTranslate.Select(i => PluginHelper.PrepareResult(format, i)).ToList();
                        intentoTagReplacement = false;
                    }

					int resultLength = resultBatchTranslate.resultTranslate.Count;
					if (segs.Length != resultLength)
                        throw new Exception(string.Format("Invalid result from Intento API: segments count is different {0}/{1}", segs.Length, resultLength));
                    for (int i = 0; i < resultLength; i++)
                    {
						//  if the segment returned null, then an error occurred while translating
						if (resultBatchTranslate.resultTranslate[i] != null)
						{
							results[i] = intentoTagReplacement ?
								iData[i].GetTranslationResult(resultBatchTranslate.resultTranslate[i])
								: ConverStringToSegment(resultBatchTranslate.resultTranslate[i], segs[i].ITags, tagged || implicitTagged);
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
                return LocalizationHelper.MemoQConvertSegment2Html(seg, tagged);
            return seg.PlainText;
        }

        public static  TranslationResult ConverStringToSegment(string translation, IList<InlineTag> tags, bool tagged)
        {
            TranslationResult result = new TranslationResult();
            if (translation != null)
            {
                if (tagged)
                    result.Translation = LocalizationHelper.MemoQConvertHtml2Segment(translation, tags);
                else
                    result.Translation = SegmentBuilder.CreateFromTrimmedStringAndITags(translation, tags);
            }
            else
                result.Translation = SegmentBuilder.CreateFromTrimmedStringAndITags("", null);
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
    }
}
