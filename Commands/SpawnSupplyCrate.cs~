using System;
using UnityEngine;
namespace AllocsFixes.CustomCommands
{
	public class SpawnSupplyCrate : ConsoleCommand
	{
		private GameManager _manager;

		public SpawnSupplyCrate(ConsoleSdtd _console) : base(_console)
		{
			_manager = CommonMappingFunctions.GetGameManager();
		}

		public override string[] Names() {
			return new string[]
			{
				"spawnkfpsupply" , "spc"
			};
		}

		public override string Description() {
			return "Spawns a supply crate at a given position.";
		}

		public override void Run(string[] _params) {
			try	{
				if (_params.Length < 4) {
					m_Console.SendResult ("Usage: spawnkfpsupply <SupplycrateId> <x> <y> <z>");
					return;
				}
				if (_params.Length == 4) {
					int num = 1;
					int num2 = -1;
					int.TryParse(_params[0], out num2);

					foreach (int current3 in EntityClass.list.Keys)	{
						if (EntityClass.list[current3].bAllowUserInstantiate) {
							if (num == num2 || EntityClass.list[current3].entityClassName.Equals(_params[0])) {
								int x = int.MinValue;
								int y = int.MinValue;
								int z = int.MinValue;

								int.TryParse (_params [1], out x);
								int.TryParse (_params [2], out y);
								int.TryParse (_params [3], out z);

								Vector3 transformPos = new Vector3(x, y, z);
								Entity entity = EntityFactory.CreateEntity(current3, transformPos);
								_manager.World.SpawnEntityInWorld(entity);
								m_Console.SendResult("Spawned supplycrate " + EntityClass.list[current3].entityClassName);
								return;
							}
							else {
								num++;
							}
						}
					}
					m_Console.SendResult("Supplycrate '" + _params[0] + "' not found");
				}
			} catch (Exception e) {
				Log.Out ("Error in SpawnSupplyCrate.Run: " + e);
			}
		}
	}
}
