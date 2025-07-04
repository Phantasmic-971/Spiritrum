using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for TileType
using Spiritrum.Content.Tiles; // Needed for TileType<Tiles.ReborniumOre>
using System.Collections.Generic; // Needed for List<TooltipLine>

namespace Spiritrum.Content.Items.Placeable
{
    public class ReborniumOre : ModItem
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 100; // For Journey Mode research
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58; // Sets sorting priority in inventory
        }

        public override void SetDefaults() {
            // Links this item to the ReborniumOre tile
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ReborniumOreTile>());
            Item.width = 12; // Item sprite width
            Item.height = 12; // Item sprite height
            Item.value = 3000; // Item sell value (3000 copper = 30 silver)
            Item.rare = ItemRarityID.White; // Common rarity as it's a pre-boss ore
            Item.createTile = ModContent.TileType<Tiles.ReborniumOreTile>();
        }
        // No recipes needed for the ore itself

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ReborniumTreacherCollins1", "A common mineral dedicated to those with Treacher Collins Syndrome"));
            tooltips.Add(new TooltipLine(Mod, "ReborniumTreacherCollins2", "A rare genetic condition affecting facial development"));
            tooltips.Add(new TooltipLine(Mod, "ReborniumTreacherCollins3", "Causing distinctive facial features but not affecting intelligence"));
            tooltips.Add(new TooltipLine(Mod, "ReborniumTreacherCollins4", "The Terrarian is strong enough to not be affected by this condition"));
        }
    }
}
