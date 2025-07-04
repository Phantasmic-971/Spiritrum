using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Localization;

namespace Spiritrum.MagnoliaAddon.Tiles.Blocks
{
    public class FireDiamondStoneBlockPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[TileID.Stone][Type] = true;
            Main.tileMerge[TileID.Ebonstone][Type] = true;
            Main.tileMerge[TileID.Crimstone][Type] = true;
            Main.tileMerge[TileID.Pearlstone][Type] = true;
            Main.tileBlockLight[Type] = true;

            AddMapEntry(new Color(101, 101, 101), Language.GetText("Fire Diamond")); // localized text for "Metal Bar"

            DustType = DustID.Stone;
            HitSound = SoundID.Tink;

        }


        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
