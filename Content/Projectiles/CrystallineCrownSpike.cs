using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Items.Accessories;

namespace Spiritrum.Content.Projectiles
{
    public class CrystallineCrownSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crystal Spike");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 1800000;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 40;

        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            int index = (int)Projectile.ai[0];
            float angle = MathHelper.TwoPi / 6 * index + Main.GameUpdateCount * 0.09f;
            Vector2 offset = new Vector2(60, 0).RotatedBy(angle);
            Projectile.Center = player.Center + offset;
            Projectile.rotation = offset.ToRotation() + MathHelper.PiOver2;
            if (!player.active || player.dead || !player.GetModPlayer<CrystallineCrownPlayer>().crownEquipped)
                Projectile.Kill();
        }
    }
}
