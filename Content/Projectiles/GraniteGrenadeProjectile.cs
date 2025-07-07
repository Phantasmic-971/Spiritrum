using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Projectiles
{
    public class GraniteGrenadeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Granite Grenade");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 16;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            AIType = ProjectileID.Grenade;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.Kill();
        }

        [System.Obsolete]
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(4f, 4f);
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, velocity, 0, default, 1.5f);
                dust.noGravity = true;
            }
            int explosionRadius = 40;
            for (int i = 0; i < 3; i++)
            {
                Vector2 lightning = Main.rand.NextVector2CircularEdge(explosionRadius, explosionRadius);
                Dust.NewDustPerfect(Projectile.Center + lightning, DustID.Electric, lightning.SafeNormalize(Vector2.Zero) * 3f, 0, default, 1.2f);
            }
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, 
                    ProjectileID.GrenadeI, Projectile.damage, 4f, Projectile.owner);
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item94, Projectile.position);
        }
    }
}
