using System;

namespace AllocsFixes.CustomCommands
{
	public class SetTimeReal : ConsoleCommand
	{
		public SetTimeReal (ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "set current ingame time, params: <day> <hour> <min>";
		}

		public override string[] Names ()
		{
			return new string[] { "settimereal", "str" };
		}

		public override void Run (string[] _params)
		{
			try {
				if (_params.Length != 3) {
					m_Console.SendResult ("Usage: settimereal <day> <hour> <min>");
					return;
				}

				int day, hour, min;
				if (!int.TryParse (_params [0], out day)) {
					m_Console.SendResult ("Could not parse day number \"" + _params [0] + "\"");
					return;
				}
				if (day < 1) {
					m_Console.SendResult ("Day must be >= 1");
					return;
				}
				if (!int.TryParse (_params [1], out hour)) {
					m_Console.SendResult ("Could not parse hour \"" + _params [1] + "\"");
					return;
				}
				if (hour > 23) {
					m_Console.SendResult ("Hour must be <= 23");
					return;
				}
				if (!int.TryParse (_params [2], out min)) {
					m_Console.SendResult ("Could not parse minute \"" + _params [2] + "\"");
					return;
				}
				if (min > 59) {
					m_Console.SendResult ("Minute must be <= 59");
					return;
				}
				if ((day < 1) || (hour < 8 && day < 1)) {
					m_Console.SendResult ("Time may not be prior to day 1, 8:00");
					return;
				}

				ulong time = ((ulong)(day - 1) * 24000) + ((ulong)hour * 1000) + ((ulong)min * 1000 / 60) - 8000;
				CommonMappingFunctions.GetGameManager ().World.gameTime = time;
				m_Console.SendResult (String.Format ("Set time to Day {0}, {1:00}:{2:00} = {3}", day, hour, min, time));
			} catch (Exception e) {
				Log.Out ("Error in SetTimeReal.Run: " + e);
			}
		}
	}
}
