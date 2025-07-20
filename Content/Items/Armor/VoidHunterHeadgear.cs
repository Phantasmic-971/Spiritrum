using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Players;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHunterHeadgear : ModItem
    {
        public override void SetStaticDefaults() { }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Red;
            Item.defense = 28; // Average defense for melee
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.18f;
            player.GetCritChance(DamageClass.Melee) += 12f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VoidHunterPlate>() && legs.type == ModContent.ItemType<VoidHunterLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            // Set the tooltip displayed below the armor items directly
            player.setBonus = "15% increased melee speed and 10% increased melee damage";
            // Apply the actual bonus
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
            player.GetDamage(DamageClass.Melee) += 0.10f;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidHunterHeadgearStats", "18% increased melee damage and 12% increased melee critical strike chance"));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 18)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
