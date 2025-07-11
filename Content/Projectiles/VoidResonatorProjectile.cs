using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;

namespace Spiritrum.Content.Projectiles
{
    public class VoidResonatorProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Bolt");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8; // Set the trail length
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // Pixel-perfect trail
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 3; // Pierce through up to 3 enemies
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 180; // 3 seconds
            Projectile.light = 0.5f; // Emits light
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 6; // 6 ticks = 1/8th second
            
            // Determine appearance and behavior
            Projectile.alpha = 100; // Slightly transparent
            Projectile.extraUpdates = 1; // Update more frequently for smoother movement
        }

        public override void AI()
        {
            // Rotate the projectile based on its velocity
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            
            // Create glowing dust particles
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleCrystalShard,
                    0f, 0f, 0, default, 1f);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
                dust.velocity *= 0.3f;
            }

            // Make it emit purple light
            Lighting.AddLight(Projectile.Center, 0.5f, 0.1f, 0.7f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Create an explosion effect on hit
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    target.position,
                    target.width,
                    target.height,
                    DustID.PurpleCrystalShard,
                    0f, 0f, 0, default, 1.5f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
            }
            {
            DoExplosion();
            }
            
            // Chance to apply debuff
            if (Main.rand.NextBool(3)) // 33% chance
            {
                target.AddBuff(BuffID.ShadowFlame, 180); // 3 seconds of Shadowflame
            }
            
            // Play hit sound
            SoundEngine.PlaySound(SoundID.Item10, target.position);
            
            // Reduce projectile penetration count
            Projectile.penetrate--;
            
            // If this is the last target, create a larger explosion
            if (Projectile.penetrate <= 0)
            {
                // Create a void explosion effect
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(
                        Projectile.Center - new Vector2(20, 20),
                        40, 40,
                        DustID.PurpleTorch,
                        0f, 0f, 0, default, 2f);
                    dust.noGravity = true;
                    dust.velocity = Main.rand.NextVector2Circular(8f, 8f);
                }
                
                // Create explosion sound
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Bounce the projectile with decreased velocity (once)
            if (Projectile.penetrate > 1)
            {
                Projectile.penetrate--;
                
                // Play bounce sound
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
                
                // Bounce with reduced velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                {
                    Projectile.velocity.X = -oldVelocity.X * 0.6f;
                }
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                {
                    Projectile.velocity.Y = -oldVelocity.Y * 0.6f;
                }
                
                // Create particle effects for the bounce
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Dust.NewDustDirect(
                        Projectile.position,
                        Projectile.width,
                        Projectile.height,
                        DustID.PurpleCrystalShard,
                        0f, 0f, 0, default, 1.2f);
                    dust.noGravity = true;
                    dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
                }
                
                return false; // Don't destroy the projectile
            }
            
            // Create particle effects when hitting a tile (final collision)
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.PurpleCrystalShard,
                    0f, 0f, 0, default, 1.2f);
                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
            }
            
            // Play destruction sound
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
            
            return true; // Destroy the projectile
        }
        
        

        private void DoExplosion()
        {
            // Create an explosion effect similar to CultistBossFireBallClone
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), 150, default, 1.5f);
            }
            for (int i = 0; i < 10; i++)
            {
                Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, Projectile.velocity * 0.2f, Main.rand.Next(61, 64));
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            // Splash damage
            int splashRadius = 64; // 4 tiles
            int splashDamage = (int)(Projectile.damage * 0.75f); // 75% of projectile damage
            Player owner = Main.player[Projectile.owner];
            bool isCrit = Main.rand.NextFloat() < owner.GetCritChance(DamageClass.Magic) / 100f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(Projectile.Center) < splashRadius)
                {
                    NPC.HitInfo hitInfo = new NPC.HitInfo
                    {
                        Damage = splashDamage,
                        Knockback = 4f,
                        HitDirection = Projectile.direction,
                        Crit = isCrit,
                        DamageType = DamageClass.Magic,
                    };
                    bool oldImmune = npc.immune[Projectile.owner] > 0;
                    int oldImmuneTime = npc.immune[Projectile.owner];
                    npc.immune[Projectile.owner] = 0;                    npc.StrikeNPC(hitInfo);
                    npc.immune[Projectile.owner] = oldImmune ? oldImmuneTime : 0;
                }
            }
        }

        [System.Obsolete]
        public override void OnKill(int timeLeft)
        {
            DoExplosion();
        }

        }
    }