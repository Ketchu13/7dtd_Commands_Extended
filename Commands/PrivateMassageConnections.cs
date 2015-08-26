using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class PrivateMassageConnections
	{
		private static Dictionary<ClientInfo, ClientInfo> senderOfLastPM = new Dictionary<ClientInfo, ClientInfo> ();

		public static void SetLastPMSender (ClientInfo _sender, ClientInfo _receiver)
		{
			if (senderOfLastPM.ContainsKey (_receiver))
				senderOfLastPM [_receiver] = _sender;
			else
				senderOfLastPM.Add (_receiver, _sender);
		}

		public static ClientInfo GetLastPMSenderForPlayer (ClientInfo _player)
		{
			if (senderOfLastPM.ContainsKey (_player))
				return senderOfLastPM [_player];
			return null;
		}
	}
}
