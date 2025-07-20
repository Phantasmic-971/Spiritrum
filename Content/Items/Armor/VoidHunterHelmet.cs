using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Players;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHunterHelmet : ModItem
    {
        public override void SetStaticDefaults() { }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Red;
            Item.defense = 25; // Average defense for magic
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.18f;
            player.GetCritChance(DamageClass.Magic) += 12f;
            player.manaCost -= 0.10f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VoidHunterPlate>() && legs.type == ModContent.ItemType<VoidHunterLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            // Set the tooltip displayed below the armor items directly
            player.setBonus = "12% increased magic damage and 12% reduced mana cost";
            // Apply the actual bonus
            player.GetDamage(DamageClass.Magic) += 0.12f;
            player.manaCost -= 0.12f;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidHunterHelmetStats", "18% increased magic damage, 12% increased magic critical strike chance, and 10% reduced mana cost"));
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
