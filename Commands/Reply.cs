using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class Reply : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "send a message to  the player who last sent you a PM";
		}
		public override string GetHelp () {
			return "Usage:\n" +
			"   reply <message>\n" +
			"Send the given message to the user you last received a PM from.";
		}
		public override string[] GetCommands ()
		{
			return new string[] { "reply", "re" };
		}

		private void RunInternal (ClientInfo _sender, List<string> _params)
		{
			if (_params.Count < 1) {
				SdtdConsole.Instance.Output ("Usage: reply <message>");
				return;
			}

			string message = _params [0];

			ClientInfo receiver = PrivateMassageConnections.GetLastPMSenderForPlayer (_sender);
			if (receiver != null) {
				Chat.SendMessage (receiver, _sender, message);
			} else {
				if (receiver != null) {
					SdtdConsole.Instance.Output ("The sender of the PM you last received is currently not online.");
				} else {
					SdtdConsole.Instance.Output ("You have not received a PM so far.");
				}
			}
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo) {
			if (_senderInfo.RemoteClientInfo == null) {
				Log.Out ("Command \"reply\" can only be used on clients!");
			} else {
				RunInternal (_senderInfo.RemoteClientInfo, _params);
			}
		}
	}
}
