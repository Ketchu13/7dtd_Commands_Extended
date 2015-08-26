using AllocsFixes.PersistentData;
using System;
using System.Collections.Generic;


namespace AllocsFixes.CustomCommands
{			
	public class PlayedTime : ConsoleCmdAbstract
	{

		public override string[] GetCommands ()
		{
			return new string[]	{ "playedTime",	"joue" };
		}

		public override string GetDescription ()
		{
			return "Affiche combien de temps le joueur a joue sur KFP.";
		}
		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{

			if (_params.Count < 1) {
				SdtdConsole.Instance.Output ("Utilisation: played <NomDuJoueur>");
				return;
			}
			try 
			{
                string nameFilter = _params[0].ToLower();
				string text = string.Empty;
				foreach (string sid in PersistentContainer.Instance.Players.SteamIDs) {
					Player p = PersistentContainer.Instance.Players [sid,false];

					if (nameFilter.Length == 0 || p.Name.ToLower ().Contains (nameFilter)) {
						text = String.Format("{0} a joue sur le serveur {1}, {2} minutes (=> {3} heure(s)) depuis le 23-11-2014.",
							p.Name,
							"KFP",
							(p.TotalPlayTime / 60),
							(p.TotalPlayTime / 3600)					
						);
//						CommonMappingFunctions.GetGameManager().GetRPCNetworkView().RPC("RPC_ChatMessage", UnityEngine.RPCMode.AllBuffered, new object[] {
//							text,
//							-1,
//							string.Empty,
//							false
//						});
						return;
					}
				}
			} catch (Exception e) {
				Log.Out ("Error in PlayedTime.Run: " + e);
				SdtdConsole.Instance.Output("Error in PlayedTime.Run: " + e);
			}
		}
	}
}
