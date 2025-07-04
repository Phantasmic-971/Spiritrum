using Microsoft.Xna.Framework; 
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Spiritrum.Content.Tiles
{
    public class ReborniumBarTile : ModTile
    {
        public override void SetStaticDefaults() {
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            HitSound = SoundID.Tink;

            AddMapEntry(new Color(100, 220, 100), Language.GetText("MapObject.MetalBar")); // Light green color for Rebornium
        }

        public override void AnimateTile(ref int frame, ref int frameCounter) {
            frameCounter++;
            if (frameCounter > 5) {
                frameCounter = 0;
                frame++;
                if (frame > 3) {
                    frame = 0;
                }
            }
        }
    }
}
