using System;

namespace AllocsFixes.CustomCommands
{
	public class Chat {

		public static void SendMessage (ClientInfo _receiver, ClientInfo _sender, string _message) {
			string senderName;
			if (_sender != null) {
				PrivateMassageConnections.SetLastPMSender (_sender, _receiver);
				senderName = _sender.playerName;
			} else {
				senderName = "Server";
			}
			_receiver.SendPackage (new NetPackageGameMessage (_message, senderName + " (PM)"));
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
