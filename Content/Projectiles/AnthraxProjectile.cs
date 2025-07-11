using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Projectiles
{
    public class AnthraxProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Anthrax Gas Grenade");
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = 2; // Grenade
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1; // No piercing
            Projectile.timeLeft = 180;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.rotation = 0f; // Prevent spinning
            Projectile.scale = 0.5f; // Default scale
        }
        public override void AI()
        {
            // Point the projectile in the direction of its velocity (towards the weapon's firing direction)            if (Projectile.velocity.Length() > 0.01f)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            }
        }

        [System.Obsolete]
        public override void OnKill(int timeLeft)
        {
            // Gas cloud effect (visuals and debuff)
            for (int i = 0; i < 30; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ToxicBubble, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 150, default, 1.2f);
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            int radius = 40; // Smaller AOE
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.Distance(Projectile.Center) < radius)
                {
                    npc.AddBuff(BuffID.Venom, 480);
                    npc.AddBuff(BuffID.BrokenArmor, 480);
                    npc.AddBuff(BuffID.Ichor, 480);
                    // Splash damage: 30% of projectile's damage
                    if (npc.whoAmI != Projectile.owner)
                    {
                        int splashDamage = (int)(Projectile.damage * 0.5f);
                        Player owner = Main.player[Projectile.owner];
                        bool isCrit = Main.rand.NextFloat() < owner.GetCritChance(DamageClass.Ranged) / 100f;
                        NPC.HitInfo hitInfo = new NPC.HitInfo
                        {
                            Damage = splashDamage,
                            Knockback = 2f,
                            HitDirection = Projectile.direction,
                            Crit = isCrit,
                            DamageType = DamageClass.Ranged,
                        };
                        bool oldImmune = npc.immune[Projectile.owner] > 0;
                        int oldImmuneTime = npc.immune[Projectile.owner];
                        npc.immune[Projectile.owner] = 0;
                        npc.StrikeNPC(hitInfo);
                        npc.immune[Projectile.owner] = oldImmune ? oldImmuneTime : 0;
                    }
                }
            }
        }
    }
}
