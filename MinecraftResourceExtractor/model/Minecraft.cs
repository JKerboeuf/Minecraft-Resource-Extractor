using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mre.model
{
	public class Minecraft : Target
	{
		public string McPath { get; set; } = null;
		public List<string> Versions { get; set; } = null;
		public string TargetVersion { get; set; } = null;
		public Assets AssetsFiles { get; set; } = null;

		public Minecraft()
		{
			FindMcPath();
			FindMcVersions();
		}

		public bool FindMcPath(string path = null)
		{
			string supposedPath = Environment.GetEnvironmentVariable("appdata") + "\\.minecraft";
			McPath = path ?? supposedPath;
			return Directory.Exists(McPath);
		}

		public bool FindMcVersions(string path = null)
		{
			Versions = null;
			List<string> mcVersions = new List<string>();
			DirectoryInfo versionDir = new DirectoryInfo((path ?? McPath) + "\\versions");

			if (versionDir.Exists)
			{
				mcVersions.AddRange(from directory in versionDir.GetDirectories()
									where !directory.Name.Contains("forge") && !directory.Name.Contains("fabric")
									select directory.Name);
				if (mcVersions.Count > 0)
				{
					Versions = mcVersions;
					return true;
				}
			}
			return false;
		}
	}
}