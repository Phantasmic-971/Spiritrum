using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Terraria.IO;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Systems
{
    public class BromiumOreSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genPass => genPass.Name.Equals("Shinies"));

            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, new PassLegacy("Bromium Ore", (progress, configuration) =>
                {
                    progress.Message = "Adding a French Mineral";

                    int spawnMinY = Main.maxTilesY - 350;
                    int spawnMaxY = Main.maxTilesY - 100;

                    int attempts = (int)((Main.maxTilesX * Main.maxTilesY) * 0.00006); // Much less frequent spawn

                    for (int i = 0; i < attempts; i++)
                    {
                        int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                        int y = WorldGen.genRand.Next(spawnMinY, spawnMaxY);

                        WorldGen.TileRunner(
                            x,
                            y,
                            WorldGen.genRand.Next(4, 6), // Slightly more consistent size
                            WorldGen.genRand.Next(4, 6), // Longer vein steps
                            TileType<Content.Tiles.BromiumOre>(),
                            false,
                            0f,
                            0f,
                            false,
                            true
                        );
                    }
                }));
            }
        }
    }
}
