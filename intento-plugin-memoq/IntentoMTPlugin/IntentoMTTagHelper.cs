using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntentoMTPlugin
{
	public static class IntentoMTTagHelper
	{
		private static IReadOnlyDictionary<string, string> specialCodesCustomIn
		   = new Dictionary<string, string>
		{
		   { "<inline_tag id=\""	, "<span intentodata id=\"" },
		   { "</inline_tag>"		, "</span>" }
		};
		private static IReadOnlyDictionary<string, string> specialCodesCustomOut
			= new Dictionary<string, string>
		{
		   { "<span intentodata id=\""	, "<inline_tag id=\""},
		   { "</span>"					, "</inline_tag>"}
		};

		public static string CustomPrepareText(string data, bool intentoTagReplacement)
		{
			if (intentoTagReplacement)
			{
				// Replacing some HTML codes with special tags
				foreach (KeyValuePair<string, string> pair in specialCodesCustomIn)
				{
					data = data.Replace(pair.Key, pair.Value);
				}
			}
			return data;
		}

		public static string CustomPrepareResult(string text, bool intentoTagReplacement)
		{
			if (intentoTagReplacement)
			{
				foreach (KeyValuePair<string, string> pair in specialCodesCustomOut)
				{
					text = text.Replace(pair.Key, pair.Value);
				}
			}
			return text;
		}


	}
}
