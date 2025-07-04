using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class ReborniumChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip handled in localization
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ItemRarityID.White;
            Item.defense = 5; // Pre-boss, slightly better than iron
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10; // Small bonus, fits 'rebirth' theme
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Placeable.ReborniumBar>(), 18);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
