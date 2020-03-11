using Intento.MT.Plugin.PropertiesForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntentoMTPlugin
{
    public static class Logs
    {
        static string tmpDir = GetTmpEnv();
        static string fileName = string.Format(@"{2}intento_log_{0}_{1}.txt", DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss"), Guid.NewGuid().ToString(), tmpDir);

        private static string GetTmpEnv()
        {
            string res = Environment.GetEnvironmentVariable("TEMP");
            if (res == null)
                res = Environment.GetEnvironmentVariable("TMP");
            if (res == null)
                res = "C:\\";

            if (!res.EndsWith("\\"))
                res = res + '\\';
            return res;
        }

        public static void Write(string text, params string[] parameters)
        {
            try
            {
                WriteLog(string.Format("{0}", string.Format(text, parameters)));
            }
            catch { }
        }

        private static void WriteLog(string text)
        {
            if (!IntentoTranslationProviderOptionsForm.IsTrace())
                return;
            try
            {
                // System.IO.File.AppendAllText(fileName, string.Format("{0}: {1}\r\n", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), text));
                return;
            }
            catch { }

            Thread.Sleep(30);
            try
            {
                // System.IO.File.AppendAllText(fileName, string.Format("{0}: {1}\r\n", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), text));
                return;
            }
            catch { }
        }

        public class Pair: IDisposable
        {
            string name;
            public Pair(string name, string text=null, params string[] parameters)
            {
                if (!IntentoTranslationProviderOptionsForm.IsTrace())
                    return;

                try
                {
                    this.name = name;
                    if (text == null)
                        WriteLog(string.Format("{0}->", name));
                    else
                        WriteLog(string.Format("{0}->{1}", name, string.Format(text, parameters)));
                }
                catch { }
            }

            public void Dispose()
            {
                try
                {
                    WriteLog(string.Format("{0}<-", name));
                }
                catch { }
            }
        }
    }
}
