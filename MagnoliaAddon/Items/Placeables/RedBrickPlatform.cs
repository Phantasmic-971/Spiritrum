using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.MagnoliaAddon.Tiles.Furniture;

namespace Spiritrum.MagnoliaAddon.Items.Placeables
{
    public class RedBrickPlatform : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 200;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<RedBrickPlatformPlaced>());
            Item.width = 8;
            Item.height = 10;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(ItemID.RedBrick, 1);
            recipe.Register();
        }
    }
}
