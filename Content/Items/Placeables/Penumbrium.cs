using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Placeables
{
    public class Penumbrium : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Penumbrium");
            // Tooltip.SetDefault("Dark themed ore with purple colors. Appears after the Wall of Flesh is defeated.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.LightRed;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<Spiritrum.Content.Tiles.Penumbrium>();
        }
    }
}
