using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; // Added for Vector2
using Terraria.Audio; // Added for SoundEngine

namespace Spiritrum.Content.Projectiles
{
    public class GoogBlast_Projectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Goog Blast");
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1; // Basic projectile AI
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet; // Acts like a bullet
        }

        public override void OnKill(int timeLeft)
        {
            // Play explosion sound
            SoundEngine.PlaySound(new SoundStyle("Spiritrum/Sounds/Goog") { Volume = 4f }, Projectile.position);            // Create a larger explosion on impact
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, DustID.Smoke, 0f, 0f, 100, default, 2.5f);
                Main.dust[dustIndex].velocity *= 1.8f;
            }
            for (int i = 0; i < 35; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 6f;
                dustIndex = Dust.NewDust(Projectile.position, Projectile.width * 2, Projectile.height * 2, DustID.Torch, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].velocity *= 4f;
            }            // Damage NPCs in a larger radius
            int explosionRadius = 7; // Tiles (increased from 4)
            Player player = Main.player[Projectile.owner];
            for (int j = 0; j < Main.maxNPCs; j++)
            {
                NPC target = Main.npc[j];
                if (target.active && !target.dontTakeDamage && Vector2.Distance(Projectile.Center, target.Center) < explosionRadius * 16)
                {
                    int damage = Projectile.damage; // Explosion does half projectile damage, adjust as needed
                    bool isCrit = Main.rand.Next(1, 101) <= player.GetCritChance(DamageClass.Ranged);
                    target.SimpleStrikeNPC(damage, Projectile.direction, isCrit, 0, DamageClass.Ranged, true, player.luck);
                }
            }
        }
    }
}
