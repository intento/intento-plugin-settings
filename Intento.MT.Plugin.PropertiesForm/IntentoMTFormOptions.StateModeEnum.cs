namespace Intento.MT.Plugin.PropertiesForm
{
    // ReSharper disable once InconsistentNaming
    public partial class IntentoMTFormOptions
    {
        public enum StateModeEnum
        {
            Unknown = 0,

            /// <summary>
            /// External autherntication is prohibited, only "via Intento"
            /// </summary>
            Prohibited,

            /// <summary>
            /// External autherntication is required, "via Intento" is prohibited
            /// </summary>
            Required,

            /// <summary>
            /// External autherntication is optional
            /// </summary>
            Optional
        }
    }
}