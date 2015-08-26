//7 Days To Die - Add prefab(s) to a given position or load a custom Prefabs.xml
//@Author: Alloc => 7dtd-server-fixes => https://7dtd.illy.bz/browser/binary-improvements/7dtd-server-fixes
//@Author: ketchu13 => LoadPrefabs console command => ketchu13@hotmail.com
using System;
using System.Collections.Generic;
using AllocsFixes.PersistentData;
using System.IO;

using System.Xml;

namespace AllocsFixes.CustomCommands
{
	public class LoadPrefabs: ConsoleCommand
	{
		private readonly string prefabsPath = UnityEngine.Application.dataPath + "/../Data/Prefabs";
		private readonly string prefabsXMLpath =  UnityEngine.Application.dataPath + "/../Data/Worlds/Random Gen/PrefabsKFP.xml";
		private readonly World world = CommonMappingFunctions.GetGameManager ().World;

		public LoadPrefabs(ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "Load in game a prefab.";
		}

		public override string[] Names ()
		{
			return new string[] { "addPrefab", "adp" };
            
		}

		public override void Run (string[] _params)
		{
			try {
				if (_params.Length != 5 && _params.Length != 3 && _params.Length != 1) {
					m_Console.SendResult ("Usage: addPrefab <prefabname> <x> <y> <z> <rot>");
					m_Console.SendResult ("   or: addPrefab <prefabname> <target entityid|playername|steamid> <rot>");
					return;
				}
				if (_params.Length == 1 ){
					LoadAllPrefabs ();
				}
				if (_params.Length == 5 || _params.Length == 3) {
					LoadOnePrefab (_params);
				}
			} catch (Exception ex) {
				Log.Out("LoadPrefab error: " + ex.Message);
			}
		}

		private void LoadAllPrefabs ()
		{
			if (!File.Exists (prefabsXMLpath))
			{
				m_Console.SendResult ("Prefabs XML not found...");
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(File.ReadAllText(prefabsXMLpath));
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("prefabs");
			XmlNode xmlNode = xmlNodeList[0];

			foreach (XmlNode prefabNode in xmlNode.ChildNodes)
			{
				if (prefabNode is XmlElement)
				{
					XmlElement prefabElement = (XmlElement)prefabNode;
					string prefabName = prefabElement.GetAttribute("name");
					string[] pos = prefabElement.GetAttribute("position").Split(',');
					string prefabRotation = prefabElement.GetAttribute("rotation");

					Vector3i location =  new Vector3i(int.Parse(pos[0]), int.Parse(pos[1]), int.Parse(pos[2]));
					int rot = int.Parse(prefabRotation);

					LoadSinglePrefab(prefabName, location, rot);
				}
			}
		}

		private void LoadOnePrefab (string[] _params){
			int rotation = 0;
			string fileName = _params[0];

			Vector3i position = default(Vector3i);
			if (_params.Length == 5) {
				int x, y, z;

				if (!int.TryParse (_params [1], out x) || !int.TryParse (_params [2], out y) || !int.TryParse (_params [3], out z)) {
					m_Console.SendResult ("At least one of the given coordinates is not a valid integer");
					return;
				}
				if (!int.TryParse (_params [4], out rotation) ) {
					m_Console.SendResult ("The given rotation is not a valid integer.");
					return;
				}
				if (rotation < 0 || rotation > 3) {
					m_Console.SendResult ("The given rotation is not a valid rotation value. (Use 0 to 3)");
					return;
				}
				position.x = x;
				position.y = y;
				position.z = z;
			}
			if (_params.Length == 3) {
				Player p1 = PersistentContainer.Instance.Players.GetPlayerByNameOrId (_params [1], true);
				if (p1 == null) {
					m_Console.SendResult ("Target playername or entity/steamid id not found.");
					return;
				}
				if (!p1.IsOnline) {
					m_Console.SendResult ("Target player not online.");
					return;
				}
				if (!int.TryParse (_params [2], out rotation)) {
					m_Console.SendResult ("The given rotation is not a valid integer");
					return;
				}
				position = p1.LastPosition;
			}
			LoadSinglePrefab (fileName, position, rotation);
		}

		private void LoadSinglePrefab (string fileName, Vector3i position, int rotation) {
			if (File.Exists(string.Concat(prefabsPath, "/" , fileName, ".tts"))){

				List<BlockChangeInfo> list = new List<BlockChangeInfo>();
				Prefab prefab = new Prefab();

				prefab.Load(prefabsPath,fileName);
				for (int a = 0; a < rotation; a++)
				{
					prefab.RotateY(false);
				}
				m_Console.SendResult(string.Concat(
					"Prefab ",
					fileName,
					" is Loading. Please wait...")
				);
				Random rand = new Random(DateTime.Now.Millisecond);

				Vector3i prefabPos = default(Vector3i);
				Vector3i blockPos = default(Vector3i);
				for (prefabPos.x = 0; prefabPos.x < prefab.size.x; prefabPos.x++)	{
					for (prefabPos.z = 0; prefabPos.z < prefab.size.z; prefabPos.z++)	{
						for (prefabPos.y = 0; prefabPos.y < prefab.size.y; prefabPos.y++)	{
							BlockValue block = prefab.GetBlock(prefabPos.x, prefabPos.y, prefabPos.z);
							// Use vector arithmetics (Prefab class does not support GetBlock with Vector3i)
							blockPos = position + prefabPos;
							BlockValue block2 = world.GetBlock(prefabPos);
							if (block.rawData != block2.rawData && (block.type != 0 || prefab.bCopyAirBlocks)) {
								uint int1 = LootContainer.lootPlaceholderMap.Replace(block, rand).rawData;
								BlockChangeInfo item = new BlockChangeInfo(blockPos, int1, true);
								list.Add(item);
							}
						}
					}
				}
				if (list.Count > 0)	{
					CommonMappingFunctions.GetGameManager ().SetBlocksRPC (list);
					m_Console.SendResult(string.Concat("Your prefab is Loaded: ", fileName, " - ", list.Count, " blocks loaded." ));
				}else {
					m_Console.SendResult(string.Concat("Prefab ", fileName, " contains no blocks..."));
				}
			} else {
				m_Console.SendResult(string.Concat("Prefab ", fileName , " not found..."));
			}
		}
	}
}