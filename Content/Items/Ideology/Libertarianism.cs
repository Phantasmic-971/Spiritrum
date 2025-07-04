using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Ideology
{
    public class Libertarianism : ModItem
    {
        public override void SetStaticDefaults() { }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 8);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.12f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f; // +10% melee speed
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.statDefense -= 6;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "LibertarianismTip1", "+12% movement speed, +10% melee speed, immune to slow/confuse"));
            tooltips.Add(new TooltipLine(Mod, "LibertarianismTip2", "-6 defense"));
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FeralClaws);
            recipe.AddIngredient(ItemID.Aglet);
            recipe.AddIngredient(ItemID.HermesBoots);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
