using Microsoft.Xna.Framework; // Good practice to include
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Modes
{
	public class GeotMode : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.accessory = true;
			Item.rare = ItemRarityID.Gray; // Common rarity
			Item.value = Item.buyPrice(copper: 1); // Costs 1 dirt
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.endurance += 0.33f; // +33% damage reduction (take half damage)
			player.statDefense += 30; // General defense boost
			player.moveSpeed += 1f; // +100% movement speed (double)
			player.jumpSpeedBoost += 5f; // Increases jump speed significantly (flat boost)
			player.noFallDmg = true; // Grants immunity to fall damage
			player.noKnockback = true; // Grants immunity to knockback

			player.GetDamage(DamageClass.Generic) -= 0.60f; // -60% damage
			player.statManaMax2 = 0; // Effectively removes mana
			player.maxMinions -= 100; // Effectively removes summon slots
			player.GetCritChance(DamageClass.Generic) -= 200; // Effectively removes critical chance
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "GEoTModeTipChallenge", "Challenge"));
			tooltips.Add(new TooltipLine(Mod, "GEoTModeTipDamage", "Take half damage, deal -60% damage"));
			// Adjusted tooltip line for jump speed to be more accurate to the code
			tooltips.Add(new TooltipLine(Mod, "GEoTModeTipSpeed", "Double movement speed and significantly increased jump speed"));
			tooltips.Add(new TooltipLine(Mod, "GEoTModeTipStatsNegative", "-200 mana (base mana), no crits, no summons"));
			tooltips.Add(new TooltipLine(Mod, "GEoTModeTipFallDamage", "No fall damage"));
			tooltips.Add(new TooltipLine(Mod, "GEoTModeTipSubscribe", "Subscribe to GEoD mapping"));

			// You can add more lines or modify existing ones here
		}	

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 1); // Requires 1 dirt block
			recipe.AddTile(TileID.WorkBenches); // Crafted at any workbench
			recipe.Register();
		}
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            // Only allow equipping in the custom mode slot
            var modeSlotPlayer = player.GetModPlayer<ModeSlotPlayer>();
            return modded && slot == 0 && modeSlotPlayer != null && modeSlotPlayer.modeSlotItem.type == Item.type;
        }
	}
}