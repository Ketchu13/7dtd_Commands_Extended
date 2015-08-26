using System;
using System.Collections.Generic;
namespace AllocsFixes.CustomCommands
{
	public class SayExtendedPlus : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "Affiche un message avec le nom de Ketchu sur le client de tous les joueurs connectes";
		}

		public override string[] GetCommands ()
		{
			return new string[]	{ "say+",	string.Empty};
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo) {
			if (_params.Count < 2) {
				SdtdConsole.Instance.Output ("Utilisation: say+ <Pseudo> <Message>");
				return;
			}
			try {
				string text = string.Empty;
				for (int i = 1; i < _params.Count; i++)
				{
					text = text + " " + _params[i];
				}

				GameManager.Instance.GameMessageServer(null,text, _params[0]);
			} catch (Exception e) {
				Log.Out ("Error in sayk.Run: " + e);
				SdtdConsole.Instance.Output("Error in sayk.Run: " + e);
			}
		}


	}
}

