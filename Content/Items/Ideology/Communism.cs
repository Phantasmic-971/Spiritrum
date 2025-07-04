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
	public class Communism : ModItem
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
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.buyPrice(gold: 10);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 1; // Increased defense for collective protection
			player.endurance += 0.03f; // Higher damage reduction for shared resilience (-3% damage taken)
			player.lifeRegen += 1; // Enhanced life regeneration for communal well-being (+1 life regen)
			player.GetDamage(DamageClass.Generic) -= 0.1f; // Slightly reduced individual damage for balance (-10% damage)

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense += 1; // Boost defense of nearby allies
					ally.lifeRegen += 1; // Boost life regeneration of nearby allies
				}
			}
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "CommunismTipOur", "OUR ACCESSORY"));
			// Adjusted life regen in tooltip to match the code (which is +1)
			tooltips.Add(new TooltipLine(Mod, "CommunismTipPlayerStats1", "Increases defense by 1, life regen by 1"));
			tooltips.Add(new TooltipLine(Mod, "CommunismTipPlayerStats2", "Decreased damage by 10% and take -3% damage"));
			tooltips.Add(new TooltipLine(Mod, "CommunismTipAllies", "Allies within 800 feet have +1 defense and Life regen"));
			tooltips.Add(new TooltipLine(Mod, "CommunismTipFact", "F*bs*l was like Stalin"));

			// You can add more lines or modify existing ones here
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Bone, 30);
			recipe.AddIngredient(ItemID.Silk, 15);
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