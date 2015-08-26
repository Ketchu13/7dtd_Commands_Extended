/*7 Days To Die - Recipes Tools: ex: load a custom recipes.xml
@Author: Alloc => 7dtd-server-fixes => https://7dtd.illy.bz/browser/binary-improvements/7dtd-server-fixes
@Author: ketchu13 => Recipes console command => ketchu13@hotmail.com

Usage:
> rpc arg0 arg1* arg2* 

arg0	= list		=> List	recipe(s) name(s).
		= list+		=> List recipe(s) name(s) and properties.  
	
		arg1	= All		=> List All recipes names.
				= null		=> List All recipes names.
				= bullet	=> List	recipe(s) name(s) which contains string "bullet".

		arg2	= say		=> Server give the answer in chat.
				= null		=> Server give the answer in chat.
				= sayk		=> ketchu13 give the answer in chat.
				= zsay		=> ZBot give the answer in chat.
				/TODO/= zpm		=> ZBot give the answer in the target chat./TODO/
		Example:
		> rpc list All say  
		OR
		> rpc list All 
		OR
		> rpc list
		Server displays a list of all the recipes names, in chat's window of all connected users.

arg0	= reload 	=> Reload recipes list from xml.

		arg1	= Recipes.xml.

*/
using System;
using System.IO;

namespace AllocsFixes.CustomCommands
{
	public class Recipes : ConsoleCommand
	{
		private readonly string recipesPath = UnityEngine.Application.dataPath + "/../Data/Config";
		private UnityEngine.NetworkPlayer K = default(UnityEngine.NetworkPlayer);

		public Recipes (ConsoleSdtd cons) : base(cons)
		{
			//K = cons.issuerOfCurrentClientCommand;
		}

		public override string Description()
		{
			return "Suit of recipes tools (reload, list, list+).";
		}

		public override string[] Names()
		{
			return new string[] { "recipes", "rcp" };
		}

		public override void Run(string[] _params)
		{
			try	{
				if (_params[0].Equals("reload")) {
					LoadRecipes(_params);
					return;
					}
				int mode = -1;
				if (_params[0].Equals("list")){	mode = 1; }
				if (_params[0].Equals("list+")){ mode = 3; }
				if (mode != -1) {
					if (_params.Length == 1 || _params[1].Equals("All")) {						
						m_Console.SendResult("mode: " + mode);
						ListRecipes(mode, "All", null);
						return;
						}
					if (_params.Length == 3) {
						if (!_params[1].Equals("All")) {//ex: rpc list bullet say
							mode--;
							}
						ListRecipes(mode, _params[1], _params[2]);
						return;
					}
				}
				Usage(mode);
			}
			catch (Exception e)	{
				Log.Out("Error in Recipes.Run: " + e);
			}
		}

		private void LoadRecipes(string[] _params) {
			string filePath= null;
			string msg = null;
			if (_params.Length == 1) {//Reload TFP recipes list
				filePath = "recipes.xml";
				msg = "TFP recipes list is reloaded.";
			}
			if (_params.Length == 2) {//Reload custom recipes list
				filePath = _params[1] + ".xml";
				msg = "Custom recipes list " + filePath + " is loaded.";
			}
			if ((filePath != null) && File.Exists(string.Concat(recipesPath, "/" , filePath))) {
				CraftingManager.ClearAllRecipes();
				RecipesFromXml.LoadRecipies(new cl0042(recipesPath, filePath));
				m_Console.SendResult(msg);
				return;
			}
			Usage(4);
		}

