using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.MagnoliaAddon.Walls;

namespace Spiritrum.MagnoliaAddon.Items.Placeables
{
    public class WhitePicketFence : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 400;
        }

        public override void SetDefaults()
        {
            // ModContent.WallType<Walls.ExampleWall>() retrieves the id of the wall that this item should place when used.
            // DefaultToPlaceableWall handles setting various Item values that placeable wall items use.
            // Hover over DefaultToPlaceableWall in Visual Studio to read the documentation!
            Item.DefaultToPlaceableWall(ModContent.WallType<WhitePicketFencePlaced>());
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ItemID.Wood, 1);
            recipe.Register();
        }
    }
}
