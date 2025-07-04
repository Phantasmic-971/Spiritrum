using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Ideology
{
    public class Technocracy : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Technocracy");
            // Tooltip.SetDefault("'Rule by technical experts'\nIncreases intelligence and technical prowess");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 1);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Increases technical abilities:
            // 10% increased summon damage (automation)
            player.GetDamage(DamageClass.Summon) += 0.10f;
            
            // 8% increased magic damage (advanced knowledge)
            player.GetDamage(DamageClass.Magic) += 0.08f;
            
            // Reduce mana cost by 5% (efficiency)
            player.manaCost -= 0.05f;
            
            // 5% increased mining speed (technological progress)
            player.pickSpeed -= 0.05f;
            
            // Visual effect if not hidden
            if (!hideVisual && Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(
                    player.position,
                    player.width,
                    player.height,
                    DustID.Electric,
                    0f, 0f, 0, default, 1f);
                dust.noGravity = true;
                dust.velocity *= 0.1f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add detailed tooltips about the item's effects
            tooltips.Add(new TooltipLine(Mod, "TechnocracyMagic", "+8% Magic Damage"));
            tooltips.Add(new TooltipLine(Mod, "TechnocracySummon", "+10% Summon Damage"));
            tooltips.Add(new TooltipLine(Mod, "TechnocracyMana", "-5% Mana Cost"));
            tooltips.Add(new TooltipLine(Mod, "TechnocracyMining", "+5% Mining Speed"));
            
            // Add a lore tooltip
            TooltipLine lore = new TooltipLine(Mod, "TechnocracyLore", "A society governed by those with technological expertise")
            {
                OverrideColor = new Color(150, 150, 255)
            };
            tooltips.Add(lore);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wire, 20);
            recipe.AddIngredient(ItemID.Cog, 5);
            recipe.AddIngredient(ItemID.Ruby, 1); // Red gemstone represents power
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
