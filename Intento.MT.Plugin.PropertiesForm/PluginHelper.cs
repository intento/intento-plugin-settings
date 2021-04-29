using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intento.MT.Plugin.PropertiesForm
{
    public static class PluginHelper
    {
        public class ErrorInfo
		{
			public bool isError;
			public string visibleErrorText;
			public string clipBoardContent;

			public ErrorInfo(bool isError, string visibleErrorText, string clipBoardContent)
			{
				this.isError = isError;
				this.visibleErrorText = visibleErrorText;
				this.clipBoardContent = clipBoardContent;
			}
		}
	}
}
