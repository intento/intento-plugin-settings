using System;
using System.Collections.Generic;
using System.Linq;

namespace Intento.MT.Plugin.PropertiesForm
{
    /// <summary>
    /// Builds the <c>ClientUserAgent</c> for every connection the settings form opens.
    /// <para>
    /// Usage logs resolve the product from the <c>Intento.*</c> tokens of the User-Agent by
    /// **highest rank**, not by position — <c>Intento.MemoqPlugin</c> (rank 40) beats
    /// <c>Intento.PluginSettingsForm</c> (15) wherever it appears. Position only decides a rank
    /// tie. What matters here is therefore that the host's token is *present*; keeping it last is
    /// what wins a tie and is the convention every producer follows, so it stays.
    /// </para>
    /// <para>
    /// The form's own token stays in front of it, which keeps config traffic distinguishable from
    /// translate traffic without owning the product name.
    /// </para>
    /// <para>
    /// <b>Known gap (CGL-49 / R2):</b> the <see cref="UnknownHostProduct"/> fallback is emitted but
    /// resolves to nothing — no registry, neither the hardcoded list in <c>intento-python-common</c>
    /// nor <c>usage_products</c>, carries a row for it. Until such a row exists, a host that supplies
    /// no token of its own still resolves to <c>pluginsettingsform</c>, the value the fallback exists
    /// to replace. <see cref="CarriesProductToken"/> also only tests the <c>Intento.</c> prefix, so
    /// any unregistered host token suppresses the fallback entirely.
    /// </para>
    /// Pure value: kept free of WinForms so the rule is reachable from tests.
    /// </summary>
    public static class ClientUserAgentComposer
    {
        public const string ProductPrefix = "Intento.";

        public const string FormProduct = "Intento.PluginSettingsForm";

        public const string UnknownHostProduct = "Intento.UnknownHost";

        private const string UnknownVersion = "unknown";

        /// <summary>
        /// True when the host User-Agent already names a product the usage logs can resolve.
        /// </summary>
        public static bool CarriesProductToken(string hostUserAgent)
        {
            if (string.IsNullOrWhiteSpace(hostUserAgent))
            {
                return false;
            }

            return hostUserAgent
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Any(token => token.Length > ProductPrefix.Length
                              && token.StartsWith(ProductPrefix, StringComparison.Ordinal));
        }

        /// <summary>
        /// Form token first, host product token last.
        /// </summary>
        /// <param name="formVersion">Version of the settings form.</param>
        /// <param name="hostUserAgent">User-Agent of the hosting plugin; may be null when a host does not set one.</param>
        public static string Compose(string formVersion, string hostUserAgent)
        {
            var version = string.IsNullOrWhiteSpace(formVersion) ? UnknownVersion : formVersion.Trim();
            var host = (hostUserAgent ?? string.Empty).Trim();
            var parts = new List<string> { $"{FormProduct}/{version}" };

            if (host.Length != 0)
            {
                parts.Add(host);
            }

            if (!CarriesProductToken(host))
            {
                parts.Add($"{UnknownHostProduct}/{version}");
            }

            return string.Join(" ", parts);
        }
    }
}
