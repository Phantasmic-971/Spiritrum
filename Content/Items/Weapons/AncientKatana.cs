using Microsoft.Xna.Framework; // Often useful, though not strictly needed for tooltips
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Still good practice to include
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Weapons
{
	public class AncientKatana : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Display Name and Tooltip are now handled directly or via ModifyTooltips
			// DisplayName.SetDefault("Ancient Katana"); // You can set a default name here if not using localization for the name
		}

		public override void SetDefaults()
		{
			Item.damage = 20; // Increased damage
			Item.DamageType = DamageClass.Melee;
			Item.width = 35; // Reduced width
			Item.height = 35; // Reduced height
			Item.useTime = 8;
			Item.useAnimation = 8;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8; // Increased knockback
			Item.value = Item.buyPrice(gold: 10); // Increased value
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.scale = 1.2f; // Adjust scale to make the sprite more centered
			Item.useTurn = true; // Allows the player to turn while using the item
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines here
			tooltips.Add(new TooltipLine(Mod, "AncientKatanaTipAerial", "Very effective against aerial targets"));
			tooltips.Add(new TooltipLine(Mod, "AncientKatanaTipReach", "Bad horizontal reach, good vertical reach"));

			// You can add more lines or modify existing ones here
		}


		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemType<BromiumBar>(), 15); // Requires 15 Bromium Bars (Assuming BromiumBar is another item in your mod)
			recipe.AddIngredient(ItemID.Katana, 1); // Requires 1 Katana
			recipe.AddTile(TileID.Anvils); // Crafted at an Anvil
			recipe.Register();
		}
	}
}