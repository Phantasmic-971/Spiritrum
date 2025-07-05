using Microsoft.Xna.Framework; // Good practice to include
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Accessories
{
	public class GelaticCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Uncommented DisplayName.SetDefault as it was already there
			// DisplayName.SetDefault("Gelatic Crown");

			// Tooltip is now handled directly via ModifyTooltips
			// Removed the commented-out Tooltip.SetDefault line
		}

		public override void SetDefaults()
		{
			Item.width = 28; // Adjust to match the sprite dimensions
			Item.height = 28; // Adjust to match the sprite dimensions
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink; // Rarity matching post-King Slime items
			Item.value = Item.buyPrice(gold: 5); // Adjust value as needed
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.moveSpeed += 0.3f; // Increases movement speed by 30%
			player.jumpSpeedBoost += 3f; // Increases jump speed (Note: This is a flat boost, not a percentage)
			player.noFallDmg = true; // Grants immunity to fall damage
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "GelaticCrownTipStats", "Increases jump and movement speed by 30%")); // Tooltip text as provided
			tooltips.Add(new TooltipLine(Mod, "GelaticCrownTipFallDamage", "Immunity to fall damage"));
		}

		public override void AddRecipes()
		{
			// No recipe as it is a drop from King Slime
		}
	}
}