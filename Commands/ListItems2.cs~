using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class ListItems2 : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "lists all items that contain the given substring";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "listitems2", "li2" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				if (_params.Count != 1 || _params [0].Length == 0) {
					SdtdConsole.Instance.Output ("Usage: listitems <searchString>");
					return;
				}

				int n = 0;
				foreach (string s in ItemList.Instance.ItemNames) {
					if (s.ToLower ().Contains (_params [0].ToLower ())) {
						SdtdConsole.Instance.Output ("    " + s);
						n++;
					}
				}

				SdtdConsole.Instance.Output ("Listed " + n + " matching items.");
			} catch (Exception e) {
				Log.Out ("Error in ListItems.Run: " + e);
			}
		}
	}
}
