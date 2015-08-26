using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class ListPlayersExtended : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "lists all players with extended attributes";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "listplayersextended", "lpe" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				World w = GameManager.Instance.World;
				int num = 0;
				foreach (KeyValuePair<int, EntityPlayer> current in w.Players.dict) {
					ClientInfo clientInfoFromEntityID = ConnectionManager.Instance.GetClientInfoForEntityId(current.Key);
					//CommonMappingFunctions.GetClientInfoFromEntityID (current.Key);
					string ip = string.Empty;
					//int str1 = 0;
					int str2 = 0;
					string str3 = "unk";
					string str4 = "unk";
					if (clientInfoFromEntityID != null)	{
						ip = clientInfoFromEntityID.ip;
						//str1 = clientInfoFromEntityID.netConnection.clientId;
						str2 = clientInfoFromEntityID.entityId;
						str3 = clientInfoFromEntityID.ownerId;
						str4 = clientInfoFromEntityID.playerId;
					}
					PersistentData.Player p = PersistentData.PersistentContainer.Instance.Players ["" + current.Value.entityId, false];
					SdtdConsole.Instance.Output(string.Concat (new object[]
						{
							string.Empty, 
							++num, 
							". id=",
							current.Value.entityId,
							", ",
							current.Value.EntityName,
							", pos=",
							current.Value.GetPosition (),
							", Hp=",
							current.Value.Health,
							", dth=",
							current.Value.Died,
							", zkill=",
							current.Value.KilledZombies,
							", pkill=",
							current.Value.KilledPlayers,
							", scr=",
							current.Value.Score,
							", Sid=",
							str4,
							", ip=",
							ip,
							", ping=",
							current.Value.pingToServer,
							", level=",
							current.Value.GetLevel(),
							", playtime=",
							p.TotalPlayTime / 60


							/*,
							" clid=",
							str1,
							". entid=",
							str2,
							", ownid",
							str3,
							", plyid",
							str4*/
						}));
				}
				SdtdConsole.Instance.Output("Total of " + w.Players.list.Count + " in the KFP game");
			} catch (Exception e) {
				Log.Out ("Error in ListPlayersExtended.Run: " + e);
			}
		}
	}
}
