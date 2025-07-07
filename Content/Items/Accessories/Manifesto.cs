using Microsoft.Xna.Framework; // Good practice to include
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Accessories
{
	public class Manifesto : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Manifesto"); // Keep commented or uncomment if you want to set the name here

			// Tooltip is now handled directly via ModifyTooltips
			// Removed the commented-out Tooltip.SetDefault line
		}

		// This method is called when the item is first loaded into the game.
		// You can use it to set static properties like the item's name and tooltip.

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 30;
			Item.accessory = true;
			Item.value = Item.buyPrice(gold: 10);
			Item.rare = ItemRarityID.Orange;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.discountAvailable = true;

			player.GetDamage(DamageClass.Generic) += 0.15f;
			player.moveSpeed += 0.05f;
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(Mod, "ManifestoTipDiscount", "Decreases shop prices by 20%"));
			
			tooltips.Add(new TooltipLine(Mod, "ManifestoTipAllPlayers", "All players gain 15% damage and 5% movement speed"));

			// You can add more lines or modify existing ones here
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Book, 1);
			recipe.AddIngredient(ItemID.FallenStar, 3);
           		recipe.AddIngredient<Items.Materials.Paper>(2);
			recipe.AddIngredient(ItemID.DiscountCard, 1);
			recipe.AddIngredient(ItemID.AvengerEmblem, 1);
			recipe.AddTile(TileID.Bookcases);
			recipe.Register();
		}
	}
}
