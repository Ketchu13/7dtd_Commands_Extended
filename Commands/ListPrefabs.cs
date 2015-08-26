//7 Days To Die - List prefabs files in /Data/Prefabs folder.
//@Author: Alloc => 7dtd-server-fixes => https://7dtd.illy.bz/browser/binary-improvements/7dtd-server-fixes
//@Author: ketchu13 => Unban console command => ketchu13@hotmail.com
using System;
using System.IO;


namespace AllocsFixes.CustomCommands
{
	public class ListPrefabs: ConsoleCommand
    {
        private readonly string prefabsPath = UnityEngine.Application.dataPath + "/../Data/Prefabs";
        //private readonly string prefabsXMLpath = UnityEngine.Application.dataPath + "/../Data/Worlds/Random Gen/PrefabsKFP.xml";
        //private readonly World world = CommonMappingFunctions.GetGameManager().World;

		public ListPrefabs(ConsoleSdtd cons) : base(cons)
		{
		}

		public override string Description ()
		{
			return "List all prefab files in /Data/Prefabs folder.";
		}

		public override string[] Names ()
		{
			return new string[] { "lisprefabs", "lpf" };
		}

		public override void Run (string[] _params)
		{
			try {
				if (_params.Length != 1 || _params [0].Length == 0) {
					m_Console.SendResult ("Usage: listprefabs <searchString>");
					m_Console.SendResult ("    or listprefabs All");
					return;
				}
				
                string[] prefabsList = Directory.GetFiles(prefabsPath, "*.tts");
                int num = 0;
               

				if (_params[0].Equals("All")) {
					m_Console.SendResult("***Liste de tous les prefabs***");
					foreach (string s in prefabsList) {
						m_Console.SendResult (num + "   " + Path.GetFileNameWithoutExtension(s));
						num++;
					}
					m_Console.SendResult ("Listed " + num + "prefabs.");
				}else {
					m_Console.SendResult("***Liste des prefabs contenant " + _params [0] + "***");
					foreach (string s in prefabsList) {
						string fileName = Path.GetFileNameWithoutExtension(s);
						if (fileName.ToLower ().Contains (_params [0].ToLower ())) {
							m_Console.SendResult (num + "   " +  fileName);
							num++;
						}
					}
					m_Console.SendResult ("Listed " + num + " matching prefabs.");
				}
			}
			catch (Exception ex) {
				Log.Out("ListPrefab error: " + ex.Message);
			}
		}
	}
}


