using System;
using System.Linq;
using System.Collections.Generic;
using MemoQ.Addins.Common.DataStructures;
using MemoQ.MTInterfaces;
using MemoQ.Addins.Common.Utils;
using Intento.MT.Plugin.PropertiesForm;

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
            using (new Logs.Pair("IntentoMTSession.ctor"))
            {
                this.options = options;
                this.srcLangCode = srcLangCode;
                this.trgLangCode = trgLangCode;
                this.options = options;
                this.serviceHelper = serviceHelper;
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
            using (new Logs.Pair("IntentoMTSession.TranslateCorrectSegment-1"))
            { 
                TranslationResult result = new TranslationResult();
                try
                {
                    bool tagged = segm.ITags.Length != 0;
                    string data = ConvertSegmentToString(segm, tagged);
                    string res;
                    if (!string.IsNullOrEmpty(options.GeneralSettings.ProviderId))
                    {   // provider_id available
                        string format = tagged ? GetTaggedFormat() : null;
                        string data2 = PluginHelper.PrepareText(format, data);

                        string res2 = serviceHelper.Translate(options, data2, this.srcLangCode, this.trgLangCode, format: format);

                        res = PluginHelper.PrepareResult(format, res2);
                    }
                    else
                    {   // smart routing. Format is provided by special smart routing table
                        res = serviceHelper.Translate(options, data, this.srcLangCode, this.trgLangCode, routing: tagged ? "intento-tagged" : null);
                    }
                    result = ConverStringToSegment(res, segm.ITags, tagged);
                }
                catch (Exception e)
                {
                    result.Exception = new MTException(e.Message, e.Message, e);
                    Logs.Write(string.Format("IntentoMTSession.TranslateCorrectSegment-1 exception: {0}", e.Message));
                }
                return result;
            }
        }

        /// <summary>
        /// Translates multiple segments, possibly using a fuzzy TM hit for improvement
        /// </summary>
        public TranslationResult[] TranslateCorrectSegment(Segment[] segs, Segment[] tmSources, Segment[] tmTargets)
        {
            using (new Logs.Pair("IntentoMTSession.TranslateCorrectSegment-2"))
            {
                TranslationResult[] results = new TranslationResult[segs.Length];
                Exception ex = null;
                try
                {
                    // Check for necessity to use tagged translation
                    bool tagged = false;
                    try
                    {   // Only memoQ 8.7 and higher support tagged docs
                        var ver = IntentoMTServiceHelper.memoqVersion.Split('.');
                        int major = int.Parse(ver[0]);
                        if (major > 8 || (major == 8 && int.Parse(ver[1]) >= 7))
                            tagged = segs.Any(i => i.ITags.Length != 0);
                    }
                    catch { }

                    IList<string> data = segs.Select(i => ConvertSegmentToString(i, tagged)).ToList();

                    List<string> res;
                    if (!string.IsNullOrEmpty(options.GeneralSettings.ProviderId))
                    {   // provider_id available
                        string format = tagged ? GetTaggedFormat() : null;
                        IEnumerable<string> data2 = data.Select(i => PluginHelper.PrepareText(format, i));

                        List<string> res2 = serviceHelper.BatchTranslate(options, data2, this.srcLangCode, this.trgLangCode, format: format);

                        res = res2.Select(i => PluginHelper.PrepareResult(format, i)).ToList();
                    }
                    else
                    {   // smart routing. Format is provided by special smart routing table
                        res = serviceHelper.BatchTranslate(options, data, this.srcLangCode, this.trgLangCode, routing: tagged ? "intento-tagged" : null);
                    }
                    if (segs.Length != res.Count)
                        throw new Exception(string.Format("Invalid result from Intento API: segments count is different {0}/{1}", segs.Length, res.Count));
                    for (int i = 0; i < segs.Length; i++)
                    {
                        //  if the segment returned null, then an error occurred while translating = new JArray(){ null }
                        if (res[i] != null)
                            results[i] = ConverStringToSegment(res[i], segs[i].ITags, tagged);
                    }
                }
                catch (Exception e)
                {
                    ex = e;
                    Logs.Write(string.Format("IntentoMTSession.TranslateCorrectSegment-2 exception: {0}", e.Message));
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

        private string ConvertSegmentToString(Segment seg, bool tagged)
        {
            if (tagged)
                return SegmentHtmlConverter.ConvertSegment2Html(seg, tagged);
            return seg.PlainText;
        }

        private TranslationResult ConverStringToSegment(string translation, IList<InlineTag> tags, bool tagged)
        {
            TranslationResult result = new TranslationResult();
            if (translation != null)
            {
                if (tagged)
                    result.Translation = SegmentHtmlConverter.ConvertHtml2Segment(translation, tags);
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
