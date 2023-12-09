using mre.view;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace mre.model
{
	public class JarFile
	{
		public string Path { get; set; }
		public string FullName { get; set; }
		public string Name { get; set; }
		public List<string> Folders { get; set; }

		public JarFile(string path)
		{
			Path = path;
			FullName = Path.Substring(Path.LastIndexOf('\\') + 1);
			Name = FullName.Substring(0, FullName.LastIndexOf("."));
		}

		public List<string> ListJarFolders(string javaPath, FrmMre view)
		{
			Folders = new List<string>();
			Process javaProcess = new Process();
			javaProcess.StartInfo.FileName = javaPath;
			javaProcess.StartInfo.Arguments = "-tf \"" + Path + "\"";
			javaProcess.StartInfo.UseShellExecute = false;
			javaProcess.StartInfo.RedirectStandardOutput = true;
			javaProcess.StartInfo.CreateNoWindow = true;
			javaProcess.Start();

			view.Status("Step 3/4 > Loading jar...");
			List<string> strings = javaProcess.StandardOutput.ReadToEnd().Split('\n').ToList();
			for (int i = 0; i < strings.Count; i++)
			{
				if (strings[i] != string.Empty
					&& !strings[i].Contains("META-INF")
					&& !strings[i].Contains(".class")
					&& !strings[i].Contains(".mcassetsroot")
					&& !strings[i].Contains(".xml")
					&& strings[i].Contains('/')
					&& strings[i].Contains('.'))
				{
					strings[i] = strings[i].Split('/')[0];
					if (!Folders.Contains(strings[i]) && strings[i].Length < 64)
					{
						Folders.Add(strings[i]);
					}
				}
			}

			javaProcess.WaitForExit();
			javaProcess.Close();
			view.Status("Step 3/4 > Jar loaded");
			return Folders;
		}

		public void ExtractJarFolder(string folder, Settings settings)
		{
			Directory.CreateDirectory(settings.MreDirPath + "\\mre-output\\" + Name);
			Process javaProcess = new Process();
			javaProcess.StartInfo.FileName = settings.JavaPath;
			javaProcess.StartInfo.Arguments = "-xvf \"" + Path + "\" \"" + folder + "\"";
			javaProcess.StartInfo.UseShellExecute = false;
			javaProcess.StartInfo.CreateNoWindow = true;
			javaProcess.StartInfo.WorkingDirectory = settings.MreDirPath + "\\mre-output\\" + Name;
			javaProcess.Start();
			javaProcess.WaitForExit();
			javaProcess.Close();
		}
	}
}

// 4642