using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Projectiles
{
    public class FrozoFlake : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frozonite Flake");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of the trail
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The trail drawing mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 17;
            Projectile.height = 17;
            Projectile.aiStyle = 0; // Custom AI
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1; // Only hits one enemy before being destroyed
            Projectile.timeLeft = 600; // 10 seconds
            Projectile.alpha = 100; // Slightly transparent
            Projectile.light = 0.5f; // Emits light
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 4; // Run the AI four times per frame (for better collision detection)
        }

        public override void AI()
        {
            // Spin the projectile
            Projectile.rotation += 0.4f;
            
            // Create dust effects
            if (Main.rand.NextBool(3)) // 1/3 chance to spawn dust each frame
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 
                    DustID.IceTorch, 0f, 0f, 100, default, 1f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }

            // Create a slight wobble effect in movement
            float wobbleSpeed = 0.2f; // Speed of wobble
            float wobbleAmplitude = 1.5f; // Size of wobble
            
            // Use the projectile's AI array to track time for wobble
            Projectile.ai[0]++;
            
            // Calculate wobble effect
            float wobbleOffset = (float)Math.Sin(Projectile.ai[0] * wobbleSpeed) * wobbleAmplitude;
            
            // Apply wobble perpendicular to movement direction
            Vector2 perpendicular = new Vector2(-Projectile.velocity.Y, Projectile.velocity.X).SafeNormalize(Vector2.Zero);
            Projectile.position += perpendicular * wobbleOffset;

            // Slow down slightly over time
            if (Projectile.ai[0] > 30) // After 30 frames, start slowing down
            {
                Projectile.velocity *= 0.995f; // Slight slowdown
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Apply Frost Burn for 3 seconds (180 frames)
            target.AddBuff(BuffID.Frostburn2, 180);
            
            // Create ice impact visual effect
            for (int i = 0; i < 15; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(4f, 4f);
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 
                    DustID.IceRod, speed.X, speed.Y, 0, default, Main.rand.NextFloat(1f, 1.5f));
                d.noGravity = true;
            }
            
            // Play hit sound
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Create ice impact visual effect
            for (int i = 0; i < 8; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(2f, 2f);
                Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 
                    DustID.IceRod, speed.X, speed.Y, 0, default, Main.rand.NextFloat(0.8f, 1.2f));
                d.noGravity = true;
            }
            
            // Play impact sound
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
            
            // When hitting a tile, the projectile is destroyed
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Get projectile texture
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            
            // Calculate the source rectangle
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            
            // Calculate position and origin
            Vector2 origin = sourceRectangle.Size() / 2f;
            Vector2 drawPos = Projectile.Center - Main.screenPosition;
            
            // Draw the trail
            Color trailColor = Color.Cyan * 0.5f;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPosTrail = Projectile.oldPos[k] + Projectile.Size / 2f - Main.screenPosition;
                float trailFade = (float)(Projectile.oldPos.Length - k) / Projectile.oldPos.Length;
                Main.spriteBatch.Draw(texture, drawPosTrail, sourceRectangle, trailColor * trailFade, 
                    Projectile.rotation, origin, Projectile.scale * (0.8f + 0.2f * trailFade), SpriteEffects.None, 0);
            }
            
            // Draw the actual projectile
            Color color = Projectile.GetAlpha(lightColor); // Apply transparency
            Main.spriteBatch.Draw(texture, drawPos, sourceRectangle, color, 
                Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);
            
            return false; // Don't draw normally, we've handled it here
        }
    }
}
