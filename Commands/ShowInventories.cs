using AllocsFixes.PersistentData;
using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class ShowInventory : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "list inventory of a given player";
		}

		public override string GetHelp () {
			return "Usage:\n" +
				   "   showinventory <steam id / player name / entity id>\n" +
				   "Show the inventory of the player given by his SteamID, player name or\n" +
				   "entity id (as given by e.g. \"lpi\")." +
				   "Note: This only shows the player's inventory after it was first sent to\n" +
				   "the server which happens at least every 30 seconds.";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "showinventory", "si" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				if (_params.Count < 1) {
					SdtdConsole.Instance.Output ("Usage: showinventory <steamid|playername|entityid>");
					return;
				}

				string steamid = PersistentContainer.Instance.Players.GetSteamID (_params [0], true);
				if (steamid == null) {
					SdtdConsole.Instance.Output ("Playername or entity/steamid id not found or no inventory saved (first saved after a player has been online for 30s).");
					return;
				}

				Player p = PersistentContainer.Instance.Players [steamid, false];
				PersistentData.Inventory inv = p.Inventory;

				SdtdConsole.Instance.Output ("Belt of player " + p.Name + ":");
				for (int i = 0; i < inv.belt.Count; i++) {
					if (inv.belt [i] != null){
						if (inv.belt [i].quality < 0) {
							SdtdConsole.Instance.Output (string.Format ("    Slot {0}: {1:000} * {2}", i, inv.belt [i].count, inv.belt [i].itemName));
						}else
						{
							SdtdConsole.Instance.Output (string.Format ("    Slot {0}: {1:000} * {2}", i, inv.belt [i].count, inv.belt [i].itemName, inv.belt[i].quality));
						}
					}
							}
				SdtdConsole.Instance.Output (string.Empty);
				SdtdConsole.Instance.Output ("Bagpack of player " + p.Name + ":");
				for (int i = 0; i < inv.bag.Count; i++) {
					if (inv.bag [i] != null){
						if (inv.bag [i].quality < 0) {
							SdtdConsole.Instance.Output (string.Format ("    Slot {0}: {1:000} * {2}", i, inv.bag [i].count, inv.bag [i].itemName));
						}else
						{
						SdtdConsole.Instance.Output (string.Format ("    Slot {0}: {1:000} * {2} -- {3}", i, inv.bag [i].count, inv.bag [i].itemName, inv.bag[i].quality));
						}
					}
				}
				SdtdConsole.Instance.Output (string.Empty);
			} catch (Exception e) {
				Log.Out ("Error in ShowInventory.Run: " + e);
			}
		}
	}
}
