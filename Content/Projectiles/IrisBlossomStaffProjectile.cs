using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Projectiles
{
    public class IrisBlossomStaffProjectile : ModProjectile
    {
        private int timeAlive = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName set in localization
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 420;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.light = 0.2f;
            Projectile.aiStyle = 0; // Custom movement
        }

        public override void AI()
        {
            timeAlive++;
            // Make the projectile rotate in the direction of travel
            if (Projectile.velocity.LengthSquared() > 0.01f)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
            if (timeAlive >= 180) // 3 seconds at 60 FPS
            {
                Projectile.velocity *= 0.4f; // Gradually slow down
                if (Projectile.velocity.Length() < 0.1f)
                {
                    Projectile.velocity = Vector2.Zero;
                }
            }
        }
    }
}
