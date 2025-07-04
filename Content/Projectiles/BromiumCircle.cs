using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace Spiritrum.Content.Projectiles
{
    public class BromiumCircle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bromium Circle");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            // Bouncing logic
            if (Projectile.velocity.Y < 10f)
            {
                Projectile.velocity.Y += 0.2f; // Gravity effect
            }

            // Homing logic
            NPC target = Main.npc.Where(npc => npc.active && !npc.friendly && npc.Distance(Projectile.Center) < 400f)
                                  .OrderBy(npc => npc.Distance(Projectile.Center))
                                  .FirstOrDefault();

            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction * 10f, 0.1f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Bounce with 80% velocity retention on collision
            if (Projectile.velocity.X != oldVelocity.X)
                Projectile.velocity.X = -oldVelocity.X * 0.8f;
            if (Projectile.velocity.Y != oldVelocity.Y)
                Projectile.velocity.Y = -oldVelocity.Y * 0.8f;
            return false;
        }
    }
}
