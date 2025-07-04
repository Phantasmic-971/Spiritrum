using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System.Collections.Generic;
using System.Threading;
using Terraria.IO;
using Terraria.Localization;
using Terraria.WorldBuilding;

namespace Spiritrum.MagnoliaAddon.Tiles.Blocks
{
    public class AncientWoodPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;

            DustType = 7;
            HitSound = SoundID.Dig;

            AddMapEntry(new Color(64, 64, 64));
        }
    }
}
