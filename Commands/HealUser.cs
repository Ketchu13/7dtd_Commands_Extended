using System;
using System.Collections.Generic;
using UnityEngine;

namespace AllocsFixes.CustomCommands
{
	public class HealUser : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "Soigne un joueur (Gameid ou pseudo)";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "heal", "hl" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
            try {

				int num;
				if (_params.Count != 1)
				{
					SdtdConsole.Instance.Output(string.Concat("Wrong number of arguments, expected 1, found ", _params.Count, "."));
					SdtdConsole.Instance.Output("Utilisation: heal <nomdujouer|gameid>");
					return;
				}
				Entity item = null;

				ClientInfo clientInfoForNameOrId = ConsoleHelper.ParseParamIdOrName (_params [0]);

				if (clientInfoForNameOrId != null)
				{
					item = GameManager.Instance.World.Players.dict[clientInfoForNameOrId.entityId];
				}
				else if (int.TryParse(_params[0], out num) && GameManager.Instance.World.Entities.dict.ContainsKey(num))
				{
					item = GameManager.Instance.World.Entities.dict[num];
				}
				if (item == null)
				{
					SdtdConsole.Instance.Output("Pseudo ou GameId introuvable.");
					return;
				}
				item.DamageEntity(new DamageSource(EnumDamageSourceType.Bullet), -9999, false);

				/*try {
					CommonMappingFunctions.GetGameManager().GetRPCNetworkView().RPC("RPC_ChatMessage", ci.networkPlayer,
						new object[]
						{
							"ZBot vient de vous soigner.",
							-1,
							string.Empty,
							false
						});			
								
					SdtdConsole.Instance.Output ("Message prive a destination du joueur " + (receiverName != null ? "\"" + receiverName + "\"" : "unknownName"));
				} catch (Exception e) {
					Log.Out("Error in Heal1.Run: " + e);
				}*/
                SdtdConsole.Instance.Output("Joueur soigne " + _params[0]);
            } catch (Exception e) {
                Log.Out("Error in Heal2.Run: " + e);
            }
        }
    }
}