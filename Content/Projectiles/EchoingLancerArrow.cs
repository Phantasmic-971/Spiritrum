using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Projectiles
{
    public class EchoingLancerArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Spectral arrow for Echoing Lancer");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Combo logic: track hits per target per player
            Player owner = Main.player[Projectile.owner];
            if (!target.GetGlobalNPC<EchoingLancerArrowGlobalNPC>().comboHits.ContainsKey(owner.whoAmI))
                target.GetGlobalNPC<EchoingLancerArrowGlobalNPC>().comboHits[owner.whoAmI] = new List<int>();
            var hitTimes = target.GetGlobalNPC<EchoingLancerArrowGlobalNPC>().comboHits[owner.whoAmI];
            hitTimes.Add((int)Main.GameUpdateCount);
            // Remove old hits
            hitTimes.RemoveAll(t => Main.GameUpdateCount - t > 30);
            if (hitTimes.Count == 3)
            {
                // All 3 arrows hit within 0.5s (30 ticks) 
                int echo = Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.UnitY.RotatedByRandom(MathHelper.TwoPi) * 0.1f, Projectile.type, (int)(Projectile.damage * 0.5f), Projectile.knockBack, owner.whoAmI);
                Main.projectile[echo].hostile = false;
                Main.projectile[echo].friendly = true;
                Main.projectile[echo].DamageType = DamageClass.Ranged;
                hitTimes.Clear();
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // +15% damage if comboing (if 2+ hits in 0.5s)
            Player owner = Main.player[Projectile.owner];
            var hitTimes = target.GetGlobalNPC<EchoingLancerArrowGlobalNPC>().comboHits.ContainsKey(owner.whoAmI) ? target.GetGlobalNPC<EchoingLancerArrowGlobalNPC>().comboHits[owner.whoAmI] : null;
            if (hitTimes != null && hitTimes.Count >= 2 && Main.GameUpdateCount - hitTimes[hitTimes.Count - 2] <= 30)
            {
                modifiers.SourceDamage += 0.15f;
            }
        }
    }

    public class EchoingLancerArrowGlobalNPC : GlobalNPC
    {
        public Dictionary<int, List<int>> comboHits = new();
        public override bool InstancePerEntity => true;
        public override void ResetEffects(NPC npc)
        {
            comboHits.Clear();
        }
    }
}
