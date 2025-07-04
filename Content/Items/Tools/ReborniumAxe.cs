using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for ItemType
using System.Collections.Generic; // Needed for List<T>
using Spiritrum.Content.Items.Placeable;

namespace Spiritrum.Content.Items.Tools // Recommended namespace for weapons/tools
{
    public class ReborniumAxe : ModItem
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
            Item.axe = 12; // Axe power

            // Set weapon properties (Hamaxes can also be used as melee weapons)
            Item.damage = 8; // Moderate melee damage for its tier
            Item.knockBack = 3f; // Moderate knockback

            // Change 'DamageClass' to 'DamageType'
            Item.DamageType = DamageClass.Melee; // Specify that this item deals melee damage

            // Set value and rarity
            Item.value = Item.sellPrice(gold: 2); // Example value: 2 gold coins
            Item.rare = ItemRarityID.White; // Match Bromium armor rarity

            // Set sound played when used
            Item.UseSound = SoundID.Item1; // Standard swing sound
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<ReborniumBar>(), 10); // Example: Requires 12 Bromium Bars
            recipe.AddTile(TileID.Anvils); // Crafted at an Anvil (or a different crafting station if desired)
            recipe.Register();
        }
    }
}
