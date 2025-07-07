using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Spiritrum.Content.Buffs;

namespace Spiritrum.Content.Projectiles
{
    public class ElementalBladeProjectile : ModProjectile
    {
        // AI State variables
        private enum AIState
        {
            Idle = 0,
            Dashing = 1,
            Stabbing = 2,
            Spinning = 3,
            Returning = 4
        }

        private AIState currentState = AIState.Idle;
        private int targetNPC = -1;
        private int stateTimer = 0;
        private Vector2 dashStartPos;
        private Vector2 idleOffset;
        private bool hasStabbed = false;
        
        // Constants
        private const float MAX_DETECT_RANGE = 1000f; // 62.5 blocks * 16 pixels
        private const float DASH_SPEED = 25f;
        private const float IDLE_SPEED = 8f;
        private const float RETURN_SPEED = 16f;
        private const int STAB_DURATION = 8;
        private const float WING_OFFSET_DISTANCE = 60f;

        public override void SetStaticDefaults()
        {
            // This projectile should appear in front of most enemies
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            
            // These are necessary for proper minion behavior
            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet/minion
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // Needed for right-click despawn
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;         // Uses 1 minion slot
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;           // Infinite penetration
            Projectile.timeLeft = 18000;         // Match Snowy Owl Staff
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            
            // Custom AI - no vanilla AI style
            Projectile.aiStyle = -1;
            
            // Light emission
            Projectile.light = 0.5f;
            
            // Local immunity for Terraprisma-like behavior
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 17; // Apply 17 ticks of local immunity
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            
            // Check if player is alive, active, and has the buff
            if (!player.active || player.dead || !player.HasBuff(ModContent.BuffType<ElementalBlades>()))
            {
                Projectile.Kill();
                return;
            }

            // Refresh the buff while the minion exists
            player.AddBuff(ModContent.BuffType<ElementalBlades>(), 2);
            
            // Initialize on first spawn - teleport to behind player
            if (Projectile.ai[0] == 0f)
            {
                Vector2 playerPos = player.Center;
                Vector2 behindPlayer = playerPos + new Vector2(-60f * player.direction, -20f);
                Projectile.Center = behindPlayer;
                Projectile.ai[0] = 1f; // Mark as initialized
                currentState = AIState.Idle;
                CalculateIdlePosition(player);
                Projectile.netUpdate = true;
            }

            stateTimer++;

            switch (currentState)
            {
                case AIState.Idle:
                    HandleIdleState(player);
                    break;
                case AIState.Dashing:
                    HandleDashingState(player);
                    break;
                case AIState.Stabbing:
                    HandleStabbingState(player);
                    break;
                case AIState.Spinning:
                    HandleSpinningState(player);
                    break;
                case AIState.Returning:
                    HandleReturningState(player);
                    break;
            }

            // Visual effects
            CreateDustEffects();
        }

        private void HandleIdleState(Player player)
        {
            // Clear target from ai[1] when idle
            Projectile.ai[1] = -1;
            
            // Look for targets
            NPC target = FindNearestTarget(player);
            if (target != null)
            {
                targetNPC = target.whoAmI;
                Projectile.ai[1] = targetNPC; // Store target in ai[1] for other blades to see
                currentState = AIState.Dashing;
                stateTimer = 0;
                dashStartPos = Projectile.Center;
                hasStabbed = false;
                Projectile.netUpdate = true;
                return;
            }

            // Move to idle position behind player
            Vector2 targetPos = player.Center + idleOffset;
            Vector2 direction = targetPos - Projectile.Center;
            
            if (direction.Length() > 200f)
            {
                // Teleport if too far
                Projectile.Center = targetPos;
            }
            else
            {
                // Smooth movement to idle position
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, direction * 0.1f, 0.2f);
                if (direction.Length() < 10f)
                {
                    Projectile.velocity *= 0.9f;
                }
            }

            // Point toward player's direction
            Projectile.rotation = MathHelper.ToRadians(player.direction == 1 ? 45f : 135f);
        }

