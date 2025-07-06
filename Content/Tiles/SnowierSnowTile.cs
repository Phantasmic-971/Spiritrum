using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Spiritrum.Content.Tiles
{
    public class SnowierSnowTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[TileID.Stone][Type] = true;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            Main.tileMerge[TileID.IceBlock][Type] = true;
            Main.tileMerge[TileID.ClayBlock][Type] = true;
            Main.tileMerge[TileID.Sand][Type] = true;
            Main.tileMerge[TileID.Slush][Type] = true;
            Main.tileBlockLight[Type] = true;

            DustType = DustID.Stone;
            HitSound = SoundID.Item48;

            AddMapEntry(new Color(165, 237, 255));
        }


        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
