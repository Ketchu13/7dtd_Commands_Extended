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
	public class RemoveBlocks: ConsoleCmdAbstract
	{
		
		private readonly string prefabsPath = UnityEngine.Application.dataPath + "/../Data/Prefabs";
		private readonly string prefabsXMLpath =  UnityEngine.Application.dataPath + "/../Data/Worlds/Random Gen/PrefabsKFP.xml";


		public override string GetDescription ()
		{
			return "Load in game a prefab.";
		}

		public override string[] GetCommands ()
		{			
			return new string[] { "removeblock", "rmb" };            
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				if (_params.Count != 6) {
					SdtdConsole.Instance.Output ("Usage: removeblock <target entityid|playername|steamid> <targetBlockId> <newBlockId> <sizex> <sizez> <sizey>");
					SdtdConsole.Instance.Output ("Usage: removeblock ketchu13 295 200");
					return;
				}
				if (_params.Count == 6 ){
					RemoveBlock (_params);
				}
			
			} catch (Exception ex) {
				Log.Out("RemoveBlocks error: " + ex.Message);
			}
		}

		private void RemoveBlock (List<string> _params)
		{

			string steamid = PersistentContainer.Instance.Players.GetSteamID (_params [0], true);
			if (steamid == null) {
				SdtdConsole.Instance.Output ("Playername or entity/steamid id not found or no inventory saved (first saved after a player has been online for 30s).");
				return;
			}

			Player p1 = PersistentContainer.Instance.Players [steamid, false];
			if (p1 == null) {
				SdtdConsole.Instance.Output ("Target playername or entity/steamid id not found.");
				return;
			}
			if (!p1.IsOnline) {
				SdtdConsole.Instance.Output ("Target player not online.");
				return;
			}
			int targetBlockId = 0;
			int newBlockId = 0;
			int sizex = 0;
			int sizez = 0;
			int sizey = 0;
			if (!int.TryParse(_params[1],out targetBlockId) || !int.TryParse(_params[2],out newBlockId) || !int.TryParse(_params[3],out sizex) || !int.TryParse(_params[4],out sizez) || !int.TryParse(_params[5],out sizey )) {
				SdtdConsole.Instance.Output ("Not a good value...");
				return;
			}
			Vector3i position = default(Vector3i);
			position = p1.LastPosition;		

			List<BlockChangeInfo> list = new List<BlockChangeInfo>();
			
			SdtdConsole.Instance.Output(string.Concat(
				"Effacement en cours. ",
				"",
				" Please wait...")
			);
			int midSizex = (int) Math.Floor ((sizex/2d));
			int midSizez = (int) Math.Floor ((sizez/2d));
			int midSizey = (int) Math.Floor ((sizey/2d));
			SdtdConsole.Instance.Output(string.Concat("targetBlockId: " , targetBlockId , "  newBlockId: ", newBlockId ));
			World world = GameManager.Instance.World;
			Vector3i prefabPos =  default(Vector3i);
			try
			{
				for (prefabPos.x = - midSizex; prefabPos.x <= midSizex; prefabPos.x++)	{
					for (prefabPos.z = - midSizez; prefabPos.z <= midSizez; prefabPos.z++)	{
						for (prefabPos.y = - midSizey; prefabPos.y <= midSizey; prefabPos.y++)	{	

							BlockValue block = world.GetBlock(prefabPos + position);

							try
							{
								SdtdConsole.Instance.Output(string.Concat("posx: ", (prefabPos.x + position.x), " blockraw:" , block.rawData , ", block type: ", block.type, ", block string: " , block.ToString() ));
								if (block.type == targetBlockId || targetBlockId == -1 ) {
									block.damage = 0;//reparatoin
									SdtdConsole.Instance.Output(string.Concat("int1: " , newBlockId ));
									uint int1 = (uint) newBlockId;
									SdtdConsole.Instance.Output(string.Concat("int1: " , int1 ));
									BlockChangeInfo item = new BlockChangeInfo(prefabPos + position , int1, true);
									list.Add(item);
								}
							}catch (Exception ex)
							{
								Log.Out("RemoveBlocksd check block error: " + ex.Message);
							}	
						}
					}
				}
			}catch (Exception ex)
				{
				Log.Out("RemoveBlocks check block error: " + ex.Message);
			}		
			if (list.Count > 0)	{
				GameManager.Instance.SetBlocksRPC (list);
				SdtdConsole.Instance.Output(string.Concat("Nettoyage terminé : ", list.Count, " blocks effacés." ));
			}else {
				SdtdConsole.Instance.Output(string.Concat("Nettoyage impossible ", newBlockId, " block introuvable."));
			}
		}

	}
}