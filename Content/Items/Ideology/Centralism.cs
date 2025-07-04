using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Ideology
{
    public class Centralism : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip is handled in the localization file.
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(30, 1)); // Link to Centralism.gif
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Cyan; // Post-Golem rarity
            Item.value = Item.buyPrice(gold: 25);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 8; // Combined defense boost
            player.endurance += 0.1f; // Combined damage reduction
            player.lifeRegen += 3; // Combined life regeneration
            player.moveSpeed += 0.15f; // Combined movement speed boost
            player.GetDamage(DamageClass.Generic) += 0.1f; // Combined damage boost
            player.GetCritChance(DamageClass.Generic) += 5; // Combined critical strike chance

            foreach (Player ally in Main.player)
            {
                if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
                {
                    ally.statDefense += 2; // Small defense boost for allies
                    ally.lifeRegen += 1; // Small life regeneration boost for allies
                    ally.GetDamage(DamageClass.Generic) += 0.03f; // Small damage boost for allies
                    ally.GetCritChance(DamageClass.Generic) += 3; // Small critical strike chance bonus for allies
                    ally.moveSpeed += 0.05f; // Small movement speed boost for allies
                    ally.endurance += 0.02f; // Small damage reduction for allies
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Socialism>(), 1); // Requires Socialism accessory
            recipe.AddIngredient(ModContent.ItemType<Conservatism>(), 1); // Requires Conservatism accessory
            recipe.AddIngredient(ModContent.ItemType<Liberalism>(), 1); // Requires Liberalism accessory
            recipe.AddTile(TileID.LunarCraftingStation); // Crafted at the Ancient Manipulator
            recipe.Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line1 = new TooltipLine(Mod, "CentralismTip1", "+8 defense, +10% damage reduction, +3 life regen, +15% movement speed");
            tooltips.Add(line1);

            TooltipLine line2 = new TooltipLine(Mod, "CentralismTip2", "+10% damage, +5% crit chance");
            tooltips.Add(line2);

            TooltipLine line3 = new TooltipLine(Mod, "CentralismTip3", "Nearby allies: +2 defense, +1 life regen, +3% damage, +3% crit, +5% speed, +2% damage reduction");
            tooltips.Add(line3);
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item)
                return true;
            return false;
        }
    }
}
