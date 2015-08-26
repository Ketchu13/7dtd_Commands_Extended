using System;
using System.Collections.Generic;
using UnityEngine;

namespace AllocsFixes.CustomCommands
{
	public class EntitiesTools : ConsoleCmdAbstract
	{
		private readonly World world = GameManager.Instance.World;

		public override string GetDescription ()
		{
			return "Tools for manage Entities";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "listentsT", "let" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try {
				World w = GameManager.Instance.World;
				if (_params.Count != 2) {
					SdtdConsole.Instance.Output ("Usage: let list <entities>");
					SdtdConsole.Instance.Output ("    or let list All");
					SdtdConsole.Instance.Output ("    or let kill <entities>");
					return;
				}
				int num = 0;
				if (_params.Count > 0 && _params[1] != null && _params[0].Length > 3) {
					for (int i = w.Entities.list.Count - 1; i >= 0; i--) {
						Entity item = w.Entities.list[i];
						EntityAlive _entityAlive = null;
						Boolean _boolean = false;

						if (item is EntityAlive) {
							_entityAlive = (EntityAlive)item;
							try
							{
							AIDirectorPlayerInventory inventory = AIDirectorPlayerInventory.FromEntity(_entityAlive);

							AIDirectorPlayerInventory.ItemId itmid = new AIDirectorPlayerInventory.ItemId();
							itmid.id = 1356;
							itmid.count = 500;
							inventory.bag.Add(itmid);
							NetPackagePlayerInventory pkh = new NetPackagePlayerInventory(_entityAlive,inventory);
							ClientInfo ci1 = ConsoleHelper.ParseParamIdOrName (_entityAlive.EntityName);
								ci1.SendPackage (pkh);
							}
							catch (Exception e)
							{
								Log.Out ("Error in EntitiesTools.Run " + e);
								SdtdConsole.Instance.Output ("Error in EntitiesTools.Run " + e);
							}

						}
						if ( _params[1].ToLower().Equals("all")) {
							_boolean = true;
						}
						SdtdConsole.Instance.Output (item.entityClass.ToString () );
						if (item.entityClass.ToString ().ToLower ().Equals (_params [1]) ) {
							_boolean = true;

						}
						if (_entityAlive != null) {
							SdtdConsole.Instance.Output (_entityAlive.EntityName );
							if (_entityAlive.EntityName.ToLower ().Equals (_params [1].ToLower ())) {
								_boolean = true;
							}
						}
						if (_boolean == true) {
							num += 1;
							if (_params [0].ToLower ().Equals ("kill") == true) {//todo check entity health
								item.DamageEntity (new DamageSource (EnumDamageSourceType.Bullet), 9999, false, 1f);
							}
							if (_params [0].ToLower ().Equals ("list") == true) {
								object[] empty = new object[13];
								empty [0] = string.Empty;
								empty [1] = "\t" + num;
								empty [2] = ". id=";
								empty [3] = item.entityId;
								empty [4] = ", ";
								empty [5] = item.ToString ();
								empty [6] = ", pos=";
								empty [7] = item.GetPosition ();
								empty [8] = ", dead=";
								empty [9] = item.IsDead ();
								empty [10] = ", ";
								empty [11] = (_entityAlive == null ? string.Empty : string.Concat ("health=", _entityAlive.Health));
								empty [12] = ", class=" + item.entityClass.ToString ();
								//empty [19] = ", " + item.GetType ().ToString();
								try {
									SdtdConsole.Instance.Output (string.Concat (empty) );
								} catch (Exception e) {
									Log.Out ("Error in EntitiesTools.Run " + e);
									SdtdConsole.Instance.Output ("Error in EntitiesTools.Run " + e);
								}
							}
						}
					}
					if (_params [0].ToLower ().Equals ("kill") == true )  {
						SdtdConsole.Instance.Output(string.Concat("Total of ", num, " entities ", _params[1] , " killed in the game."));
					}
					try {
						if (_params [0].ToLower ().Equals ("list") == true) {
						SdtdConsole.Instance.Output(string.Concat("Total of ", num, "/" , w.Entities.list.Count , " entities ", _params[1] , " in the game."));
						}
					} catch (Exception e) {
						Log.Out ("Error in EntitiesTools.Run " + e);
						SdtdConsole.Instance.Output ("Error in EntitiesTools.Run " + e);
					}
					if (_params [0].ToLower ().Equals ("count") == true) {
						SdtdConsole.Instance.Output(string.Concat("Total of ", num, " ", _params[1] , " entities in the game."));
					}
				} else {
					SdtdConsole.Instance.Output ("Usage: let list <entities>");
					SdtdConsole.Instance.Output ("    or let list All");
					SdtdConsole.Instance.Output ("    or let kill <entities>");
					return;
				}
			} catch (Exception e) {
				Log.Out ("Error in EntitiesTools.Run " + e);
			}
		}

	}
}


