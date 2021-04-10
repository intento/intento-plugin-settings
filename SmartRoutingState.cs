using Intento.MT.Plugin.PropertiesForm.WinForms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using static Intento.MT.Plugin.PropertiesForm.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm
{
	public class SmartRoutingState : BaseState
	{
		// Parent state
		public ApiKeyState apiKeyState;

		// Controlled components
		public ProviderState providerState;

		public string routing;
		public string routingDescription;

		Dictionary<string, string> routingTable;
		static Dictionary<string, string> defaultRoutingTable = new Dictionary<string, string>() { { "", "Disabled" }, { "best", "General routing based on Intento benchmarks" } };


		public SmartRoutingState(ApiKeyState apiKeyState, IntentoMTFormOptions _options) : base(apiKeyState, _options)
		{
			this.apiKeyState = apiKeyState;
			routingTable = new Dictionary<string, string>(defaultRoutingTable);
			routing = _options.Routing;
			List <dynamic> routingList = FilterByLanguagePairs(apiKeyState.routings, form.LanguagePairs);
			foreach (dynamic p in routingList)
			{
				string name = (string)p.name;
				if (routingTable.ContainsKey(name))
					routingTable[name] = (string)p.description;
				else
					routingTable.Add(name, (string)p.description);
			}

			CreateChildStates();
		}

		public static string Draw(IntentoTranslationProviderOptionsForm form, SmartRoutingState state)
		{
			if (state == null)
			{
				form.formMT.RoutingTable = new Dictionary<string, string>(defaultRoutingTable);
				form.formMT.comboBoxRouting.SelectedIndex = 0;
				ProviderState.Draw(form, null);
				return null;
			}

			return state.Draw();
		}

		public string Draw()
		{
			form.formMT.RoutingTable = routingTable;
			if (routing != null)
				form.formMT.comboBoxRouting.SelectedValue = routing;

			return ProviderState.Draw(form, providerState);
		}

		public void CheckedChanged()
		{
			routing = ((KeyValuePair<string, string>)formMT.comboBoxRouting.SelectedItem).Key;
			routingDescription = ((KeyValuePair<string, string>)formMT.comboBoxRouting.SelectedItem).Value;


			CreateChildStates();

			if (SmartRouting)
				options.Format = "[\"text\",\"html\",\"xml\"]";

			EnableDisable();
		}

		public bool IsOK
		{
			get { return true; }
		}

		public bool SmartRouting
		{ get { return routing != ""; } }

		public string Routing
		{ get { return routing; } }

		public string RoutingDescription
		{
			get
			{
				return routingDescription;
			}
	    }

		public static void FillOptions(SmartRoutingState state, IntentoMTFormOptions options)
		{

			if (state == null)
			{
				options.Routing = "best";
				options.RoutingDisplayName = Resource.BestSmartRouteDescription;
				options.SmartRouting = true;
				ProviderState.FillOptions(null, options);
			}
			else
			{
				options.SmartRouting = state.SmartRouting;
				options.Routing = state.Routing;
				options.RoutingDisplayName = state.RoutingDescription;
				ProviderState.FillOptions(state.providerState, options);
			}
			if (options.SmartRouting)
				options.Format = "[\"text\",\"html\",\"xml\"]";
		}

		private void CreateChildStates()
		{
			if (IsOK)
			{
				providerState = new ProviderState(this, options);
				if (SmartRouting)
				{
					providerState.ClearOptions(options);
					providerState = null;
				}
			}
			else
				providerState = null;
		}

		private List<dynamic> FilterByLanguagePairs(List<dynamic> recRouting, LangPair[] languagePairs)
		{
			if (languagePairs == null)
				return recRouting;

			// copy the list to keep original recRouting intact
			List<dynamic> ret = new List<dynamic>(recRouting);
			foreach (LangPair pair in languagePairs)
			{
				ret.RemoveAll(item => !RoutingSupportsPair(item, pair));
			}
			return ret;
		}

		private static bool RoutingSupportsPair(dynamic routing, LangPair pair)
		{
			foreach (dynamic p in routing.pairs)
			{
				if (p.from == "" || p.from == pair.from)
					if (p.to == "" || p.to == pair.to)
						return true;
			}
			return false;
		}

	}
}
