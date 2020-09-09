using MemoQ.Addins.Common.DataStructures;
using MemoQ.Addins.Common.Utils;
using MemoQ.MTInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntentoMTPlugin
{
    class ConverterHtml
    {
        public class IntentoSegment
        {
            public Segment segment { get; }
            public IntentoInlineTag[] intentoInlineTags { get; }

            public IntentoSegment(Segment segment = null)
            {
                if (segment != null)
                {
                    this.segment = segment;
                    int lenght = segment.ITags.Count();
                    intentoInlineTags = new IntentoInlineTag[lenght];
                    if (segment.NumberOfInlineTags > 0)
                    {
                        Array.Copy(segment.ITags.Select(x => new IntentoInlineTag(x)).ToArray(), intentoInlineTags, lenght);
                    }
                }
            }

            public string Encode()
            {
                string segmentText = segment.ToString();
                if (string.IsNullOrEmpty(segmentText))
                    return segmentText;
                string ret = string.Empty;
                int i = 0;
                for (i = 0; i < intentoInlineTags.Length; i++)
                {
                    if (string.IsNullOrEmpty(intentoInlineTags[i].Substitute))
                    {
                        if (intentoInlineTags[i].TagType != InlineTagTypes.Open)
                        {
                            // закрывающий тэг к которому не была подобрана пара раньше не имеет пары
                            intentoInlineTags[i].Substitute = CreateSubstitute(InlineTagTypes.Empty, i);
                        }
                        else
                        {
                            string findTag = intentoInlineTags[i].Name;
                            List<string> tree = new List<string>();
                            for (var j = i + 1; j <= intentoInlineTags.Length; j++)
                            {
                                if (j == intentoInlineTags.Length)
                                {
                                    // все последующие теги уже перебрали и не нашли пары. соотв.
                                    intentoInlineTags[i].Substitute = CreateSubstitute(InlineTagTypes.Empty, i);
                                    break;
                                }

                                if (intentoInlineTags[j].TagType == InlineTagTypes.Empty)
                                    continue;
                                else if (intentoInlineTags[j].TagType == InlineTagTypes.Open)
                                {
                                    // внутри искомого есть новая пара тегов. добивим открывающий в список для идентификации закрывающего
                                    tree.Add(intentoInlineTags[j].Name);
                                    continue;
                                }
                                else //InlineTagTypes.Close
                                {
                                    if (tree.Any(x => x == intentoInlineTags[j].Name))
                                    {
                                        tree.Remove(intentoInlineTags[j].Name); // был открывающий тег внутри искомого
                                        continue;
                                    }
                                    else if (intentoInlineTags[j].Name == intentoInlineTags[i].Name)
                                    { // нашли закрывающий для искомого
                                        intentoInlineTags[i].Substitute = CreateSubstitute(InlineTagTypes.Open, i);
                                        intentoInlineTags[j].Substitute = CreateSubstitute(InlineTagTypes.Close, i);
                                        break;
                                    }
                                    else // no paired closing tag
                                    {
                                        // внутри искомого найден закрывающий без открывающего. 
                                        intentoInlineTags[i].Substitute = CreateSubstitute(InlineTagTypes.Empty, i);
                                        break;
                                    }
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(intentoInlineTags[i].Substitute))
                            intentoInlineTags[i].Substitute = CreateSubstitute(InlineTagTypes.Empty, i);
                    }
                } // for i


                i = 0;
                char separator = (char)65533;
                var pos = segmentText.IndexOf(separator);
                while (pos != -1)
                {
                    segmentText = segmentText.Remove(pos, 1).Insert(pos, intentoInlineTags[i].Substitute);
                    pos = segmentText.IndexOf(separator);
                    i++;
                }

                return segmentText;
            }

            public string Decode(string result)
            {
                if (!string.IsNullOrEmpty(result))
                {
                    char separator = (char)65533;
                    foreach (var tag in intentoInlineTags)
                    {
                        result = result.Replace(tag.Substitute, separator.ToString());
                    }
                }
                return result;
            }

            public TranslationResult GetTranslationResult(string translation)
            {
                TranslationResult ret = new TranslationResult();
                for (int i= 0; i < intentoInlineTags.Length; i++)
                {
                    translation = translation.Replace(intentoInlineTags[i].Substitute, String.Format("<span data-mqitag=\"{0}\"></span>", i));
                }
                ret.Translation = LocalizationHelper.MemoQConvertHtml2Segment(translation, segment.ITags);
                //Decode(result);

                return ret;
            }

        }
        public class IntentoInlineTag : InlineTag
        {
            public string Substitute { get; set; }
            public IntentoInlineTag(InlineTag inlineTag)
                : base(inlineTag.TagType, inlineTag.Name, inlineTag.Attributes.ToArray()) { }

        }

        private static string CreateSubstitute(InlineTagTypes type, int num)
        {
            string tag = "<tg{0}>";
            switch (type)
            {
                case InlineTagTypes.Close:
                    tag = "</tg{0}>";
                    break;
                case InlineTagTypes.Empty:
                    tag = "<tg{0}></tg{0}>";
                    break;
            }
            return string.Format(tag, num);
        }

    }
}
