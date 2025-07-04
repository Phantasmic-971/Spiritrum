using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using System;

namespace Spiritrum.Content.Projectiles
{
    public class VoidHarbingerBossProjectile : ModProjectile
    {        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Harbinger Bolt");
            Main.projFrames[Type] = 4; // For animation
            
            ProjectileID.Sets.TrailCacheLength[Type] = 10; // Length of trail
            ProjectileID.Sets.TrailingMode[Type] = 2; // Smooth trail style
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 180;
            Projectile.alpha = 100; // Slightly transparent
            Projectile.light = 0.5f; // Emit light
            Projectile.extraUpdates = 1; // Move more smoothly
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
            // Different colors based on projectile type
            switch ((int)Projectile.ai[0])
            {
                case 0: // Default projectile - purple tint
                    return new Color(200, 50, 255, 200);
                    
                case 1: // Orbital projectile - blue-purple tint
                    return new Color(100, 50, 255, 200);
                    
                case 2: // Homing projectile - darker purple tint
                    return new Color(150, 20, 220, 200);
                    
                case 3: // Implosion projectile - void-like dark purple
                    // Make it pulse for a void energy effect
                    float pulseRate = 0.05f;
                    float pulse = (float)Math.Sin(Main.GlobalTimeWrappedHourly * pulseRate * 20f) * 0.2f + 0.8f;
                    return new Color(100 * pulse, 10 * pulse, 150 * pulse, 200);
                    
                default:
                    return new Color(180, 50, 255, 200);
            }
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            // Draw trail effect
            if ((int)Projectile.ai[0] > 0) // Only special projectiles get trails
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

                // Get texture and draw positions
                Texture2D texture = ModContent.Request<Texture2D>("Spiritrum/Content/Projectiles/VoidHarbingerBossProjectile").Value;
                Vector2 drawOrigin = new Vector2(texture.Width / 2, texture.Height / Main.projFrames[Projectile.type] / 2);
                
                // Get frame height
                int frameHeight = texture.Height / Main.projFrames[Projectile.type];
                
                // Draw trail with fading effect
                for (int i = 0; i < Projectile.oldPos.Length; i++)
                {
                    if (Projectile.oldPos[i] == Vector2.Zero)
                        continue;
                        
                    // Calculate alpha and scale based on position in trail
                    float progress = 1f - (float)i / Projectile.oldPos.Length;
                    Color trailColor = GetAlpha(lightColor).Value * progress * 0.5f;
                    trailColor.A = 0; // Fully transparent for additive blending
                    
                    float scale = Projectile.scale * (0.6f + 0.4f * progress);
                    
                    Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;
                    Rectangle sourceRect = new Rectangle(0, Projectile.frame * frameHeight, texture.Width, frameHeight);
                    
                    // Draw the trail segment
                    Main.spriteBatch.Draw(
                        texture,
                        drawPos,
                        sourceRect,
                        trailColor,
                        Projectile.oldRot[i],
                        drawOrigin,
                        scale,
                        SpriteEffects.None,
                        0f
                    );
                }
                
                // Reset rendering for normal drawing
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            }
            
