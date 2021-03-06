using AllocsFixes.PersistentData;
using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class ListKnownPlayers : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "lists all players that were ever online";
		}

		public override string GetHelp () {
			return "Usage:\n" +
				   "  1. listknownplayers\n" +
				   "  2. listknownplayers -online\n" +
				   "  3. listknownplayers -notbanned\n" +
				   "  4. listknownplayers <player name>\n" +
				   "1. Lists all players that have ever been online\n" +
				   "2. Lists only the players that are currently online\n" +
				   "3. Lists only the players that are not banned\n" +
				   "4. Lists all players whose name contains the given string";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "listknownplayers", "lkp" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				AdminTools admTools = GameManager.Instance.adminTools;

				bool onlineOnly = false;
				bool notBannedOnly = false;
				string nameFilter = string.Empty;

				if (_params.Count == 1) {
					if (_params [0].ToLower ().Equals ("-online")) {
						onlineOnly = true;
					} else if (_params [0].ToLower ().Equals ("-notbanned")) {
						notBannedOnly = true;
					} else {
						nameFilter = _params [0].ToLower ();
					}
				}

				int num = 0;
				foreach (string sid in PersistentContainer.Instance.Players.SteamIDs) {
					Player p = PersistentContainer.Instance.Players [sid, false];

					if (
						(!onlineOnly || p.IsOnline)
						&& (!notBannedOnly || !admTools.IsBanned (sid))
						&& (nameFilter.Length == 0 || p.Name.ToLower ().Contains (nameFilter))
					) {
						SdtdConsole.Instance.Output (String.Format ("{0}. {1}, id={2}, steamid={3}, online={4}, ip={5}, playtime={6} m, seen={7}",
						                                    ++num, p.Name, p.EntityID, sid, p.IsOnline, p.IP,
						                                    p.TotalPlayTime / 60,
						                                    p.LastOnline.ToString ("yyyy-MM-dd HH:mm"))
						);
					}
				}
				SdtdConsole.Instance.Output ("Total of " + PersistentContainer.Instance.Players.Count + " known");
			} catch (Exception e) {
				Log.Out ("Error in ListKnownPlayers.Run: " + e);
			}
		}
	}
}
