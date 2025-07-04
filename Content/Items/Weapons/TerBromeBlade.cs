using Microsoft.Xna.Framework; // Good practice to include
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Weapons
{
	public class TerBromeBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			// The Display Name of this item can be edited in the 'Localization/en-US_Mods.Spiritrum.hjson' file.
			// Tooltip is now handled directly via ModifyTooltips.
		}

		public override void SetDefaults()
		{
			Item.damage = 128; // Increased damage
			Item.DamageType = DamageClass.Melee;
			Item.width = 20; // Reduced width
			Item.height = 20; // Reduced height
			Item.useTime = 14;
			Item.useAnimation = 14;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8; // Increased knockback
			Item.value = Item.buyPrice(gold: 50); // Increased value
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.scale = 2f; // Adjust scale to make the sprite more centered
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip line based on the provided text
			tooltips.Add(new TooltipLine(Mod, "TeBromeBladeTipFlavor", "The end of the line of Bromium"));

			// You can add more lines or modify existing ones here
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemType<Bromecalibur>(), 1); // Requires 1 Bromecalibur (Assuming Bromecalibur is another item in your mod)
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
			recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			recipe.Register();
		}
	}
}