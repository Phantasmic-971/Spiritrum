using Microsoft.Xna.Framework; // For Color
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Spiritrum.Content.Tiles
{
    public class ReborniumOreTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Basic tile properties
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;            // Add the map entry with a custom name
            var name = CreateMapEntryName();
            AddMapEntry(new Color(0, 255, 0), name); // Bright lime green color for Rebornium

            // Mining and drop settings
            DustType = DustID.Stone;
            MinPick = 45; // 45% pickaxe power - Silver or better

            // If you want to manually specify the item drop, uncomment this:
            // ItemDrop = ModContent.ItemType<Items.Placeable.ReborniumOre>();
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!fail)
            {
                base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
            }
        }
    }
}
