using mre.view;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace mre.model
{
	public class Settings
	{
		private static Settings _instance = null;
		public string MreDirPath { get; set; }
		public string JavaPath { get; set; } = null;

		private Settings()
		{
			MreDirPath = Directory.GetCurrentDirectory();
			JavaPath = LocateJava();
		}

		public static Settings GetInstance()
		{
			if (_instance == null)
			{
				_instance = new Settings();
			}
			return _instance;
		}

		private string LocateJava()
		{
			DirectoryInfo[] searchResult;
			DirectoryInfo start = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			DirectoryInfo startX86 = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));

			var javaDirs = Enumerable.Empty<DirectoryInfo>();
			if (start.Exists)
				javaDirs = javaDirs.Concat(start.GetDirectories("Java", SearchOption.TopDirectoryOnly));
			if (startX86.Exists)
				javaDirs = javaDirs.Concat(startX86.GetDirectories("Java", SearchOption.TopDirectoryOnly));

			searchResult = javaDirs.ToArray();

			foreach (var dir in searchResult)
			{
				FileInfo[] foundFiles = dir.GetFiles("jar.exe", SearchOption.AllDirectories);
				if (foundFiles.Length != 0)
				{
					return foundFiles[0].FullName;
				}
			}
			using (var dlg = new FrmJarPathPrompt())
			{
				var result = dlg.ShowDialog();
				if (result == DialogResult.OK)
					return dlg.SelectedPath;
			}
			return null;
		}
	}
}