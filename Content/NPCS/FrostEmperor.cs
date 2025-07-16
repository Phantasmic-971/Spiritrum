using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.NPCS
{
    [AutoloadBossHead]
    public class FrostEmperor : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4; // Set number of animation frames
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            
            // Boss music
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 80;
            NPC.damage = 50; // Increased from 60
            NPC.defense = 60; // Increased from 40
            NPC.lifeMax = 20000; // Increased from 15000
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 25); // Increased from 15
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1; // Custom AI
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
        }

        private Player Target => Main.player[NPC.target];
        private bool secondPhase = false;
        private int attackTimer = 0;
        private int attackPattern = 0;
        private float orbitalAngle = 0f;
        private const float ORBITAL_DISTANCE = 200f;
        private Vector2 empressCenter = Vector2.Zero;
        private bool isAttacking = false;

        public override void AI()
        {
            // Check if Frost Empress is alive
            int empressIndex = NPC.FindFirstNPC(NPCType<FrostEmpress>());
            FrostEmpress empress = null;
            
            if (empressIndex != -1)
            {
                empress = Main.npc[empressIndex].ModNPC as FrostEmpress;
                empressCenter = Main.npc[empressIndex].Center;
            }

            // Target validation
            if (NPC.target < 0 || NPC.target == 255 || Target.dead || !Target.active)
            {
                NPC.TargetClosest();
            }

            if (Target.dead)
            {
                NPC.velocity.Y -= 0.04f;
                if (NPC.timeLeft > 10)
                {
                    NPC.timeLeft = 10;
                }
                return;
            }

            // Check for second phase transition - COMPLETE INVULNERABILITY AT 50% HEALTH
            if (empressIndex != -1 && (NPC.life <= NPC.lifeMax * 0.5f || Main.npc[empressIndex].life <= Main.npc[empressIndex].lifeMax * 0.5f))
            {
                if (!secondPhase)
                {
                    secondPhase = true;
                    // Make completely invulnerable at 50% health
                    NPC.dontTakeDamage = true;
                    NPC.life = (int)(NPC.lifeMax * 0.5f); // Lock health at 50%
                    // Trigger phase transition
                    TriggerPhaseTransition();
                }
            }

            if (!secondPhase)
            {
                DoPhaseOne(empress);
            }
            else
            {
                DoPhaseTwo(empress);
            }

            attackTimer++;
        }

        private void DoPhaseOne(FrostEmpress empress)
        {
            if (empress == null) return;

            // Orbital movement around empress
            orbitalAngle += 0.04f;
            Vector2 orbitalPosition = empressCenter + new Vector2(
                (float)Math.Cos(orbitalAngle) * ORBITAL_DISTANCE,
                (float)Math.Sin(orbitalAngle) * ORBITAL_DISTANCE
            );

            // Move towards orbital position with increased speed
            Vector2 moveDirection = (orbitalPosition - NPC.Center).SafeNormalize(Vector2.Zero);
            NPC.velocity = moveDirection * 8f; // Increased from 3f

            // Alternate attacking - only attack when empress is not attacking
            if (!empress.IsAttacking() && attackTimer > 45) // Much faster attacks (was 90)
            {
                isAttacking = true;
                DoRangedAttack();
                attackTimer = 0;
            }
            else
            {
                isAttacking = false;
            }

            // Set invulnerability when attacking
            NPC.dontTakeDamage = isAttacking;
        }

        private void DoPhaseTwo(FrostEmpress empress)
        {
            if (empress == null) return;

            // Both bosses move to center and perform synchronized attacks
            Vector2 centerPosition = Target.Center + new Vector2(0, -200);
            Vector2 moveDirection = (centerPosition - NPC.Center).SafeNormalize(Vector2.Zero);
            NPC.velocity = moveDirection * 2f;

            // Synchronized attack pattern
            if (attackTimer > 90) // 3 second attack cycle
            {
                if (attackTimer < 360) // Extended vulnerability window (10 seconds - 600 ticks plus 180 offset)
                {
                    NPC.dontTakeDamage = false;
                    // "Kiss" animation moment - both bosses are vulnerable for 10 seconds
                }
                else
                {
                    NPC.dontTakeDamage = true;
                    DoSynchronizedAttack();
                    
                    if (attackTimer > 270) // Extended total cycle (18 seconds)
                    {
                        attackTimer = 0;
                    }
                }
            }
            else
            {
                NPC.dontTakeDamage = true;
            }
        }

        private void DoRangedAttack()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                // Fire ice projectiles at the player - ENSURE HOSTILE
                Vector2 direction = (Target.Center - NPC.Center).SafeNormalize(Vector2.Zero);
                Vector2 velocity = direction * 10f; // Increased speed from 8f
                
                for (int i = 0; i < 5; i++) // Increased from 3
                {
                    Vector2 perturbedVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedVelocity, 
                        ProjectileID.FrostBeam, NPC.damage / 3, 1f, Main.myPlayer); // Ensure hostile to player
                }
            }
        }

        private void DoSynchronizedAttack()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient && attackTimer % 60 == 0)
            {
                // Fire ice shards in a pattern - ENSURE HOSTILE
                for (int i = 0; i < 8; i++)
                {
                    Vector2 velocity = new Vector2(6f, 0).RotatedBy(MathHelper.TwoPi / 8 * i);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, 
                        ProjectileID.FrostShard, NPC.damage / 3, 1f, Main.myPlayer); // Ensure hostile to player
                }
            }
        }

        private void TriggerPhaseTransition()
        {
            // Spawn the merged boss
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int mergedBoss = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, 
                    NPCType<FrostEmpire>());
                
                // Remove both individual bosses
                NPC.active = false;
                int empressIndex = NPC.FindFirstNPC(NPCType<FrostEmpress>());
                if (empressIndex != -1)
                {
                    Main.npc[empressIndex].active = false;
                }
            }
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[Type])
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool CheckDead()
        {
            if (!secondPhase)
            {
                // Don't die in phase 1, trigger phase 2 instead
                NPC.life = 1;
                if (!secondPhase)
                {
                    secondPhase = true;
                    TriggerPhaseTransition();
                }
                return false;
            }
            return true;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Add boss drops here when needed
        }
    }
}
