using Microsoft.Xna.Framework; // Good practice to include
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Consumables
{
	public class Spectralite : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip is now handled directly via ModifyTooltips
		}

		public override bool CanUseItem(Player player)
		{
			// Can only use if mana is at the base cap (200) and Spectralite hasn't been consumed yet
			return player.statManaMax == 200 && player.GetModPlayer<SpectralitePlayer>().spectraliteConsumed == 0; // Limit to 1 consumption
		}

		public override bool? UseItem(Player player)
		{
			// Set the mana increase value in the ModPlayer and mark as consumed
			player.GetModPlayer<SpectralitePlayer>().spectraliteMana = 100; // Set max mana increase to 100
			player.GetModPlayer<SpectralitePlayer>().spectraliteConsumed = 1; // Mark as consumed

			// Optional: Play a sound effect on use
			// SoundEngine.PlaySound(SoundID.Item43, player.position); // Example sound

			return true; // Consume the item
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 9999; // Increase stack size
			Item.value = Item.buyPrice(silver: 10); // Value per unit
			Item.rare = ItemRarityID.Pink; // Rarity level
			Item.useStyle = ItemUseStyleID.HoldUp; // Use style for consumable items
			Item.useAnimation = 20; // Animation time
			Item.useTime = 20; // Use time
			Item.consumable = true; // Mark as consumable
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "SpectraliteTipManaIncrease", "Can be consumed once to increase max mana by 100 ignoring the 400 cap"));
			// Adjusted tooltip to accurately reflect that the effect resets on respawning
			tooltips.Add(new TooltipLine(Mod, "SpectraliteTipWarning", "Warning! Resets on respawning. Very Unstable."));

			// You can add more lines or modify existing ones here
			// You might want to add a line if it's already consumed
			// if (Main.LocalPlayer.GetModPlayer<SpectralitePlayer>().spectraliteConsumed > 0)
			// {
			//     tooltips.Add(new TooltipLine(Mod, "SpectraliteTipConsumed", "Already consumed for this character.") { OverrideColor = Color.Red });
			// }
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(3); // Produces 3 Spectralite
			recipe.AddIngredient(ItemID.Ectoplasm, 1); // Requires 1 Ectoplasm
			recipe.AddIngredient(ItemID.HallowedBar, 1); // Requires 1 Hallowed Bar
			recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			recipe.Register();
		}
	}

	// Add a custom ModPlayer class to handle the mana increase
	public class SpectralitePlayer : ModPlayer
	{
		public int spectraliteMana = 0; // Stores the amount of mana increased by Spectralite
		public int spectraliteConsumed = 0; // Track if Spectralite has been consumed (0 = no, 1 = yes)

		// This hook applies permanent stat changes. It's called after player stats are reset.
		public override void ResetEffects()
		{
			// Apply the mana increase from Spectralite
			Player.statManaMax2 += spectraliteMana; // statManaMax2 is used for mana above the crystal cap (200+200)
		}

		// Note: If you want it to reset on leaving the world as well,
		// you would need to add the PlayerDisconnect hook:
		// public override void PlayerDisconnect()
		// {
		//     spectraliteMana = 0;
		//     spectraliteConsumed = 0;
		// }

		// If you want the effect to persist across saving and loading,
		// you would need to implement SaveData and LoadData hooks.
	}
}
