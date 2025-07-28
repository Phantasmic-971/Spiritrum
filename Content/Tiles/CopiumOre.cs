using Terraria.ModLoader;
using Terraria.ID;
using Terraria.ObjectData;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Spiritrum.Content.Tiles
{
    public class CopiumOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileOreFinderPriority[Type] = 200;
            TileID.Sets.Ore[Type] = true;
            AddMapEntry(new Color(230, 230, 230), Language.GetText("Copium Ore"));
            DustType = DustID.Silver;
            RegisterItemDrop(ModContent.ItemType<Items.Placeables.CopiumOre>());
            MinPick = 0;
        }
    }
}
