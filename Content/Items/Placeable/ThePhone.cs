using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Tiles;

namespace Spiritrum.Content.Items.Placeable
{
    public class ThePhone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip should be set in localization files
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 5);
            Item.rare = ItemRarityID.White;
            Item.createTile = ModContent.TileType<ThePhoneTile>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 3)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
