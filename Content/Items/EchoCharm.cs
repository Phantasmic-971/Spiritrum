using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Spiritrum.Content.Buffs; // Added using directive for the buff's namespace
using System.Collections.Generic; // Needed for List<TooltipLine>
using Terraria.Localization; // Needed for TooltipLine

namespace Spiritrum.Content.Items // Using the namespace you provided
{
    // This class defines the Echo Charm accessory
    public class EchoCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Set the item's name - Handled via localization files (.hjson)
            // DisplayName.SetDefaults("Echo Charm"); // This line is removed

            // Set the item's tooltip - Handled via localization files (.hjson)
            // Tooltip.SetDefault("Grants a 15% chance to dodge an attack.\nUpon a successful dodge, emits a shadowy burst."); // This line is removed

            // This makes the item an accessory - This was redundant with Item.accessory = true in SetDefaults and seems outdated based on error log.
            // ItemID.Sets.IsACCESSORY[Type] = true; // This line is removed

            // You can set the display name and tooltip here using Language.GetTextValue if needed for code logic,
            // but it's primarily for localization files.
            // DisplayName.SetDefault("Echo Charm"); // Example using SetDefault (older syntax, but might work depending on version)
            // Tooltip.SetDefault("..."); // Example using SetDefault
            // Or for newer versions, rely on localization files and access like Item.DisplayName.GetTranslation()
        }

        public override void SetDefaults()
        {
            Item.width = 24; // Item texture width (adjust)
            Item.height = 24; // Item texture height (adjust)
            Item.accessory = true; // Mark as accessory (This is the correct way)
            Item.rare = ItemRarityID.Blue; // Set rarity (Blue is typically pre-Hardmode, Post-Skeletron)
            Item.value = Item.sellPrice(gold: 20); // Set sell price (adjust)
        }        // This method applies the accessory's effects to the player
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Apply the CharmingEcho buff when the accessory is equipped
            // The buff duration is set to 2 frames to ensure it's always active while equipped
            // The actual dodge functionality is handled in the EchoPlayer.PreHurt method
            player.AddBuff(ModContent.BuffType<CharmingEcho>(), 2);
        }        // --- Add customized tooltips ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add clear tooltip lines describing the accessory's effect
            tooltips.Add(new TooltipLine(Mod, "EchoCharmTooltip1", "Grants a 15% chance to dodge an attack"));
            tooltips.Add(new TooltipLine(Mod, "EchoCharmTooltip2", "Upon a successful dodge, emits a shadowy burst that damages nearby enemies"));
        }
    }
}