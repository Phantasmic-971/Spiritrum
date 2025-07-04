using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Projectiles
{
    public class WhisperingBladeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // trail length
            ProjectileID.Sets.TrailingMode[Projectile.type]      = 0; // basic trail
        }

        public override void SetDefaults()
        {
            Projectile.width        = 16;
            Projectile.height       = 16;
            Projectile.aiStyle      = 1;                          // bullet AI
            AIType                  = ProjectileID.Bullet;        // mimic bullets
            Projectile.friendly     = true;
            Projectile.hostile      = false;
            Projectile.DamageType   = DamageClass.Melee;
            Projectile.penetrate    = 4;                         // pierce limit changed from infinite to 4
            Projectile.timeLeft     = 300;                        // 5s
            Projectile.ignoreWater  = true;
            Projectile.tileCollide  = true;
            Projectile.extraUpdates = 1;                          // smoother
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.Shadowflame,      // ID 27
                    Projectile.velocity.X * 0.2f,
                    Projectile.velocity.Y * 0.2f
                );
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 300);               // ID 153
        }
    }
}
