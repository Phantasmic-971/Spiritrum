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
    public class AzuriteHat : ModItem
    {
        public static readonly int MaxMinionIncrease = 1;

        public override void SetStaticDefaults()
        {
            // No SetDefault calls needed in modern tModLoader
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += MaxMinionIncrease; // Increase how many minions the player can have by one
        }

        public override void SetDefaults()
        {
            Item.width = 32; // Width of the item
            Item.height = 28; // Height of the item
            Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
            Item.rare = ItemRarityID.Orange; // The rarity of the item
            Item.defense = 5; // The amount of defense the item will give when equipped
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add tooltip for minion increase
            TooltipLine minionLine = new TooltipLine(Mod, "AzuriteHatMinion", "+1 Minion Slots")
            {
                IsModifier = true
            };
            tooltips.Add(minionLine);
        }
        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<AzuritePlatemail>() && legs.type == ItemType<AzuriteGreaves>();
        }
        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<AzuriteBar>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            // Set the tooltip displayed below the armor items directly
            player.setBonus = "+10% magic and summon damage";

            // Apply the actual bonus: +10% magic and summon damage
            player.GetDamage(DamageClass.Magic) += 0.10f;
            player.GetDamage(DamageClass.Summon) += 0.10f;
        }
    }
}

