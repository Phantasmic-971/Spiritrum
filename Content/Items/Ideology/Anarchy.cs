using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Added for localization support in tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Ideology
{
	public class Anarchy : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip is handled in the ModifyTooltips method now.
			// DisplayName.SetDefault("Anarchy"); // Optional: Keep if you want a default name
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.buyPrice(gold: 10);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense -= 5; // Reduced defense to reflect chaos
			player.endurance -= 0.05f; // Increased damage taken for lack of order
			player.GetDamage(DamageClass.Generic) += Main.rand.NextFloat(-0.2f, 0.5f); // Randomized damage boost or reduction
			player.moveSpeed += Main.rand.NextFloat(-0.1f, 0.3f); // Randomized movement speed boost
			player.GetCritChance(DamageClass.Generic) += Main.rand.Next(-15, 15); // Randomized critical strike chance
			player.GetAttackSpeed(DamageClass.Generic) += Main.rand.NextFloat(-0.3f, 0.4f); // Randomized attack speed

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense -= 3; // Reduced defense of nearby allies
					ally.GetDamage(DamageClass.Generic) += Main.rand.NextFloat(-0.05f, 0.15f); // Randomized damage boost for allies
					ally.moveSpeed += Main.rand.NextFloat(-0.05f, 0.1f); // Randomized movement speed boost for allies
				}
			}
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// "You never know what happens when there are no rules"
			tooltips.Add(new TooltipLine(Mod, "AnarchyTipChaos", Language.GetTextValue("You never know what happens when there are no rules"))); // Using localization for the first line

			// "-5 defense, take 5% more damage"
			tooltips.Add(new TooltipLine(Mod, "AnarchyTipDefense", "-5 defense, take 5% more damage"));

			// "Deal -20 to +50% damage, -15 to +15% crit chance"
			tooltips.Add(new TooltipLine(Mod, "AnarchyTipDamageCrit", "Deal -20 to +50% damage, -15 to +15% crit chance"));

			// "-10 to +30% movement speed, -30 to +40% attack speed"
			tooltips.Add(new TooltipLine(Mod, "AnarchyTipSpeed", "-10 to +30% movement speed, -30 to +40% attack speed"));

			// "Allies lose 3 defense, but deal -5 to +15% damage and have -5 to +10% movement speed"
			tooltips.Add(new TooltipLine(Mod, "AnarchyTipAllies", "Allies within 800 feet lose 3 defense, but deal -5 to +15% damage and have -5 to +10% movement speed"));

			// You can add more lines or logic here if needed.
			// For example, you could change the color of a line:
			// tooltips.Find(t => t.Name == "AnarchyTipChaos").OverrideColor = Color.Red;
		}

		public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			if (player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item)
				return true;
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemType<Spectralite>(), 20); // Assuming Spectralite is another item in your mod
			recipe.AddIngredient(ItemID.SoulofNight, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}