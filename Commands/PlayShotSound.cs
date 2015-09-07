using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class PlayShotSound  : ConsoleCmdAbstract
    {
       
		public override string GetDescription ()
		{
            return "Joue un son sur le client du joueur (Gameid ou pseudo).";
        }

		public override string[] GetCommands ()
		{
            return new string[] { "sounds", "pss" };
        }

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
            try
            {
                if (_params.Count != 1) {
                    SdtdConsole.Instance.Output("Utilisation: sound <nomdujoueur1|gameid1> ...");
                    return;
                }
				int x = int.MinValue;

				int.TryParse (_params [0], out x);
				World world = GameManager.Instance.World;
				for (int i = world.Entities.list.Count - 1; i >= 0; i--) {
					Entity item = world.Entities.list[i];
					SdtdConsole.Instance.Output("item.entityId: " + item.entityId);
					SdtdConsole.Instance.Output("x: " + x);
					if(item.entityId == x) {
						item.PlayOneShot("Weapons/Ranged/44Magnum/44magnum_fire");
						SdtdConsole.Instance.Output("Sound played.");
						return;
					}
				}                
            }
            catch (Exception e)
            {
                Log.Out("Error in Heal.Run: " + e);
            }
        }
    }
}

