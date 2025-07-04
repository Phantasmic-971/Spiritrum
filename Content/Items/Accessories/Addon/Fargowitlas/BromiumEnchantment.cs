using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.Creative;
using System.Linq;
using Spiritrum.Content.Projectiles;
using Spiritrum.Content.Items;
using Spiritrum.Content.Items.Weapons;
using Spiritrum.Content.Items.Modes;

namespace Spiritrum.Content.Items.Accessories.Addon.Fargowitlas
{
    public class BromiumEnchantment : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bromium Enchantment");
            // Tooltip.SetDefault("Spawns bouncing bromium circles after being hit.\n" +
            //     "When paired with the Wizard Enchantment, spawns 4 bromium circles instead and they deal 150% more damage.");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 10);
        }
        
            public override void AddRecipes()
            {
                Recipe recipe = CreateRecipe(); // Makes 1 at a time
                recipe.AddIngredient(ModContent.ItemType<BromiumHelmet>(), 1);
                recipe.AddIngredient(ModContent.ItemType<BromiumChestplate>(), 1);
                recipe.AddIngredient(ModContent.ItemType<BromiumLeggings>(), 1);
                recipe.AddIngredient(ModContent.ItemType<BromiumSlayer>(), 1);
                recipe.AddIngredient(ModContent.ItemType<BromeBlaster>(), 1);
                recipe.AddIngredient(ModContent.ItemType<BromeMode>(), 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
                }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BromeCirclePlayer>().EnableBromeCircleAccessory = true;
        }
    }    public class BromeCirclePlayer : ModPlayer
    {
        public bool EnableBromeCircleAccessory;
        
        public override void ResetEffects()
        {
            EnableBromeCircleAccessory = false;
        }        public override void OnHurt(Player.HurtInfo info)
        {
            if (EnableBromeCircleAccessory)
            {
                SpawnBromeCircles(Player);
            }
        }
        
        private void SpawnBromeCircles(Player player)
        {
            int circleCount = 2;
            float damageMultiplier = 1f;

            // Check for Bromium Enchantment effect
            if (player.armor.Any(item => item.type == ModContent.ItemType<BromiumEnchantment>()))
            {
                circleCount = 4;
                damageMultiplier = 1.5f;
            }

            for (int i = 0; i < circleCount; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                int damage = (int)(35 * damageMultiplier);
                
                int proj = Projectile.NewProjectile(
                    Entity.GetSource_FromThis(),
                    player.Center,
                    velocity,
                    ModContent.ProjectileType<BromiumCircle>(),
                    damage,
                    1f,
                    player.whoAmI);
                    }
            
            }  
        }
    }
