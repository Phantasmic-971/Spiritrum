using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Ideology
{
    public class Futurism : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Futurism");
            // Tooltip.SetDefault("'Embrace technological change and progress'\nIncreases movement speed and ranged abilities");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.sellPrice(gold: 2);
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // 15% increased movement speed
            player.moveSpeed += 0.15f;
            
            // 10% increased ranged damage
            player.GetDamage(DamageClass.Ranged) += 0.10f;
            
            // 5% increased ranged crit chance
            player.GetCritChance(DamageClass.Ranged) += 5;
              // 8% increased ranged velocity
            player.GetModPlayer<IdeologySlotPlayer>().rangedVelocity += 0.08f;
            // Visual effect
            if (!hideVisual && Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(
                    player.position,
                    player.width,
                    player.height,
                    DustID.BlueFairy,
                    0f, 0f, 0, default, 1f);
                dust.noGravity = true;
                dust.velocity.Y = -1f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add detailed tooltips about the item's effects
            tooltips.Add(new TooltipLine(Mod, "FuturismMovement", "+15% Movement Speed"));
            tooltips.Add(new TooltipLine(Mod, "FuturismDamage", "+10% Ranged Damage"));
            tooltips.Add(new TooltipLine(Mod, "FuturismCrit", "+5% Ranged Critical Strike Chance"));
            tooltips.Add(new TooltipLine(Mod, "FuturismVelocity", "+8% Projectile Velocity"));
            
            // Add a lore tooltip
            TooltipLine lore = new TooltipLine(Mod, "FuturismLore", "Forward momentum is the only constant")
            {
                OverrideColor = new Color(100, 180, 255)
            };
            tooltips.Add(lore);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Nanites, 20);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.Sapphire, 1); // Blue gemstone represents the future
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
