using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Global
{
    public class FrostWaveGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.FrostWave)
            {
                // More slowly accelerate the FrostWave (reduced from 2.5% to 1% per tick)
                if (projectile.velocity.Length() < 16f)
                {
                    projectile.velocity *= 1.01f; // 1% acceleration per tick
                }
                // Make FrostWave always point towards its movement direction with 90-degree offset
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
        }
    }
}
