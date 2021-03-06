﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Timers;

namespace Intento.MT.Plugin.PropertiesForm
{

	public static class Logs
	{
		/// <summary>
		/// Limiting the request rate, ms
		/// </summary>
		const int sleepTime = 5000;
		/// <summary>
		/// Limit of entries from the queue in one request
		/// </summary>
		const int maxEntries = 300;

		static string _consumer_id;
		static string _session_id;
		public static string ApiKey { get; set; }
		public static string PluginName { get; set; }

		/// <summary>
		/// A stream instance that checks the data queue and sends it to the cloud
		/// </summary>
		static Timer sender;

		/// <summary>
		/// Data queue
		/// </summary>
		static ConcurrentQueue<KeyValuePair<char, string>> queue = new ConcurrentQueue<KeyValuePair<char, string>>();

		const string url = "https://api.inten.to/telemetry/upload_json";

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

		private static void WriteRemoteLog(char identificator, string text)
		{
			if (!IsLogging())
				return;

			if (text == null)
				text = "";

			if (string.IsNullOrWhiteSpace(ApiKey))
				return;

			queue.Enqueue(new KeyValuePair<char, string>(identificator, text));
			if (sender == null)
			{
				sender = new Timer();
				sender.Interval = sleepTime;
				sender.Elapsed += OnTimedEvent;
				sender.Start();
			}
		}
		private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
		{
			try
			{
				Dictionary<char, string> inprogress = new Dictionary<char, string>();
				KeyValuePair<char, string> item;
				JObject data = new JObject();
				data["session_id"] = SessionId;
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
				dynamic jsonResult;
				int entries = 0;
				while (queue.TryDequeue(out item) && entries < maxEntries)
				{
					if (inprogress.ContainsKey(item.Key))
						inprogress[item.Key] += "\n" + item.Value;
					else
						inprogress.Add(item.Key, item.Value);
					entries++;
				}

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
			catch { }
		}
	}

}