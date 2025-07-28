using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace Spiritrum.Systems
{
    public class CopiumOreWorldGenSystem : ModSystem
    {
        public override void PostWorldGen()
        {
            int amount = (int)((Main.maxTilesX * Main.maxTilesY) * 0.0005f); // Amount scales with world size
            for (int i = 0; i < amount; i++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200); // Underground and Cavern
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(2, 6), ModContent.TileType<Spiritrum.Content.Tiles.CopiumOre>());
            }
        }
    }
}
