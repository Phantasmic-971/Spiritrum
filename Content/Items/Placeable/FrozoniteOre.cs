using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for TileType
using Spiritrum.Content.Tiles; // Needed for TileType<Tiles.ReborniumOre>
using System.Collections.Generic; // Needed for List<TooltipLine>

namespace Spiritrum.Content.Items.Placeable
{
    public class FrozoniteOre : ModItem
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 100; // For Journey Mode research
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58; // Sets sorting priority in inventory
        }

        public override void SetDefaults() {
            // Links this item to the ReborniumOre tile
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.FrozoniteOreTile>());
            Item.width = 12; // Item sprite width
            Item.height = 12; // Item sprite height
            Item.value = 3000; // Item sell value (3000 copper = 30 silver)
            Item.rare = ItemRarityID.Cyan; // Common rarity as it's a pre-boss ore
            Item.createTile = ModContent.TileType<Tiles.FrozoniteOreTile>();
        }
    }
}
