using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for TileType
using Spiritrum.Content.Tiles; // Needed for TileType<Tiles.BromiumOre>

namespace Spiritrum.Content.Items.Placeable // This namespace MUST match the file path
{
    public class BromiumOre : ModItem // This is your item class
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 100; // For Journey Mode research
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58; // Sets sorting priority in inventory
        }
        public override void SetDefaults() {
            // Links this item to the BromiumOre tile
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.BromiumOre>());
            Item.width = 12; // Item sprite width
            Item.height = 12; // Item sprite height
            Item.value = 3000; // Item sell value (Adjust as needed, 3000 copper = 30 silver)
            Item.rare = ItemRarityID.Green; // The rarity of the item (Adjusted to Green as per your code)
            Item.createTile = ModContent.TileType<Tiles.BromiumOre>();
        }
    }
}