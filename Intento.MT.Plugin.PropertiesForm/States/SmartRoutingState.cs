using System.Collections.Generic;
using System.Linq;
using Intento.MT.Plugin.PropertiesForm.Models;
using Intento.MT.Plugin.PropertiesForm.WinForms;
using Intento.SDK.Translate.DTO;
using static Intento.MT.Plugin.PropertiesForm.WinForms.IntentoTranslationProviderOptionsForm;

namespace Intento.MT.Plugin.PropertiesForm.States
{
	public class SmartRoutingState : BaseState
	{
		// Parent state
		public ApiKeyState ApiKeyState { get; }

		// Controlled components
		public ProviderState ProviderState { get; set; }

		private readonly Dictionary<string, string> routingTable;
		private static readonly Dictionary<string, string> DefaultRoutingTable = new()
		{
			{ "", "Disabled" }, 
			{ "best", "General routing based on Intento benchmarks" }
		};

		public SmartRoutingState(ApiKeyState apiKeyState, IntentoMTFormOptions options) : base(apiKeyState, options)
		{
			ApiKeyState = apiKeyState;
			routingTable = new Dictionary<string, string>(DefaultRoutingTable);
			Routing = options.Routing;
			var routingList = FilterByLanguagePairs(apiKeyState.Routings, Form.LanguagePairs);
			foreach (var p in routingList)
			{
				var name = p.Name;
				if (routingTable.ContainsKey(name))
					routingTable[name] = p.Description;
				else
					routingTable.Add(name, p.Description);
			}

			CreateChildStates();
		}

		public static string Draw(IntentoTranslationProviderOptionsForm form, SmartRoutingState state)
		{
			if (state == null)
			{
				form.FormMt.RoutingTable = new Dictionary<string, string>(DefaultRoutingTable);
				form.FormMt.comboBoxRouting.SelectedIndex = 0;
				ProviderState.Draw(form, null);
				return null;
			}

			return state.Draw();
		}

		private string Draw()
		{
			Form.FormMt.RoutingTable = routingTable;
			if (Routing != null)
			{
				Form.FormMt.comboBoxRouting.SelectedValue = Routing;
			}

			return ProviderState.Draw(Form, ProviderState);
		}

		public void CheckedChanged()
		{
			Routing = ((KeyValuePair<string, string>)FormMt.comboBoxRouting.SelectedItem).Key;
			RoutingDescription = ((KeyValuePair<string, string>)FormMt.comboBoxRouting.SelectedItem).Value;

			CreateChildStates();

			if (SmartRouting)
			{
				Options.Format = "[\"text\",\"html\",\"xml\"]";
			}

			EnableDisable();
		}

		private static bool IsOK => true;

		public bool SmartRouting => Routing != "";

		private string Routing { get; set; }

		public string RoutingDescription { get; set; }

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
				ProviderState.FillOptions(state.ProviderState, options);
			}
			if (options.SmartRouting)
				options.Format = "[\"text\",\"html\",\"xml\"]";
		}

		private void CreateChildStates()
		{
			if (IsOK)
			{
				ProviderState = new ProviderState(this, Options);
				if (!SmartRouting) return;
				ProviderState.ClearOptions(Options);
				ProviderState = null;
			}
			else
			{
				ProviderState = null;
			}
		}

		private static IEnumerable<Routing> FilterByLanguagePairs(IEnumerable<Routing> recRouting, LangPair[] languagePairs)
		{
			if (languagePairs == null)
			{
				return recRouting;
			}

			// copy the list to keep original recRouting intact
			var ret = new List<Routing>(recRouting);
			foreach (var pair in languagePairs)
			{
				ret.RemoveAll(item => !RoutingSupportsPair(item, pair));
			}
			return ret;
		}

		private static bool RoutingSupportsPair(Routing routing, LangPair pair)
		{
			return routing.Pairs.Where(p => p.From == "" || p.From == pair.From)
				.Any(p => p.To == "" || p.To == pair.To);
		}

	}
}
