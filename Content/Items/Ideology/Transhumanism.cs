using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Spiritrum.Players;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures; // Add this for MeleeSpeed

namespace Spiritrum.Content.Items.Ideology
{
    public class Transhumanism : ModItem
    {
        public override void SetStaticDefaults() { }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.buyPrice(gold: 20);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Celestial Stone-like buffs
            player.statLifeMax2 += 40;
            player.statManaMax2 += 40;
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 5;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            // Celestial Stone effects
            player.statDefense += 4;
            player.endurance += 0.10f;
            player.lifeRegen += 2;
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
            player.moveSpeed += 0.10f;
            player.jumpSpeedBoost += 2f;
            player.nightVision = true;
            player.pickSpeed -= 0.15f;
            player.accMerman = true;
            player.accDivingHelm = true;
            player.lavaImmune = true;
            player.fireWalk = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "TranshumanismTip1", "+40 max life/mana"));
            tooltips.Add(new TooltipLine(Mod, "TranshumanismTip2", "+10% all damage, +5% crit"));
            tooltips.Add(new TooltipLine(Mod, "TranshumanismTip3", "+4 defense, +10% damage reduction"));
            tooltips.Add(new TooltipLine(Mod, "TranshumanismTip4", "+2 life regen, +10% melee speed, +10% move speed, +2 jump speed"));
            tooltips.Add(new TooltipLine(Mod, "TranshumanismTip5", "Night vision, water breathing, lava/fire block immunity"));
            tooltips.Add(new TooltipLine(Mod, "TranshumanismTip6", "Immune to poison, venom, slow, and confusion"));
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CelestialShell);
            recipe.AddIngredient(ItemID.Nanites, 25);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
