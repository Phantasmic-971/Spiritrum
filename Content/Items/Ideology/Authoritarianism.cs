using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Ideology
{
    public class Authoritarianism : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in localization or ModifyTooltips
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(gold: 10);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 12;
            player.endurance += 0.12f;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.fireWalk = true;
            player.noKnockback = true;
            player.GetDamage(DamageClass.Generic) -= 0.10f;
            player.moveSpeed -= 0.10f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "AuthoritarianismTip1", "+12 defense, +12% damage reduction, immune to confusion and silence, no knockback"));
            tooltips.Add(new TooltipLine(Mod, "AuthoritarianismTip2", "-10% damage, -10% movement speed"));
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PaladinsShield);
            recipe.AddIngredient(ItemID.TitanGlove);
            recipe.AddIngredient(ItemID.ObsidianShield);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
