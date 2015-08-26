using System;
using System.Collections.Generic;
using UnityEngine;

namespace AllocsFixes.CustomCommands
{
	public class Give : ConsoleCmdAbstract {
		public override string GetDescription () {
			return "give an item to a player (entity id or name)";
		}

		public override string GetHelp () {
			return "Give an item to a player by dropping it in front of that player\n" +
				"Usage:\n" +
				"   give <name / entity id> <item name> <amount>\n" +
				"   give <name / entity id> <item name> <amount> <quality>\n" +
				"Either pass the full name of a player or his entity id (given by e.g. \"lpi\").\n" +
				"Item name has to be the exact name of an item as listed by \"listitems\".\n" +
				"Amount is the number of instances of this item to drop (as a single stack).\n" +
				"Quality is the quality of the dropped items for items that have a quality.";
		}

		public override string[] GetCommands () {
			return new string[] { "give", string.Empty };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo) {
			try {
				if (_params.Count != 3 && _params.Count != 4) {
					SdtdConsole.Instance.Output ("Wrong number of arguments, expected 3 or 4, found " + _params.Count + ".");
					return;
				}

				ClientInfo ci = ConsoleHelper.ParseParamIdOrName (_params [0]);

				if (ci == null) {
					SdtdConsole.Instance.Output ("Playername or entity id not found.");
					return;
				}

				ItemValue iv = ItemList.Instance.GetItemValue (_params [1]);
				if (iv == null) {
					SdtdConsole.Instance.Output ("Item not found.");
					return;
				}

				int n = int.MinValue;
				if (!int.TryParse (_params [2], out n) || n <= 0) {
					SdtdConsole.Instance.Output ("Amount is not an integer or not greater than zero.");
					return;
				}

				if (_params.Count == 4) {
					if (!iv.HasQuality) {
						SdtdConsole.Instance.Output ("Item " + _params [1] + " does not support quality.");
						return;
					}

					int quality = int.MinValue;
					if (!int.TryParse (_params [3], out quality) || quality <= 0) {
						SdtdConsole.Instance.Output ("Quality is not an integer or not greater than zero.");
						return;
					}
					iv.Quality = quality;
				}

				EntityPlayer p = GameManager.Instance.World.Players.dict [ci.entityId];

				ItemStack invField = new ItemStack (iv, n);

				GameManager.Instance.ItemDropServer (invField, p.GetPosition (), Vector3.zero, -1, 50);

				SdtdConsole.Instance.Output ("Dropped item");
			} catch (Exception e) {
				Log.Out ("Error in Give.Run: " + e);
			}
		}
	}
}
