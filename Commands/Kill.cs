using System;


namespace AllocsFixes.CustomCommands
{
	public class Kill : ConsoleCommand
	{
		public Kill (ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "kill a given player (entity id or name)";
		}

		public override string[] Names ()
		{
			return new string[] { "kill", "pk" };
		}

		public override void Run (string[] _params)
		{
			try {
				if (_params.Length != 1) {
					m_Console.SendResult ("Utilisation: kill <nomdujoueur|gameid>");
					return;
				}

				ClientInfo ci = CommonMappingFunctions.GetClientInfoFromNameOrID (_params [0], false);

				if (ci == null) {
					m_Console.SendResult ("Playername or entity id not found.");
					return;
				}
				string receiverName = CommonMappingFunctions.GetPlayerName (ci);
				EntityPlayer p = CommonMappingFunctions.GetEntityPlayer (ci);
				p.DamageEntity (new DamageSource (EnumDamageSourceType.Bullet), 9999, false);
				try	{
				ConnectionManager.Instance.SendPackage (new NetPackage_GameInfoMessage ("ZBot vient de vous tuer.", "Server"),new PackageDestinationSingleEntityID (p.entityId));
			
				/*try	{
				 * ConnectionManager.Instance.SendPackage (new NetPackage_GameInfoMessage ("ZBot vient de vous tuer.", senderName + " (Chuchote)"), new PackageDestinationSingleEntityID (ci.networkPlayer));
					CommonMappingFunctions.GetGameManager().GetRPCNetworkView().RPC("RPC_ChatMessage", ci.networkPlayer,
						new object[]
						{
							"ZBot vient de vous tuer.",
							-1,
							string.Empty,
							false
						});			

					m_Console.SendResult ("Message prive a destination du joueur " + (receiverName != null ? "\"" + receiverName + "\"" : "unknownName"));*/
				} catch (Exception e){
					Log.Out("Error in Kill1.Run: " + e);
				}
				m_Console.SendResult ("Joueur tue " + _params [0] + ".");
			} catch (Exception e) {
				Log.Out ("Error in Kill2.Run: " + e);
			}
		}
	}
}