        private void HandleDashingState(Player player)
        {
            if (targetNPC < 0 || targetNPC >= Main.maxNPCs || !Main.npc[targetNPC].active)
            {
                ReturnToIdle(player);
                return;
            }

            NPC target = Main.npc[targetNPC];
            
            // Check if target is too far from player
            if (Vector2.Distance(target.Center, player.Center) > MAX_DETECT_RANGE)
            {
                ReturnToIdle(player);
                return;
            }

            // Ensure ai[1] is set to current target
            Projectile.ai[1] = targetNPC;

            // Dash toward target
            Vector2 direction = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
            Projectile.velocity = direction * DASH_SPEED;
            Projectile.rotation = direction.ToRotation();

            // Check if close enough to start stabbing
            if (Vector2.Distance(Projectile.Center, target.Center) < 50f)
            {
                currentState = AIState.Stabbing;
                stateTimer = 0;
                Projectile.ResetLocalNPCHitImmunity(); // Reset immunity for new attack
                Projectile.netUpdate = true;
            }
        }

        private void HandleStabbingState(Player player)
        {
            if (targetNPC < 0 || targetNPC >= Main.maxNPCs || !Main.npc[targetNPC].active)
            {
                ReturnToIdle(player);
                return;
            }

            NPC target = Main.npc[targetNPC];
            
            // Ensure ai[1] is set to current target
            Projectile.ai[1] = targetNPC;
            
            // Quick stab motion
            Projectile.velocity *= 0.8f; // Slow down
            
            if (stateTimer >= STAB_DURATION)
            {
                currentState = AIState.Spinning;
                stateTimer = 0;
                hasStabbed = true;
                Projectile.netUpdate = true;
            }
        }

        private void HandleSpinningState(Player player)
        {
            if (targetNPC < 0 || targetNPC >= Main.maxNPCs || !Main.npc[targetNPC].active)
            {
                ReturnToIdle(player);
                return;
            }

            NPC target = Main.npc[targetNPC];
            
            // Check if target is too far from player
            if (Vector2.Distance(target.Center, player.Center) > MAX_DETECT_RANGE)
            {
                ReturnToIdle(player);
                return;
            }

            // Ensure ai[1] is set to current target
            Projectile.ai[1] = targetNPC;

            // Orbit around target while spinning
            Vector2 targetCenter = target.Center;
            float orbitRadius = 40f;
            float orbitSpeed = 0.15f;
            float angle = stateTimer * orbitSpeed;
            
            Vector2 orbitPos = targetCenter + new Vector2(
                (float)Math.Cos(angle) * orbitRadius,
                (float)Math.Sin(angle) * orbitRadius
            );
            
            Vector2 direction = (orbitPos - Projectile.Center).SafeNormalize(Vector2.Zero);
            Projectile.velocity = direction * 12f;
            
            // Spin rotation
            Projectile.rotation += 0.3f;
        }

        private void HandleReturningState(Player player)
        {
            // Clear target from ai[1] when returning
            Projectile.ai[1] = -1;
            
            Vector2 targetPos = player.Center + idleOffset;
            Vector2 direction = targetPos - Projectile.Center;
            
            if (direction.Length() < 30f)
            {
                currentState = AIState.Idle;
                stateTimer = 0;
                Projectile.netUpdate = true;
                return;
            }
            
            Projectile.velocity = direction.SafeNormalize(Vector2.Zero) * RETURN_SPEED;
            Projectile.rotation = direction.ToRotation();
        }

