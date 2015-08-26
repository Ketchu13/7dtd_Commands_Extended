
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class RenderMap : ConsoleCmdAbstract
	{
		/*private readonly string prefabsPath = UnityEngine.Application.dataPath + "/../Data/Prefabs";
		private readonly string prefabsXMLpath =  UnityEngine.Application.dataPath + "/../Data/Worlds/Random Gen/PrefabsKFP.xml";
		private readonly World world = CommonMappingFunctions.GetGameManager ().World;*/

	
		public override string GetDescription ()
		{
			return "RenderMap in game a prefab.";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "rendermap", "cumu" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				//if (_params.Length > 1) {
					Process runMapReader = new Process();
					string mapReaderPath = "D:\\Inventories\\map_reader1.3.0_64bits.exe";
					string mapFilesPath = "D:\\Inventories\\map";
					string mapTilesPath = "D:\\Inventories\\tiles";
					runMapReader.StartInfo.CreateNoWindow = false;
					runMapReader.StartInfo.UseShellExecute = false;
					runMapReader.StartInfo.FileName = mapReaderPath;
					runMapReader.StartInfo.Arguments = "-g \"" + mapFilesPath + "\" -t \"" + mapTilesPath + "\"";
					runMapReader.Start();				//}
			} catch (Exception ex) {
				Log.Out("LoadPrefab error: " + ex.Message);
			}		
		}
	}
}
