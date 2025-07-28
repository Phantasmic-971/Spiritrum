using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Placeables
{
    public class CopiumOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Copium Ore");
            // Tooltip.SetDefault("Early ore found on nearly every biome. Looks like tin but weaker and with a white color.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(copper: 10);
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Spiritrum.Content.Tiles.CopiumOre>();
        }
    }
}
