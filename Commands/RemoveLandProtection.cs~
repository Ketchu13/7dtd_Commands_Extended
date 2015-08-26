using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class RemoveLandProtection : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "removes the association of a land protection block to the owner";
		}

		public override string GetHelp () {
			return "Usage:" +
				   "  1. removelandprotection <steamid>\n" +
				   "  2. removelandprotection <x> <y> <z>\n" +
				   "1. Remove all land claims owned by the user with the given SteamID\n" +
				   "2. Remove only the claim block on the exactly given block position";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "removelandprotection", "rlp" };
		}

		private void removeById (string _id)
		{
			try {
				PersistentPlayerList ppl = GameManager.Instance.GetPersistentPlayerList ();

				if (_id.Length < 1 || !ppl.Players.ContainsKey (_id)) {
					SdtdConsole.Instance.Output ("Not a valid Steam ID or user has never logged on. Use \"listlandprotection\" to get a list of keystones.");
					return;
				}
				if (ppl.Players [_id].LPBlocks == null || ppl.Players [_id].LPBlocks.Count == 0) {
					SdtdConsole.Instance.Output ("Player does not own any keystones. Use \"listlandprotection\" to get a list of keystones.");
					return;
				}

				List<BlockChangeInfo> changes = new List<BlockChangeInfo> ();
				foreach (Vector3i pos in ppl.Players[_id].LPBlocks) {
					BlockChangeInfo bci = new BlockChangeInfo (pos, 0, true);
					changes.Add (bci);
				}
				GameManager.Instance.SetBlocksRPC (changes);

				SdtdConsole.Instance.Output ("Tried to remove #" + changes.Count + " land protection blocks for player \"" + _id + "\". Note "+
				                      "that only blocks in chunks that are currently loaded (close to any player) could be removed. "+
				                      "Please check for remaining blocks by running:");
				SdtdConsole.Instance.Output("  listlandprotection " + _id);
			} catch (Exception e) {
				Log.Out ("Error in RemoveLandProtection.removeById: " + e);
			}
		}

		private void removeByPosition (List<string> _coords)
		{
			try {
				int x = int.MinValue;
				int.TryParse (_coords [0], out x);
				int y = int.MinValue;
				int.TryParse (_coords [1], out y);
				int z = int.MinValue;
				int.TryParse (_coords [2], out z);

				if (x == int.MinValue || y == int.MinValue || z == int.MinValue) {
					SdtdConsole.Instance.Output ("At least one of the given coordinates is not a valid integer");
					return;
				}

				Vector3i vectorKfpValue = new Vector3i(x, y, z);
				Vector3i vectorKfpMin = new Vector3i (x-3, y-3, z-3);
				Vector3i vectorKfpMax = new Vector3i (x+3, y+3, z+3);

				PersistentPlayerList PlayersLst = GameManager.Instance.GetPersistentPlayerList ();
				Boolean KFound = false;
				Dictionary<Vector3i, PersistentPlayerData> positionToLPBlockOwner = PlayersLst.m_lpBlockMap;			
				foreach (KeyValuePair<Vector3i, PersistentPlayerData> current in positionToLPBlockOwner)
				{
					if ((int)current.Key.x >= (int)vectorKfpMin.x && (int)current.Key.x <= (int)vectorKfpMax.x)
					{
						SdtdConsole.Instance.Output("Coordinates X found: " + (int)current.Key.x);

						if ((int)current.Key.y >= (int)vectorKfpMin.y && (int)current.Key.y <= (int)vectorKfpMax.y)
						{

							SdtdConsole.Instance.Output("Coordinates Y found: " + (int)current.Key.y); 

							if ((int)current.Key.z >= (int)vectorKfpMin.z && (int)current.Key.z <= (int)vectorKfpMax.z)
							{
								SdtdConsole.Instance.Output("Coordinates Z found: " + (int)current.Key.z);

								Vector3i vectorKfpEnd = new Vector3i ((int)current.Key.x, (int)current.Key.y, (int)current.Key.z);
								BlockChangeInfo bci = new BlockChangeInfo (vectorKfpEnd, 0, true);
								List<BlockChangeInfo> changes = new List<BlockChangeInfo> ();
								changes.Add (bci);
								GameManager.Instance.SetBlocksRPC (changes);

								SdtdConsole.Instance.Output ("Land protection block at (" + vectorKfpEnd.ToString() + ") removed");
								KFound = true;
								return;
							}
						}
					}
				}
				if (!KFound)
				{
					SdtdConsole.Instance.Output("Aucune keystone trouvee a cet emplacement (" + vectorKfpValue.ToString() + ").");
				}
			} catch (Exception e) {
				Log.Out ("Error in RemoveLandProtection.removeByPosition: " + e);
			}
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				if (_params.Count == 1) {
					removeById (_params [0]);
				} else if (_params.Count == 3) {
					removeByPosition (_params);
				} else {
					SdtdConsole.Instance.Output ("Usage: removelandprotection <x> <y> <z>  OR  removelandprotection <steamid>");
				}
			} catch (Exception e) {
				Log.Out ("Error in RemoveLandProtection.Run: " + e);
			}
		}
	}
}
