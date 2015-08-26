using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace AllocsFixes.CustomCommands
{
	public class ExportItemIcons : ConsoleCmdAbstract
	{
		public override string GetDescription ()
		{
			return "Exports all ItemIcons";
		}

		public override string GetHelp () {
			return "Exports all ItemIcons currently in the game to the folder \"ItemIcons\" in the game root";
		}

		public override string[] GetCommands ()
		{
			return new string[] { "exportitemicons" };
		}

		public override void Execute (List<string> _params, CommandSenderInfo _senderInfo) {
			string exportPath = Utils.GetGameDir ("ItemIcons");

			GameObject atlasObj = GameObject.Find ("/NGUI Root (2D)/ItemIconAtlas");
			if (atlasObj == null) {
				SdtdConsole.Instance.Output ("Atlas object not found");
				return;
			}
			DynamicUIAtlas atlas = atlasObj.GetComponent<DynamicUIAtlas> ();
			if (atlas == null) {
				SdtdConsole.Instance.Output ("Atlas component not found");
				return;
			}

			Texture2D atlasTex = atlas.texture as Texture2D;

			if (Directory.Exists (exportPath)) {
				SdtdConsole.Instance.Output ("Export path (" + exportPath + ") already exists");
				return;
			}
			Directory.CreateDirectory (exportPath);

			foreach (UISpriteData data in atlas.spriteList) {
				string name = data.name;
				Texture2D tex = new Texture2D (data.width, data.height, TextureFormat.ARGB32, false);
				tex.SetPixels (atlasTex.GetPixels (data.x, atlasTex.height - data.height - data.y, data.width, data.height));
				byte[] pixData = tex.EncodeToPNG ();
				File.WriteAllBytes (exportPath + "/" + name + ".png", pixData);

				UnityEngine.Object.Destroy (tex);
			}

		}
	}
}
