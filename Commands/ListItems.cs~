using System;
using System.Collections.Generic;
using System.IO;


namespace AllocsFixes.CustomCommands
{
	public class ListItems : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "lists all items that contain the given substring";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "listitems", "li" };
		}
		public void Execute2()
		{
			NGuiInvGridCreativeMenu cm = new NGuiInvGridCreativeMenu ();
			//SdtdConsole.Instance.Output ("##Liste des objets disponibles##");
			List<string> itemsListKFP = new List<string> () { };

			foreach (ItemStack invF in cm.GetAllItems()) {
				ItemClass ib = ItemClass.list [invF.itemValue.type];
				string name = ib.GetItemName ();
				int stackNumber = ib.Stacknumber.Value ;
				if (name != null && name.Length > 0) {
					itemsListKFP.Add(name + "\t" + stackNumber.ToString());
				}
			}
			foreach (ItemStack invF in cm.GetAllBlocks()) {
				ItemClass ib = ItemClass.list [invF.itemValue.type];
				string name = ib.GetItemName ();
				int stackNumber = ib.Stacknumber.Value ;
				if (name != null && name.Length > 0) {
					itemsListKFP.Add(name + "\t" + stackNumber.ToString());
				}
			}
			try {
				itemsListKFP.Sort();
				int num = 0;
				string itemsList = string.Empty;
				for (int i = 0; i < itemsListKFP.Count; i ++) {
					if( itemsListKFP[i] != null)	{
						itemsList += itemsListKFP[i] + "\n";
						num++;  
					}
				}
				string exportPath = Utils.GetGameDir ("ItemsInfos");
				StreamWriter sw = new StreamWriter(exportPath  + "/itemsList.txt");
				sw.Write(itemsList);
				sw.Close();
				itemsList = null;
				SdtdConsole.Instance.Output  ("Items List saved to file successfully..");
			} catch (Exception arg) {
					Log.Out("Error in ListItems.Run.listAll: " + arg);
				}
		}
		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				if (_params.Count != 1 || _params [0].Length == 0) {
					SdtdConsole.Instance.Output ("Usage: listitems <searchString>");
					SdtdConsole.Instance.Output ("   or: li <All>");
					return;
				}
				int num = 0;
				if (_params[0].Equals("All")) {
					SdtdConsole.Instance.Output ("Liste des objets de base.");	
					foreach (string s in ItemList.Instance.ItemNames) {
						SdtdConsole.Instance.Output  (num + ". " + s);
						num++;
					}
				}else{
					if (_params[0].Equals("Save")) {
						Execute2();
						return;
					}
					else {
						SdtdConsole.Instance.Output ("Liste des objets de base.");	
						foreach (string s in ItemList.Instance.ItemNames) {
							if (s.ToLower ().Contains (_params [0].ToLower ())) {
								SdtdConsole.Instance.Output  (num + ". " + s);
								num++;
							}
						}
					}
				}
				SdtdConsole.Instance.Output  ("La liste contient " + num + " objets.");
			} catch (Exception e) {
				Log.Out ("Error in ListItems.Run: " + e);
			}
		}
	}
}
