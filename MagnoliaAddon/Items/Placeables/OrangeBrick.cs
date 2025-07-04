using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.MagnoliaAddon.Tiles.Blocks;

namespace Spiritrum.MagnoliaAddon.Items.Placeables
{
    public class OrangeBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;

            // Mods can be translated to any of the languages tModLoader supports. See https://github.com/tModLoader/tModLoader/wiki/Localization
            // Translations go in localization files (.hjson files), but these are listed here as an example to help modders become aware of the possibility that users might want to use your mod in other lauguages:
            // English: "Example Block", "This is a modded tile."
            // German: "Beispielblock", "Dies ist ein modded Block"
            // Italian: "Blocco di esempio", "Questo è un blocco moddato"
            // French: "Bloc d'exemple", "C'est un bloc modgé"
            // Spanish: "Bloque de ejemplo", "Este es un bloque modded"
            // Russian: "???? ???????", "??? ???????????????? ????"
            // Chinese: "???", "???????"
            // Portuguese: "Bloco de exemplo", "Este é um bloco modded"
            // Polish: "Przykladowy blok", "Jest to modded blok"
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<OrangeBrickPlaced>());
            Item.width = 12;
            Item.height = 12;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient<Items.Other.FireDiamond>(1);
            recipe.AddIngredient(ItemID.StoneBlock, 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}

