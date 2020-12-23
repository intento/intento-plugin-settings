using MemoQ.Addins.Common.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntentoMTPlugin
{
	public static class IntentoTagHelper
	{
		private static IReadOnlyDictionary<string, string> memoqTagsReplaceList
		   = new Dictionary<string, string>
		{
		   { "<inline_tag id=\""    , "<span id=\"intnt" },
		   { "</inline_tag>"        , "</span>" }
		};
		private static IReadOnlyDictionary<string, string> memoqTagsRevertList
			= new Dictionary<string, string>
		{
		   { "<span id=\"intnt"  , "<inline_tag id=\""},
		   { "</span>"           , "</inline_tag>"}
		};
		private static IReadOnlyList<string> openTags = new List<string>	{ "<inline_tag id="};
		private static IReadOnlyList<string> closeTags = new List<string>	{ "</inline_tag>" };

		public class CustomTransformer
		{
			string _data;
			bool _intentoTagReplacement;
			int skew = 0;

			public CustomTransformer(string data, bool intentoTagReplacement, InlineTag[] tags)
			{
				_data = data;
				_intentoTagReplacement = intentoTagReplacement;
				if (tags != null)
				{
					int openCount = tags.Where(i => i.TagType == InlineTagTypes.Open).Count();
					int closeCount = tags.Where(i => i.TagType == InlineTagTypes.Close).Count();
					skew = openCount - closeCount;
				}
			}

			public string PreparedText
			{
				get
				{
					string data = _data;
					if (_intentoTagReplacement)
					{
						// Replacing some HTML codes with special tags
						foreach (KeyValuePair<string, string> pair in memoqTagsReplaceList)
						{
							data = data.Replace(pair.Key, pair.Value);
						}

						string tag;
						if (skew > 0)
						{ // add close tags
							tag = "</span>";
							for (int i = 0; i < skew; i++)
								data = data + tag;
						}
						else if (skew < 0)
						{ // add open tags
							for (int i = 0; i > skew; i--)
								data = string.Format("<span id=\"balancing_intento_tag{0}\">{1}", Math.Abs(i), data);
						}
					}
					return data;
				}
			}

			public string PrepareResult(string text)
			{
				if (_intentoTagReplacement)
				{
					string tag;
					bool mismatch = false;
					if (skew > 0)
					{ // add close tags
						tag = "</span>";
						for (int i = 0; i < skew; i++)
						{
							mismatch = text.LastIndexOf(tag) == -1;
							if (mismatch)
								break;

							text = text.Substring(0, text.LastIndexOf(tag)) + text.Substring(text.LastIndexOf(tag) + tag.Length);
						}
					}
					else if (skew < 0)
					{ // add open tags
						for (int i = 0; i > skew; i--)
						{
							tag = string.Format("<span id=\"balancing_intento_tag{0}\">", Math.Abs(i));
							text = text.Substring(0, text.IndexOf(tag)) + text.Substring(text.IndexOf(tag) + tag.Length);
							var pos = text.IndexOf(tag);
							mismatch = pos == -1;
							if (mismatch)
								break;
							text = text.Substring(0, pos) + text.Substring(pos + tag.Length);
						}
					}

					foreach (KeyValuePair<string, string> pair in memoqTagsRevertList)
					{
						text = text.Replace(pair.Key, pair.Value);
					}
					text = text.Replace("\" />", "\"/>");
				}
				return text;
			}
		}

	}
}
