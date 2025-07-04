using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Ideology
{
	public class Socialism : ModItem
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
			Item.rare = ItemRarityID.Yellow; // Post-Skeletron Prime rarity
			Item.value = Item.buyPrice(gold: 15);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 4; // Increased defense for collective strength
			player.endurance += 0.1f; // Reduced damage taken for shared resilience (-10%)
			player.lifeRegen += 3; // Enhanced life regeneration for communal well-being
			player.moveSpeed += 0.1f; // Increased movement speed for individual freedom (+10%)
			player.GetDamage(DamageClass.Generic) -= 0.05f; // Balanced damage (decreased by 5%)

			// Note: The player does NOT gain crit chance from this item based on the code below.

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense += 2; // Boost defense of nearby allies
					ally.lifeRegen += 2; // Boost life regeneration of nearby allies
					ally.GetDamage(DamageClass.Generic) += 0.05f; // Increase damage output of nearby allies (+5%)
					// Note: Allies do NOT gain move speed or crit chance from this item based on the code.
				}
			}
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "SocialismTipPower", "Grants the power of Socialism"));
			tooltips.Add(new TooltipLine(Mod, "SocialismTipSkittle", "The red skittle"));
			tooltips.Add(new TooltipLine(Mod, "SocialismTipPlayerStats1", "Increases defense by 4, life regen by 3"));
			// Adjusted tooltip to remove player crit chance as it's not in the code
			tooltips.Add(new TooltipLine(Mod, "SocialismTipPlayerStats2", "movement speed by 10%"));
			tooltips.Add(new TooltipLine(Mod, "SocialismTipPlayerStats3", "Damage is decreased by 5%"));
			tooltips.Add(new TooltipLine(Mod, "SocialismTipPlayerStats4", "Take 10% less damage"));
			// Aligned allies tooltip with the code effects
			tooltips.Add(new TooltipLine(Mod, "SocialismTipAllies", "Allies within 800 feet deal 5% more damage and gain 2 life regen and defense"));

			// You can add more lines or modify existing ones here
		}

		public override void AddRecipes()
		{
			Recipe adamantiteRecipe = CreateRecipe();
			adamantiteRecipe.AddIngredient(ItemID.SoulofFright, 10); // Requires 10 Souls of Fright
			adamantiteRecipe.AddIngredient(ItemID.AdamantiteBar, 5); // Requires 5 Adamantite Bars
			adamantiteRecipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			adamantiteRecipe.Register();

			Recipe titaniumRecipe = CreateRecipe();
			titaniumRecipe.AddIngredient(ItemID.SoulofFright, 10); // Requires 10 Souls of Fright
			titaniumRecipe.AddIngredient(ItemID.TitaniumBar, 5); // Requires 5 Titanium Bars
			titaniumRecipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			titaniumRecipe.Register();
		}
		public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			if (player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item)
				return true;
			return false;
		}
	}
}