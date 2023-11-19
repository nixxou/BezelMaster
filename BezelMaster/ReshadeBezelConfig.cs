using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

public class ReshadeBezelConfig
{
	public ReshadeBezelSettings settings;

	public bool ConfigFileExist = false;

	private string _mainIniFile = "";
	private string _configIniFile = "";
	private string _section = "Bezel.fx";



	public ReshadeBezelConfig(string plateformName)
	{
		settings = new ReshadeBezelSettings(plateformName);
	}

	public bool Load(string ReshadeIniFile)
	{
		if (!File.Exists(ReshadeIniFile)) return false;
		if(Path.GetFileName(ReshadeIniFile).ToLower() != "reshade.ini") return false;

		_mainIniFile = ReshadeIniFile;
		return CheckConfig();
	}

	public bool CheckConfig()
	{
		if (ConfigFileExist == false && File.Exists(_mainIniFile))
		{
			var MainReshadeIni = new IniFile(_mainIniFile);
			var ConfigIniFile = MainReshadeIni.Read("PresetPath", "GENERAL");
			if (!Path.IsPathRooted(ConfigIniFile))
			{
				if (ConfigIniFile.StartsWith(".\\")) ConfigIniFile = ConfigIniFile.Substring(2);
				ConfigIniFile = Path.Combine(Path.GetDirectoryName(_mainIniFile), ConfigIniFile);
			}
			if (File.Exists(ConfigIniFile))
			{
				_configIniFile = ConfigIniFile;
				ConfigFileExist = true;
			}
		}
		return ConfigFileExist;
	}

	public bool ReadFromReshadeConfig()
	{
		if (!ConfigFileExist) return false;
		var ConfigReshadeIni = new IniFile(_configIniFile);
		int compteur_lecture = 0;
		float tmpFlt = 0;
		if (ConfigReshadeIni.KeyExists("Bezel_Blend", _section) && float.TryParse(ConfigReshadeIni.Read("Bezel_Blend", _section), NumberStyles.Float, CultureInfo.InvariantCulture, out tmpFlt))
		{
			compteur_lecture++;
			settings.Blend = tmpFlt;
		}
		if (ConfigReshadeIni.KeyExists("Bezel_Resize", _section) && float.TryParse(ConfigReshadeIni.Read("Bezel_Resize", _section), NumberStyles.Float, CultureInfo.InvariantCulture, out tmpFlt))
		{
			compteur_lecture++;
			settings.Resize = tmpFlt;
		}
		if (ConfigReshadeIni.KeyExists("Bezel_Resize_X", _section) && float.TryParse(ConfigReshadeIni.Read("Bezel_Resize_X", _section), NumberStyles.Float, CultureInfo.InvariantCulture, out tmpFlt))
		{
			compteur_lecture++;
			settings.Size_X = tmpFlt;
		}
		if (ConfigReshadeIni.KeyExists("Bezel_Resize_Y", _section) && float.TryParse(ConfigReshadeIni.Read("Bezel_Resize_Y", _section), NumberStyles.Float, CultureInfo.InvariantCulture, out tmpFlt))
		{
			compteur_lecture++;
			settings.Size_Y = tmpFlt;
		}


		if (ConfigReshadeIni.KeyExists("Bezel_Pos", _section))
		{
			string res = ConfigReshadeIni.Read("Bezel_Pos", _section);
			string[] parts = res.Split(',');
			if (parts.Length == 2)
			{
				compteur_lecture++;
				float firstValue;
				float secondValue;

				if (float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out firstValue) && float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out secondValue))
				{
					settings.PosA = firstValue;
					settings.PosB = secondValue;
				}
			}
		}

		if (ConfigReshadeIni.KeyExists("PreprocessorDefinitions", _section))
		{
			var PreprocessorDef = ConfigReshadeIni.Read("PreprocessorDefinitions", _section);

			if (!string.IsNullOrEmpty(PreprocessorDef))
			{
				Match match = Regex.Match(PreprocessorDef, @"BEZEL_DISABLE=(\d+)");
				if (match.Success)
				{
					settings.Disable = Int32.Parse(match.Groups[1].Value);
				}
			}
		}
		if (compteur_lecture >= 5) return true;
		else return false;


	}

	public bool WriteToReshadeConfig()
	{
		if (CheckConfig())
		{
			var ConfigReshadeIni = new IniFile(_configIniFile);
			ConfigReshadeIni.Write("Bezel_Blend", settings.Blend.ToString(System.Globalization.CultureInfo.InvariantCulture), _section);
			ConfigReshadeIni.Write("Bezel_Pos", $"{settings.PosA.ToString(System.Globalization.CultureInfo.InvariantCulture)},{settings.PosB.ToString(System.Globalization.CultureInfo.InvariantCulture)}", _section);
			ConfigReshadeIni.Write("Bezel_Resize", settings.Resize.ToString(System.Globalization.CultureInfo.InvariantCulture), _section);
			ConfigReshadeIni.Write("Bezel_Resize_X", settings.Size_X.ToString(System.Globalization.CultureInfo.InvariantCulture), _section);
			ConfigReshadeIni.Write("Bezel_Resize_Y", settings.Size_Y.ToString(System.Globalization.CultureInfo.InvariantCulture), _section);
			string PreprocessorValue = $"BEZEL_DISABLE={settings.Disable}";
			ConfigReshadeIni.Write("PreprocessorDefinitions", PreprocessorValue, _section);
			return true;
		}
		return false;
	}


}