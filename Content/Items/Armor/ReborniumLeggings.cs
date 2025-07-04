using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class ReborniumLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip handled in localization
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 25);
            Item.rare = ItemRarityID.White;
            Item.defense = 5; // Pre-boss, slightly better than iron
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.08f; // 8% movement speed
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Placeable.ReborniumBar>(), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
