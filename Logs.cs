using System;
using System.Threading;
using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Intento.MT.Plugin.PropertiesForm;

namespace Intento.MT.Plugin.PropertiesForm
{
    public class Logs
    {
        private const string bucketName = "intento-external-logs";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
        private static IAmazonS3 client = null;
        private static int n = 0;
        private static string time_prefix = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        private static string guid_prefix = Guid.NewGuid().ToString();

        private static IAmazonS3 AwsClient()
        {
            if (client != null)
                return client;
            try
            {

                client = new AmazonS3Client(
                    "AKIAX7JK6EQEP57GIIQP", 
                    "Q4CotSp99dLLiA9wRo6P4/bC75p1wMd/7d+QIBt6", 
                    RegionEndpoint.USWest2);
                return client;
            }
            catch(Exception)
            {
                return null;
            }
        }

        private static bool IsTrace()
        {
            return IntentoTranslationProviderOptionsForm.IsTrace();
        }

        public static void Write2(string info, string text, params object[] parameters)
        {
            try
            {
                if (parameters.Length != 0)
                    WriteLog(info, string.Format("{0}", string.Format(text, parameters)));
                else
                    WriteLog(info, text);
            }
            catch { }
        }

        public static bool IsLogging()
        {
            string env = Environment.GetEnvironmentVariable("intento_plugin_logging");
            if (env != null)
            {
                env = env.ToLower();
                if (env == "1" || env == "true")
                    return true;
            }
            return IntentoTranslationProviderOptionsForm.IsTrace();
        }

        private static void WriteLog(string info, string text)
        {
            if (!IsLogging())
               return;

            if (text == null)
                text = "";
            var dt = DateTime.UtcNow;
            string name;
            if (string.IsNullOrEmpty(info))
                name = string.Format("{0} {1}", dt.ToString("HH:mm:ss"), n++.ToString("D3"));
            else
                name = string.Format("{0} {1}: {2}", dt.ToString("HH:mm:ss"), n++, info);
            var putRequest1 = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = string.Format("memoq/{0}/{2}", time_prefix, guid_prefix, name),
                ContentBody = text ?? ""
            };

            try
            {
                var awsClient = AwsClient();
                if (awsClient == null)
                    return;

                PutObjectResponse response1 = client.PutObject(putRequest1);

                return;
            }
            catch
            {
                Thread.Sleep(30);
                // Second attempt
                try
                {
                    var awsClient = AwsClient();
                    if (awsClient == null)
                        return;

                    PutObjectResponse response1 = client.PutObject(putRequest1);
                    return;
                }
                catch { }
            }
        }

        public class Pair : IDisposable
        {
            string info;
            public Pair(string info, string text = null, params string[] parameters)
            {
                if (!IntentoTranslationProviderOptionsForm.IsTrace())
                    return;

                try
                {
                    this.info = info;
                    if (text == null)
                        WriteLog(string.Format("{0}->", info), null);
                    else
                        WriteLog(string.Format("{0}->", info), string.Format(text, parameters));
                }
                catch { }
            }

            public void Dispose()
            {
                try
                {
                    WriteLog(string.Format("{0}<-", info), null);
                }
                catch { }
            }
        }
    }
}
