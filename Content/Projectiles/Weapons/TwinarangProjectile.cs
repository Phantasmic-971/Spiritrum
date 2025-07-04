using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Projectiles.Weapons
{
    public class TwinarangProjectile : BaseTwinarangProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 30;
            Projectile.height = 30;
            
            // Default boomerang settings
            TrailColor = new Color(255, 100, 100); // Reddish trail (similar to Twins' eyes)
        }

        protected override void CustomAI()
        {
            // Standard boomerang behavior is handled by the base class
            
            // Emit dust trails for visual effect
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default, 0.8f);
            }
        }
    }
}
