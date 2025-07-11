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
    public class SteelHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // No SetDefault calls needed in modern tModLoader
        }

        public override void SetDefaults()
        {
            Item.width = 32; // Width of the item
            Item.height = 28; // Height of the item
            Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 6; // The amount of defense the item will give when equipped
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add tooltip for steel helmet
            TooltipLine helmetLine = new TooltipLine(Mod, "SteelHelmetDesc", "A sturdy helmet forged from steel")
            {
                IsModifier = false
            };
            tooltips.Add(helmetLine);
        }
        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<SteelBreastplate>() && legs.type == ItemType<SteelLeggings>();
        }
        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldHelmet, 1);
            recipe.AddIngredient(ItemType<SteelBar>(), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumHelmet, 1);
            recipe.AddIngredient(ItemType<SteelBar>(), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateArmorSet(Player player)
        {
            // Set the tooltip displayed below the armor items directly
            player.setBonus = "+5 defense";

            // Apply the actual bonus: +5 defense
            player.statDefense += 5;
        }
    }
}
