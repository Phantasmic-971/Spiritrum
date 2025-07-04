using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Systems
{
    public class BiomeSwapSystem : ModSystem
    {
        public override void PostUpdateInput()
        {
            if (Main.dedServ || !Main.hasFocus) return;
            Player player = Main.LocalPlayer;

            // Check if player is near a Demon/Crimson Altar
            Point tilePos = player.Center.ToTileCoordinates();
            for (int x = -2; x <= 2; x++)
            for (int y = -2; y <= 2; y++)
            {
                int checkX = tilePos.X + x;
                int checkY = tilePos.Y + y;
                if (!WorldGen.InWorld(checkX, checkY)) continue;
                int type = Main.tile[checkX, checkY].TileType;
                if (type == TileID.DemonAltar || type == TileID.DemonAltar)
                {
                    // If player right-clicks with a seed in hand, swap it
                    if (Main.mouseRight && Main.mouseRightRelease)
                    {
                        var item = player.HeldItem;
                        if (item.type == ItemID.CorruptSeeds)
                        {
                            item.SetDefaults(ItemID.CrimsonSeeds);
                            Main.NewText("Corruption Seed swapped for Crimson Seed!");
                        }
                        else if (item.type == ItemID.CrimsonSeeds)
                        {
                            item.SetDefaults(ItemID.CorruptSeeds);
                            Main.NewText("Crimson Seed swapped for Corruption Seed!");
                        }
                    }
                }
            }
        }
    }
}
