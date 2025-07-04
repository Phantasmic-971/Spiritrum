using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace Spiritrum.Content.Projectiles
{
    public class Hockarang_Projectile : ModProjectile
    {
        private bool returning = false;
        private int bounceCount = 0;
        private const int maxBounces = 0;        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = ProjAIStyleID.Boomerang;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.light = 0.3f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.BloodyMachete;
        }        public override void AI()
        {
            // Add some snow particles for thematic effect
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow, 0f, 0f, 100, default, 0.8f);
            }

            // Rotation
        }        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Create snow/ice particles on hit
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, DustID.Snow, 0f, 0f, 100, default, 1.2f);
            }

            // Small chance to inflict Frostburn in snow biome
            Player player = Main.player[Projectile.owner];
            if (player.ZoneSnow && Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Frostburn, 180); // 3 seconds
            }
        }        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Bounce off tiles with some sound
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            
            // Create some snow particles on tile collision
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Snow);
            }

            return true; // Kill projectile on tile collision
        }
    }
}
