using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoQ.Addins.Common.DataStructures;
using IntentoMTPlugin;

namespace IntentoMemoQMTPlugin
{
    [TestClass]
    public partial class MemoQPlugin
    {
        //[TestMethod]
   //     public void CustomTagConverter()
   //     {
   //         List<Segment> sourceSegment = new List<Segment>();
   //         SegmentBuilder sb;

   //         #region initial data
   //         //"Some starting text.",
   //         sb = new SegmentBuilder();
   //         sb.AppendString("Some starting text");
   //         sourceSegment.Add(sb.ToSegment());

   //         //"Later <i>italic text</i> and normal text, <b>bold continious to the next sentence text. <b>first level <u>second level</u></b>.", 
   //         sb = new SegmentBuilder();
   //         sb.AppendString("Later ");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "i", null));
   //         sb.AppendString("italic text");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Close, "i", null));
   //         sb.AppendString(" and normal text, ");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "b", null));
   //         sb.AppendString("bold continious to the next sentence text. ");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "b", null));
   //         sb.AppendString("first level ");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "u", null));
   //         sb.AppendString("second level");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Close, "u", null));
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Close, "b", null));
   //         sourceSegment.Add(sb.ToSegment());

   //         //"Second<b> line <i>deranged italic <u>underline</i></u> text, Not bold, <b>first level, <u>underlined second level</u></b>."
   //         sb = new SegmentBuilder();
   //         sb.AppendString("Second");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "b", null));
   //         sb.AppendString(" line ");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "i", null));
   //         sb.AppendString("deranged italic ");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "u", null));
   //         sb.AppendString("underline");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Close, "i", null));
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Close, "u", null));
   //         sb.AppendString(" text, Not bold, ");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "b", null));
   //         sb.AppendString("first level, ");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Open, "u", null));
   //         sb.AppendString("underlined second level");
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Close, "u", null));
   //         sb.AppendInlineTag(new InlineTag(InlineTagTypes.Close, "b", null));
   //         sourceSegment.Add(sb.ToSegment());
   //         //sb.AppendString("");
   //         //sb.AppendInlineTag();
   //         #endregion initial data

   //         foreach (Segment seg in sourceSegment)
   //         {
			//	string data;
			//	data = IntentoMTSession.ConvertSegmentToString(seg, seg.NumberOfInlineTags > 0);
			//	var memoqResult = IntentoMTSession.ConverStringToSegment(data, seg.ITags, seg.NumberOfInlineTags > 0);
			//	IntentoSegment intentoSegment = new IntentoSegment(seg);
			//	data = intentoSegment.Encode();
			//	var intentoResult = intentoSegment.GetTranslationResult(data);
			//	Assert.AreEqual(memoqResult.Translation, intentoResult.Translation);
			//}

   //     }

    }
}