using System;
using System.Collections.Generic;
using AllocsFixes.PersistentData;


namespace AllocsFixes.CustomCommands
{
	class SearchInInventory : ConsoleCmdAbstract
    {
       
	    public override string GetDescription ()
	    {
		    return "Recherche un objet dans les inventaires des joueurs.";
	    }
        

		public override string[] GetCommands ()
	    {
		    return new string[] { "searchItem", "shi" };
	    }

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo) {
		 try
         {
			    if (_params.Count < 1) {
				    SdtdConsole.Instance.Output ("Utilisation: searchItem <NomItem>");
				    return;
			    } 
			   // int entityId =  -1;		   
            	//string playerName = _params [0].ToLower ();
				World w = GameManager.Instance.World;
				foreach (KeyValuePair<int, EntityPlayer> kvp in w.Players.dict) 
	            {
	               // entityId = kvp.Key;
					try
	                    {
	                       string UserName = kvp.Value.EntityName;
	                       string steamid = PersistentContainer.Instance.Players.GetSteamID(kvp.Value.EntityName, true);

	                       Player p = PersistentContainer.Instance.Players[steamid, false];
	                       PersistentData.Inventory inv = p.Inventory;
	                        if (inv == null) 
	                        {	
			                     SdtdConsole.Instance.Output ("Item non trouve ou inventaire le contenant non enregistre (Re-essayez dans 30s).");				         
	                            return;     
	                        }
	                        for (int i = 0; i < inv.belt.Count; i++)
	                        {
	                            if (inv.belt[i] != null) {
	                                string ItemName = inv.belt [i].itemName.ToLower();
	                                if (ItemName.Contains(_params [0].ToLower()))
	                                {
	                                    SdtdConsole.Instance.Output(string.Format(" Trouve => {0} dans la ceinture de {1} .", _params[0], UserName));
	                                    SdtdConsole.Instance.Output(string.Format("    {0:000} * {1}", inv.belt[i].count, inv.belt[i].itemName));
	                                }
	                            }
	                        }                                    
	                        for (int i = 0; i < inv.bag.Count; i++) 
	                        {
	                            if (inv.bag[i] != null)
	                                {
	                                    string ItemName = inv.bag[i].itemName.ToLower();
	                                    if (ItemName.Contains(_params[0].ToLower()))
	                                    {
	                                        SdtdConsole.Instance.Output(string.Format(" Trouve => {0} dans le sac a dos de {1} .", _params[0], UserName));
	                                        SdtdConsole.Instance.Output(string.Format("    {0:000} * {1}", inv.bag[i].count, inv.bag[i].itemName));
	                                    }
	                                }
	                        }
	                    }
	                 catch (Exception eh)
	                 {
	                     SdtdConsole.Instance.Output("Error in SearchInventory.Run: " + eh);
	                 }
	            } 
			}
			catch (Exception e)  
			{
				SdtdConsole.Instance.Output("Error in SearchInventory.Run: " + e);
			}
		}
	}
}


 