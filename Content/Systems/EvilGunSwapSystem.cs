using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Spiritrum.Content.Systems
{
    public class EvilGunSwapSystem : ModSystem
    {
        public override void AddRecipes()
        {
            // Musket to Undertaker
            Recipe musketToUndertaker = Recipe.Create(ItemID.TheUndertaker)
                .AddIngredient(ItemID.Musket)
                .AddTile(TileID.DemonAltar)
                .Register();

            // Undertaker to Musket
            Recipe undertakerToMusket = Recipe.Create(ItemID.Musket)
                .AddIngredient(ItemID.TheUndertaker)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}
