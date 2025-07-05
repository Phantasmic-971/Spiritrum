using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
    public class ElementalRevolver : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elemental Revolver");
            Item.ResearchUnlockCount = 1;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add tooltips
            tooltips.Add(new TooltipLine(Mod, "ElementalRevolverEffect1", "25% chance to fire a Frozo Flake, a Flamelash or a Heat Ray"));
            tooltips.Add(new TooltipLine(Mod, "ElementalRevolverEffect2", "25% chance to fire all 3"));
            
            // Color the tooltip lines
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "ElementalRevolverEffect1" || line.Name == "ElementalRevolverEffect2")
                {
                    line.OverrideColor = new Color(255, 210, 120); // Golden color for effect tooltips
                }
            }
        }

        public override void SetDefaults()
        {
            // General weapon properties
            Item.width = 32;
            Item.height = 28;
            Item.rare = ItemRarityID.Yellow; // Post-Golem rarity (Yellow)
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.scale = 0.5f;

            // Weapon classification
            Item.DamageType = DamageClass.Magic;
            Item.damage = 65;
            Item.knockBack = 3f;
            Item.crit = 6;

            // Usage properties
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noMelee = true;
    
            
            // Magic weapon properties
            Item.mana = 8; // Mana cost per use
            
            // Shooting properties
            Item.shoot = ProjectileID.PurificationPowder; // Default projectile, will be overridden in Shoot method
            Item.shootSpeed = 15f;
            
            // Sound
            Item.UseSound = SoundID.Item41; // Revolver/gun sound
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Randomize projectile selection
            int projectileRoll = Main.rand.Next(100);
            
            // Get the adjusted position for projectile spawn
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 30f;
            if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
            {
                muzzleOffset.Y *= 0.5f; // Reduces the offset's Y component if shooting mostly horizontally
            }
            else
            {
                muzzleOffset.X *= 0.5f; // Reduces the offset's X component if shooting mostly vertically
            }
            position += muzzleOffset;
            
            if (projectileRoll < 25) // 25% chance for FrozoFlake
            {
                int proj = Projectile.NewProjectile(source, position, velocity, ProjectileType<FrozoFlake>(), damage, knockback, player.whoAmI);
                // Custom behavior for FrozoFlake if needed
                Main.projectile[proj].friendly = true;
                Main.projectile[proj].hostile = false;
            }
            else if (projectileRoll < 50) // 25% chance for Heat Ray
            {
                Projectile.NewProjectile(source, position, velocity, ProjectileID.HeatRay, damage, knockback, player.whoAmI);
            }
            else if (projectileRoll < 75) // 25% chance for Flamelash
            {
                Vector2 targetPos = Main.MouseWorld;
                Vector2 direction = targetPos - position;
                direction.Normalize();
                direction *= velocity.Length(); // Maintain original speed
                Projectile.NewProjectile(source, position, direction, ProjectileID.Flamelash, damage, knockback, player.whoAmI);
            }
            else // 25% chance to fire all three
            {
                // Spawn FrozoFlake
                int proj1 = Projectile.NewProjectile(source, position, velocity, ProjectileType<FrozoFlake>(), damage, knockback, player.whoAmI);
                Main.projectile[proj1].friendly = true;
                Main.projectile[proj1].hostile = false;
                
                // Spawn Heat Ray
                Projectile.NewProjectile(source, position, velocity, ProjectileID.HeatRay, damage, knockback, player.whoAmI);
                
                // Spawn Flamelash
                Vector2 targetPos = Main.MouseWorld;
                Vector2 direction = targetPos - position;
                direction.Normalize();
                direction *= velocity.Length(); // Maintain original speed
                Projectile.NewProjectile(source, position, direction, ProjectileID.Flamelash, damage, knockback, player.whoAmI);
            }
            
            // Return false to prevent the vanilla projectile from spawning
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Revolver, 1) // Revolver component
                .AddIngredient(ItemID.HeatRay, 1) // Heat Ray component
                .AddIngredient(ItemID.Flamelash, 1) // Flamelash component
                .AddIngredient(ItemType<Placeables.FrozoniteBar>(), 10) // Frozonite bars
                .AddTile(TileID.MythrilAnvil) // Crafted at Mythril/Orichalcum Anvil
                .Register();
        }
        
        // Add a visual effect for the muzzle flash
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 2);
        }
    }
}
