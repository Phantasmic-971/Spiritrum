using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Projectiles
{
    public class SnowyOwlStaffProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName set in localization
            Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 5; // 5-frame animation
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed for right-click to despawn and proper minion slot management
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            // Animation
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 6) // 6 ticks per frame (10 fps)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
            
            Player player = Main.player[Projectile.owner];
            

            // Check if player is active, alive, and has the buff
            if (!player.active || player.dead || !player.HasBuff(ModContent.BuffType<Spiritrum.Content.Buffs.SnowyOwlStaffBuff>()))
            {
                Projectile.Kill();
                return;
            }

            // Refresh the buff duration as long as the minion exists
            if (player.HasBuff(ModContent.BuffType<Spiritrum.Content.Buffs.SnowyOwlStaffBuff>()))
            {
                player.AddBuff(ModContent.BuffType<Spiritrum.Content.Buffs.SnowyOwlStaffBuff>(), 3600000);
            }

            // Enhanced minion AI: sometimes swap to a new target if one is far enough away
            float detectRadius = 500f;
            NPC target = null;
            float minDist = detectRadius;
            int currentTarget = -1;
            if (Projectile.ai[0] >= 0 && Projectile.ai[0] < Main.maxNPCs && Main.npc[(int)Projectile.ai[0]].CanBeChasedBy(this))
            {
                currentTarget = (int)Projectile.ai[0];
                target = Main.npc[currentTarget];
                minDist = Vector2.Distance(Projectile.Center, target.Center);
            }

            // 1 in 90 chance per tick to swap target if a viable one is at least 8 tiles (8*16=128px) away
            if (Main.rand.NextBool(90))
            {
                float farDist = 128f;
                NPC swapTarget = null;
                float swapDist = farDist;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy(this))
                    {
                        float dist = Vector2.Distance(Projectile.Center, npc.Center);
                        if (dist > farDist && dist < detectRadius)
                        {
                            // Prefer the closest far target
                            if (swapTarget == null || dist < swapDist)
                            {
                                swapTarget = npc;
                                swapDist = dist;
                                currentTarget = i;
                            }
                        }
                    }
                }
                if (swapTarget != null)
                {
                    target = swapTarget;
                    Projectile.ai[0] = currentTarget;
                }
            }
            // If no target or current target is invalid, pick nearest
            if (target == null || !target.active || !target.CanBeChasedBy(this))
            {
                minDist = detectRadius;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy(this))
                    {
                        float dist = Vector2.Distance(Projectile.Center, npc.Center);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            target = npc;
                            currentTarget = i;
                        }
                    }
                }
                if (target != null)
                    Projectile.ai[0] = currentTarget;
            }

            if (target != null)
            {
                Vector2 toTarget = target.Center - Projectile.Center;
                float speed = 8f;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, toTarget.SafeNormalize(Vector2.Zero) * speed, 0.1f);
            }
            else
            {
                // Idle: hover near player
                Vector2 idlePos = player.Center + new Vector2(-40 * player.direction, -60);
                Vector2 toIdle = idlePos - Projectile.Center;
                float idleSpeed = 6f;
                if (toIdle.Length() > 200f)
                    Projectile.velocity = toIdle.SafeNormalize(Vector2.Zero) * idleSpeed;
                else
                    Projectile.velocity = Vector2.Lerp(Projectile.velocity, toIdle.SafeNormalize(Vector2.Zero) * idleSpeed, 0.05f);
            }

            // Make the owl face the direction it is moving
            if (Projectile.velocity.X != 0)
                Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0) ? 1 : -1;
        }
    }
}
