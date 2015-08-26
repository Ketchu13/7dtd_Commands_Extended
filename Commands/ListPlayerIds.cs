using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class ListPlayerIds : ConsoleCommand
	{
		public ListPlayerIds (ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "lists all players with their IDs for ingame commands";
		}

		public override string[] Names ()
		{
			return new string[] { "listplayerids", "lpi" };
		}

		public override void Run (string[] _params)
		{
			try {
				World w = CommonMappingFunctions.GetGameManager ().World;
				int num = 0;
				foreach (KeyValuePair<int, EntityPlayer> current in w.Players.dict) {
					m_Console.SendResult (string.Concat (new object[]
						{
							string.Empty,
							++num,
							". id=",
							current.Value.entityId,
							", ",
							current.Value.EntityName,
						}
					)
					);
				}
				m_Console.SendResult ("Total of " + w.Players.list.Count + " in the game");
			} catch (Exception e) {
				Log.Out ("Error in ListPlayerIds.Run: " + e);
			}
		}
	}
}
