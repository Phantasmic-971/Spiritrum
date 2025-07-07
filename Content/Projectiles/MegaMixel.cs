using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using Terraria.Audio;


namespace Spiritrum.Content.Projectiles
{
    public class MegaMixel : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 90;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 13;
        }

        public override void AI()
        {

            Projectile.rotation += 0.285f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] >= 25f)
            {
                Projectile.ai[0] = 25f;
                Projectile.velocity.Y += 0.133f;
            }
            if (Projectile.velocity.Y > 15f)
            {
                Projectile.velocity.Y = 17f;
            }
        }
    }
}