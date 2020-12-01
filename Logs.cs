using Intento.MT.Plugin.PropertiesForm;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{

	public static class Logs
	{
		static string _session_id;
		public static string ApiKey { get; set; }
		public static string PluginName { get; set; }

		public static string SessionId
		{
			get
			{
				try
				{
					if (_session_id == null)
						_session_id = Guid.NewGuid().ToString();
				}
				catch { }

				return _session_id;
			}
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

		public static void Write(string text, Exception ex = null)
		{
			try
			{
				if (string.IsNullOrEmpty(PluginName))
					PluginName = "undefined";
				if (!string.IsNullOrEmpty(ApiKey))
				{
					if (ex != null)
					{
						var exArr = Logs.LoggingEx(ex).ToArray();
						text += "\n" + string.Join("\n", exArr);

					}
					WriteRemoteLog(text);
				}
			}
			catch { }
		}

		public static void Logging(string subject, string comment = null, Exception ex = null)
		{
			if (!IntentoTranslationProviderOptionsForm.IsTrace())
				return;

			try
			{
				DateTime now = DateTime.UtcNow;
				List<string> content = new List<string>();
				content.Add("------------------------");
				content.Add(string.Format("{0} {1}", now.ToString("yyyy-MM-dd HH:mm:ss.fffff"), subject));
				if (comment != null)
					content.Add(comment);
				if (ex != null)
					content.AddRange(Logs.LoggingEx(ex));

				string flag = Environment.GetEnvironmentVariable("file_logging");
				if (string.IsNullOrEmpty(flag))
				{
					Logs.Write(string.Join("\n", content.ToArray()));
				}
				else
				{
					string path = Environment.GetEnvironmentVariable("temp");
					if (string.IsNullOrEmpty(path))
						path = Environment.GetEnvironmentVariable("tmp");
					if (string.IsNullOrEmpty(path))
						return;
					string filename = string.Format("{0}\\Intento_Logs_{1}", path, now.ToString("yyyy-MM-dd-HH"));
					File.AppendAllLines(filename, content);
				}
			}
			catch { }
		}
		public static IEnumerable<string> LoggingEx(Exception ex)
		{
			List<string> items = new List<string>();
			items.Add(string.Format("Exception {0}", ex.Message));
			if (ex.StackTrace != null)
			{
				items.Add("Stack Trace:");
				items.Add(ex.StackTrace);
			}
			if (ex.InnerException != null)
				items.AddRange(LoggingEx(ex.InnerException));
			return items;
		}

		private static async void WriteRemoteLog(string text)
		{
			if (!IsLogging())
				return;

			if (text == null)
				text = "";

			dynamic jsonResult;
			string url = "https://api.inten.to/telemetry/upload_json";
			JObject data = new JObject();
			data["plugin_name"] = PluginName;
			data["session_id"] = SessionId;
			data["logs"] = text;
			var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
			// Call to Intento API 
			using (var conn = new HttpClient())
			{
				conn.DefaultRequestHeaders.Add("apikey", ApiKey);
				jsonResult = await conn.PostAsync(url, content);
				//string s = await jsonResult.Content.ReadAsStringAsync();
			}
		}

	}

}