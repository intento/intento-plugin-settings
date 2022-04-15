using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MemoQ.Addins.Common.DataStructures;

namespace IntentoMTPlugin
{
    /// <summary>
    /// Support all APIs from MemoQ.Addins.Common
    /// </summary>
    public class WrapperMemoQAddinsCommon
    {
        private readonly DynamicMethodInvoker methodConvertSegment2Html;
        private readonly DynamicMethodInvoker methodConvertHtml2Segment;
        private readonly DynamicMethodInvoker methodCreateFromTrimmedStringAndITags;

        /// <summary>
        /// Is advanced SDK of memoQ (new version)
        /// </summary>
        public bool AdvancedSdk { get; }

        public WrapperMemoQAddinsCommon(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();
            var typeDll = types.FirstOrDefault(i => i.Name == "SegmentXMLConverter");
            if (typeDll != null)
            {
                AdvancedSdk = true;
                methodConvertSegment2Html = new DynamicMethodInvoker(typeDll, "ConvertSegment2Xml");
                methodConvertHtml2Segment = new DynamicMethodInvoker(typeDll, "ConvertXML2Segment");
            }
            else
            {
                typeDll = types.FirstOrDefault(i => i.Name == "SegmentHtmlConverter");
                if (typeDll != null)
                {
                    methodConvertSegment2Html = new DynamicMethodInvoker(typeDll, "ConvertSegment2Html");
                    methodConvertHtml2Segment = new DynamicMethodInvoker(typeDll, "ConvertHtml2Segment");
                }
            }

            typeDll = types.FirstOrDefault(i => i.Name == "SegmentBuilder");
            if (typeDll != null)
            {
                methodCreateFromTrimmedStringAndITags =
                    new DynamicMethodInvoker(typeDll, "CreateFromTrimmedStringAndITags");
            }
        }

        /// <summary>
        /// Convert segment to HTML
        /// </summary>
        /// <param name="seg"></param>
        /// <param name="tagged"></param>
        /// <returns></returns>
        public string ConvertSegment2Html(Segment seg, bool tagged)
        {
            return (string) methodConvertSegment2Html?.Invoke(null, seg, tagged, false);
        }

        /// <summary>
        /// Convert HTML text to Segment
        /// </summary>
        /// <param name="html"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Segment ConvertHtml2Segment(string html, IList<InlineTag> tags)
        {
            return (Segment) methodConvertHtml2Segment?.Invoke(null, html, tags);
        }

        /// <summary>
        /// Convert text to Segment
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Segment CreateFromTrimmedStringAndITags(string txt, IList<InlineTag> tags)
        {
            return (Segment) methodCreateFromTrimmedStringAndITags?.Invoke(null, txt, tags);
        }

        #region PrivateInvoker

        private class DynamicMethodInvoker
        {
            private MethodInfo Method { get; }

            private ParameterInfo[] Parameters { get; }

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="type"></param>
            /// <param name="methodName"></param>
            public DynamicMethodInvoker(Type type, string methodName)
            {
                Method = type.GetMethod(methodName);
                if (Method != null)
                {
                    Parameters = Method.GetParameters();
                }
            }

            /// <summary>
            /// Invoke method with given params
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="params"></param>
            /// <returns></returns>
            public object Invoke(object obj, params object[] @params)
            {
                if (@params.Length == Parameters.Length)
                {
                    return Method.Invoke(obj, @params);
                }

                var allParams = new List<object>();
                if (@params.Length > Parameters.Length)
                {
                    allParams.AddRange(@params.Take(Parameters.Length));
                }
                else
                {
                    allParams.AddRange(@params);
                    for (var i = @params.Length; i < Parameters.Length; i++)
                    {
                        var param = Parameters[i];
                        allParams.Add(param.IsOptional ? param.DefaultValue : null);
                    }
                }

                return Method.Invoke(obj, allParams.ToArray());
            }
        }

        #endregion
    }
}