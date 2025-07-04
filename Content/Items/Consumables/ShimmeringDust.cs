using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Buffs;
using System.Collections.Generic; // Needed for List<TooltipLine>
using Terraria.Localization; // Needed for TooltipLine

namespace Spiritrum.Content.Items.Consumables
{
    public class ShimmeringDust : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip are best set in localization (.hjson) files.
            // Example: Tooltip.SetDefault("Temporarily increases movement speed and jump height.\nLeaves a trail of sparkling dust.");
            // Example: DisplayName.SetDefault("Shimmering Dust");

            // You might set research counts here if needed:
            // CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20; // Example: 20 needed
        }

        public override void SetDefaults()
        {
            // --- These properties define the item's instance behavior ---
            Item.consumable = true; // <<<< MAKES THE ITEM CONSUMABLE >>>>
            Item.useStyle = ItemUseStyleID.DrinkLiquid; // Use a drinking animation
            Item.useAnimation = 17; // Animation time (how long the animation plays)
            Item.useTime = 17; // Use time (how long before you can use another item)
            Item.maxStack = Item.CommonMaxStack; // Standard max stack size (usually 999 or 9999)
            Item.width = 16; // Item texture width
            Item.height = 16; // Item texture height
            Item.rare = ItemRarityID.LightRed; // Set rarity (LightRed is Hardmode tier 1)
            Item.value = Item.sellPrice(silver: 50); // Set sell price (50 silver)
            Item.UseSound = SoundID.Item3; // Sound when used (drinking sound)

            // --- Buff application properties (optional but recommended) ---
            // Setting these automatically applies the buff when UseItem returns true or null
            Item.buffType = ModContent.BuffType<ShimmeredDust>(); // The buff to apply
            Item.buffTime = 60 * 60; // Duration in ticks (60 ticks/sec * 60 sec = 1 minute)
        }

        // This method is called when the item is used.
        // If you ONLY want to apply the buff set in SetDefaults, you can remove this override entirely.
        public override bool? UseItem(Player player)
        {
            // Buff application is handled automatically by Item.buffType and Item.buffTime in SetDefaults
            // when this method returns true or null.

            // You only need code here for *additional* effects beyond the buff.
            // For example, spawning dust particles:
            /*
            for (int i = 0; i < 15; i++) {
                Dust.NewDust(player.Center, 0, 0, DustID.ShimmerSpark, Scale: 1.2f); // Example dust effect
            }
            */

            // Return true to confirm the item use was successful.
            // This allows consumption (because Item.consumable = true) and triggers the buff application.
            // Returning null would also work for consumption and buff application.
            // Returning false would prevent consumption and the buff.
            return true;
        }

        // --- Add the ModifyTooltips method ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add your custom tooltip lines
            // You can add color tags like [c/FF0000:This is red text]
            tooltips.Add(new TooltipLine(Mod, "ShimmeringDustTooltip", "Temporarily increases movement speed and jump height."));
            tooltips.Add(new TooltipLine(Mod, "ShimmeringDustTooltip2", "Leaves a trail of sparkling dust."));

            // You can find specific lines (like the defense line) and modify or remove them if needed
            /*
            foreach (var line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "Defense")
                {
                    // Example: Modify the default defense tooltip line
                    // line.Text = "Increased Defense";
                }
            }
            */
        }

        // Remember to have the ShimmeringDustBuff class defined in its own file,
        // likely within the Spiritrum.Content.Buffs namespace.
    }
}