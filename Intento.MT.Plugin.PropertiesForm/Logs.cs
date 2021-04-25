using Intento.MT.Plugin.PropertiesForm;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{

	public static class Logs
	{
		static string _consumer_id;
		static string _session_id;
		public static string ApiKey { get; set; }
		public static string PluginName { get; set; }
		static object locker = new object();

		/// <summary>
		/// A stream instance that checks the data queue and sends it to the cloud
		/// </summary>
		static Task writer;

		/// <summary>
		/// Data queue
		/// </summary>
		static Dictionary<char, string> queue = new Dictionary<char, string>();

		const string url = "https://api.inten.to/telemetry/upload_json";

		/// <summary>
		/// Limiting the request rate, ms
		/// </summary>
		const int sleepTime = 5000;

		public static string ConsumerId
		{
			get
			{
				if (_consumer_id == null)
					_consumer_id = DateTime.UtcNow.ToString("yyyy-MM-dd_HH");
				return _consumer_id;
			}
		}
		public static string SessionId
		{
			get
			{
				if (_session_id == null)
					_session_id = Guid.NewGuid().ToString();
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

		public static void Write(char identificator, string subject, string comment = null, Exception ex = null)
		{
			if (!IntentoTranslationProviderOptionsForm.IsTrace())
				return;

			try
			{
				DateTime now = DateTime.UtcNow;
				List<string> content = new List<string>();
				content.Add(string.Format("{0} {1}", now.ToString("yyyy-MM-dd HH:mm:ss.fffff"), subject));
				if (comment != null)
					content.Add(comment);
				if (ex != null)
					content.AddRange(LoggingEx(identificator, ex));

				WriteRemoteLog(identificator, string.Join("\n", content.ToArray()));
			}
			catch { }
		}

		//[Obsolete("Logging is deprecated, please use Write instead.")]
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
					content.AddRange(LoggingEx('U', ex));

				string flag = Environment.GetEnvironmentVariable("file_logging");
				if (string.IsNullOrEmpty(flag))
				{
					Write('U', string.Join("\n", content.ToArray()));
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

		public static IEnumerable<string> LoggingEx(char identificator, Exception ex)
		{
			List<string> items = new List<string>();
			items.Add(string.Format("Exception {0}", ex.Message));
			if (ex.StackTrace != null)
			{
				items.Add("Stack Trace:");
				items.Add(ex.StackTrace);
			}
			if (ex.InnerException != null)
				items.AddRange(LoggingEx(identificator, ex.InnerException));
			return items;
		}

		private static async void WriteRemoteLog(char identificator, string text)
		{
			if (!IsLogging())
				return;

			if (text == null)
				text = "";

			if (string.IsNullOrWhiteSpace(ApiKey))
				return;

			lock (locker)
			{
				if (queue.ContainsKey(identificator))
					queue[identificator] += "\n" + text;
				else
					queue.Add(identificator, text);
			}
			if (writer == null || writer.IsCompleted)
			{
				writer = new Task(sender);
				writer.Start();
			}

		}

		public static async void sender()
		{
			try
			{
				Dictionary<char, string> inprogress = new Dictionary<char, string>();
				JObject data = new JObject();
				data["session_id"] = SessionId;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
				dynamic jsonResult;

				while (true)
				{
					inprogress.Clear();
					lock (locker)
					{
						if (queue.Any())
						{
							inprogress = new Dictionary<char, string>(queue);
							queue.Clear();
						}
					}
					inprogress = null;
					if (inprogress.Any())
					{
						foreach (KeyValuePair<char, string> kp in inprogress)
						{
							data["plugin_name"] = string.Format("{0}-{1}", PluginName, kp.Key);
							data["logs"] = kp.Value;
							var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
							// Call to Intento API 
							using (var conn = new HttpClient())
							{
								conn.DefaultRequestHeaders.Add("apikey", ApiKey);
								conn.DefaultRequestHeaders.Add("x-consumer-id", ConsumerId);
								jsonResult = await conn.PostAsync(url, content);
							}
						}
					}
					Thread.Sleep(sleepTime);
				}
			}
			catch { }
		}


	}

}