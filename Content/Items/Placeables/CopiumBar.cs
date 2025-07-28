using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Tiles;

namespace Spiritrum.Content.Items.Placeables
{
    public class CopiumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Optional: DisplayName.SetDefault("Copium Bar");
            // Optional: Tooltip.SetDefault("A weak, pale metal bar");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(silver: 1);
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Tiles.CopiumBar>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CopiumOre>(), 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}