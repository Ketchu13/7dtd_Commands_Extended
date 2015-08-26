using System;

namespace AllocsFixes.CustomCommands
{
	public class ChatTeam {

		public static void SendMessage (ClientInfo _receiver, string senderName, string teamName, string _message) {
			if (senderName != null && teamName != null && _message != null) {
				_receiver.SendPackage (new NetPackageGameMessage (_message, "[00ffff]" + senderName + " (" + teamName + ")[-]"));
				string receiverName = _receiver.playerName;
				string[] strArrays = new string[] { "Message to player ", null, null, null, null };
				strArrays[1] = (receiverName == null ? "unknownName" : string.Concat("\"", receiverName, "\""));
				strArrays[2] = " sent with sender \"";
				strArrays[3] = senderName;
				strArrays[4] = "\"";
				SdtdConsole.Instance.Output(string.Concat(strArrays));
			}
		}
	}
}
