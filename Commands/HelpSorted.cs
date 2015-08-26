using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AllocsFixes.CustomCommands
{
	public class HelpSorted :ConsoleCmdAbstract
	{
		public Dictionary<string, ConsoleCmdHelp.scl0000> fd0000 = new Dictionary<string, ConsoleCmdHelp.scl0000>();

		public override string GetDescription ()
		{
			return "Liste toutes les commandes du serveur par odre alphabetique.)";
		}
		public override string[] GetCommands ()
		{
			return new string[] { "helpk", "hk" };
		}

		public struct scl0000
		{
			public string Description;

			public string HelpText;

			public scl0000(string _desc, string _helpText)
			{
				this.Description = _desc;
				this.HelpText = _helpText;
			}
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (_params.Count == 0)
				{
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("*** Generic Console Help ***");
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("To get further help on a specific topic or command type (without the brackets)");
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("    help <topic / command>");
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output(string.Empty);
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("Generic notation of command parameters:");
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("   <param name>              Required parameter");
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("   <entityId / player name>  Possible types of parameter values");
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("   [param name]              Optional parameter");
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output(string.Empty);
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("*** List of Help Topics ***");

					if (this.fd0000.Count != 0)
					{
						foreach (KeyValuePair<string, ConsoleCmdHelp.scl0000> keyValuePair in this.fd0000)
						{
							stringBuilder.Append(keyValuePair.Key);
							stringBuilder.Append(" => ");
							stringBuilder.Append(keyValuePair.Value.Description);
							SingletonMonoBehaviour<SdtdConsole>.Instance.Output(stringBuilder.ToString());
							stringBuilder.Length = 0;
						}
					}
					else
					{
						SingletonMonoBehaviour<SdtdConsole>.Instance.Output("None yet");
					}
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output(string.Empty);
					SingletonMonoBehaviour<SdtdConsole>.Instance.Output("*** List of Commands ***");
					IEnumerator<IConsoleCommand> enumerator = SingletonMonoBehaviour<SdtdConsole>.Instance.GetCommands().GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							IConsoleCommand current = enumerator.Current;
							string[] commands = current.GetCommands();
							for (int i = 0; i < (int)commands.Length; i++)
							{
								string str = commands[i];
								stringBuilder.Append(" ");
								stringBuilder.Append(str);
							}
							stringBuilder.Append(" => ");
							stringBuilder.Append(current.GetDescription());
							SingletonMonoBehaviour<SdtdConsole>.Instance.Output(stringBuilder.ToString());
							stringBuilder.Length = 0;
						}
					}
					finally
					{
						if (enumerator == null)
						{
						}
						enumerator.Dispose();
					}
				}
				else if (_params.Count == 1)
				{
					string str1 = null;
					string help = null;
					if (!this.fd0000.ContainsKey(_params[0]))
					{
						IConsoleCommand command = SingletonMonoBehaviour<SdtdConsole>.Instance.GetCommand(_params[0]);
						if (command != null)
						{
							str1 = string.Concat("Command: ", _params[0]);
							help = command.GetHelp();
							if (string.IsNullOrEmpty(help))
							{
								help = string.Concat("No detailed help available.\nDescription: ", command.GetDescription());
							}
						}
					}
					else
					{
						str1 = string.Concat("Topic: ", _params[0]);
						help = this.fd0000[_params[0]].HelpText;
					}
					if (str1 == null)
					{
						SingletonMonoBehaviour<SdtdConsole>.Instance.Output(string.Concat("No command or topic found by \"", _params[0], "\""));
					}
					else
					{
						SingletonMonoBehaviour<SdtdConsole>.Instance.Output(string.Concat("*** ", str1, " ***"));
						string[] strArrays = help.Split(new char[] { '\n' });
						for (int j = 0; j < (int)strArrays.Length; j++)
						{
							string str2 = strArrays[j];
							SingletonMonoBehaviour<SdtdConsole>.Instance.Output(str2);
						}
					}
				}
			}
			catch (Exception arg) 
			{
				Log.Out("Error in helpk2.Run: " + arg);
			}
		}
	}
}


//				GameManager manager = CommonMappingFunctions.GetGameManager ();
//				ConsoleSdtd cons = manager.m_GUIConsole;
//
//				List<string> array1 = new List<string>() { };
//
//				m_Console.SendResult("***Liste des commandes par odre alphabetique***");
//
//				foreach (ConsoleCommand current in cons.commands)
//				{
//					string str = string.Empty;
//					string[] array = current.Names();
//					for (int i = 0; i < array.Length; i++)
//					{
//						string str2 = array[i];
//						str = str + str2 + " ";
//					}
//					array1.Add(str + " => " + current.Description());
//				}
//
//				array1.Sort();
//
//				int num = 1;
//				try
//				{
//					for (int i = 0; i < array1.Count; i ++)
//					{
//						if( array1[i] != null)
//						{
//							m_Console.SendResult(num + ". " + array1[i] + "\n");
//							num++;
//						}
//					}
//					m_Console.SendResult("La liste des commandes contient " + num + " commandes.");
//				}
//				catch (Exception arg)
//				{
//					Log.Out("Error in helpk1.Run: " + arg);
//				} 
//			