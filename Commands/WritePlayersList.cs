using System;
using System.Collections.Generic;
using System.IO;

namespace AllocsFixes.CustomCommands
{
	public class WritePlayersList : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "write a list of all connnected players";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "WritePlayersList", "wpl" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				World w = GameManager.Instance.World;
				int num = 0;
				string playersList = "<playersList>";
				foreach (KeyValuePair<int, EntityPlayer> current in w.Players.dict) {
					ClientInfo clientInfoFromEntityID =  ConnectionManager.Instance.GetClientInfoForEntityId(current.Key);
					playersList += "<player id=\"" + ++num + "\" steamId=\"" + clientInfoFromEntityID.entityId;
					playersList += "\" name=\"" + current.Value.EntityName; 
					playersList += "\" pos=\"" + current.Value.GetPosition ();
					playersList += "\" />";
					playersList += "\n";

				}
				playersList += "</playersList>";
				playersList = playersList.Replace("(","").Replace(")","");
				//SdtdConsole.Instance.Output (playersList);
				StreamWriter sw = new StreamWriter("D:\\Inventories\\playersList.xml");
				sw.Write(playersList);
				sw.Close();
				playersList = null;
			} catch (Exception e) {
				SdtdConsole.Instance.Output ("Error in WritePlayersList.Run: " + e);
			}
		}
	}
}
