using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;

namespace Spiritrum.Systems
{
    public class PenumbriumWorldGenSystem : ModSystem
    {
    // No hooks needed here; call GeneratePenumbriumOre from a GlobalNPC

    public void GeneratePenumbriumOre()
        {
            int amount = (int)((Main.maxTilesX * Main.maxTilesY) * 0.00008f); // Less than Copium, post-WoF
            for (int i = 0; i < amount; i++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 400); // Underground and Cavern
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(1, 5), WorldGen.genRand.Next(1, 4), ModContent.TileType<Spiritrum.Content.Tiles.Penumbrium>());
            }
        }
    }
}
