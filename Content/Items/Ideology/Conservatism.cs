using Microsoft.Xna.Framework; // Good practice to include
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Ideology
{
	public class Conservatism : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip is now handled directly via ModifyTooltips
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Yellow; // Post-Destroyer rarity
			Item.value = Item.buyPrice(gold: 15);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 5; // Increased defense for stability and order
			player.endurance += 0.05f; // Reduced damage taken for resilience (-5%)
			player.lifeRegen += 3; // Enhanced life regeneration for preservation
			player.GetDamage(DamageClass.Generic) += 0.1f; // Increased damage output for individual strength (+10%)
			player.moveSpeed += 0.05f; // Increased movement speed for adaptability (+5%)
			player.GetCritChance(DamageClass.Generic) += 5; // Increased critical strike chance for precision (+5%)
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "ConservatismTipPower", "Grants the power of Conservatism"));
			tooltips.Add(new TooltipLine(Mod, "ConservatismTipSkittle", "The blue skittle"));
			tooltips.Add(new TooltipLine(Mod, "ConservatismTipPlayerStats1", "Increases defense by 5, life regen by 3"));
			tooltips.Add(new TooltipLine(Mod, "ConservatismTipPlayerStats2", "Damage by 10%, movement speed and crit chance by 5%"));
			tooltips.Add(new TooltipLine(Mod, "ConservatismTipPlayerStats3", "Take 5% less damage"));

			// You can add more lines or modify existing ones here
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofMight, 10); // Requires 10 Souls of Might
			recipe.AddIngredient(ItemID.CobaltBar, 5); // Requires 5 Cobalt Bars
			recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			recipe.Register();

			Recipe palladiumRecipe = CreateRecipe();
			palladiumRecipe.AddIngredient(ItemID.SoulofMight, 10); // Requires 10 Souls of Might
			palladiumRecipe.AddIngredient(ItemID.PalladiumBar, 5); // Requires 5 Palladium Bars
			palladiumRecipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			palladiumRecipe.Register();
		}
		public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			if (player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item)
				return true;
			return false;
		}
	}
}