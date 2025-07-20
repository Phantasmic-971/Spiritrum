using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Spiritrum.Players;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHunterMask : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 12);
            Item.rare = ItemRarityID.Red;
            Item.defense = 26; // Average defense for ranged
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.18f;
            player.GetCritChance(DamageClass.Ranged) += 12f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VoidHunterPlate>() && legs.type == ModContent.ItemType<VoidHunterLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            // Set the tooltip displayed below the armor items directly
            player.setBonus = "10% increased ranged crit chance, 10% increased ranged damage";
            // Apply the actual bonus
            player.GetCritChance(DamageClass.Ranged) += 10f;
            player.GetDamage(DamageClass.Ranged) += 0.10f;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidHunterMaskStats", "18% increased ranged damage and 12% increased ranged critical strike chance"));
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
