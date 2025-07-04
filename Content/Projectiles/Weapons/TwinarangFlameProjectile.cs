using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Projectiles.Weapons
{
    public class TwinarangFlameProjectile : BaseTwinarangProjectile
    {
        private int flameTimer = 0; // Timer for flame emission
        private readonly int flameRate = 5; // How often to emit flames (lower is more frequent)

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
            
            // Customization for the flame boomerang
            TrailColor = new Color(255, 150, 0); // Orange-red trail
            
            // Light emission
            Projectile.light = 0.8f;
        }

        protected override void CustomAI()
        {
            // Emit flames in 4 directions periodically
            flameTimer++;
            if (flameTimer >= flameRate && !Returning)
            {
                flameTimer = 0;
                float rotationAmount = MathHelper.PiOver2; // 90 degrees

                if (Main.myPlayer == Projectile.owner)
                {
                    // Emit flames in 4 directions
                    for (int i = 0; i < 4; i++)
                    {
                        Vector2 flameVelocity = new Vector2(8, 0).RotatedBy(rotationAmount * i);
                          // Spawn Spazmatism's eye fire projectiles
                        int flameProj = Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            flameVelocity,
                            ProjectileID.EyeFire, // Using Spazmatism's eye fire
                            Projectile.damage / 2, // Less damage than the boomerang
                            0f,
                            Projectile.owner);
                          // Configure the flame projectile
                        if (Main.projectile.IndexInRange(flameProj))
                        {
                            Main.projectile[flameProj].friendly = true;
                            Main.projectile[flameProj].hostile = false;
                            Main.projectile[flameProj].DamageType = DamageClass.Melee;
                            Main.projectile[flameProj].timeLeft = 30; // Short lifetime
                            Main.projectile[flameProj].usesLocalNPCImmunity = true; // Independent immunity frames
                            Main.projectile[flameProj].localNPCHitCooldown = 10; // 10 frame cooldown
                            Main.projectile[flameProj].CritChance = 4; // Base crit chance
                        }
                    }
                }
                
                // Create dust effects
                for (int d = 0; d < 4; d++)
                {
                    Dust.NewDust(Projectile.Center, 10, 10, DustID.CursedTorch, 0f, 0f, 0, default, 1f);
                }
            }
            
            // Emit dust trails
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 0, default, 1f);
            }
        }
    }
}
