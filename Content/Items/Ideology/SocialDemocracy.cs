using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Spiritrum.Players;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Ideology
{
    public class SocialDemocracy : ModItem
    {
        public override void SetStaticDefaults() { }
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
            player.statLifeMax2 += 20;
            player.lifeRegen += 2;
            player.GetDamage(DamageClass.Generic) += 0.06f;
            player.endurance += 0.06f;
            player.GetModPlayer<IdeologySlotPlayer>().shareBuff = true;

            // Apply buffs to nearby allies
            foreach (Player ally in Main.player)
            {
                if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
                {
                    ally.lifeRegen += 1; // +1 life regen
                    ally.endurance += 0.02f; // +2% endurance
                }
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SocialDemocracyTip1", "+20 max life, +2 life regen, +6% damage, +6% damage reduction"));
            tooltips.Add(new TooltipLine(Mod, "SocialDemocracyTip2", "Nearby allies gain +1 regen and +2% endurance"));
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BandofRegeneration);
            recipe.AddIngredient(ItemID.CrossNecklace);
            recipe.AddIngredient(ItemID.StarVeil);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
