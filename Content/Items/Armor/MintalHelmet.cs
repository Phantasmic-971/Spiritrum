using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class MintalHelmet : ModItem
    {
        public static readonly int RangedDamageBonus = 12;
        public static readonly int RangedCritBonus = 16;

        public override void SetStaticDefaults()
        {
            // No SetDefault calls needed in modern tModLoader
        }

        public override void SetDefaults()
        {
            Item.width = 32; // Width of the item
            Item.height = 28; // Height of the item
            Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
            Item.rare = ItemRarityID.LightRed; // The rarity of the item
            Item.defense = 15; // The amount of defense the item will give when equipped
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add tooltip for crit chance
            TooltipLine critLine = new TooltipLine(Mod, "MintalHelmetCrit", "+16% Ranged Crit Chance")
            {
                IsModifier = true
            };
            tooltips.Add(critLine);
        }
        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<MintalBreastplate>() && legs.type == ItemType<MintalLeggings>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += RangedCritBonus;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Mintal>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void UpdateArmorSet(Player player)
        {
            // Set the tooltip displayed below the armor items directly
            player.setBonus = "Increases ranged damage by 20%, critical strike chance by 8%, and endurance by 7.5%";

            // Apply the actual bonus: +20% ranged damage, +8% crit, +7.5% endurance
            player.GetDamage(DamageClass.Ranged) += 0.20f;
            player.GetCritChance(DamageClass.Ranged) += 8;
            player.endurance += 0.075f;
        }
    }
}
