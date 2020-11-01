using System;
using Intento.MT.Plugin.PropertiesForm;

public class Logs
{
	//static Logs2 logs;

	//private static Logs2 GetLogs()
	//{
	//    try
	//    {
	//        if (logs == null)
	//            logs = new Logs2();
	//    }
	//    catch { }

	//    return logs;
	//}

	public static void Write2(string info, string text, params object[] parameters)
	{
		return;
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
}


