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
	public class ListBlocks: ConsoleCmdAbstract
	{
		
		public override string GetDescription ()
		{
			return "Load in game a prefab.";
		}

		public override string[] GetCommands ()
		{			
			return new string[] { "ListBlocks", "lb" };            
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				if (_params.Count != 1) {
					SdtdConsole.Instance.Output ("Usage: ListBlocks <playername> or lb <playername>");
					return;
				}
				if (_params.Count == 1 ){
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
				SdtdConsole.Instance.Output ("Playername or entity/steamid id not found or no" +
					"infos saved (first saved after a player has been online for 30s).");
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

			Vector3i position = default(Vector3i);
			position = p1.LastPosition;		

			List<BlockChangeInfo> list = new List<BlockChangeInfo>();
			
			SdtdConsole.Instance.Output(string.Concat(
				"Liste en cours. ",
				"",
				" Please wait...")
			);

			try
			{				
				position.y -= 1;
				World world = GameManager.Instance.World;
				BlockValue block = (BlockValue)world.GetBlock(position);
				SdtdConsole.Instance.Output(string.Concat("posy: ", (position.y), " blockraw:" , block.rawData , ", block type: ", block.type, ", block string: " , block.ToString() ));
				return;
								
			}catch (Exception ex)
				{
				Log.Out("RemoveBlocks check block error: " + ex.Message);
			}		
			SdtdConsole.Instance.Output(string.Concat("List error."));
			return;
		}
	}
}