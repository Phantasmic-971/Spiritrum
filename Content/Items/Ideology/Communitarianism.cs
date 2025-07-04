using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Spiritrum.Players;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Ideology
{
    public class Communitarianism : ModItem
    {
        public override void SetStaticDefaults() { }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightPurple;
            Item.value = Item.buyPrice(gold: 8);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 30;
            player.lifeRegen += 2;
            player.GetModPlayer<IdeologySlotPlayer>().teamBuff = true;

            // Apply buffs to nearby allies
            foreach (Player ally in Main.player)
            {
                if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
                {
                    ally.statDefense += 2; // +2 defense
                    ally.lifeRegen += 1;   // +1 life regen
                }
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "CommunitarianismTip1", "+30 max life, +2 life regen"));
            tooltips.Add(new TooltipLine(Mod, "CommunitarianismTip2", "Nearby team members gain +1 regen and +2 defense"));
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BandofRegeneration);
            recipe.AddIngredient(ItemID.PanicNecklace);
            recipe.AddIngredient(ItemID.HoneyComb);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
