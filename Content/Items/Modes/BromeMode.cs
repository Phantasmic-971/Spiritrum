using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;

// Rename the namespace reference if you moved the file
namespace Spiritrum.Content.Items.Modes // Assuming the item is still in the Items folder
{
	// Renamed the class from Brome to BromeMode
	public class BromeMode : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Setting the display name directly (as requested earlier)
			// DisplayName.SetDefault("Brome Mode");

			// Tooltip is handled via ModifyTooltips
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
			// Note: Aggro is applied here. The chat messages and potion sickness are handled in the BromePlayer class.
			player.aggro += 5000000; // +5000000 aggro
			player.GetAttackSpeed(DamageClass.Generic) += 0.6f; // +60% attack speed
			player.lifeRegen -= 8;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "BromeModeTipChallenge", "Challenge")); // Updated internal name
			tooltips.Add(new TooltipLine(Mod, "BromeModeTipAttackSpeed", "+60% attack speed")); // Tooltip text from your request
			tooltips.Add(new TooltipLine(Mod, "BromeModeTipAggroFrench", "You are now french and gain 5 Million aggro")); // Tooltip text from your request
				tooltips.Add(new TooltipLine(Mod, "BromeModeTipLifeRegen", "-8 life regen"));
			// Tooltip for the on-hit effect
			tooltips.Add(new TooltipLine(Mod, "BromeModeTipPotionSickness", "When you get hit, gain potion sickness for 5 seconds")); // Updated internal name
			// Tooltip for the chat messages - You can add a line explaining the effect
			tooltips.Add(new TooltipLine(Mod, "BromeModeTipChatMessages", "Every 5 seconds say something in the chat")); // Updated internal name

			tooltips.Add(new TooltipLine(Mod, "BromeModeTipSubscribe", "Subscribe to Brome")); // Tooltip text from your request
		}

		public override void AddRecipes()
		{
			// CreateRecipe() implicitly creates a recipe for this item (BromeMode)
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 1); // Requires 1 dirt block
			recipe.AddTile(TileID.WorkBenches); // Crafted at any workbench
			recipe.Register();
		}
		public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			// Only allow equipping in the custom mode slot
			if (player.GetModPlayer<Spiritrum.Players.ModeSlotPlayer>().modeSlotItem == Item)
				return true;
			return false;
		}
	}
}