//7 Days To Die - Dedicated Server
//@Author: Alloc => 7dtd-server-fixes => https://7dtd.illy.bz/browser/binary-improvements/7dtd-server-fixes
//@Author: ketchu13 => SavePrefab console command => ketchu13@hotmail.com
using System;
using System.IO;
using AllocsFixes.PersistentData;


namespace AllocsFixes.CustomCommands
{
	public class SavePrefab: ConsoleCommand
	{
		private readonly string prefabsPath = UnityEngine.Application.dataPath + "/../Data/Prefabs";
		//private readonly string prefabsXMLpath =  UnityEngine.Application.dataPath + "/../Data/Worlds/Random Gen/PrefabsKFP.xml";
		private readonly World world = CommonMappingFunctions.GetGameManager ().World;

		public SavePrefab(ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "Save in game a prefab.";
		}

		public override string[] Names ()
		{
			return new string[] { "savePrefab", "svp" };
		}

		public override void Run (string[] _params)
		{
			try {
				if (_params.Length < 5 || _params.Length > 8) {
					m_Console.SendResult ("Usage: savePrefab <prefabname> <x> <y> <z> <x size> <y size> <z size> <overwrite*>");
					m_Console.SendResult ("   or: savePrefab <prefabname> <target entityid|playername|steamid> <x size> <y size> <z size> <overwrite*>");
					m_Console.SendResult ("Optional <overwrite*> = true or false (Default set to False)");  
					return;
				}
				if (_params.Length >= 5 && _params.Length <= 8) {
					SaveSinglePrefab (_params);
				}
			} catch (Exception ex) {
				Log.Out ("savePrefab error: " + ex.Message);
			}
		}

		private void SaveSinglePrefab(string[] _params) {
			string fileName = _params[0];
			int overwrite = 0;

			if (_params.Length == 6 || _params.Length == 8) {
				if(!int.TryParse(_params[_params.Length-1], out overwrite)){
					m_Console.SendResult ("Optional <overwrite*> 1 = true or 0 = false (Default set to False)");  
					return;
				}
			}
			if (overwrite == 1) {
				if (File.Exists (prefabsPath + "/" + fileName + ".tts" )) {
					m_Console.SendResult ("This prefab " + fileName + " already exist. Add true at the end of the command line to write on a existing file");
					return;
				}
			}
			int x, y, z;
			int sizeX= 50;
			int sizeY= 50;
			int sizeZ = 50;

			Vector3i positionC = default(Vector3i);

			if (_params.Length >= 7) {
				if (!int.TryParse (_params [1], out x) ||
					!int.TryParse (_params [2], out y) ||
					!int.TryParse (_params [3], out z) ||
					!int.TryParse (_params [4], out sizeX) ||
					!int.TryParse (_params [5], out sizeY) ||
					!int.TryParse (_params [6], out sizeZ))  {
					m_Console.SendResult ("At least one of the given sizes or coordinates is not a valid integer");
					return;
				}
				positionC.x = x;
				positionC.y = y;
				positionC.z = z;
			}

			if (_params.Length >= 5) {
				Player p1 = PersistentContainer.Instance.Players.GetPlayerByNameOrId (_params [1], true);
				if (p1 == null) {
					m_Console.SendResult ("Playername or entity/steamid id not found.");
					return;
				}
				if (!p1.IsOnline) {
					m_Console.SendResult ("Player not online.");
					return;
				}
				if (!int.TryParse (_params [2], out sizeX) ||
					!int.TryParse (_params [3], out sizeY) ||
					!int.TryParse (_params [4], out sizeZ)) {
					m_Console.SendResult ("At least one of the given sizes is not a valid integer");
					return;
				}
				UnityEngine.Vector3 vector3 = p1.Entity.GetPosition();
				positionC.x = (int)vector3.x;
				positionC.y = (int)vector3.y;
				positionC.z = (int)vector3.z;
			}

			Vector3i positionStart = default(Vector3i);
			Vector3i positionEnd = default(Vector3i);

			positionStart.x =(int)(positionC.x - sizeX / 2) ;
			positionStart.y = (int)(positionC.y - sizeY / 2) ;
			positionStart.z = (int)(positionC.z - sizeZ / 2) ;
			positionEnd.x = (int)(positionC.x + sizeX / 2) ;
			positionEnd.y =(int)(positionC.y + sizeY / 2) ;
			positionEnd.z = (int)(positionC.z + sizeZ / 2) ;

			Prefab prefab = new Prefab(new Vector3i(sizeX, sizeY, sizeZ));
			prefab.bCopyAirBlocks = true;
			prefab.bAllowTopSoilDecorations = true;//***
			prefab.filename = fileName;
			prefab.CopyFromWorld (world, positionStart, positionEnd);
			prefab.Save (fileName);
			m_Console.SendResult (string.Concat("Your prefab is saved: ", fileName));
		}
	}
}