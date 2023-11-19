using ReshadeBezel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;

namespace BezelMaster
{
	public partial class Form1 : Form
	{
		private static string _pluginPath = "";
		private ReshadeBezelSettings _settings = null;
		public Form1()
		{
			InitializeComponent();
		}


		public static string GetPluginPath()
		{
			if (_pluginPath != "") return _pluginPath;
			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);

			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
			string relativePluginPath = @"Plugins\BezelMaster";
			_pluginPath = Path.Combine(launchBoxRootPath, relativePluginPath);
			return _pluginPath;
		}


		private void Form1_Load(object sender, EventArgs e)
		{
			var plateforms = PluginHelper.DataManager.GetAllPlatforms();
			foreach(var plateform in plateforms)
			{
				comboBox1.Items.Add(plateform.Name);

			}
			chk_links.Checked = BezelData.UseLink;
			chk_Reshade.Checked = BezelData.DisableReshadeBezel;

		}


		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string plateformName = comboBox1.SelectedItem.ToString();
			if (!string.IsNullOrEmpty(plateformName))
			{
				button1.Enabled = false;
				groupBox1.Enabled = true;
				_settings = new ReshadeBezelSettings(plateformName);
				num_PosA.Value = (decimal)_settings.PosA;
				num_PosB.Value = (decimal)_settings.PosB;
				num_ResizeX.Value = (decimal)_settings.Size_X;
				num_ResizeY.Value = (decimal)_settings.Size_Y;
				num_Scale.Value = (decimal)_settings.Resize;
				num_Blend.Value = (decimal)_settings.Blend;
				if (_settings.Disable != 0) chk_Disable.Checked = true;
				else chk_Disable.Checked = false;
			}
			
		}



		private void button1_Click(object sender, EventArgs e)
		{
			string plateformName = comboBox1.SelectedItem.ToString();
			if (!string.IsNullOrEmpty(plateformName))
			{
				string configFile = Path.Combine(GetPluginPath(), "defaults_settings", plateformName + ".json");
				string json = _settings.Serialize();
				File.WriteAllText(configFile,json);
				MessageBox.Show($"Config Saved for {plateformName}");

			}

		}

		private void num_PosA_ValueChanged(object sender, EventArgs e)
		{
			if(_settings != null) _settings.PosA = (float)num_PosA.Value;
			button1.Enabled = true;
		}

		private void num_PosB_ValueChanged(object sender, EventArgs e)
		{
			if (_settings != null) _settings.PosB = (float)num_PosB.Value;
			button1.Enabled = true;
		}

		private void num_ResizeX_ValueChanged(object sender, EventArgs e)
		{
			if (_settings != null) _settings.Size_X = (float)num_ResizeX.Value;
			button1.Enabled = true;
		}

		private void num_ResizeY_ValueChanged(object sender, EventArgs e)
		{
			if (_settings != null) _settings.Size_Y = (float)num_ResizeY.Value;
			button1.Enabled = true;
		}

		private void num_Scale_ValueChanged(object sender, EventArgs e)
		{
			if (_settings != null) _settings.Resize = (float)num_Scale.Value;
			button1.Enabled = true;
		}

		private void num_Blend_ValueChanged(object sender, EventArgs e)
		{
			if (_settings != null) _settings.Blend = (float)num_Blend.Value;
			button1.Enabled = true;
		}

		private void chk_Disable_CheckedChanged(object sender, EventArgs e)
		{
			if (chk_Disable.Checked) _settings.Disable = 1;
			else _settings.Disable = 0;
			button1.Enabled = true;
		}

		private void chk_links_CheckedChanged(object sender, EventArgs e)
		{
			BezelData.UseLink = chk_links.Checked;
			BezelData.SaveConfig();
		}

		private void chk_Reshade_CheckedChanged(object sender, EventArgs e)
		{
			BezelData.DisableReshadeBezel = chk_Reshade.Checked;
			BezelData.SaveConfig();
		}
	}
}
