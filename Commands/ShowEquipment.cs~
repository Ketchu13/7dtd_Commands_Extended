using AllocsFixes.PersistentData;
using System;
using System.Collections.Generic;
using System.IO;

namespace AllocsFixes.CustomCommands
{
	public class ShowEquipment : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "list inventory of a given player (steam id, entity id or name)";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "ShowEquipment", "she" };
		}
		public void saveInventory(Player p, PersistentData.Inventory inv, string steamID) {

		

		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				if (_params.Count < 1) {
					SdtdConsole.Instance.Output ("Usage: ShowEquipment <steamid|playername|entityid>");
					SdtdConsole.Instance.Output ("   or: ShowEquipment <steamid|playername|entityid> <1>");
					return;
				}

				string steamid = PersistentContainer.Instance.Players.GetSteamID (_params [0], true);
				if (steamid == null) {
					SdtdConsole.Instance.Output ("Playername or entity/steamid id not found or no inventory saved (first saved after a player has been online for 30s).");
					return;
				}

				Player p = PersistentContainer.Instance.Players [steamid, false];
				PersistentData.Inventory inv = p.Inventory;
				if (p.IsOnline == true) {
					EntityPlayer ply = p.Entity;
					int num = 0;
					int vc = ply.equipment.GetSlotCount();
					SdtdConsole.Instance.Output ("**** Equipment of player " + ply.EntityName  + " ****");
					for (int i = 0; i < vc; i++){
						//ib[k].itemValue.type = 
						ItemValue gg = p.Entity.equipment.GetSlotItem(i);
						if (gg != null)
						{
							try
							{
								if (gg.type >= 1){
								ItemClass itemClass = ItemClass.GetForId(gg.type);						
								string name = itemClass.GetItemName(); //ib..GetItemName ();
								if (name != null) {
									num ++;
									SdtdConsole.Instance.Output (num + ". *    Slot # " + i +"/" + vc + " => " + name + " *");
									}
								}

							}catch (Exception e) {
								
							}
						}
					}
					SdtdConsole.Instance.Output ("**** Total of " + num + " slots equiped ****");
				}
			} catch (Exception e) {
				Log.Out ("Error in ShowEquipment.Run: " + e);
			}
		}
	}

}