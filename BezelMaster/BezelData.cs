using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;

namespace ReshadeBezel
{
	public class BezelData : ISystemEventsPlugin
	{
		public static Dictionary<int, string> DataBaseBezel = new Dictionary<int, string>();
		public static Dictionary<string, string> CoreMatch = new Dictionary<string, string>();
		public static Dictionary<string, string> BezelMatch = new Dictionary<string, string>();
		public static bool DataLoaded = false;
		public static bool UseLink = false;
		public static bool DisableReshadeBezel = false;

		public static void SaveConfig()
		{
			Dictionary<string, string> configKeys = new Dictionary<string, string>();
			string ConfigFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "config.json");
			configKeys.Add("UseLink", UseLink ? "True" : "False");
			configKeys.Add("UseReshadeBezel", DisableReshadeBezel ? "True" : "False");
			try
			{
				string json = JsonConvert.SerializeObject(configKeys, Newtonsoft.Json.Formatting.Indented);
				File.WriteAllText(ConfigFile, json);
			}
			catch { }
		}
		public void OnEventRaised(string eventType)
		{
			if (eventType == "LaunchBoxStartupCompleted" || eventType == "BigBoxStartupCompleted")
			{
				if (!DataLoaded)
				{
					string defaultBezelMatchFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "defaultbezelmatch.json");
					BezelMatch = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(defaultBezelMatchFile));

					string corematchFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "corematch.json");
					CoreMatch = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(corematchFile));

					string DataBaseBezelFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "databaseBezel.json");
					DataBaseBezel = JsonConvert.DeserializeObject<Dictionary<int, string>>(File.ReadAllText(DataBaseBezelFile));

					Dictionary<string, string> configKeys = new Dictionary<string, string>();
					string ConfigFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "config.json");
					if(File.Exists(ConfigFile))
					{
						configKeys = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(ConfigFile));
						if (configKeys.ContainsKey("UseLink"))
						{
							if (configKeys["UseLink"].ToLower() == "true")
							{
								UseLink = true;
							}
							else
							{
								UseLink = false;
							}
						}
						if (configKeys.ContainsKey("UseReshadeBezel"))
						{
							if (configKeys["UseReshadeBezel"].ToLower() == "true")
							{
								DisableReshadeBezel = true;
							}
							else
							{
								DisableReshadeBezel = false;
							}
						}
					}


					DataLoaded = true;

					// Get the Windows identity of the current user



				}


			}
		}
	}
}
