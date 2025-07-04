using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace Spiritrum.Content.Items.Weapons
{
    public class RoPro9000 : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true; // This makes the item have a staff-like animation
        }

        public override void SetDefaults()
        {
            // Basic item properties
            Item.width = 56;
            Item.height = 64;
            Item.rare = ItemRarityID.LightPurple; // Matches post-Twins rarity (Light Purple)
            Item.value = Item.sellPrice(gold: 5); // Valuable post-mechanical boss weapon

            // Magic weapon properties
            Item.damage = 225; // Strong base damage
            Item.DamageType = DamageClass.Magic;
            Item.mana = 36; // Mana cost per use
            Item.knockBack = 14f; // Strong knockback for crowd control
            Item.crit = -1000; // 15% base critical chance            // Usage properties
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.useTime = 25; // Slow attack speed
            Item.useAnimation = 25;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item20; // Magical explosion sound            // Projectile properties
            Item.shoot = ProjectileID.SandnadoFriendly; // Using this as a base, but we'll customize in Shoot()
            Item.shootSpeed = 0.1f; // Very slow, since we want the explosion to occur close to the player
            Item.noMelee = true; // No melee damage from swinging
            Item.scale = 0.25f;
            
            // Enable cursor targeting
            Item.useTurn = true;
        }        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Create an explosion at the crosshair position
            Vector2 spawnPos = targetPosition; // Use the already calculated target position from HoldItem
            
            // Calculate the explosion radius (5 tiles = 80 pixels, since 1 tile = 16 pixels)
            float explosionRadius = 96f; // 5 tiles
            
            // Main white torch dust explosion - larger quantity for better visual impact
            for (int i = 0; i < 120; i++)
            {
                // Create dust in a circle pattern with radius of explosionRadius
                float dustDistance = Main.rand.NextFloat() * explosionRadius;
                float dustAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                Vector2 dustPosition = spawnPos + new Vector2(
                    (float)Math.Cos(dustAngle) * dustDistance,
                    (float)Math.Sin(dustAngle) * dustDistance
                );
                
                Vector2 speed = new Vector2(
                    (float)Math.Cos(dustAngle),
                    (float)Math.Sin(dustAngle)
                ) * Main.rand.NextFloat(2f, 8f);
                
                Dust dust = Dust.NewDustPerfect(
                    dustPosition, 
                    DustID.WhiteTorch, // White dust as requested
                    speed,
                    0, 
                    Color.White, 
                    Main.rand.NextFloat(1.5f, 2.5f)
                );
                dust.noGravity = true;
                dust.fadeIn = 1.1f;
            }

            // Create additional white dust for visual impact
            for (int i = 0; i < 60; i++)
            {
                float dustDistance = Main.rand.NextFloat() * explosionRadius * 0.8f;
                float dustAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                Vector2 dustPosition = spawnPos + new Vector2(
                    (float)Math.Cos(dustAngle) * dustDistance,
                    (float)Math.Sin(dustAngle) * dustDistance
                );
                
                Vector2 speed = new Vector2(
                    (float)Math.Cos(dustAngle),
                    (float)Math.Sin(dustAngle)
                ) * Main.rand.NextFloat(3f, 10f);
                
                Dust dust = Dust.NewDustPerfect(
                    dustPosition, 
                    DustID.Smoke, // Smoke dust for explosion feel
                    speed * 0.8f,
                    0, 
                    Color.White, 
                    Main.rand.NextFloat(2f, 3f)
                );
                dust.noGravity = true;
                dust.fadeIn = 1.2f;
            }
            
            // Create some bright sparkles throughout the explosion
            for (int i = 0; i < 40; i++)
            {
                float dustDistance = Main.rand.NextFloat() * explosionRadius * 0.7f;
                float dustAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                Vector2 dustPosition = spawnPos + new Vector2(
                    (float)Math.Cos(dustAngle) * dustDistance,
                    (float)Math.Sin(dustAngle) * dustDistance
                );
                
                Vector2 speed = new Vector2(
                    (float)Math.Cos(dustAngle),
                    (float)Math.Sin(dustAngle)
                ) * Main.rand.NextFloat(2f, 6f);
                
                Dust dust = Dust.NewDustPerfect(
                    dustPosition, 
                    DustID.SparkForLightDisc, // Bright sparkles
                    speed,
                    0, 
                    Color.White, 
                    Main.rand.NextFloat(1f, 1.5f)
                );
                dust.noGravity = true;
                dust.fadeIn = 0.8f;
            }
            
            // Damage NPCs in the explosion radius
            // Find all NPCs within the explosion radius and damage them
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && !npc.dontTakeDamage && !npc.townNPC)
                {
                    float distance = Vector2.Distance(npc.Center, spawnPos);
                    if (distance <= explosionRadius)
                    {
                        // Calculate damage falloff based on distance from explosion center
                        float damageMultiplier = 1f - (distance / explosionRadius) * 1f;
                        int adjustedDamage = (int)(damage * damageMultiplier);
                        
                        bool canCrit = Main.rand.Next(1, 101) <= player.GetCritChance(DamageClass.Magic);
                        player.ApplyDamageToNPC(npc, adjustedDamage, knockback, player.direction, crit: canCrit);
                        
                        // Create hit dust on the NPC for visual feedback
                        for (int d = 0; d < 10; d++)
                        {
                            Dust.NewDust(npc.position, npc.width, npc.height, DustID.WhiteTorch, 
                                Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f),
                                0, default, 1.5f);
                        }
                    }
                }
            }

            return false; // Prevent the default projectile from being created
        }

        // Track when this weapon is being held
        private bool isHeld = false;
        private Vector2 targetPosition;        public override void HoldItem(Player player)
        {
            // Set that the weapon is being held
            isHeld = true;
            
            // Set the target position directly to the cursor position in the world
            targetPosition = Main.MouseWorld;
            
            // Draw a static crosshair using permanent dust particles
            
            // Central point - always visible
            Dust dust = Dust.NewDustPerfect(
                targetPosition,
                DustID.Electric, // More visible electric dust
                Vector2.Zero,
                100, // High alpha (fully visible)
                Color.Red,
                1f
            );
            dust.noGravity = true;
            dust.noLight = false;
            dust.noLightEmittence = false;
            dust.fadeIn = 0;  // No fade in
            dust.scale = 1f;  // Consistent size
            dust.velocity = Vector2.Zero; // No movement
            dust.active = true;
            
            // Draw crosshair lines - more points for a more solid appearance
            for (int i = 0; i < 4; i++) // Four directions
            {
                float angle = MathHelper.PiOver2 * i;
                for (int d = 0; d < 5; d++) // More points for a more solid line
                {
                    float distance = 6f + (d * 4f);
                    Vector2 dustPos = targetPosition + new Vector2(
                        (float)Math.Cos(angle) * distance,
                        (float)Math.Sin(angle) * distance
                    );
                    
                    dust = Dust.NewDustPerfect(
                        dustPos,
                        DustID.Electric,
                        Vector2.Zero,
                        100,
                        Color.Red,
                        0.8f
                    );
                    dust.noGravity = true;
                    dust.noLight = false;
                    dust.fadeIn = 0;
                    dust.scale = 0.8f; // Consistent size
                    dust.velocity = Vector2.Zero; // No movement
                    dust.active = true;
                }
            }

            // Draw a circle to indicate the explosion radius (5 tiles = 96 pixels)
            float explosionRadius = 96f;
            for (int i = 0; i < 32; i++) // More points for a smoother circle
            {
                float angle = MathHelper.TwoPi * ((float)i / 32);
                Vector2 dustPos = targetPosition + new Vector2(
                    (float)Math.Cos(angle) * explosionRadius,
                    (float)Math.Sin(angle) * explosionRadius
                );
                
                dust = Dust.NewDustPerfect(
                    dustPos,
                    DustID.Electric,
                    Vector2.Zero,
                    100,
                    new Color(255, 50, 50, 150), // Lighter red with some transparency
                    0.7f
                );
                dust.noGravity = true;
                dust.noLight = false;
                dust.fadeIn = 0;
                dust.scale = 0.7f;
                dust.velocity = Vector2.Zero; // No movement
                dust.active = true;
            }
        }
        
        public override void UpdateInventory(Player player)
        {
            // Reset the held status if we're not the active item
            if (player.HeldItem != Item)
                isHeld = false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SoulofSight, 15); // Requires Souls of Sight from The Twins
            recipe.AddIngredient(ItemID.CrystalShard, 25); // Crystal Shards for the magical effect
            recipe.AddIngredient(ItemID.HallowedBar, 12); // Some Hallowed Bars for post-mech flavor
            recipe.AddTile(TileID.MythrilAnvil); // Requires a Mythril or Orichalcum Anvil
            recipe.Register();
        }        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 0f); // Adjusted Y value to center the weapon better in the player's hands
        }
    }
}
