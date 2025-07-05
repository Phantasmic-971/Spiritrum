using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Armor
{    [AutoloadEquip(EquipType.Head)]
    public class FrozoniteHeadgear : ModItem
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
            Item.rare = ItemRarityID.Cyan;
            Item.defense = 16; // Pre-boss, slightly better than iron
        }        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 4;
        }
        
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<FrozoniteChestplate>() && legs.type == ItemType<FrozoniteSkates>();
        }
        
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% chance to not consume ammo, +8% crit chance"; // Set bonus description
            player.setBonus = "+8% damage, +40 mana and +1 summon slot";
            player.GetDamage(DamageClass.Generic) += 0.08f; // 5% increased damage
            player.GetCritChance(DamageClass.Generic) += 8f; // 5% increased critical strike chance
            player.maxMinions += 1;
            player.statManaMax2 += 40;
            player.ammoBox = true; // 20% chance to not consume ammo
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Placeables.FrozoniteBar>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
