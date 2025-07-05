using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for TileType<T>()

namespace Spiritrum.Content.Items.Placeables
{
    public class FrozoniteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Optional: Display name and tooltip can go in localization .hjson files
            Item.ResearchUnlockCount = 25; // For Journey Mode research
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(silver: 25); // Worth 25 silver (half of Bromium)
            Item.rare = ItemRarityID.Cyan; // Common rarity as it's a pre-boss material
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrozoniteOre>(), 4); // Requires 3 Rebornium Ore
            recipe.AddTile(TileID.Furnaces); // Crafted at a basic Furnace (pre-boss)
            recipe.Register();
        }
    }
}
