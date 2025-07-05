using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FrozoniteChestplate : ModItem
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
            Item.rare = ItemRarityID.Cyan;
            Item.defense = 18; // Pre-boss, slightly better than iron
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Placeables.FrozoniteBar>(), 18);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
