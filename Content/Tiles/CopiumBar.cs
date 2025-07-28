using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;

namespace Spiritrum.Content.Tiles
{
    public class CopiumBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(200, 200, 200), Terraria.Localization.Language.GetText("CopiumBar"));
            DustType = DustID.Platinum;
            RegisterItemDrop(ModContent.ItemType<Items.Placeables.CopiumBar>());
        }
    }
}
