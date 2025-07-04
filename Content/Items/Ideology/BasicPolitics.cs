using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Still good practice to include
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Ideology
{
	public class BasicPolitics : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip is now handled directly via ModifyTooltips
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime; // Pre-Plantera rarity
			Item.value = Item.buyPrice(gold: 20);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			// Effects from Democracy (Note: These effects seem to be different/combined from the Democracy item code provided earlier)
			player.statDefense += 5;
			player.moveSpeed += 0.15f;
			player.endurance += 0.1f; // This corresponds to -10% damage taken
			player.lifeRegen += 4;
			player.GetCritChance(DamageClass.Generic) += 5;
			player.GetDamage(DamageClass.Generic) += 0.12f;

			foreach (Player ally in Main.player)
			{
				if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
				{
					ally.statDefense += 3; // Boost defense of nearby allies
					ally.lifeRegen += 2; // Boost life regeneration of nearby allies
					ally.GetDamage(DamageClass.Generic) += 0.05f; // Increase damage output of nearby allies
					// Note: Ally movement speed from the Anarchy code is not included here
				}
			}
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines here based on the provided text
			tooltips.Add(new TooltipLine(Mod, "BasicPoliticsTipMix", "A mix of sharing, power and freedom"));
			tooltips.Add(new TooltipLine(Mod, "BasicPoliticsTipPlayerStats1", "Increases defense by 5, movement speed by 15%, crit chance by 5%"));
			// Note: The code gives player.endurance += 0.1f which means -10% damage taken, matching the tooltip
			tooltips.Add(new TooltipLine(Mod, "BasicPoliticsTipPlayerStats2", "Life regen by 4, damage by 12% and take -10% damage"));
			// Note: The code gives allies +0.05f damage, not +5%. I've kept the tooltip text as you provided.
			tooltips.Add(new TooltipLine(Mod, "BasicPoliticsTipAllies", "Allies within 800 feet gain +5% damage, 3 defense and 2 life regen"));

			// You can add more lines or modify existing ones here
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemType<Democracy>()); // Assuming Democracy is another item in your mod
			recipe.AddIngredient(ItemType<Communism>()); // Assuming Communism is another item in your mod
			recipe.AddIngredient(ItemType<Fascism>()); // Assuming Fascism is another item in your mod
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
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