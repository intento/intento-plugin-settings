namespace Intento.MT.Plugin.PropertiesForm.Models
{
    /// <summary>
    /// Pair of lang
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LangPair
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public LangPair(string from, string to)
        {
            if (from.Contains("-"))
            {
                from = from.Substring(0, from.IndexOf('-'));
            }

            if (to.Contains("-"))
            {
                to = to.Substring(0, to.IndexOf('-'));
            }

            From = from;
            To = to;
        }

        public string From { get; }

        public string To { get; }
    }

}