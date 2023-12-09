using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace mre.model
{
	public class Assets
	{
		public string Version { get; set; }
		public string IndexPath { get; set; }
		public Dictionary<string, string> Hashes { get; set; }

		public Assets()
		{
			Hashes = new Dictionary<string, string>();
		}

		public bool FindAssetsIndex(string version, string mcPath)
		{
			string versionJson = mcPath + "\\versions\\" + version + "\\" + version + ".json";
			string jsonString = File.ReadAllText(versionJson);
			Version = (string)JObject.Parse(jsonString).SelectToken("assetIndex.id");
			IndexPath = mcPath + "\\assets\\indexes\\" + Version + ".json";
			return File.Exists(IndexPath);
		}
	}
}