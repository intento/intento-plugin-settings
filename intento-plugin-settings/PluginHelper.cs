using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{
    public static class PluginHelper
    {
         private static IReadOnlyDictionary<string, string> specialCodesIn
            = new Dictionary<string, string>
         {
           { "&amp;gt;" ,   "<tph1/>" },
           { "&amp;lt;" ,   "<tph2/>" },
           { "&lt;"     ,   "<tph3/>" },
           { "&gt;"     ,   "<tph4/>" }
         };

         private static IReadOnlyDictionary<string, string> specialCodesOut
            = new Dictionary<string, string>
        {
           { "<tph1>" ,   "&amp;gt;"  },
           { "<tph2>" ,   "&amp;lt;"  },
           { "<tph3>" ,   "&lt;"      },
           { "<tph4>" ,   "&gt;"      },
           { "<tph1/>",   "&amp;gt;"  },
           { "<tph2/>",   "&amp;lt;"  },
           { "<tph3/>",   "&lt;"      },
           { "<tph4/>",   "&gt;"      },
           { "<tph1 />",  "&amp;gt;"  },
           { "<tph2 />",  "&amp;lt;"  },
           { "<tph3 />",  "&lt;"      },
           { "<tph4 />",  "&gt;"      },
           { "</tph1>",   ""          },
           { "</tph2>",   ""          },
           { "</tph3>",   ""          },
           { "</tph4>",   ""          }
        }; 

        public static string PrepareText(string format, string data)
        {
            // Remove parasite character for memoq
            data = new string(data.Where(c => (int)c != 9727).ToArray());

            // Replacing some HTML codes with special tags
            foreach (KeyValuePair<string, string> pair in specialCodesIn)
            {
                data = data.Replace(pair.Key, pair.Value);
            }
            if (format == "xml")
                return string.Format("<root>{0}</root>", data);
            return data;
        }

        public static string PrepareResult(string format, string text)
        {
            // Return HTML codes instead of special tags
            foreach (KeyValuePair<string, string> pair in specialCodesOut)
            {
                text = text.Replace(pair.Key, pair.Value);
            }
            if (format == "xml")
            {
                // Remove <? > tag
                int n1 = text.IndexOf("<?");
                string text2 = text;
                if (n1 != -1)
                {
                    int n2 = text.IndexOf(">");
                    text2 = text.Substring(n2 + 1);
                }

                // Remove <root> and </root> tags
                string text3 = text2.Replace("<root>", "").Replace("</root>", "");
                return text3;
            }

            if (format == "html")
            {
                // Remove <meta> and </meta> tags
                int n1 = text.IndexOf("<meta");
                string text2 = text;
                if (n1 != -1)
                {
                    int n2 = text.IndexOf(">");
                    text2 = text.Substring(n2 + 1);
                }

                return text2;
            }

            return text;
        }

		public class ErrorInfo
		{
			public bool isError;
			public string visibleErrorText;
			public string clipBoardContent;

			public ErrorInfo(bool isError, string visibleErrorText, string clipBoardContent)
			{
				this.isError = isError;
				this.visibleErrorText = visibleErrorText;
				this.clipBoardContent = clipBoardContent;
			}
		}
    }
}
