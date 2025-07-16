using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Weapons
{ 
	public class AdminBlade : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Spiritrum.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 20000; // Increased damage
			Item.DamageType = DamageClass.Melee;
			Item.width = 64; // Reduced width
			Item.height = 64; // Reduced height
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2; // Increased knockback
			Item.value = Item.buyPrice(gold: 22); // Increased value
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.scale = 4f; // Adjust scale to make the sprite more centered
		}
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "No", "You are not supposed to have that outside of testing"));
        }
	}
}
