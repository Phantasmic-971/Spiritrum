using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Players;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHunterVisor : ModItem
    {
        public override void SetStaticDefaults() { }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Red;
            Item.defense = 24; // Average defense for summon
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.18f;
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VoidHunterPlate>() && legs.type == ModContent.ItemType<VoidHunterLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            // Set the tooltip displayed below the armor items directly
            player.setBonus = "+2 max minions and 10% increased summon damage";
            // Apply the actual bonus
            player.maxMinions += 2;
            player.GetDamage(DamageClass.Summon) += 0.10f;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidHunterVisorStats", "18% increased summon damage and +1 max minions"));
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
