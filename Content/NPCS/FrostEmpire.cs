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
    public class FrostEmpire : ModNPC
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

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("The Frost Emperor and the Frost Empress are ancient rulers of a long-lost frozen kingdom. Their love is eternal — cold and majestic, like their realm itself. They bound together even in darkness. Their daughter, the Frost Queen, is just as cold and merciless. She chose the path of destructive power, and her parents proudly stood by her side. Together, they see good not as salvation, but as a threat to the order they seek to restore to the world.")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 120;
            NPC.damage = 60;
            NPC.defense = 60;
            NPC.lifeMax = 25000; // Combined health for phase 2
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 30);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 25f;
            NPC.aiStyle = -1; // Custom AI
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
        }

        private Player Target => Main.player[NPC.target];
        private int attackTimer = 0;
        private int attackCycle = 0;
        private bool isVulnerable = false;
        private bool isKissing = false;
        private int kissTimer = 0;
        private Vector2 hoverPosition = Vector2.Zero;

        public override void AI()
        {
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

            // Update hover position
            hoverPosition = Target.Center + new Vector2(0, -250);

            // Main attack cycle
            attackTimer++;
            
            if (attackTimer <= 180) // 3 seconds - attack phase
            {
                DoAttackPhase();
                isVulnerable = false;
                isKissing = false;
                NPC.dontTakeDamage = true;
            }
            else if (attackTimer <= 420) // 10 seconds - vulnerable "kiss" phase (600 ticks = 10 seconds)
            {
                DoKissPhase();
                isVulnerable = true;
                isKissing = true;
                NPC.dontTakeDamage = false;
            }
            else // Reset cycle
            {
                attackTimer = 0;
                attackCycle++;
                isVulnerable = false;
                isKissing = false;
            }
        }

        private void DoAttackPhase()
        {
            // Move to hover position above player
            Vector2 moveDirection = (hoverPosition - NPC.Center).SafeNormalize(Vector2.Zero);
            NPC.velocity = Vector2.Lerp(NPC.velocity, moveDirection * 4f, 0.1f);

            // Synchronized attacks every 30 frames
            if (attackTimer % 30 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                switch (attackCycle % 4)
                {
                    case 0:
                        DoIceShardSpiral();
                        break;
                    case 1:
                        DoFrostBeamBarrage();
                        break;
                    case 2:
                        DoBlizzardAttack();
                        break;
                    case 3:
                        DoIceSpearRain();
                        break;
                }
            }
        }

        private void DoKissPhase()
        {
            kissTimer++;
            
            // Slow down movement during kiss
            NPC.velocity *= 0.95f;
            
            // Create hearts effect during kiss
            if (kissTimer % 10 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 heartVelocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-3f, -1f));
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center + new Vector2(Main.rand.Next(-40, 40), 0), 
                    heartVelocity, ProjectileID.FairyQueenMagicItemShot, 0, 0f); // Non-damaging heart effect
            }
            
            if (kissTimer >= 60)
            {
                kissTimer = 0;
            }
        }

        private void DoIceShardSpiral()
        {
            for (int i = 0; i < 12; i++)
            {
                float angle = MathHelper.TwoPi / 12 * i + attackTimer * 0.1f;
                Vector2 velocity = new Vector2(12f, 0).RotatedBy(angle);
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, 
                    ProjectileID.FrostShard, NPC.damage / 6, 1f, Main.myPlayer); // Ensure hostile
            }
        }

        private void DoFrostBeamBarrage()
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 targetPosition = Target.Center + new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-100, 100));
                Vector2 direction = (targetPosition - NPC.Center).SafeNormalize(Vector2.Zero);
                Vector2 velocity = direction * 15f;
                
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, 
                    ProjectileID.FrostBeam, NPC.damage / 3, 1f, Main.myPlayer); // Ensure hostile
            }
        }

        private void DoBlizzardAttack()
        {
            // Create blizzard effect around the player
            for (int i = 0; i < 3; i++)
            {
                Vector2 spawnPosition = Target.Center + new Vector2(Main.rand.Next(-400, 400), -300);
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(4f, 8f));
                
                Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPosition, velocity, 
                    ProjectileID.IcewaterSpit, NPC.damage / 3, 1f, Main.myPlayer); // Ensure hostile
            }
        }

        private void DoIceSpearRain()
        {
            // Rain of ice spears from above
            for (int i = 0; i < 6; i++)
            {
                Vector2 spawnPosition = Target.Center + new Vector2(Main.rand.Next(-250, 250), -400);
                Vector2 velocity = new Vector2(0, 12f);
                
                Projectile.NewProjectile(NPC.GetSource_FromAI(), spawnPosition, velocity, 
                    ProjectileID.IceSpike, NPC.damage / 3, 1f, Main.myPlayer); // Ensure hostile
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (isKissing)
            {
                // Use specific frames for kiss animation
                NPC.frame.Y = frameHeight * 3; // Assume frames 4-5 are kiss frames
                
                NPC.frameCounter++;
                if (NPC.frameCounter >= 15)
                {
                    NPC.frameCounter = 0;
                    if (NPC.frame.Y == frameHeight * 3)
                        NPC.frame.Y = frameHeight * 4;
                    else
                        NPC.frame.Y = frameHeight * 3;
                }
            }
            else
            {
                // Normal animation
                NPC.frameCounter++;
                if (NPC.frameCounter >= 4)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y >= frameHeight * 4) // Use first 4 frames for normal animation
                    {
                        NPC.frame.Y = 0;
                    }
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Add exclusive drops for the merged boss
            // You can add special weapons, accessories, or materials here
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 offset)
        {
            if (isKissing)
            {
                scale = 1.5f + (float)Math.Sin(Main.GameUpdateCount * 0.2f) * 0.1f;
            }
            return null;
        }
    }
}
