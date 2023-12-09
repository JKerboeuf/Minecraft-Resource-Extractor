using System;
using System.IO;
using System.Linq;

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

			searchResult = start.GetDirectories("Java").Concat(startX86.GetDirectories("Java")).ToArray();

			foreach (var dir in searchResult)
			{
				FileInfo[] foundFiles = dir.GetFiles("jar.exe", SearchOption.AllDirectories);
				if (foundFiles.Length != 0)
				{
					return foundFiles[0].FullName;
				}
			}
			return null;
		}
	}
}