using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Runtime;

public class ReshadeBezelSettings
{
	public float Blend = 1f;
	public float PosA = 0.5f;
	public float PosB = 0.5f;
	public float Resize = 1f;
	public float Size_X = 0f;
	public float Size_Y = 0f;
	public int Disable = 0;

	public ReshadeBezelSettings(string plateformName)
	{
		if(plateformName != "")
		{
			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
			string relativePluginPath = @"Plugins\BezelMaster";
			string pluginPath = Path.Combine(launchBoxRootPath, relativePluginPath);
			string configFile = Path.Combine(pluginPath, "defaults_settings", plateformName + ".json");
			if (File.Exists(configFile))
			{
				Unserialize(File.ReadAllText(configFile));
			}
		}
	}

	public string Serialize()
	{
		string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
		return json;
	}

	public bool Unserialize(string json)
	{
		try
		{
			ReshadeBezelSettings newSettings = (ReshadeBezelSettings)JsonConvert.DeserializeObject<ReshadeBezelSettings>(json);
			this.Blend = newSettings.Blend;
			this.PosA = newSettings.PosA;
			this.PosB = newSettings.PosB;
			this.Resize = newSettings.Resize;
			this.Size_X = newSettings.Size_X;
			this.Size_Y = newSettings.Size_Y;
			this.Disable = newSettings.Disable;
			return true;
		}
		catch
		{
			return false;
		}

	}

	public bool IsDefault(string Plateformname)
	{
		ReshadeBezelSettings newSettings = new ReshadeBezelSettings(Plateformname);
		if (newSettings.Blend != this.Blend) return false;
		if(newSettings.PosA != this.PosA) return false;
		if(newSettings.PosB != this.PosB) return false;
		if(newSettings.Resize != this.Resize) return false;
		if(newSettings.Size_X != this.Size_X) return false;
		if(newSettings.Size_Y != this.Size_Y) return false;
		if(newSettings.Disable != this.Disable) return false;

		return true;
	}

}
