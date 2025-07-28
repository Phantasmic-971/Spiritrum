using Microsoft.Xna.Framework; // For Color
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Tiles
{
    public class FrozoniteOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Basic tile properties
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;            // Add the map entry with a custom name
            var name = CreateMapEntryName();
            // name.SetDefault("Bromium Ore");
            AddMapEntry(new Color(0, 144, 255), name);

            // Mining and drop settings
            DustType = DustID.Stone;
            MinPick = 200;

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
