using System;

namespace AllocsFixes.CustomCommands
{
	public class GetTime : ConsoleCommand
	{
		public GetTime (ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "retrieves current ingame time";
		}

		public override string[] Names ()
		{
			return new string[] { "gettime", "gt" };
		}

		public override void Run (string[] _params)
		{
			try {
				ulong time = CommonMappingFunctions.GetGameManager ().World.gameTime;
				int day = (int)(time / 24000) + 1;
				int hour = (int)(time % 24000) / 1000 + 8;
				if (hour > 23) {
					day++;
					hour -= 24;
				}
				int min = (int)(time % 1000) * 60 / 1000;
				m_Console.SendResult (String.Format ("Day {0}, {1:00}:{2:00} ", day, hour, min));
			} catch (Exception e) {
				Log.Out ("Error in GetTime.Run: " + e);
			}
		}
	}
}