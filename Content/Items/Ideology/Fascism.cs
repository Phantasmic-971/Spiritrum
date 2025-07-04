using Microsoft.Xna.Framework; // Good practice to include
using Spiritrum.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Ideology
{
	public class Fascism : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip is handled via ModifyTooltips
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink; // Post-Skeletron rarity
			Item.value = Item.buyPrice(gold: 10);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense -= 2; // Decreased defense
			player.endurance -= 0.05f; // Take 5% MORE damage (reducing endurance increases damage taken)
			player.GetDamage(DamageClass.Generic) += 0.1f; // Increased damage output (+10%)
			player.moveSpeed += 0.05f; // Increased movement speed (+5%)

            // Note: The shop price increase is handled in the FascismPlayer class
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "FascismTipBanned", "This one might get you banned"));
			tooltips.Add(new TooltipLine(Mod, "FascismTipStats1", "Increases damage by 10%, movement speed by 5%"));
			tooltips.Add(new TooltipLine(Mod, "FascismTipStats2", "Decreased defense by 2 and take +5% damage"));

			// You can add more lines or modify existing ones here
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Bone, 25);
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
		public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			if (player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item)
				return true;
			return false;
		}
	}
}