            return true;
        }
        
        public override void PostDraw(Color lightColor)
        {
            // Add glow effect for special projectiles
            if ((int)Projectile.ai[0] > 0)
            {
                Texture2D glowTexture = ModContent.Request<Texture2D>("Spiritrum/Content/Projectiles/VoidHarbingerBossProjectile").Value;
                int frameHeight = glowTexture.Height / Main.projFrames[Projectile.type];
                Rectangle sourceRect = new Rectangle(0, Projectile.frame * frameHeight, glowTexture.Width, frameHeight);
                Vector2 drawOrigin = new Vector2(glowTexture.Width / 2, frameHeight / 2);
                
                // Get glow color based on projectile type
                Color glowColor = Color.White;
                switch ((int)Projectile.ai[0])
                {
                    case 1: // Orbital - blue glow
                        glowColor = new Color(100, 100, 255, 0) * 0.5f;
                        break;
                        
                    case 2: // Homing - purple glow
                        glowColor = new Color(200, 50, 255, 0) * 0.5f;
                        break;
                        
                    case 3: // Implosion - dark purple pulsing glow
                        float pulse = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.3f + 0.7f;
                        glowColor = new Color(100, 10, 150, 0) * (0.5f * pulse);
                        break;
                }
                
                // Draw the glow effect with additive blending
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
                
                Main.spriteBatch.Draw(
                    glowTexture,
                    Projectile.Center - Main.screenPosition,
                    sourceRect,
                    glowColor,
                    Projectile.rotation,
                    drawOrigin,
                    Projectile.scale * 1.2f, // Slightly larger for glow effect
                    SpriteEffects.None,
                    0f
                );
                
                // Reset rendering
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
            }
        }public override void AI()
        {
            // Different behavior based on AI mode
            switch ((int)Projectile.ai[0])
            {
                case 0: // Default behavior - basic projectile
                    DefaultProjectileBehavior();
                    break;
                
                case 1: // Orbital behavior - orbits around its spawn point
                    OrbitingProjectileBehavior();
                    break;
                
                case 2: // Homing behavior - slowly tracks the player
                    HomingProjectileBehavior();
                    break;
                
                case 3: // Implosion behavior - converges to a point
                    ImplosionProjectileBehavior();
                    break;
                
                default:
                    DefaultProjectileBehavior();
                    break;
            }
            
            // Animation for all projectile types
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
        }
        
        private void DefaultProjectileBehavior()
        {
            // Create a trail effect
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1.2f);
                dust.noGravity = true;
                dust.velocity *= 0.2f;
            }

            // Make it emit light
            Lighting.AddLight(Projectile.Center, 0.4f, 0.1f, 0.6f);
            
            // Make it rotate
            Projectile.rotation += 0.2f;
        }
        
        private void OrbitingProjectileBehavior()
        {
            // For orbiting projectiles, gradually increase orbital radius
            if (Projectile.ai[1] == 0f)
            {
                // Store owner center as reference point if not already set
                Projectile.ai[1] = 1f;
                Projectile.localAI[0] = Projectile.Center.X;
                Projectile.localAI[1] = Projectile.Center.Y;
            }
            
            // Calculate orbital movement
            Vector2 orbitCenter = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
            Vector2 toCenter = orbitCenter - Projectile.Center;
            float distance = toCenter.Length();
            toCenter.Normalize();
            
            // Calculate orbital velocity
            float orbitSpeed = 0.05f;
            Vector2 orbitalVelocity = toCenter.RotatedBy(MathHelper.PiOver2) * distance * orbitSpeed;
            
            // Apply velocity with slight inward pull to maintain orbit
            Projectile.velocity = orbitalVelocity + toCenter * 0.05f;
            
            // Create more pronounced orbit effect
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1.5f);
                dust.noGravity = true;
                dust.velocity *= 0.1f;
            }
            
            // Make it emit more light
            Lighting.AddLight(Projectile.Center, 0.6f, 0.1f, 0.8f);
            
            // Make it rotate faster
            Projectile.rotation += 0.3f;
        }
          private void HomingProjectileBehavior()
        {
            // No longer homes in on player, maintain straight path with visuals only
            
            // Create trailing effect for projectiles
            for (int i = 0; i < 2; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleTorch,
                    -Projectile.velocity.X * 0.2f, -Projectile.velocity.Y * 0.2f, 0, default, 1.3f);
                dust.noGravity = true;
            }
            
            // Make it emit pulsating light
            float pulse = (float)Math.Sin(Projectile.timeLeft * 0.1f) * 0.2f + 0.6f;
            Lighting.AddLight(Projectile.Center, 0.2f, 0.1f, pulse);
            
            // Make it rotate based on velocity
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }
        
        private void ImplosionProjectileBehavior()
        {
            // After a certain time, these projectiles should vanish (they're part of the implosion effect)
            if (Projectile.timeLeft < 60)
            {
                // Increase alpha (fading out)
                Projectile.alpha += 5;
                if (Projectile.alpha >= 255)
                {
                    Projectile.Kill();
                    return;
                }
                
                // Slow down as they converge
                Projectile.velocity *= 0.97f;
            }
            
            // Create intense trailing effect for implosion projectiles
            if (Main.rand.NextBool())
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1.5f);
                dust.noGravity = true;
                dust.velocity = -Projectile.velocity * 0.5f;
            }
            
            // Create occasional shadow dust
            if (Main.rand.NextBool(5))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Shadowflame,
                    0f, 0f, 0, default, 1.2f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
            }
            
            // Make it emit strong light
            Lighting.AddLight(Projectile.Center, 0.7f, 0.1f, 0.9f);
            
            // Make it rotate rapidly
            Projectile.rotation += 0.4f;
        }        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            // Different effects based on projectile type
            switch ((int)Projectile.ai[0])
            {
                case 0: // Default projectile
                    // Basic debuffs
                    target.AddBuff(BuffID.Darkness, 240); // 4 seconds of darkness
                    target.AddBuff(BuffID.Slow, 120); // 2 seconds of slowness
                    break;
                    
                case 1: // Orbital projectile
                    // Disorienting effect
                    target.AddBuff(BuffID.Confused, 180); // 3 seconds of confusion
                    target.AddBuff(BuffID.Slow, 120); // 2 seconds of slowness
                    break;
                    
                case 2: // Homing projectile
                    // More severe debuffs
                    target.AddBuff(BuffID.Darkness, 300); // 5 seconds of darkness  
                    target.AddBuff(BuffID.Weak, 600); // 10 seconds of weakness
                    break;
                    
                case 3: // Implosion projectile
                    // Most severe effect
                    target.AddBuff(BuffID.Darkness, 300); // 5 seconds of darkness
                    target.AddBuff(BuffID.Slow, 300); // 5 seconds of slowness
                    target.AddBuff(BuffID.Blackout, 180); // 3 seconds of blackout (stronger darkness)
                    break;
                    
                default:
                    // Fallback debuffs
                    target.AddBuff(BuffID.Darkness, 240); // 4 seconds of darkness
                    break;
            }
            
            // Visual effects for all projectile types
            for (int i = 0; i < 15; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.5f);
                dust.noGravity = true;
            }
            
            // Add additional visual effects based on projectile type
            if (Projectile.ai[0] == 2 || Projectile.ai[0] == 3)
            {
                // More dramatic effect for homing and implosion projectiles
                for (int i = 0; i < 10; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(8f, 8f);
                    Dust dust = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Shadowflame, velocity.X, velocity.Y, 0, default, 1.8f);
                    dust.noGravity = true;
                }
                
                // Sound effect
                SoundEngine.PlaySound(SoundID.Item73, target.position);
            }
            else
            {
                // Normal hit sound
                SoundEngine.PlaySound(SoundID.Item101, target.position);
            }
        }        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            bool destroyProjectile = true;
            
            // Different effects based on projectile type
            switch ((int)Projectile.ai[0])
            {
                case 0: // Default projectile
                    // Basic impact
                    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                    
                    // Create dust effect
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    break;
                    
                case 1: // Orbital projectile
                    // Bounce off tiles with reduced velocity
                    if (Math.Abs(oldVelocity.X - Projectile.velocity.X) > 0.1f)
                    {
                        Projectile.velocity.X = -oldVelocity.X * 0.8f;
                    }
                    
                    if (Math.Abs(oldVelocity.Y - Projectile.velocity.Y) > 0.1f)
                    {
                        Projectile.velocity.Y = -oldVelocity.Y * 0.8f;
                    }
                    
                    // Play bounce sound
                    SoundEngine.PlaySound(SoundID.Item56, Projectile.position);
                    
                    // Bounce effect dust
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(3f, 3f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.2f);
                        dust.noGravity = true;
                    }
                    
                    // Don't destroy on collision
                    destroyProjectile = false;
                    
                    // But reduce remaining time
                    Projectile.timeLeft = Math.Min(Projectile.timeLeft, 60);
                    break;
                    
                case 2: // Homing projectile
                    // Explodes on impact
                    SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                    
                    // Create more dramatic explosion effect
                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(8f, 8f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.8f);
                        dust.noGravity = true;
                    }
                    
                    // Shadow flame effect
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, velocity.X, velocity.Y, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    break;
                    
                case 3: // Implosion projectile
                    // Just vanishes with a subtle effect
                    SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
                    
                    // Create implosion effect
                    for (int i = 0; i < 15; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                        Vector2 dustPos = Projectile.Center + Main.rand.NextVector2Circular(20f, 20f);
                        Vector2 dustVel = (Projectile.Center - dustPos) * 0.1f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleCrystalShard, dustVel, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    break;
                    
                default:
                    // Fallback effect
                    SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                    
                    // Create dust effect
                    for (int i = 0; i < 15; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                        Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    break;
            }
            
            return destroyProjectile;
        }
    }
}
