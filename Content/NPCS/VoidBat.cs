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
            Main.npcFrameCount[Type] = 5;
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
            NPC.damage = Main.dayTime ? 40 : 60;
            NPC.defense = 15;
            NPC.lifeMax = 1200;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath4;
            NPC.value = Item.buyPrice(0, 0, 30, 0);
            NPC.knockBackResist = 0.3f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.alpha = 50;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.25f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }

        public override void AI()
        {
            Player target = Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)];
            if (!target.active || target.dead)
            {
                NPC.velocity.Y -= 0.2f;
                if (NPC.timeLeft > 60)
                    NPC.timeLeft = 60;
                return;
            }

            NPC.direction = NPC.spriteDirection = NPC.Center.X < target.Center.X ? 1 : -1;

            attackTimer++;
            lifespan++;
            
            if (!isCharging)
            {
                Vector2 idealPosition = target.Center + new Vector2(
                    Main.rand.Next(-250, 250),
                    Main.rand.Next(-180, -80)
                );

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

                if (attackTimer > 150 && Main.rand.NextBool(240))
                {
                    isCharging = true;
                    chargeTimer = 0;
                    targetPosition = target.Center;
                    attackTimer = 0;

                    SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                    
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
                chargeTimer++;
                
                if (chargeTimer < 45)
                {
                    NPC.velocity *= 0.92f;
                    
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
                else if (chargeTimer == 60)
                {
                    Vector2 chargeDirection = (targetPosition - NPC.Center).SafeNormalize(Vector2.Zero);
                    NPC.velocity = chargeDirection * 12f; // Reduced from 15f
                    
                    SoundEngine.PlaySound(SoundID.Item1, NPC.position);
                }
                else if (chargeTimer > 45 && chargeTimer < 105)
                {
                    NPC.velocity *= 0.97f;
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
                    isCharging = false;
                    chargeTimer = 0;
                    NPC.velocity *= 0.9f;
                }
            }

            if (Main.rand.NextBool(12))
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                    DustID.PurpleTorch, 0f, 0f, 0, default, 0.8f);
                dust.noGravity = true;
                dust.velocity *= 0.2f;
            }

            // Add light
            Lighting.AddLight(NPC.Center, 0.3f, 0.1f, 0.4f);

            if (lifespan > 1500)
            {
                NPC.velocity.Y -= 0.15f;
                if (NPC.timeLeft > 90)
                    NPC.timeLeft = 90;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Darkness, 120);
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                    DustID.PurpleTorch, hit.HitDirection * 2f, -1f, 0, default, 1f);
                dust.noGravity = true;
            }
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
            return 0f;
        }
    }
}
