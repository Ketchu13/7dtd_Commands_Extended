using System;
using System.Collections.Generic;
using UnityEngine;

namespace AllocsFixes.CustomCommands
{
	public class AnnonceChat : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "Affiche un message sur le client de tous les joueurs connectes";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "annonce", "sayA" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			if (_params.Count < 1) {
				SdtdConsole.Instance.Output ("Utilisation: annonce <Message>");
				return;
			}
			try {
				string text = string.Empty;
				for (int i = 0; i < _params.Count; i++) {
					text = text + " " + _params[i];
				}
				//ConnectionManager.Instance.SendPackage (new NetPackage_GameInfoMessage (text, string.Empty ), new pac);
				GameManager.Instance.GameMessageServer(null,text, string.Empty);
			} catch (Exception e) {
				Log.Out ("Error in SayToPlayer.Run: " + e);
				SdtdConsole.Instance.Output ("Error in SayToPlayer.Run: " + e);
			}
		}


	}
}
