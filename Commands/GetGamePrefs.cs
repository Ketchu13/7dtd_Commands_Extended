using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class GetGamePrefs : ConsoleCommand
	{
		private string[] forbiddenPrefs = new string[] {
			"telnet",
			"adminfilename",
			"controlpanel",
			"password",
			"savegamefolder",
			"options",
			"last"
		};

		private bool prefAccessAllowed (EnumGamePrefs gp)
		{
			string gpName = gp.ToString ().ToLower ();
			foreach (string s in forbiddenPrefs) {
				if (gpName.Contains (s)) {
					return false;
				}
			}
			return true;
		}

		public GetGamePrefs (ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "gets a game pref";
		}

		public override string[] Names ()
		{
			return new string[]
		{
			"getgamepref",
			"gg"
		};
		}

		public override void Run (string[] _params)
		{
			try {
				EnumGamePrefs enumGamePrefs = EnumGamePrefs.Last;

				if (_params.Length > 0) {
					try {
						enumGamePrefs = (EnumGamePrefs)((int)Enum.Parse (typeof(EnumGamePrefs), _params [0]));
					} catch (Exception e) {
						Log.Out ("Error in GetGamePrefs.Run: " + e);
					}
				}

				if (enumGamePrefs == EnumGamePrefs.Last) {
					SortedList<string, string> sortedList = new SortedList<string, string> ();
					foreach (EnumGamePrefs gp in Enum.GetValues(typeof(EnumGamePrefs))) {
						if ((_params.Length == 0) || (gp.ToString ().ToLower ().Contains (_params [0].ToLower ()))) {
							if (prefAccessAllowed (gp)) {
								sortedList.Add (gp.ToString (), string.Format ("{0} = {1}", gp.ToString (), GamePrefs.GetObject (gp)));
							}
						}
					}
					foreach (string s in sortedList.Keys) {
						m_Console.SendResult (sortedList [s]);
					}
				} else {
					if (prefAccessAllowed (enumGamePrefs))
						m_Console.SendResult (string.Format ("{0} = {1}", enumGamePrefs, GamePrefs.GetObject (enumGamePrefs)));
					else
						m_Console.SendResult ("Je ne peux communiquer cette information.");
				}
			} catch (Exception e) {
				Log.Out ("Error in GetGamePrefs.Run: " + e);
			}
		}
	}
}
