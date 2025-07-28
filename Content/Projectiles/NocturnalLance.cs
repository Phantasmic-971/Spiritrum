using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Spiritrum.Content.Projectiles
{
    public class NocturnalLance : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nocturnal Lance");
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 1; // Arrow style
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 6; // 6 pierce
            Projectile.extraUpdates = 600; // High speed
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void AI()
        {
            // Cyan-like dust trail
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 150, default, 1.2f);
            Main.dust[dust].noGravity = true;
        }
    }
}
