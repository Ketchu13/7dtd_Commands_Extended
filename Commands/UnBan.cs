//@Author: Alloc => 7dtd-server-fixes => https://7dtd.illy.bz/browser/binary-improvements/7dtd-server-fixes
//@Author: ketchu13 => Unban console command => ketchu13@hotmail.com
using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class UnBan : ConsoleCommand
	{
		public UnBan (ConsoleSdtd cons)	: base(cons)
		{
		}

		public override string Description ()
		{
			return "Deban un joueur (Steamid)";
		}

		public override string[] Names ()
		{
			return new string[] { "unban", "ub" };
		}

		public override void Run (string[] _params)
		{
			try	{
				if (_params.Length != 1) {
					m_Console.SendResult("Utilisation: unban <steamidDuJoueur>");
					return;
				}

				long tempLong;
				if (_params [0].Length != 17 || !long.TryParse (_params [0], out tempLong)) {
					m_Console.SendResult ("Not a valid Steam ID.");
					return;
				}

				AdminTools at = CommonMappingFunctions.GetGameManager ().adminTools;
				if (!at.IsBanned (_params [0])) {
					m_Console.SendResult ("Steam ID is not banned.");
					return;
				}

				at.RemoveBan (_params [0]);
				m_Console.SendResult ("Joueur deban " + _params [0]);
			} catch (Exception e) {
				Log.Out ("Error in Unban.Run: " + e);
			}
		}
	}
}
