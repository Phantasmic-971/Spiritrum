using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Spiritrum.Content.Tiles
{
    public class VoidHarbingerTrophyTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.CoordinateHeights = new[] { 30, 30 };
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(80, 0, 120), CreateMapEntryName());
            DustType = DustID.PurpleTorch;
            HitSound = SoundID.Shatter;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(i, j), i * 30, j * 30, 48, 32, ModContent.ItemType<Items.Placeable.VoidHarbingerTrophy>());
        }
    }
}