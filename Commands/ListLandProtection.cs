using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class ListLandProtection : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "lists all land protection blocks and owners";
		}

		public override string GetHelp () {
			return "Usage:\n" +
				   "  1. listlandprotection summary\n" +
				   "  2. listlandprotection <steam id>\n" +
				   "  3. listlandprotection <player name / entity id>\n" +
				   "  4. listlandprotection nearby\n" +
				   "  5. listlandprotection nearby <radius>\n" +
				   "1. Lists only players that own claimstones, the number they own and the protection status\n" +
				   "2. Lists only the claims of the player given by his SteamID including the individual claim positions\n" +
				   "3. Same as 2 but player given by his player name or entity id (as given by e.g. \"lpi\")\n" +
				   "4. Lists claims in a square with edge length of 64 around the executing player\n" +
				   "5. Same as 4 but square edge length can be specified";
		}

    	public override string[] GetCommands ()
		{
			return new string[] { "listlandprotection", "llp" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				if (_senderInfo.RemoteClientInfo != null) {
					if (_params.Count >= 1 && _params [0].ToLower ().Equals ("nearby")) {
						_params.Add (_senderInfo.RemoteClientInfo.playerId);
					}
				}

				World w = GameManager.Instance.World;
				PersistentPlayerList ppl = GameManager.Instance.GetPersistentPlayerList ();

				bool summaryOnly = false;
				string steamIdFilter = string.Empty;
				Vector3i closeTo = default(Vector3i);
				bool onlyCloseToPlayer = false;
				int closeToDistance = 32;

				if (_params.Count == 1) {
					long tempLong;

					if (_params [0].ToLower ().Equals ("summary")) {
						summaryOnly = true;
					} else if (_params [0].Length == 17 && long.TryParse (_params [0], out tempLong)) {
						steamIdFilter = _params [0];
					} else {
						ClientInfo ci = ConsoleHelper.ParseParamIdOrName (_params [0]);
						if (ci != null) {
							steamIdFilter = ci.playerId;
						} else {
							SdtdConsole.Instance.Output ("Player name or entity id \"" + _params [0] + "\" not found.");
							return;
						}
					}
				} else if (_params.Count >= 2) {
					if (_params [0].ToLower ().Equals ("nearby")) {
						try {
							if (_params.Count == 3) {
								if (!int.TryParse (_params[1], out closeToDistance)) {
									SdtdConsole.Instance.Output ("Given radius is not an integer!");
								}
							}
							ClientInfo ci = ConsoleHelper.ParseParamSteamIdOnline (_params [_params.Count - 1]);
							EntityPlayer ep = w.Players.dict [ci.entityId];
							closeTo = new Vector3i (ep.GetPosition ());
							onlyCloseToPlayer = true;
						} catch (Exception e) {
							SdtdConsole.Instance.Output ("Error getting current player's position");
							Log.Out ("Error in ListLandProtection.Run: " + e);
						}
					} else {
						SdtdConsole.Instance.Output ("Illegal parameter list");
						return;
					}
				}
				Boolean Bol = false;
				Dictionary<Vector3i, PersistentPlayerData> d = ppl.m_lpBlockMap;
				if (d != null) {
					Dictionary<PersistentPlayerData, List<Vector3i>> owners = new Dictionary<PersistentPlayerData, List<Vector3i>> ();
					foreach (KeyValuePair<Vector3i, PersistentPlayerData> kvp in d) {
						if (!onlyCloseToPlayer || (Math.Abs (kvp.Key.x - closeTo.x) <= closeToDistance && Math.Abs (kvp.Key.z - closeTo.z) <= closeToDistance)) {
							if (!owners.ContainsKey (kvp.Value)) {
								owners.Add (kvp.Value, new List<Vector3i> ());
							}
							owners [kvp.Value].Add (kvp.Key);
						}
					}

					foreach (KeyValuePair<PersistentPlayerData, List<Vector3i>> kvp in owners) {
						if (steamIdFilter.Length == 0 || kvp.Key.PlayerId.Equals (steamIdFilter)) {
							PersistentData.Player p = PersistentData.PersistentContainer.Instance.Players [kvp.Key.PlayerId, false];
							string name = string.Empty;
							if (p != null) {
								name = p.Name;
							}
							name += " (" + kvp.Key.PlayerId + ")";
							Bol = true;

							SdtdConsole.Instance.Output (String.Format (
								"Player \"{0}\" owns {3} keystones (protected: {1}, current hardness multiplier: {2})",
								name,
								w.IsLandProtectionValidForPlayer (kvp.Key),
								w.GetLandProtectionHardnessModifierForPlayer (kvp.Key),
								kvp.Value.Count)
							);
							if (!summaryOnly) {
								foreach (Vector3i v in kvp.Value) {
									SdtdConsole.Instance.Output ("   (" + v.ToString () + ")");
								}
							}
						}
					}
				}
				if (Bol == false)
					SdtdConsole.Instance.Output ("Player not found...");
				
				if (steamIdFilter.Length == 0)
					SdtdConsole.Instance.Output ("Total of " + d.Count + " keystones in the game");
			} catch (Exception e) {
				Log.Out ("Error in ListLandProtection.Run: " + e);
			}
		}
	}
}
