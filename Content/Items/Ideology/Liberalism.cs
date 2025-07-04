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
	public class Liberalism : ModItem
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
			Item.rare = ItemRarityID.Yellow; // Post-Twins rarity
			Item.value = Item.buyPrice(gold: 15);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 4; // General defense boost
			player.moveSpeed += 0.1f; // General movement speed boost (+10%)
			player.GetDamage(DamageClass.Generic) += 0.05f; // General damage boost (+5%)
			player.endurance += 0.05f; // General damage reduction (-5% damage taken)
			player.lifeRegen += 1; // General life regeneration boost
			player.GetCritChance(DamageClass.Generic) += 2; // Small critical strike chance bonus (+2%)

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense += 1; // Small defense boost for allies
					ally.moveSpeed += 0.05f; // Small movement speed boost for allies (+5%)
					ally.GetDamage(DamageClass.Generic) += 0.02f; // Small damage boost for allies (+2%)
					ally.GetCritChance(DamageClass.Generic) += 1; // Small critical strike chance bonus for allies (+1%)
				}
			}
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "LiberalismTipPower", "Grants the power of Liberalism"));
			tooltips.Add(new TooltipLine(Mod, "LiberalismTipSkittle", "The green skittle"));
			tooltips.Add(new TooltipLine(Mod, "LiberalismTipPlayerStats1", "Increases defense by 4, life regen by 1"));
			tooltips.Add(new TooltipLine(Mod, "LiberalismTipPlayerStats2", "Damage by 5%, movement speed by 10%, crit chance by 2%"));
			tooltips.Add(new TooltipLine(Mod, "LiberalismTipPlayerStats3", "Take 5% less damage"));

			// You can add more lines or modify existing ones here
			// You might want to add a line about ally boosts:
			// tooltips.Add(new TooltipLine(Mod, "LiberalismTipAllies", "Nearby allies gain +1 defense, +1 life regen, +5% move, +2% damage, +1% crit"));
		}

		public override void AddRecipes()
		{
			Recipe mythrilRecipe = CreateRecipe();
			mythrilRecipe.AddIngredient(ItemID.SoulofSight, 10); // Requires 10 Souls of Sight
			mythrilRecipe.AddIngredient(ItemID.MythrilBar, 5); // Requires 5 Mythril Bars
			mythrilRecipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			mythrilRecipe.Register();

			Recipe orichalcumRecipe = CreateRecipe();
			orichalcumRecipe.AddIngredient(ItemID.SoulofSight, 10); // Requires 10 Souls of Sight
			orichalcumRecipe.AddIngredient(ItemID.OrichalcumBar, 5); // Requires 5 Orichalcum Bars
			orichalcumRecipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			orichalcumRecipe.Register();
		}
		public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			if (player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item)
				return true;
			return false;
		}
	}
}