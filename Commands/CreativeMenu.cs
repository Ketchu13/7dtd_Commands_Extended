using System;


namespace AllocsFixes.CustomCommands
{
	public class CreativeMenu : ConsoleCommand
	{
		public CreativeMenu (ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "enable/disable creative menu";
		}

		public override string[] Names ()
		{
			return new string[] { "kreativemenu", "km" };
		}

		public override void Run (string[] _params)
		{
			try {
				if (_params.Length != 1) {
					m_Console.SendResult ("Usage: creativemenu <0/1>");
					return;
				}

				if (_params[0].Equals("1"))
				{
					GameStats.Set(EnumGameStats.IsCreativeMenuEnabled, true);
					this.m_Console.SendResult("Menu creative active");
					return;
				}

				if (_params[0].Equals("0"))                    
				{                    
					GameStats.Set(EnumGameStats.IsCreativeMenuEnabled, false);                    
					this.m_Console.SendResult("Menu creative desactive");
					return;
				}
			} catch (Exception e) {
				Log.Out ("Error in CreativeMenu.Run: " + e);
			}
		}
	}
}