        private NPC FindNearestTarget(Player player)
        {
            // Get list of targets that other blades are already attacking
            var occupiedTargets = GetOccupiedTargets();
            
            NPC closestNPC = null;
            float closestDistance = MAX_DETECT_RANGE;
            
            // First pass: try to find an unoccupied target
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.CanBeChasedBy(this))
                {
                    float distanceToPlayer = Vector2.Distance(npc.Center, player.Center);
                    float distanceToProjectile = Vector2.Distance(npc.Center, Projectile.Center);
                    
                    if (distanceToPlayer < MAX_DETECT_RANGE && distanceToProjectile < closestDistance)
                    {
                        // Skip if another blade is already targeting this NPC
                        if (!occupiedTargets.Contains(i))
                        {
                            closestDistance = distanceToProjectile;
                            closestNPC = npc;
                        }
                    }
                }
            }
            
            // If no unoccupied targets found, fall back to any valid target
            if (closestNPC == null)
            {
                closestDistance = MAX_DETECT_RANGE;
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && npc.CanBeChasedBy(this))
                    {
                        float distanceToPlayer = Vector2.Distance(npc.Center, player.Center);
                        float distanceToProjectile = Vector2.Distance(npc.Center, Projectile.Center);
                        
                        if (distanceToPlayer < MAX_DETECT_RANGE && distanceToProjectile < closestDistance)
                        {
                            closestDistance = distanceToProjectile;
                            closestNPC = npc;
                        }
                    }
                }
            }
            
            return closestNPC;
        }

        private System.Collections.Generic.HashSet<int> GetOccupiedTargets()
        {
            var occupiedTargets = new System.Collections.Generic.HashSet<int>();
            
            // Check all other ElementalBlade projectiles owned by the same player
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && 
                    proj.type == Projectile.type && 
                    proj.owner == Projectile.owner && 
                    proj.identity != Projectile.identity) // Don't include this projectile
                {
                    // Get the target NPC ID from the other blade's ai[1] value
                    int otherTarget = (int)proj.ai[1];
                    if (otherTarget >= 0 && otherTarget < Main.maxNPCs && Main.npc[otherTarget].active)
                    {
                        occupiedTargets.Add(otherTarget);
                    }
                }
            }
            
            return occupiedTargets;
        }

        private void ReturnToIdle(Player player)
        {
            currentState = AIState.Returning;
            stateTimer = 0;
            targetNPC = -1;
            Projectile.ai[1] = -1; // Clear target from ai[1]
            CalculateIdlePosition(player);
            Projectile.netUpdate = true;
        }

        private void CalculateIdlePosition(Player player)
        {
            // Calculate wing-like formation for multiple swords
            int swordIndex = 0;
            int totalSwords = 0;
            
            // Count this sword's index among all ElementalBlade projectiles
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.type == Projectile.type && proj.owner == Projectile.owner)
                {
                    totalSwords++;
                    if (proj.identity < Projectile.identity)
                        swordIndex++;
                }
            }
            
            // Calculate wing position
            float wingSpread = WING_OFFSET_DISTANCE;
            float verticalOffset = -30f;
            
            if (totalSwords == 1)
            {
                idleOffset = new Vector2(-wingSpread * player.direction, verticalOffset);
            }
            else
            {
                // Wing formation for multiple swords
                float angleStep = MathHelper.PiOver4 / Math.Max(1, totalSwords - 1);
                float angle = -MathHelper.PiOver4 / 2f + angleStep * swordIndex;
                
                idleOffset = new Vector2(
                    (float)Math.Cos(angle) * wingSpread * -player.direction,
                    verticalOffset + (float)Math.Sin(angle) * 20f
                );
            }
        }

        private void CreateDustEffects()
        {
            // Create fire and ice particles based on current state
            if (Main.rand.NextBool(currentState == AIState.Spinning ? 2 : 4))
            {
                int dustType = Main.rand.NextBool() ? DustID.Torch : DustID.IceTorch;
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 
                    dustType, 0f, 0f, 100, default, 0.8f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Get projectile texture
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            
            // Drawing position
            Vector2 drawPos = Projectile.position - Main.screenPosition + drawOrigin;
            
            // Draw with a trail effect
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
            {
                // Calculate trail alpha and scale
                float trailOpacity = 0.8f * ((float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type]);
                Vector2 trailPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;
                Color trailColor = Projectile.GetAlpha(lightColor) * trailOpacity;
                
                // Draw trail
                Main.spriteBatch.Draw(texture, trailPos, null, trailColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            }
            
            // Draw main sprite with appropriate color
            Color elementColor = Color.Lerp(Color.OrangeRed, Color.SkyBlue, (float)Math.Sin(Main.GameUpdateCount * 0.05f) * 0.5f + 0.5f);
            Main.spriteBatch.Draw(texture, drawPos, null, Projectile.GetAlpha(elementColor), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0f);
            
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Apply fire or ice effects randomly
            if (Main.rand.NextBool())
            {
                target.AddBuff(BuffID.OnFire3, 180); // 3 seconds of On Fire!
            }
            else
            {
                target.AddBuff(BuffID.Frostburn2, 180); // 3 seconds of Frostburn
            }
        }
    }
}
