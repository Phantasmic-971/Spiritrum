using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Terraria.IO;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Systems
{
    public class ReborniumOreSystem : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genPass => genPass.Name.Equals("Shinies"));

            if (shiniesIndex != -1)
            {
                tasks.Insert(shiniesIndex + 1, new PassLegacy("Rebornium Ore", (progress, configuration) =>
                {
                    progress.Message = "Making the ground reborn"; // Custom message when generating                    // We want this ore to be at higher elevations than Bromium but rarer
                    // For pre-boss content that requires silver+ pickaxe to mine
                    int spawnMinY = (int)(Main.worldSurface * 1.4); // Below surface
                    int spawnMaxY = (int)(Main.worldSurface + 400); // Into the cavern layer

                    // Making it rarer - approximately half as common
                    int attempts = (int)((Main.maxTilesX * Main.maxTilesY) * 0.00009);

                    for (int i = 0; i < attempts; i++)
                    {
                        int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                        int y = WorldGen.genRand.Next(spawnMinY, spawnMaxY);                        WorldGen.TileRunner(
                            x,
                            y,
                            WorldGen.genRand.Next(5, 6), // Smaller veins to make it rarer
                            WorldGen.genRand.Next(5, 8), // Fewer steps for shorter veins
                            TileType<Content.Tiles.ReborniumOreTile>(),
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
