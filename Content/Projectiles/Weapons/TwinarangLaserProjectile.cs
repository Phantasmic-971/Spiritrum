using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Projectiles.Weapons
{
    public class TwinarangLaserProjectile : BaseTwinarangProjectile
    {
        private int laserTimer = 0; // Timer for laser emission
        private readonly int laserRate = 3; // How often to emit lasers (lower is more frequent)

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // Custom name would be set in localization
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 30;
            Projectile.height = 30;
            
            // Customization for the laser boomerang
            TrailColor = new Color(150, 150, 255); // Blue trail
            
            // Light emission
            Projectile.light = 0.8f;
        }

        protected override void CustomAI()
        {
            // Emit lasers in 2 directions periodically
            laserTimer++;
            if (laserTimer >= laserRate && !Returning)
            {
                laserTimer = 0;
                float rotationAmount = MathHelper.Pi; // 180 degrees

                if (Main.myPlayer == Projectile.owner)
                {
                    // Emit lasers in 2 directions (left and right relative to travel path)
                    for (int i = 0; i < 2; i++)
                    {
                        Vector2 perpendicularVelocity = new Vector2(Projectile.velocity.Y, -Projectile.velocity.X).RotatedBy(rotationAmount * i);
                        perpendicularVelocity.Normalize();
                        perpendicularVelocity *= 14f; // Fast laser speed
                        
                        // Spawn laser projectiles
                        int laserProj = Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            perpendicularVelocity,
                            ProjectileID.LaserMachinegunLaser, // Using laser projectile
                            (int)(Projectile.damage * 0.75), // Slightly less damage than the boomerang
                            0f,
                            Projectile.owner);
                          // Configure the laser projectile
                        if (Main.projectile.IndexInRange(laserProj))
                        {
                            Main.projectile[laserProj].DamageType = DamageClass.Melee;
                            Main.projectile[laserProj].timeLeft = 40; // Short lifetime
                            Main.projectile[laserProj].usesLocalNPCImmunity = true; // Independent immunity frames
                            Main.projectile[laserProj].localNPCHitCooldown = 8; // Shorter cooldown for faster hitting
                            Main.projectile[laserProj].CritChance = 6; // Slightly higher crit chance
                            // Play laser sound occasionally
                            if (Main.rand.NextBool(5))
                            {
                                SoundEngine.PlaySound(SoundID.Item12, Projectile.position);
                            }
                        }
                    }
                }
                
                // Create dust effects
                for (int d = 0; d < 2; d++)
                {
                    Dust.NewDust(Projectile.Center, 10, 10, DustID.Electric, 0f, 0f, 0, default, 1f);
                }
            }
            
            // Emit dust trails
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, 0f, 0f, 0, default, 1f);
            }
        }
    }
}
