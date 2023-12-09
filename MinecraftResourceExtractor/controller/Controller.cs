using mre.model;
using mre.view;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace mre.controller
{
	public class Controller
	{
		public Settings settings { get; }
		private FrmMre view { get; }
		public Target target { get; set; }

		public Controller(FrmMre view)
		{
			this.view = view;
			settings = Settings.GetInstance();
		}

		public string GetJavaPath()
		{
			return settings.JavaPath;
		}

		public List<string> GetJarFolders()
		{
			return target.Jar.ListJarFolders(settings.JavaPath, view);
		}

		public void LocateMinecraft()
		{
			target = new Minecraft();
			if (((Minecraft)target).McPath != null && ((Minecraft)target).Versions != null)
			{
				view.txtPath.Text = ((Minecraft)target).McPath;
				view.cmbVersions.Items.Clear();
				foreach (var ver in ((Minecraft)target).Versions)
				{
					view.cmbVersions.Items.Add(ver);
				}
				view.Log("Please choose the version you wish to extract resources from.");
				StepsController.Step2(view);
			}
			else
			{
				view.Log("Your .minecraft folder was not located automatically. Please provide the path to your .minecraft folder.", "DarkRed");
				FolderBrowserDialog browse = new FolderBrowserDialog
				{
					Description = "Locate your .minecraft folder"
				};
				if (browse.ShowDialog() == DialogResult.OK)
				{
					if (((Minecraft)target).FindMcPath(browse.SelectedPath) && ((Minecraft)target).FindMcVersions(browse.SelectedPath))
					{
						view.txtPath.Text = ((Minecraft)target).McPath;
						view.cmbVersions.Items.Clear();
						foreach (var ver in ((Minecraft)target).Versions)
						{
							view.cmbVersions.Items.Add(ver);
						}
						view.Log("Correctly located .minecraft folder. Please choose the version you wish to extract resources from.");
						StepsController.Step2(view);
					}
					else
					{
						MessageBox.Show(
							"ERROR : Your versions folder could not be found !\n\n" +
							"Make sure you selected the correct folder or launched the game at least once.\n\n" +
							"Your .minecraft folder should be located at \"C:\\Users\\YourName\\AppData\\Roaming\\.minecraft\"",
							"Error",
							MessageBoxButtons.OK,
							MessageBoxIcon.Error
						);
						view.Log("You seem to have not provided the correct folder, make sure you selected the .minecraft folder and that you launched the game at least once.", "DarkRed");
					}
				}
			}
		}

		public void LocateJarFile()
		{
			target = new Target();
			OpenFileDialog browse = new OpenFileDialog
			{
				Filter = "Jar files (*.jar)|*.jar",
				Title = "Locate a .jar file"
			};
			if (browse.ShowDialog() == DialogResult.OK)
			{
				target.Jar = new JarFile(browse.FileName);
				view.txtPath.Text = browse.FileName;
				view.FillCheckedBox(view.chkExtFolders, target.Jar.ListJarFolders(settings.JavaPath, view));
				view.SetCheckAll(view.chkExtFolders, true);
				view.Log("Jar folders found, please choose the folders you would like to extract.");
				StepsController.Step4(view);
			}
		}

		public void FindVersionJar(string version)
		{
			((Minecraft)target).TargetVersion = version;
			target.Jar = new JarFile(((Minecraft)target).McPath
				+ "\\versions\\"
				+ version + "\\"
				+ version + ".jar");
			StepsController.Step3(view);
		}

		public void CheckVersionJar()
		{
			if (!File.Exists(target.Jar.Path))
			{
				Directory.CreateDirectory(settings.MreDirPath + "\\mre-tmp");
				string jsonPath = target.Jar.Path.Replace(".jar", ".json");
				string jsonString = File.ReadAllText(jsonPath);
				string jarUrl = (string)JObject.Parse(jsonString).SelectToken("downloads.client.url");
				target.Jar.Path = settings.MreDirPath + "\\mre-tmp\\" + target.Jar.FullName;
				view.Log("Version " + ((Minecraft)target).TargetVersion + " jar file was not found. Downloading it now...");
				StartDownload(jarUrl, target.Jar.Path, JarDownloadCompleteEvent);
			}
			else
			{
				view.FillCheckedBox(view.chkExtFolders, GetJarFolders());
				view.SetCheckAll(view.chkExtFolders, true);
				view.Log("Jar folders found, please choose the folders you would like to extract.");
				StepsController.Step4(view);
			}
		}

		private void StartDownload(string url, string fileName, AsyncCompletedEventHandler completedHandler)
		{
			Thread thread = new Thread(() =>
			{
				WebClient client = new WebClient();
				client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressEvent);
				client.DownloadFileCompleted += new AsyncCompletedEventHandler(completedHandler);
				client.DownloadFileAsync(new Uri(url), fileName);
				client.Dispose();
			});
			thread.Start();
		}

		private void DownloadProgressEvent(object sender, DownloadProgressChangedEventArgs e)
		{
			view.BeginInvoke((MethodInvoker)delegate
			{
				double bytesIn = double.Parse(e.BytesReceived.ToString());
				double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
				double percentage = bytesIn / totalBytes * 100;
				view.Status("Downloading " + e.BytesReceived / 1000 + "/" + e.TotalBytesToReceive / 1000 + " KB");
				view.pgbProgress.Value = int.Parse(Math.Truncate(percentage).ToString());
			});
		}

		private void JarDownloadCompleteEvent(object sender, AsyncCompletedEventArgs e)
		{
			view.BeginInvoke((MethodInvoker)delegate
			{
				view.Log("Jar file downloaded.");
				view.FillCheckedBox(view.chkExtFolders, GetJarFolders());
				view.SetCheckAll(view.chkExtFolders, true);
				view.Log("Jar folders found, please choose the folders you would like to extract.");
				StepsController.Step4(view);
			});
		}

		private void IndexDownloadCompleteEvent(object sender, AsyncCompletedEventArgs e)
		{
			view.BeginInvoke((MethodInvoker)delegate
			{
				view.Status("Completed");
				view.Log("Index file downloaded.");
				ReadAssets();
				MoveAssets();
			});
		}

		public void ExtractJarFolder(string folder)
		{
			view.Status("Extracting folder " + folder + " from jar...");
			view.SwitchUiLock(4);
			target.Jar.ExtractJarFolder(folder, settings);
			view.SwitchUiLock(4);
			view.Status("Jar Extracted");
		}

		public void GetAssets()
		{
			Minecraft assetsTarget = (Minecraft)target;
			assetsTarget.AssetsFiles = new Assets();
			if (!assetsTarget.AssetsFiles.FindAssetsIndex(assetsTarget.TargetVersion, assetsTarget.McPath))
			{
				view.Log("The index file was not found, it will be downloaded but this means that some files may be missing ! You should launch this version first to ensure you have all the needed files.", "DarkRed");
				if (!Directory.Exists(settings.MreDirPath + "\\mre-tmp"))
					Directory.CreateDirectory(settings.MreDirPath + "\\mre-tmp");
				string jsonPath = assetsTarget.McPath + "\\versions\\" + assetsTarget.TargetVersion + "\\" + assetsTarget.TargetVersion + ".json";
				string jsonString = File.ReadAllText(jsonPath);
				string jarUrl = (string)JObject.Parse(jsonString).SelectToken("assetIndex.url");
				assetsTarget.AssetsFiles.IndexPath = settings.MreDirPath + "\\mre-tmp\\" + assetsTarget.AssetsFiles.Version + ".json";
				StartDownload(jarUrl, assetsTarget.AssetsFiles.IndexPath, IndexDownloadCompleteEvent);
			}
			else
			{
				ReadAssets();
				MoveAssets();
			}
		}

		public void ReadAssets()
		{
			Assets assets = ((Minecraft)target).AssetsFiles;
			string jsonString = File.ReadAllText(assets.IndexPath);
			var obj = JObject.Parse(jsonString).SelectToken("objects").ToObject<JObject>();
			foreach (var x in obj)
			{
				assets.Hashes.Add(x.Key.Replace('/', '\\'), x.Value.SelectToken("hash").Value<string>());
			}
		}

		public void MoveAssets()
		{
			Minecraft mc = (Minecraft)target;
			int missingFiles = 0;
			view.Log("Starting assets extraction, this can take a while, please be patient...");
			foreach (var obj in mc.AssetsFiles.Hashes)
			{
				string folder = obj.Value.Substring(0, 2);
				string hashPath = mc.McPath + "\\assets\\objects\\" + folder + "\\" + obj.Value;
				string objPath = settings.MreDirPath + "\\mre-output\\" + mc.TargetVersion + "-assets\\" + obj.Key;
				Directory.CreateDirectory(objPath.Substring(0, objPath.LastIndexOf('\\')));
				try
				{
					File.Copy(hashPath, objPath, true);
				}
				catch (FileNotFoundException)
				{
					missingFiles++;
				}
			}
			if (missingFiles > 0)
				view.Log("Successfully copied " + (mc.AssetsFiles.Hashes.Count - missingFiles) + " assets with " + missingFiles + " missing files !", "DarkRed");
			else
				view.Log("Successfully copied " + (mc.AssetsFiles.Hashes.Count - missingFiles) + " assets ! Your files are located in the \"mre-output\" folder.", "DarkGreen");
			view.Status("Job completed !");
			view.Log("Thank you for using the Minecraft Resource Extractor made by Julien Kerboeuf !", "DarkGreen");
			Process.Start("explorer.exe", settings.MreDirPath + "\\mre-output");
		}
	}
}