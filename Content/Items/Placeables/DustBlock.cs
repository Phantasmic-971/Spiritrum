using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.MagnoliaAddon.Tiles.Blocks;

namespace Spiritrum.Content.Items.Placeables
{
    public class DustBlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;

            // Mods can be translated to any of the languages tModLoader supports. See https://github.com/tModLoader/tModLoader/wiki/Localization
            // Translations go in localization files (.hjson files), but these are listed here as an example to help modders become aware of the possibility that users might want to use your mod in other lauguages:
            // English: "Example Block", "This is a modded tile."
            // German: "Beispielblock", "Dies ist ein modded Block"
            // Italian: "Blocco di esempio", "Questo � un blocco moddato"
            // French: "Bloc d'exemple", "C'est un bloc modg�"
            // Spanish: "Bloque de ejemplo", "Este es un bloque modded"
            // Russian: "???? ???????", "??? ???????????????? ????"
            // Chinese: "???", "???????"
            // Portuguese: "Bloco de exemplo", "Este � um bloco modded"
            // Polish: "Przykladowy blok", "Jest to modded blok"
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<DustBlockPlaced>());
            Item.width = 12;
            Item.height = 12;

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            // Replaced missing Sawdust with vanilla dust (Item ID 16)
            recipe.AddIngredient(ItemID.Cobweb, 10);
            recipe.Register();
        }
    }
}