		private void ListRecipes (int mode, string arg1, string arg2) {//arg1 "All" or "query string"  - arg2 output type
			int num1 = 0;
			int num2 = 0;
			string msg1, msg2 = "";
			Recipe[] allRecipes = CraftingManager.GetAllRecipes ();
			SendResult (arg2, "## Recipes List ##");
			for (int i = 0; i < allRecipes.Length; i++) 
			{
				Recipe recipe = allRecipes [i];
				if ((mode == 0 && recipe.GetName ().ToLower ().Contains (arg1)) || (mode == 1)) {//list recipes names
					num1++;
					SendResult (arg2, string.Concat(new object[] {
						num1,
						". ",
						recipe.GetName ()}));
				}
				if ((mode == 2 && recipe.GetName ().ToLower ().Contains (arg1)) || (mode == 3)) {//list recipes Names And properties
					num2++;
					SendResult(arg2, "**" + recipe.GetName() + "**");
					msg1 = string.Concat (
						new object[] {
							num2, ". ",	recipe.GetName (),
							", count= ", recipe.count,
							", scrapable= ", recipe.scrapable,	
							(recipe.tooltip == null) ? string.Empty : (", tooltip= " + recipe.tooltip),
							(recipe.craftingArea == null || recipe.craftingArea.Length <= 0) ? string.Empty : (", craftingArea= " + recipe.craftingArea),
							(recipe.craftingTime <= 0f) ? string.Empty : (", craftingTime= " + recipe.craftingTime.ToString ())
						});
					SendResult (arg2, msg1);
					SendResult (arg2,"**Ingredients**");
					foreach (Vector2i current in recipe.ingredients.Keys) {//ingredients
						InventoryField inventoryField = recipe.ingredients[current];
						msg2 = string.Concat(
							new object[] {
								"Name= ", ItemBase.list[inventoryField.itemValue.type].GetItemName(),
								", count= ", inventoryField.count,
								(recipe.craftingArea == null || !recipe.craftingArea.Equals ("campfire")) ? (", gridPosition= " + current) : string.Empty
							});
						SendResult (arg2, msg2);
					}
					SendResult(arg2, "****");
				}
			}
			SendResult (arg2, string.Concat("Listed ", (num1 >= num2) ? num1 : num2 , " matching recipes."));
			return;
		}

		private void SendResult (string arg1, string arg2) {
			string msg = string.Concat (new object[] { "\t", arg2 });
			string name = null;
			string outPut = arg1.ToLower ();
			switch (outPut) {
			case "say":
				name = "Server";//Server
				break;
			case "zsay":
				name = "ZBot";//ZBot
				break;
			case "sayk":
				name = "ketchu13";//ketchu13
				break;
			case "zpm":
				//ZBot private
				CommonMappingFunctions.GetConnectionManager ().networkView.RPC ("RPC_ChatMessage", K,
					new object[] {
						msg,
						-1,
						"ZBot(Chuchote)",
						true
					}
				);
				return;
			case "sayA":
				CommonMappingFunctions.GetGameManager().GetRPCNetworkView().RPC("RPC_ChatMessage", UnityEngine.RPCMode.AllBuffered, 
					new object[] {
						msg,
						-1,
						string.Empty,
						false
					}
				);
				return;
			}
			if (name != null) {
				CommonMappingFunctions.GetGameManager ().SendChatMessage (msg, -1, name);
				return;
			}
			m_Console.SendResult (msg);			
		}

		public void Usage(int mode) {
			switch (mode) {
			case -1:
				m_Console.SendResult ("Usage: recipes list <searchString> <outPutType*>");
				m_Console.SendResult ("   or: recipes list All <outPutType*>");
				m_Console.SendResult ("   or: recipes list+ <searchString> <outPutType*>");
				m_Console.SendResult ("   or: recipes list+ All <outPutType*>");
				m_Console.SendResult ("   or: recipes reload <customRecipes.xml>");
				m_Console.SendResult ("   or: recipes reload");
				goto case 13;
			case 0:
				m_Console.SendResult("Usage: recipes list <searchString> <outPutType>");
				goto case 13;
			case 1:
				m_Console.SendResult("Usage: recipes list All <outPutType>");
				m_Console.SendResult("   or: recipes list");
				goto case 13;			
			case 2:
				m_Console.SendResult("Usage: recipes list+ <searchString> <outPutType>");
				goto case 13;
			case 3:
				m_Console.SendResult("Usage: recipes list+ All <outPutType>");
				m_Console.SendResult("   or: recipes list+");
				goto case 13;
			case 4:
				m_Console.SendResult("Usage: recipes reload <customRecipes.xml>");
				m_Console.SendResult("   or: recipes reload");
				break;
			case 13:
				m_Console.SendResult ("*<outPutType>: say, sayk, zsay, sayA, cons");
				break;
			}
		}
	}
}