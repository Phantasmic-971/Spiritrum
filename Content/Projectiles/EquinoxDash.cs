using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Spiritrum.Content.Projectiles
{
    public class EquinoxDash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.tileCollide = true; // Can't go through blocks
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 9; // Shorter duration
            Projectile.hide = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft == 9) // Adjusted for shorter duration
            {
                // Initial dash burst - reduced speed
                player.velocity = new Vector2(player.direction * 14f, 0f); // Reduced from 18f to 12f
                player.immune = true;
                player.immuneTime = 7; // Reduced immunity time
            }
            // Keep player with projectile
            player.Center = Projectile.Center;
            player.fallStart = (int)(player.position.Y / 16f);
        }

        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            player.immune = false;
        }
    }
}