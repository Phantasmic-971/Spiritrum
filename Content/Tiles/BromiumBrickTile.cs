using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Spiritrum.Content.Tiles
{
    public class BromiumBrickTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[TileID.Stone][Type] = true;
            Main.tileMerge[TileID.Ebonstone][Type] = true;
            Main.tileMerge[TileID.Crimstone][Type] = true;
            Main.tileMerge[TileID.Pearlstone][Type] = true;
            Main.tileMerge[TileID.Granite][Type] = true;
            Main.tileMerge[TileID.Marble][Type] = true;
            Main.tileBlockLight[Type] = true;

            DustType = DustID.Stone;
            HitSound = SoundID.Tink;

            AddMapEntry(new Color(200, 200, 200));
        }


        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
