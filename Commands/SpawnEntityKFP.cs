using System;
using UnityEngine;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class SpawnEntityKFP : ConsoleCmdAbstract
	{
		
		public override string[] GetCommands ()
		{
			return new string[]
			{
				"spawnentityk" , "sek"
			};
		}

		public override string GetDescription ()
		{			
			return "Spawns x entity at a given position";
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try
			{
				if (_params.Count < 4) {
					SdtdConsole.Instance.Output("Usage: spawnentityk <EntityId> <x> <y> <z>");
					SdtdConsole.Instance.Output("   or: spawnentityk <EntityId> <nbr> <x> <y> <z>");
					return;
				}
				if (_params.Count >= 4) {

					int num = 1;
					int num2 = -1;
					int nbr = 1;

					int x = int.MinValue;
					int y = int.MinValue;
					int z = int.MinValue;

					int.TryParse(_params[0], out num2);
					int.TryParse (_params [1], out x);
					int.TryParse (_params [2], out y);
					int.TryParse (_params [3], out z);							

					if (_params.Count == 5){
						int.TryParse(_params[1], out nbr);
						int.TryParse (_params [2], out x);
						int.TryParse (_params [3], out y);
						int.TryParse (_params [4], out z);
						if (nbr >= 80) {
							SdtdConsole.Instance.Output("More than 80 entities ?! 80 is too much...");
						}
					}

					Vector3 transformPos = new Vector3(x, y, z);

					foreach (int current3 in EntityClass.list.Keys)
					{

						if (EntityClass.list[current3].bAllowUserInstantiate)
						{
							if (num == num2 || EntityClass.list[current3].entityClassName.Equals(_params[0])) {
								for(int k = 1; k <= nbr; k++){
									Entity entity = EntityFactory.CreateEntity(current3, transformPos);

									GameManager.Instance.World.SpawnEntityInWorld(entity);
								}
								SdtdConsole.Instance.Output("Spawned " + nbr + " entity " + EntityClass.list[current3].entityClassName);
								return;
							}
							else {
								num++;
							}
						}
					}
					SdtdConsole.Instance.Output("Entity '" + _params[0] + "' not found");
				}
			} catch (Exception e) {
				Log.Out ("Error in SpawnEntityK.Run: " + e);
			}
		}
	}
}
