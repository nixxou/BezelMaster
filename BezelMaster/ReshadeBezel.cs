using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;



namespace ReshadeBezel
{
	public class ReshadeBezel : IGameLaunchingPlugin
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern bool CreateSymbolicLink(string symlinkFileName, string targetFileName, int flags);

		// Constante pour le type de lien symbolique
		private const int SYMBOLIC_LINK_FLAG_FILE = 0x0;
		private const int SYMBOLIC_LINK_FLAG_DIRECTORY = 0x1;

		IGame CurrentGame = null;
		string CurrentReshadeIni = "";
		public void OnAfterGameLaunched(IGame game, IAdditionalApplication app, IEmulator emulator)
		{

		}

		public void OnBeforeGameLaunching(IGame game, IAdditionalApplication app, IEmulator emulator)
		{
			string BezelJSONData = "";
			var ReshadeConfig = new ReshadeBezelConfig(game.Platform);

			if (!BezelData.DisableReshadeBezel)
			{
				var customFields = game.GetAllCustomFields();
				foreach (var field in customFields)
				{
					if (field.Name == "Bezel_DATA")
					{
						BezelJSONData = field.Value;
						break;
						//game.TryRemoveCustomField(field);
					}
				}
			}


			string RetroArchPath = "";
			string RetroArchCmdLine = "";
			string pathBezelImg = "";

			var ExistingBezelFile = Path.Combine(Path.GetDirectoryName(emulator.ApplicationPath), "reshade-shaders", "Textures", "bezel.png");
			string ReshadeIni = Path.Combine(Path.GetDirectoryName(emulator.ApplicationPath), "ReShade.ini");

			if (!BezelData.DisableReshadeBezel)
			{
				var DestShader = Path.Combine(Path.GetDirectoryName(emulator.ApplicationPath), "reshade-shaders", "Shaders", "Bezel.fx");
				var SourceShader = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Bezel.fx");
				if(!File.Exists(DestShader) && Directory.Exists(Path.GetDirectoryName(DestShader)))
				{
					File.Copy(SourceShader, DestShader, true);
					Thread.Sleep(100);
				}


				if (File.Exists(ExistingBezelFile)) File.Delete(ExistingBezelFile);
				Thread.Sleep(100);
				bool haveReshadeIni = ReshadeConfig.Load(ReshadeIni);
				if (haveReshadeIni)
				{
					if (BezelJSONData != "")
					{
						if (BezelJSONData != "")
						{
							ReshadeConfig.settings.Unserialize(BezelJSONData);
						}
					}
					ReshadeConfig.WriteToReshadeConfig();
					CurrentGame = game;
					CurrentReshadeIni = ReshadeIni;
				}
				else
				{
					CurrentGame = null;
					CurrentReshadeIni = "";
				}
			}
			else
			{
				bool haveReshadeIni = ReshadeConfig.Load(ReshadeIni);
				if (haveReshadeIni)
				{
					ReshadeConfig.settings.Disable = 1;
					ReshadeConfig.WriteToReshadeConfig();
				}
			}

			if (emulator.Title.ToLower().Contains("retroarch"))
			{
				foreach (var emulatorPlatform in emulator.GetAllEmulatorPlatforms())
				{
					if (emulatorPlatform.Platform == game.Platform)
					{
						RetroArchPath = Path.GetDirectoryName(emulator.ApplicationPath);
						RetroArchCmdLine = emulatorPlatform.CommandLine;
						break;
					}

				}
			}

			if (RetroArchPath == "" && game.Platform != null)
			{
				foreach (var RetroArchEmulator in PluginHelper.DataManager.GetAllEmulators())
				{
					if(RetroArchEmulator.Title.ToLower().Contains("retroarch"))
					{
						foreach (var emulatorPlatform in RetroArchEmulator.GetAllEmulatorPlatforms())
						{
							if (emulatorPlatform.Platform == game.Platform)
							{
								RetroArchPath = Path.GetDirectoryName(RetroArchEmulator.ApplicationPath);
								RetroArchCmdLine = emulatorPlatform.CommandLine;
								break;
							}

						}
						if (RetroArchPath != "") break;
					}
					if (RetroArchPath != "") break;
				}
			}
			if(RetroArchPath == "")
			{
				return;
			}

			Match match = Regex.Match(RetroArchCmdLine, @"\""([^\""]+\.(?:dll|DLL))\""");
			if (match.Success)
			{
				string dllName = Path.GetFileName(match.Groups[1].Value);

				if (BezelData.CoreMatch.ContainsKey(dllName))
				{
					string coredir = Path.Combine(RetroArchPath, "config", BezelData.CoreMatch[dllName]);
					if (Directory.Exists(coredir))
					{
						string base_cfg_source = Path.Combine(coredir, Path.GetFileNameWithoutExtension(game.ApplicationPath) + ".cfg");
						string cfg_source = GetEmulatorBezelConfig(game, coredir);
						if (cfg_source != "")
						{

							if (cfg_source != base_cfg_source)
							{
								File.Copy(cfg_source, base_cfg_source, true);
								File.SetCreationTime(base_cfg_source, DateTime.Now);
								File.SetLastWriteTime(base_cfg_source, DateTime.Now);
								Thread.Sleep(100);
							}
						}

						if (cfg_source != "")
						{
							pathBezelImg = GetBezelFromConfig(cfg_source, RetroArchPath);
						}
						else
						{
							pathBezelImg = FindDefaultBezel(game, coredir, RetroArchPath);
						}
					}
				}
			}
			else
			{
				return;
			}
			if (pathBezelImg == "") return;

			if (BezelData.DisableReshadeBezel) return;

			var ReshadeShaderPath = Path.Combine(Path.GetDirectoryName(emulator.ApplicationPath), "reshade-shaders", "Shaders");
			if(!Directory.Exists(ReshadeShaderPath))
			{
				return;
			}
			else
			{
				var ReshadeTexturePath = Path.Combine(Path.GetDirectoryName(emulator.ApplicationPath), "reshade-shaders", "Textures");
				if (!Directory.Exists(ReshadeTexturePath)) Directory.CreateDirectory(ReshadeTexturePath);
				string BezelOut = Path.Combine(ReshadeTexturePath, "bezel.png");
				//if(File.Exists(BezelOut)) File.Delete(BezelOut);
				if (BezelData.UseLink)
				{
					CreateSoftlink(pathBezelImg, BezelOut);

				}
				else
				{
					File.Copy(pathBezelImg, BezelOut, true);
				}
				Thread.Sleep(100);
			}

			

		}

		public void OnGameExited()
		{
			if (!BezelData.DisableReshadeBezel)
			{
				if (CurrentGame != null && CurrentReshadeIni != "")
				{
					var ReshadeConfig = new ReshadeBezelConfig(CurrentGame.Platform);
					bool haveReshadeIni = ReshadeConfig.Load(CurrentReshadeIni);
					if (!haveReshadeIni) return;
					if (ReshadeConfig.ReadFromReshadeConfig())
					{
						if (!ReshadeConfig.settings.IsDefault(CurrentGame.Platform))
						{
							string jsonConfig = ReshadeConfig.settings.Serialize();
							var customFields = CurrentGame.GetAllCustomFields();
							foreach (var field in customFields)
							{
								if (field.Name == "Bezel_DATA")
								{
									CurrentGame.TryRemoveCustomField(field);
								}
							}
							var newField = CurrentGame.AddNewCustomField();
							newField.Name = "Bezel_DATA";
							newField.Value = jsonConfig;
						}
					}
				}
			}
		}

		public static string GetBezelFromConfig(string cfg_source, string RetroArchPath)
		{
			string cfg_content = File.ReadAllText(cfg_source);
			Match matchBezel = Regex.Match(cfg_content, @"\""([^\""]+)\""");
			if (matchBezel.Success)
			{
				string pathBezel = matchBezel.Groups[1].Value;
				if (pathBezel.StartsWith(":/"))
				{
					pathBezel = pathBezel.Substring(2);
				}
				if (pathBezel.StartsWith("/"))
				{
					pathBezel = pathBezel.Substring(1);
				}
				if (!Path.IsPathRooted(pathBezel))
				{
					pathBezel = Path.Combine(RetroArchPath, pathBezel);
				}
				string pathBezelImg = Path.Combine(Path.GetDirectoryName(pathBezel), Path.GetFileNameWithoutExtension(pathBezel)) + ".png";
				if (File.Exists(pathBezelImg))
				{
					return pathBezelImg;
				}
			}
			return "";
		}

		public static string GetEmulatorBezelConfig(IGame game, string coredir)
		{
			string cfg_source = Path.Combine(coredir, Path.GetFileNameWithoutExtension(game.ApplicationPath) + ".cfg");

			if (File.Exists(cfg_source))
			{
				return cfg_source;
			}
			else
			{
				var platforms = PluginHelper.DataManager.GetAllPlatforms();
				foreach(var plat in platforms)
				{
					if (plat.Name == game.Platform)
					{
						foreach (var otherGames in plat.GetAllGames(true, true))
						{
							if ((otherGames.LaunchBoxDbId == game.LaunchBoxDbId) && (otherGames.Id != game.Id))
							{
								cfg_source = Path.Combine(coredir, Path.GetFileNameWithoutExtension(otherGames.ApplicationPath) + ".cfg");
								if (File.Exists(cfg_source))
								{
									return cfg_source;
								}
							}

						}
					}

				}
			}

			if(game.LaunchBoxDbId != null && game.LaunchBoxDbId != 0)
			{
				if (BezelData.DataBaseBezel.ContainsKey((int)game.LaunchBoxDbId))
				{
					cfg_source = Path.Combine(coredir, BezelData.DataBaseBezel[(int)game.LaunchBoxDbId]);
					if (File.Exists(cfg_source))
					{
						return cfg_source;
					}

				}
			}
			
			return "";
		}

		public static string FindDefaultBezel(IGame game, string coredir, string RetroArchPath)
		{
			var platforms = PluginHelper.DataManager.GetAllPlatforms();
			foreach (var plat in platforms)
			{
				if(plat.Name == game.Platform)
				{
					foreach (var otherGames in plat.GetAllGames(true, true))
					{
						if (otherGames.Id != game.Id)
						{
							string cfg_source_other = Path.Combine(coredir, Path.GetFileNameWithoutExtension(otherGames.ApplicationPath) + ".cfg");
							if (File.Exists(cfg_source_other))
							{
								string pathBezelImg = GetBezelFromConfig(cfg_source_other, RetroArchPath);

								string DirBezel = Path.GetDirectoryName(pathBezelImg);
								string PathBezel = Path.GetDirectoryName(DirBezel);
								PathBezel = Path.GetDirectoryName(PathBezel);

								DirBezel = Path.GetFileName(DirBezel);


								if (BezelData.BezelMatch.ContainsKey(DirBezel))
								{
									string DefaultBezel = Path.Combine(PathBezel, BezelData.BezelMatch[DirBezel]);
									return DefaultBezel;
								}
							}
						}

					}
				}

			}
			return "";
		}

		public static bool CreateSoftlink(string sourceFilePath, string targetFilePath)
		{
			sourceFilePath = Path.GetFullPath(sourceFilePath);
			targetFilePath = Path.GetFullPath(targetFilePath);
			// Vérifier si le fichier source existe
			if (!File.Exists(sourceFilePath))
			{
				return false;
			}

			// Créer le lien symbolique
			try
			{
				if (File.Exists(targetFilePath))
				{
					// Supprimer le fichier existant s'il existe déjà
					File.Delete(targetFilePath);
				}

				// Appeler la fonction CreateSymbolicLink pour créer le lien symbolique
				bool success = CreateSymbolicLink(targetFilePath, sourceFilePath, SYMBOLIC_LINK_FLAG_FILE);

				return success;
			}
			catch (Exception)
			{
				// Gérer les erreurs éventuelles lors de la création du lien symbolique
				return false;
			}
		}
	}
}
