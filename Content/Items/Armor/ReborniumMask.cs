using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Armor
{    [AutoloadEquip(EquipType.Head)]
    public class ReborniumMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip handled in localization
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 20);
            Item.rare = ItemRarityID.White;
            Item.defense = 4; // Pre-boss, slightly better than iron
        }        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20; // Small bonus, fits 'rebirth' theme
        }
        
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<ReborniumChestplate>() && legs.type == ItemType<ReborniumLeggings>();
        }
        
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "5% increased damage and critical strike chance"; // Set bonus description
            player.GetDamage(DamageClass.Generic) += 0.05f; // 5% increased damage
            player.GetCritChance(DamageClass.Generic) += 5f; // 5% increased critical strike chance
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Placeable.ReborniumBar>(), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
