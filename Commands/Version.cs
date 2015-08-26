using System;
using System.Collections.Generic;
using System.Reflection;

namespace AllocsFixes.CustomCommands
{
	public class Version : ConsoleCommand
	{
		public Version (ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "get the currently running version of the server fixes";
		}

		public override string[] Names ()
		{
			return new string[] { "version", string.Empty };
		}

		public override void Run (string[] _params)
		{
			try {
				m_Console.SendResult ("Server fixes version: " + Assembly.GetExecutingAssembly ().GetName ().Version);
			} catch (Exception e) {
				Log.Out ("Error in Version.Run: " + e);
			}
		}
	}
}
