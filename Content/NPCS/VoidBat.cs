using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;

namespace Spiritrum.Content.NPCS
{
    public class VoidBat : ModNPC
    {
        private int attackTimer = 0;
        private Vector2 targetPosition = Vector2.Zero;
        private bool isCharging = false;
        private int chargeTimer = 0;
        private int lifespan = 0;
        
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 4; // 4 frame animation
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f,
                Direction = 1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 32;
            NPC.damage = Main.dayTime ? 40 : 60; // Less damage during day
            NPC.defense = 15;
            NPC.lifeMax = 1200; // Moderate health for a summoned minion
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath4;
            NPC.value = Item.buyPrice(0, 0, 30, 0); // Small reward
            NPC.knockBackResist = 0.3f; // Some knockback resistance
            NPC.aiStyle = -1; // Custom AI
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            
            // Make it glow slightly
            NPC.alpha = 50;
            
            // Immune to some debuffs
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            
            // No banner for boss minions
            Banner = 0;
            BannerItem = 0;
        }

        public override void FindFrame(int frameHeight)
        {
            // Simple flying animation
            NPC.frameCounter += 0.2f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            // Find the player to target
            Player target = Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)];
            if (!target.active || target.dead)
            {
                // Despawn if no valid target
                NPC.velocity.Y -= 0.2f;
                if (NPC.timeLeft > 60)
                    NPC.timeLeft = 60;
                return;
            }

            // Face the target
            NPC.direction = NPC.spriteDirection = NPC.Center.X < target.Center.X ? 1 : -1;

            attackTimer++;
            lifespan++;

            // AI states
            if (!isCharging)
            {
                // Hovering/positioning phase
                Vector2 idealPosition = target.Center + new Vector2(
                    Main.rand.Next(-250, 250),
                    Main.rand.Next(-180, -80)
                );

                // Move towards ideal position
                Vector2 direction = idealPosition - NPC.Center;
                float distance = direction.Length();

                if (distance > 30f)
                {
                    direction.Normalize();
                    float speed = Math.Min(distance / 25f, 5f);
                    NPC.velocity = Vector2.Lerp(NPC.velocity, direction * speed, 0.08f);
                }
                else
                {
                    NPC.velocity *= 0.95f;
                }

                // Decide when to charge (give warning time)
                if (attackTimer > 150 && Main.rand.NextBool(240)) // Less frequent charges
                {
                    isCharging = true;
                    chargeTimer = 0;
                    targetPosition = target.Center;
                    attackTimer = 0;
                    
                    // Play warning sound
                    SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                    
                    // Create warning dust
                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                            DustID.PurpleTorch, 0f, 0f, 0, default, 1.8f);
                        dust.noGravity = true;
                        dust.velocity *= 0.3f;
                    }
                }
            }
            else
            {
                // Charging phase
                chargeTimer++;
                
                if (chargeTimer < 45) // Longer warning time
                {
                    // Brief pause before charging
                    NPC.velocity *= 0.92f;
                    
                    // Create charging effect
                    if (chargeTimer % 8 == 0)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                                DustID.PurpleTorch, 0f, 0f, 0, default, 1.5f);
                            dust.noGravity = true;
                            dust.velocity = (NPC.Center - dust.position) * 0.08f;
                        }
                    }
                }
                else if (chargeTimer == 45)
                {
                    // Launch the charge (slightly slower)
                    Vector2 chargeDirection = (targetPosition - NPC.Center).SafeNormalize(Vector2.Zero);
                    NPC.velocity = chargeDirection * 12f; // Reduced from 15f
                    
                    // Play charge sound
                    SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                }
                else if (chargeTimer > 45 && chargeTimer < 105)
                {
                    // Maintain charge velocity but slow down gradually
                    NPC.velocity *= 0.97f;
                    
                    // Create trail effect
                    if (Main.rand.NextBool(3))
                    {
                        Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                            DustID.PurpleTorch, 0f, 0f, 0, default, 1f);
                        dust.noGravity = true;
                        dust.velocity = -NPC.velocity * 0.2f;
                    }
                }
                else
                {
                    // End charge, return to hovering
                    isCharging = false;
                    chargeTimer = 0;
                    NPC.velocity *= 0.9f;
                }
            }

            // Create ambient void particles
            if (Main.rand.NextBool(12))
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                    DustID.PurpleTorch, 0f, 0f, 0, default, 0.8f);
                dust.noGravity = true;
                dust.velocity *= 0.2f;
            }

            // Add light
            Lighting.AddLight(NPC.Center, 0.3f, 0.1f, 0.4f);

            // Despawn after 25 seconds
            if (lifespan > 1500) // 25 seconds at 60 FPS
            {
                NPC.velocity.Y -= 0.15f;
                if (NPC.timeLeft > 90)
                    NPC.timeLeft = 90;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            // Apply a minor debuff when hitting the player
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Darkness, 120); // 2 seconds of darkness
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            // Create void dust when hit
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                    DustID.PurpleTorch, hit.HitDirection * 2f, -1f, 0, default, 1f);
                dust.noGravity = true;
            }

            // If killed, create death effect
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                        DustID.PurpleTorch, Main.rand.Next(-3, 4), Main.rand.Next(-3, 4), 0, default, 1.5f);
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
                
                SoundEngine.PlaySound(SoundID.NPCDeath39, NPC.position);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f; // Only spawned by VoidHarbinger
        }
    }
}
