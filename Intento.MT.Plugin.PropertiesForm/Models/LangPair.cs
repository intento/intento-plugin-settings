namespace Intento.MT.Plugin.PropertiesForm.Models
{
    public class LangPair
    {
        public LangPair(string from, string to)
        {
            if (from.Contains("-"))
                from = from.Substring(0, from.IndexOf('-'));
            if (to.Contains("-"))
                to = to.Substring(0, to.IndexOf('-'));
            From = from;
            To = to;
        }

        public string From { get; }

        public string To { get; }
    }

}