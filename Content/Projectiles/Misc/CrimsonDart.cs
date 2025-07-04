using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Projectiles.Misc
{
    public class CrimsonDart : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crimson Dart"); // Done in localization file
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }

        public override void SetDefaults()
        {            Projectile.width = 10; // Slightly larger hitbox for better hit detection
            Projectile.height = 10; // Slightly larger hitbox for better hit detection
            Projectile.alpha = 120; // Only partially transparent for better visibility
            Projectile.friendly = true; // Deals damage to enemies
            Projectile.hostile = false; // Doesn't deal damage to players
            Projectile.DamageType = DamageClass.Summon; // Deals summon damage (inherits minion damage bonuses)
            Projectile.penetrate = 1; // Hits 1 enemy before disappearing
            Projectile.timeLeft = 180; // 3 seconds lifetime (more appropriate for a dart)
            Projectile.light = 0.5f; // Emits a moderate amount of light
            Projectile.ignoreWater = true; // Doesn't get affected by water
            Projectile.tileCollide = true; // Collides with tiles
            Projectile.extraUpdates = 1; // Reduced updates for better control

            // Visuals
            Projectile.aiStyle = -1; // Use custom AI
        }        public override void AI()
        {
            // Initialize the projectile properly if it hasn't been yet
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;
                SoundEngine.PlaySound(SoundID.Item17, Projectile.position); // Play a shooting sound
            }
            
            // Handle rotation - always face the direction of travel
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // Create dust trail for visual effect - more reliable dust creation
            if (Main.rand.NextBool(2)) // Increased chance for more visible trail
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height,
                    DustID.Blood, 
                    Projectile.velocity.X * 0.2f, 
                    Projectile.velocity.Y * 0.2f, 
                    100, 
                    default, 
                    1.2f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
            }

            // Improved homing logic
            int targetIndex = (int)Projectile.ai[0];
            float homingStrength = Projectile.ai[1];
            
            // Validate the target index
            if (targetIndex >= 0 && targetIndex < Main.maxNPCs)
            {
                NPC target = Main.npc[targetIndex];
                
                // Make sure the target is valid
                if (target != null && target.active && !target.friendly && !target.dontTakeDamage)
                {
                    // Calculate direction to target
                    Vector2 toTarget = target.Center - Projectile.Center;
                    float distanceToTarget = toTarget.Length();
                    
                    // Only home if we're within a reasonable range
                    if (distanceToTarget < 500f)
                    {
                        // Normalize and apply homing factor
                        toTarget.Normalize();
                        
                        // Calculate a proper homing force
                        float homingFactor = MathHelper.Lerp(0.08f, 0.2f, Math.Min(1f, homingStrength));
                        
                        // Apply homing acceleration - more control over speed
                        float speed = Projectile.velocity.Length();
                        Projectile.velocity = Vector2.Normalize(Projectile.velocity + toTarget * homingFactor) * speed;
                    }
                }
            }
            
            // Add a very slight, more controlled wobble effect
            Projectile.velocity += new Vector2(
                Main.rand.NextFloat(-0.05f, 0.05f),
                Main.rand.NextFloat(-0.05f, 0.05f));
        }        [System.Obsolete]
        public override void Kill(int timeLeft)
        {
            // Play sound when the projectile is destroyed
            SoundEngine.PlaySound(SoundID.NPCDeath13, Projectile.position);

            // Create a more dramatic blood splatter effect on impact
            for (int i = 0; i < 15; i++) // Increased number of dust particles
            {
                Vector2 velocity = Main.rand.NextVector2Circular(3f, 3f); // Random direction with speed
                
                Dust dust = Dust.NewDustDirect(
                    Projectile.position, 
                    Projectile.width, 
                    Projectile.height,
                    DustID.Blood, 
                    velocity.X, 
                    velocity.Y, 
                    0, 
                    default, 
                    Main.rand.NextFloat(1.0f, 1.8f)); // Larger dust particles
                
                dust.noGravity = i % 3 != 0; // Some particles fall, some float
            }
            
            // Create a small gore effect
            if (Main.netMode != NetmodeID.Server)
            {
                Gore.NewGore(Projectile.GetSource_Death(), 
                    Projectile.Center, 
                    Projectile.velocity * 0.5f, 
                    Main.rand.Next(61, 64)); // Small blood gores
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            // Make the projectile glow slightly
            return new Color(255, 50, 50, 255) * ((255 - Projectile.alpha) / 255f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Get the texture and draw a trail
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            // Calculate the drawing position and dimensions
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Play a hit sound
            SoundEngine.PlaySound(SoundID.NPCHit13, target.position);
            
            // Create a blood splatter on the target
            for (int i = 0; i < 8; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    target.position,
                    target.width,
                    target.height,
                    DustID.Blood,
                    Projectile.velocity.X * 0.5f,
                    Projectile.velocity.Y * 0.5f,
                    0,
                    default,
                    1.2f);
                    
                dust.velocity *= 0.8f;
                dust.noGravity = true;
            }
            
            // Chance to apply a Bleeding debuff (Terraria vanilla bleeding)
            if (Main.rand.NextBool(3)) // 33% chance
            {
                target.AddBuff(BuffID.Bleeding, 180); // 3 seconds of bleeding
            }
        }
    }
}
