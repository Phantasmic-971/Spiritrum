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
    public class FrostEmpress : ModNPC
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
            NPC.damage = 60; // Increased from 70
            NPC.defense = 55; // Increased from 35
            NPC.lifeMax = 35000; // Increased from 15000
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
        private float orbitalAngle = MathHelper.Pi; // Start opposite to emperor
        private const float ORBITAL_DISTANCE = 200f;
        private Vector2 emperorCenter = Vector2.Zero;
        private bool isAttacking = false;
        private bool isDashing = false;
        private Vector2 dashTarget = Vector2.Zero;
        private int dashTimer = 0;

        public override void AI()
        {
            // Check if Frost Emperor is alive
            int emperorIndex = NPC.FindFirstNPC(NPCType<FrostEmperor>());
            FrostEmperor emperor = null;
            
            if (emperorIndex != -1)
            {
                emperor = Main.npc[emperorIndex].ModNPC as FrostEmperor;
                emperorCenter = Main.npc[emperorIndex].Center;
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
            if (emperorIndex != -1 && (NPC.life <= NPC.lifeMax * 0.5f || Main.npc[emperorIndex].life <= Main.npc[emperorIndex].lifeMax * 0.5f))
            {
                if (!secondPhase)
                {
                    secondPhase = true;
                    // Make completely invulnerable at 50% health
                    NPC.dontTakeDamage = true;
                    NPC.life = (int)(NPC.lifeMax * 0.5f); // Lock health at 50%
                    // Phase transition will be handled by emperor
                }
            }

            if (!secondPhase)
            {
                DoPhaseOne(emperor);
            }

            attackTimer++;
        }

        private void DoPhaseOne(FrostEmperor emperor)
        {
            if (emperor == null) return;

            if (!isDashing)
            {
                // Orbital movement around emperor
                orbitalAngle += 0.02f;
                Vector2 orbitalPosition = emperorCenter + new Vector2(
                    (float)Math.Cos(orbitalAngle) * ORBITAL_DISTANCE,
                    (float)Math.Sin(orbitalAngle) * ORBITAL_DISTANCE
                );

                // Move towards orbital position with increased speed
                Vector2 moveDirection = (orbitalPosition - NPC.Center).SafeNormalize(Vector2.Zero);
                NPC.velocity = moveDirection * 8f; // Increased from 3f

                // Prepare to dash when emperor is not attacking
                if (!emperor.IsAttacking() && attackTimer > 120) // Faster dash timing (reduced from 180)
                {
                    StartDash();
                    attackTimer = 0;
                }
                else
                {
                    isAttacking = false;
                }
            }
            else
            {
                DoDash();
            }

            // Set invulnerability when attacking
            NPC.dontTakeDamage = isAttacking;
        }

        private void StartDash()
        {
            isDashing = true;
            isAttacking = true;
            dashTimer = 0;
            dashTarget = Target.Center;
            
            // Play dash sound
            SoundEngine.PlaySound(SoundID.Item60, NPC.position);
        }

        private void DoDash()
        {
            dashTimer++;

            if (dashTimer < 60) // Wind up
            {
                NPC.velocity *= 0.80f;
            }
            else if (dashTimer < 180) // Dash
            {
                Vector2 dashDirection = (dashTarget - NPC.Center).SafeNormalize(Vector2.Zero);
                NPC.velocity = dashDirection * 40f;

                // Create frost trail - ENSURE HOSTILE, FrostWave slowly accelerates
                if (Main.netMode != NetmodeID.MultiplayerClient && dashTimer % 5 == 0)
                {
                    // Give FrostWaves velocity in perpendicular directions to the dash
                    Vector2 perpendicular = new Vector2(-dashDirection.Y, dashDirection.X) * Main.rand.NextFloat(2f, 5f);
                    Vector2 waveVelocity = perpendicular + dashDirection * Main.rand.NextFloat(0.5f, 2f);
                    int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, waveVelocity, 
                        ProjectileID.FrostWave, NPC.damage / 2, 1f, Main.myPlayer);
                    if (proj >= 0 && proj < Main.maxProjectiles)
                    {
                        Main.projectile[proj].aiStyle = 0; // Custom aiStyle
                        Main.projectile[proj].ai[0] = 0f; // Use ai[0] as acceleration timer
                        // Make FrostWave point towards its velocity direction, with a 90-degree offset
                        Main.projectile[proj].rotation = waveVelocity.ToRotation() + MathHelper.PiOver2;
                    }
                }
            }
            else if (dashTimer < 300) // Post-dash: Burst attack
            {
                NPC.velocity *= 0.8f;
                int burstTime = dashTimer - 180;
                // Fire a burst of FrostWaves, firerate increases over time
                int fireRate = Math.Max(30 - burstTime / 10, 5); // Starts at 30, minimum 5
                if (burstTime % fireRate == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toPlayer = (Target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    float spread = MathHelper.ToRadians(20);
                    int numProjectiles = 3; 
                    for (int i = 0; i < numProjectiles; i++)
                    {
                        float rotation = spread * (i - (numProjectiles - 1) / 2f);
                        Vector2 perturbed = toPlayer.RotatedBy(rotation) * 6f;
                        int proj = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbed, ProjectileID.FrostWave, NPC.damage / 2, 1f, Main.myPlayer);
                        if (proj >= 0 && proj < Main.maxProjectiles)
                        {
                            Main.projectile[proj].aiStyle = 0;
                            Main.projectile[proj].ai[0] = 0f;
                            // Make FrostWave point towards its velocity direction, with a 90-degree offset
                            Main.projectile[proj].rotation = perturbed.ToRotation() + MathHelper.PiOver2;
                        }
                    }
                }
            }
            else // End dash and burst
            {
                isDashing = false;
                isAttacking = false;
                dashTimer = 0;
                NPC.velocity *= 0.8f;
            }
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (isDashing)
            {
                // Add piercing effect during dash
                target.AddBuff(BuffID.Chilled, 300);
            }
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
                // Don't die in phase 1, let emperor handle phase transition
                NPC.life = 1;
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
