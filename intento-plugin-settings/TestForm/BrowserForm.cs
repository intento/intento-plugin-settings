using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
	public partial class BrowserForm : Form
	{
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		[System.Runtime.InteropServices.ComVisibleAttribute(true)]
		public class Trusted
		{
			BrowserForm form;

			public Trusted(BrowserForm form)
			{
				this.form = form;
			}
			public void Exchange(String jsonString)
			{
				dynamic data = JObject.Parse(jsonString);
				form.label2.Text = (string)data.apikey;
			}
		}

		public BrowserForm(int ieVersion = -1)
		{
			InitializeComponent();
			string err = null;
			if (BrowserEmulator.GetInternetExplorerMajorVersion() == -1 )
				err = "Internet Explorer is not installed on your operating system";
			else if (!BrowserEmulator.SetBrowserEmulationVersion(ieVersion))
				err = "Requires installed Internet Explorer version 11 or later";
			labelIEVersion.Text = BrowserEmulator.GetIEEmulationVersion().ToString();

			if (err != null)
			{
				MessageBox.Show(err, "IE error");
				Close();
			}
			else
			{
				webBrowser1.ObjectForScripting = new Trusted(this);
				buttonFillCode_Click(null, null);
				BrowserForm_Resize(null, null);
			}
		}

		private void buttonToIntento_Click(object sender, EventArgs e)
		{
			webBrowser1.Url = new Uri(((Button)sender).Text);
		}

		private void buttonFillCode_Click(object sender, EventArgs e)
		{
			//webBrowser1.DocumentText =
			//	"<html><head><script>" +
			//	"function testfromwinform(message) { alert(message); }" +
			//	"var s = '{\"apikey\":\"1234567890-0987654321\"}'" +
			//	"</script></head><body><button " +
			//	"onclick='window.external.Exchange(s)'>" +
			//	"call winform code with apikey data 1234567890-0987654321</button>" +
			//	"<br><br><strong>Code for button</strong><br>" +
			//	"<pre><xmp><script>\r\n" +
			//	"function testfromwinform(message) { alert(message); }\r\n" +
			//	"var s = '{\"apikey\":\"1234567890-0987654321\"}'\r\n" +
			//	"</script>\r\n<button " +
			//	"onclick='window.external.Exchange(s)'>" +
			//	"call winform code with apikey data 1234567890-0987654321</button>" +
			//	"</xmp><pre></body></html>";
			webBrowser1.DocumentText =
				"<html><head><script>" +
				"var s = '{\"apikey\":\"1234567890-0987654321\"}';" +
				"function exchange() { window.external.Exchange(s); }" +
				"function testfromwinform(message) { alert(message); }" +
				"</script></head><body><button " +
				"onclick='window.external.Exchange(s)'>" +
				"call winform code with apikey data 1234567890-0987654321</button>" +
				"<br><br><strong>Code for button</strong><br>" +
				"<pre><xmp><script>\r\n" +
				"function exchange() {\r\n var json_string = '{\"apikey\":\"1234567890-0987654321\"}';\r\n window.external.Exchange(json_string); \r\n}" +
				"\r\n</script>\r\n<button " +
				"onclick='exchange'>\r\n" +
				"call winform code with apikey data 1234567890-0987654321\r\n</button>" +
				"</xmp><pre></body></html>";
		}

	private void button1_Click(object sender, EventArgs e)
		{
			label2.Text = "";
		}

		private void BrowserForm_Resize(object sender, EventArgs e)
		{
			var s = string.Format("{0}x{1}", webBrowser1.Width, webBrowser1.Height);
			label4.Text = s;
		}
	}
}
