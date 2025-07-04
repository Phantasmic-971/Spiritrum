using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic; // Needed for List<TooltipLine>
using Terraria.Localization; // Needed for TooltipLine
using Spiritrum.Content.Items.Weapons;

namespace Spiritrum.Content.Items
{
	public class BrokenBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Optional: Display name and tooltip can go in localization .hjson files
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999; // Increased stack size to 9999
			Item.value = Item.sellPrice(gold: 20);
			Item.rare = ItemRarityID.Yellow;
		}
	}
}