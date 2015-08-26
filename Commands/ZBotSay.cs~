using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class ZBotSay : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "Affiche un message avec le nom de ZBot sur le chat de tous les joueurs connectes";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "zbotsay", "zsay"};
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			if (_params.Count < 1) {
				SdtdConsole.Instance.Output  ("Utilisation: zbotsay <Message>");
				return;
			}
			try {
				string text = string.Empty;
				for (int i = 0; i < _params.Count; i++)
				{
					text = text + " " + _params[i];
				}			
				//Chat.SendMessage (null, "ZBot", text);
				GameManager.Instance.GameMessageServer(null, text, "ZBot");
			} catch (Exception e) {
				Log.Out ("Error in ZBotSay.Run: " + e);
				SdtdConsole.Instance.Output ("Error in ZBotSay.Run: " + e);
			}
		}
	}
}