using Microsoft.Xna.Framework; // For Color
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace Spiritrum.Content.Tiles
{
    public class BromiumOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Basic tile properties
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;            // Add the map entry with a custom name
            var name = CreateMapEntryName();
            // name.SetDefault("Bromium Ore");
            AddMapEntry(new Color(204, 184, 0), name); // Slightly dark yellow color

            // Mining and drop settings
            DustType = DustID.Stone;
            MinPick = 65;

            // If you want to manually specify the item drop, uncomment this:
            // ItemDrop = ModContent.ItemType<Items.Placeable.BromiumOre>();
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
