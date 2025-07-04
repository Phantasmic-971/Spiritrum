using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Projectiles
{
    public class NamelessCodexProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nameless Codex Fireball");
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = 1; // Use arrow AI for movement
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.CultistBossFireBallClone;
        }

        private void DoExplosion()
        {
            // Create an explosion effect similar to CultistBossFireBallClone
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3), 150, default, 1.5f);
            }
            for (int i = 0; i < 10; i++)
            {
                Gore.NewGore(Projectile.GetSource_Death(), Projectile.position, Projectile.velocity * 0.2f, Main.rand.Next(61, 64));
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            // Splash damage
            int splashRadius = 64; // 4 tiles
            int splashDamage = (int)(Projectile.damage * 0.75f); // 75% of projectile damage
            Player owner = Main.player[Projectile.owner];
            bool isCrit = Main.rand.NextFloat() < owner.GetCritChance(DamageClass.Magic) / 100f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(Projectile.Center) < splashRadius)
                {
                    NPC.HitInfo hitInfo = new NPC.HitInfo
                    {
                        Damage = splashDamage,
                        Knockback = 4f,
                        HitDirection = Projectile.direction,
                        Crit = isCrit,
                        DamageType = DamageClass.Magic,
                    };
                    bool oldImmune = npc.immune[Projectile.owner] > 0;
                    int oldImmuneTime = npc.immune[Projectile.owner];
                    npc.immune[Projectile.owner] = 0;                    npc.StrikeNPC(hitInfo);
                    npc.immune[Projectile.owner] = oldImmune ? oldImmuneTime : 0;
                }
            }
        }

        [System.Obsolete]
        public override void Kill(int timeLeft)
        {
            DoExplosion();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            DoExplosion();
        }
    }
}
