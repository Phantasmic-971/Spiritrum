using Terraria;
using Terraria.ID;
using Terraria.Localization; // Still useful for potential future localization
using Terraria.ModLoader;
using System.Collections.Generic; // Needed for List<TooltipLine>

// Namespace should match your folder structure
namespace Spiritrum.Content.Items
{
    [AutoloadEquip(EquipType.Body)] // Links item to the body texture
    public class BromiumChestplate : ModItem
    {
        // Define constants for the stats
        public const int MaxManaIncrease = 60;
        public const int MaxMinionIncrease = 2;

        // REMOVED: public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MaxManaIncrease, MaxMinionIncrease);
        // This line requires setup in localization files (.hjson)

        public override void SetStaticDefaults()
        {
            // Optional: If you want a general description line before the stats.
            // Tooltip.SetDefault("Provides immunity to On Fire!");
        }

        public override void SetDefaults()
        {
            Item.width = 18; // Hitbox width
            Item.height = 18; // Hitbox height
            Item.value = Item.sellPrice(gold: 1); // Sell value: 1 gold
            Item.rare = ItemRarityID.Orange; // Rarity
            Item.defense = 10; // Defense value
        }

        // This is where you add effects when the item is equipped
        public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.OnFire] = true; // Grant immunity to the 'On Fire!' debuff
            player.statManaMax2 += MaxManaIncrease; // Increase max mana
            player.maxMinions += MaxMinionIncrease; // Increase max minions
        }

        // This method allows you to add or modify tooltip lines
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add a line indicating the mana increase
            TooltipLine manaLine = new TooltipLine(Mod, "ManaIncrease", $"Increases maximum mana by {MaxManaIncrease}")
            {
                IsModifier = true, // Makes the text green
                IsModifierBad = false // Ensures it's green, not red
            };
            tooltips.Add(manaLine);

            // Add a line indicating the minion increase
            TooltipLine minionLine = new TooltipLine(Mod, "MinionIncrease", $"Increases maximum number of minions by {MaxMinionIncrease}")
            {
                IsModifier = true,
                IsModifierBad = false
            };
            tooltips.Add(minionLine);

            // You could also add non-modifier lines like this:
            // tooltips.Add(new TooltipLine(Mod, "Immunity", "Immunity to On Fire!"));
            // Note: The buffImmune effect doesn't automatically add a tooltip line, so adding it manually is good practice.
             TooltipLine immunityLine = new TooltipLine(Mod, "Immunity", "Immunity to On Fire!")
            {
                 // Optional: Style it like other effects if desired
                 // ForegroudColor = Color.Orange
            };
            tooltips.Add(immunityLine);
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            // Assuming you have an item named BromiumBar
            recipe.AddIngredient(ModContent.ItemType<BromiumBar>(), 15);
            recipe.AddIngredient(ItemID.BeeBreastplate, 1);
            recipe.AddIngredient(ItemID.NecroBreastplate, 1);
            recipe.AddIngredient(ItemID.MoltenBreastplate, 1);
            recipe.AddIngredient(ItemID.JungleShirt, 1);
            recipe.AddTile(TileID.Anvils); // Crafted at an Anvil
            recipe.Register();
        }
    }
}