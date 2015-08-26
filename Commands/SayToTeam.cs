using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class SayToTeam : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "send a message to a single player";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "saytoteam", "sayt" };
		}

		public override string GetHelp () {
			return "Usage:\n" +
				"   sayt <receiver name / steam id / entity id><sender name / steam id / entity id> <message>\n" +
				"Send a msg to all your team members.";
		}

		private void RunInternal (List<string> _params)
		{
			if (_params.Count < 2) {
				SdtdConsole.Instance.Output (string.Empty);
				SdtdConsole.Instance.Output ("Utilisation: pm <NomDuJoueur|GameID> <message>");
				SdtdConsole.Instance.Output ("Evitez les pseudos a caracteres speciaux ou avec espace.");				
				SdtdConsole.Instance.Output ("Usage: sayplayer <playername|entityid> <message>");
				return;
			}
			string message =_params [3];

			ClientInfo receiver = ConsoleHelper.ParseParamIdOrName (_params [0]);
			if (receiver != null) {
				ChatTeam.SendMessage (receiver, _params [1],_params [2], message);
			} else {
				SdtdConsole.Instance.Output ("Playername or entity ID not found.");
			}
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				RunInternal (_params);
				}
			catch (Exception e) {
				Log.Out ("Error in SayToteam.Run: " + e);
			}
		}
	}
}
