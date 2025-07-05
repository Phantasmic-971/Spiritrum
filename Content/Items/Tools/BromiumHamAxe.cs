using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for ItemType
using System.Collections.Generic; // Needed for List<T>
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Tools // Recommended namespace for weapons/tools
{
    public class BromiumHamAxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Chops wood and breaks blocks with equal efficiency"); // Optional: Basic description
            // DisplayName.SetDefault("Bromium HamAxe"); // Use this if not using localization for names

            // For Journey Mode research
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32; // Item sprite width
            Item.height = 28; // Item sprite height

            // Use properties for swinging tools
            Item.useStyle = ItemUseStyleID.Swing; // The item swings
            Item.useTime = 15; // How fast the item is used
            Item.useAnimation = 15; // The animation time

            Item.autoReuse = true; // Can hold down to use
            Item.useTurn = true; // Player can turn while using

            // Set axe and hammer power
            // Adjust these values based on your desired tier relative to vanilla tools
            // 110% axe power is quite high, 79% hammer power is moderate. Adjust as needed for balance.
            Item.axe = 25; // Axe power
            Item.hammer = 79; // Hammer power

            // Set weapon properties (Hamaxes can also be used as melee weapons)
            Item.damage = 20; // Moderate melee damage for its tier
            Item.knockBack = 5f; // Moderate knockback
            Item.crit = 6; // Standard critical strike chance

            // Change 'DamageClass' to 'DamageType'
            Item.DamageType = DamageClass.Melee; // Specify that this item deals melee damage

            // Set value and rarity
            Item.value = Item.sellPrice(gold: 2); // Example value: 2 gold coins
            Item.rare = ItemRarityID.Orange; // Match Bromium armor rarity

            // Set sound played when used
            Item.UseSound = SoundID.Item1; // Standard swing sound
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            // Requires Bromium Bars to craft
            // Adjust the number of bars as needed for balance
            recipe.AddIngredient(ItemType<BromiumBar>(), 12); // Example: Requires 12 Bromium Bars
            recipe.AddIngredient(ItemID.MoltenHamaxe, 1); // Requires a Molten Hamaxe as a base 
            recipe.AddTile(TileID.Anvils); // Crafted at an Anvil (or a different crafting station if desired)
            recipe.Register();
        }

        // You can add other overrides here, like ModifyTooltips if you want custom tooltip lines
        // This method requires 'using System.Collections.Generic;' which is now added.
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Example: Add a custom line
            tooltips.Add(new TooltipLine(Mod, "BromiumHamAxeBonus", "Has a shiny finish!"));
        }
    }
}
