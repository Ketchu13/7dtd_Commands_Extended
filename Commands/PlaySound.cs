using System;
using System.Collections.Generic;

namespace AllocsFixes.CustomCommands
{
	public class PlaySound  : ConsoleCmdAbstract
    {
       
		public override string GetDescription ()
		{
            return "Joue un son sur le client du joueur (Gameid ou pseudo).";
        }

		public override string[] GetCommands ()
		{
            return new string[] { "sound", "snd" };
        }

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
            try
            {
                if (_params.Count != 1) {
                    SdtdConsole.Instance.Output("Utilisation: sound <nomdujoueur1|gameid1> ...");
                    return;
                }

				ClientInfo ci1 = ConsoleHelper.ParseParamIdOrName (_params [0]);
                if (ci1 == null) {
                    SdtdConsole.Instance.Output("Pseudo1 ou GameId1 introuvable.");
                    return;
                }
                else {
					EntityPlayer player1 = (EntityPlayer)GameManager.Instance.World.GetEntity(ci1.entityId);
                    string text2 = "Enemies/Acid_Puking_Hulk/hulkpain1";
                    player1.PlayOneShot(text2);
                    player1.PlayOneShot(text2);
                    SdtdConsole.Instance.Output("Son joue chez le joueur " + _params[0]);
                    return;
                }

            }
            catch (Exception e)
            {
                Log.Out("Error in Heal.Run: " + e);
            }
        }
    }
}

