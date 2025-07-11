using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Spiritrum.Content.Items.Weapons;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.NPCS
{
    public class VoidHarbinger : ModNPC
    {
        // Boss variables
        private const int PhaseOneMaxLife = 60000;
        private const int PhaseTwoMaxLife = 30000; // 50% health threshold
        private bool secondPhase = false;
          // AI variables
        private int attackCounter = 0;
        private int attackCycle = 0; // Track  which attack pattern to use
        private float attackSpeed = 1f;
        
        // Attack pattern variables
        private int currentAttack = 0;
        private int attackDuration = 0;
        private int teleportCount = 0;
        private Vector2 targetPosition = Vector2.Zero;
        
        // New tracking variables for advanced mechanics
        private bool spawnedClones = false;
        private bool activatedDesperation = false;
        private int desperationPhaseTimer = 0;
        private int hitsTaken = 0;
        private int consecutiveDodges = 0;
        private bool playerMarked = false;
        private Vector2 markedPosition = Vector2.Zero;
        private int markTimer = 0;
        private List<Vector2> voidZones = new List<Vector2>();
        private int adaptiveCounter = 0;
        private bool gravitationActive = false;
        private int gravityFlipTimer = 0;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Harbinger");
            Main.npcFrameCount[Type] = 4; // The number of frames the NPC has

            // Add to boss head icon list
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            
            // Boss-specific properties
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;            // Add a special entry to the bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                // Influence how the NPC looks in the Bestiary
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking
                Direction = 1, // -1 is left and 1 is right. NPCs are drawn facing the left by default
                Scale = 1.2f, // Make it appear larger in the bestiary
                PortraitPositionYOverride = -20f // Adjust the y position in the bestiary
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);        }        public override void SetDefaults()
        {
            NPC.width = 80;
            NPC.height = 96;
            NPC.damage = Main.dayTime ? 60 : 200; // Reduced from 80/300 to 60/200
            NPC.defense = 35;
            NPC.lifeMax = PhaseOneMaxLife;
            NPC.HitSound = SoundID.NPCHit54; // Shadow wraith hit sound
            NPC.DeathSound = SoundID.NPCDeath52; // Shadow wraith death sound
            NPC.value = Item.buyPrice(0, 20, 0, 0); // 20 gold
            NPC.knockBackResist = 0f; // Immune to knockback
            NPC.aiStyle = -1; // Custom AI
            NPC.noGravity = true;
            NPC.noTileCollide = true; // Can float through blocks
            
            // Boss flags
            NPC.boss = true;
            NPC.npcSlots = 10f; // Takes up multiple spawn slots, reducing other spawns
            NPC.lavaImmune = true;
            
            // Make it glow in the dark
            NPC.alpha = 100; // Slightly transparent
              // Make it immune to most debuffs (as a proper boss should be)
            for (int i = 0; i < BuffLoader.BuffCount; i++)
            {
                NPC.buffImmune[i] = true;
            }
            
            // No banner for bosses
            Banner = 0;
            BannerItem = 0;
            
            // Set boss music
            if (!Main.dedServ)
            {
                 Music = MusicID.Boss2;      
            }  
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                // Use boss category instead of biome categories
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,                // It's a boss
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                // Updated flavor text to reflect boss status
                new FlavorTextBestiaryInfoElement("A powerful entity from the void between dimensions, whose true power can only be witnessed after defeating the Lunatic Cultist. Its arrival heralds calamity as it opens rifts to drain the world's life energy. Only those brave enough to challenge this cosmic horror can prevent it from consuming all.")
            });
        }
        
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Prevent natural spawning; only allow summoning with the VoidCatalyst
            return 0f;
        }

        public override void FindFrame(int frameHeight)
        {
            // This makes the sprite animate
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int frame = (int)NPC.frameCounter;
            NPC.frame.Y = frame * frameHeight;
        }        public override void AI()
        {
            // Update damage based on time of day - 200 damage at night, 60 damage during day
            if (Main.dayTime && NPC.damage > 120)
            {
                NPC.damage = 120; // Reduced from 180
            }
            else if (!Main.dayTime && NPC.damage < 6000)
            {
                NPC.damage = 6000; // Reduced from 9900
            }
            
            // Boss despawn logic
            Player target = Main.player[NPC.target];
            if (!target.active || target.dead)
            {
                // If the target isn't active or is dead, the boss despawns
                NPC.TargetClosest(false);
                if (!target.active || target.dead)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                    if (NPC.timeLeft > 20)
                    {
                        NPC.timeLeft = 20;
                    }
                    return;
                }
            }
            
            // Phase transition logic
            if (!secondPhase && NPC.life <= NPC.lifeMax * 0.5f)
            {
                // Transition to second phase at 50% health
                secondPhase = true;
                NPC.dontTakeDamage = true; // Briefly immune while transitioning
                
                // Visual effects for phase transition
                for (int i = 0; i < 50; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(10f, 10f);
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 2f);
                }
                
                // Sound effect for phase transition
                SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                
                // Reset attack pattern
                attackCounter = 0;
                attackCycle = 0;
                attackDuration = 0;
                attackSpeed = 1.5f; // Faster in second phase
                currentAttack = 0;
                teleportCount = 0;
                
                // Special phase transition effect - void implosion
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // Create a ring of void projectiles that implode
                    int projectileCount = 12; // Reduced from 16
                    for (int i = 0; i < projectileCount; i++)
                    {
                        float rotation = MathHelper.TwoPi * i / projectileCount;
                        Vector2 velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * -8f; // Negative to implode
                        
                        int damage = NPC.damage / 4; // Reduced damage for transition attack
                        int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                        
                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            NPC.Center + new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 300, // Start far away
                            velocity,
                            projectileType,
                            damage,
                            0f,
                            Main.myPlayer
                        );
                    }
                }
            }
            
            // Make dust particles to show it's from the void (increased in phase 2)
            int dustRate = secondPhase ? 2 : 5;
            if (Main.rand.NextBool(dustRate))
            {
                Dust dust = Dust.NewDustDirect(
                    NPC.position,
                    NPC.width,
                    NPC.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, secondPhase ? 2f : 1.5f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }

            // Make it emit light (brighter in phase 2)
            Lighting.AddLight(NPC.Center, secondPhase ? 0.7f : 0.5f, 0.2f, secondPhase ? 0.9f : 0.7f);
            
            // Remove damage immunity flag after a brief moment
            if (NPC.dontTakeDamage)
            {
                NPC.ai[3]++;
                if (NPC.ai[3] >= 60) // 1 second of immunity
                {
                    NPC.dontTakeDamage = false;
                    NPC.ai[3] = 0;
                }
            }

            // Increment attack counter
            attackCounter++;
            
            // Look at the player
            NPC.direction = NPC.spriteDirection = NPC.Center.X < target.Center.X ? 1 : -1;
            
            // Different attack patterns based on phase
            if (!secondPhase)
            {
                // First phase attacks - more predictable patterns
                FirstPhaseAttacks(target);
            }
            else
            {
                // Second phase attacks - more aggressive and varied
                SecondPhaseAttacks(target);
            }
        }
          private void FirstPhaseAttacks(Player target)
        {
            // First phase uses a cycle of 5 different attack patterns
            if (attackDuration <= 0)
            {
                // Time to switch to the next attack pattern
                currentAttack = (currentAttack + 1) % 5;
                
                // Set up the new attack pattern
                switch (currentAttack)
                {                case 0: // Orbit and shoot attack
                        attackDuration = 300; // Increased from 240 for more predictable patterns
                        targetPosition = target.Center;
                        NPC.velocity = Vector2.Zero;
                        break;
                        
                    case 1: // Teleport and dash attack
                        attackDuration = 260; // Increased from 200 to slow down teleport rate
                        teleportCount = 0;
                        break;
                        
                    case 2: // Void sphere attack
                        attackDuration = 240; // Increased from 180
                        NPC.velocity = Vector2.Zero;
                        break;
                        
                    case 3: // Void bat summoning attack
                        attackDuration = 320; // Long duration for summoning
                        NPC.velocity = Vector2.Zero;
                        break;
                        
                    case 4: // Pursuit attack
                        attackDuration = 280; // Increased from 220
                        break;
                }
                
                // Reset counter for the new attack
                attackCounter = 0;
            }
            
            // Execute the current attack pattern
            switch (currentAttack)
            {
                case 0: // Orbit and shoot attack
                    OrbitalAttack(target);
                    break;
                    
                case 1: // Teleport and dash attack
                    TeleportDashAttack(target);
                    break;
                    
                case 2: // Void sphere attack
                    VoidSphereAttack(target);
                    break;
                    
                case 3: // Void bat summoning attack
                    VoidBatSummonAttack(target);
                    break;
                    
                case 4: // Pursuit attack
                    PursuitAttack(target);
                    break;
            }
            
            // Decrease the attack duration
            attackDuration--;
        }
        
        private void OrbitalAttack(Player target)
        {
            // Orbit around the player while shooting projectiles
            float orbitSpeed = 0.03f;
            float orbitRadius = 300f;
            
            // Calculate orbital position
            float angle = orbitSpeed * attackCounter;
            Vector2 orbitPosition = target.Center + new Vector2(
                (float)Math.Cos(angle) * orbitRadius,
                (float)Math.Sin(angle) * orbitRadius
            );
            
            // Move smoothly to the orbit position
            Vector2 moveDirection = orbitPosition - NPC.Center;
            float distance = moveDirection.Length();
            
            if (distance > 20f)
            {
                moveDirection.Normalize();
                float speed = Math.Min(distance / 10f, 8f);
                NPC.velocity = moveDirection * speed;
            }
            else
            {
                NPC.velocity *= 0.9f;
            }
            
            // Fire void projectiles at intervals
            if (attackCounter % 60 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 toPlayer = target.Center - NPC.Center;
                toPlayer.Normalize();
                int damage = NPC.damage / 4;
                int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                
                // Fire 3 void projectiles in a spread
                float spread = MathHelper.ToRadians(15);
                Vector2 velocity = toPlayer * 8f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, projectileType, damage, 0f, Main.myPlayer);
                
                Vector2 velocity2 = toPlayer.RotatedBy(spread) * 8f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity2, projectileType, damage, 0f, Main.myPlayer);
                
                Vector2 velocity3 = toPlayer.RotatedBy(-spread) * 8f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity3, projectileType, damage, 0f, Main.myPlayer);
            }
        }
          private void TeleportDashAttack(Player target)
        {
            // Teleport around the player and dash periodically
            int teleportInterval = 90; // Increased from 60 to reduce teleport frequency
            
            if (attackCounter % teleportInterval == 0)
            {                // Create teleport dust 1 second before teleporting (warning indicator)
                if (attackCounter > 0) // Skip first teleport warning
                {
                    Vector2 warningTeleportPos;
                    float warningDistance = Main.rand.Next(200, 350);
                    float warningAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                    warningTeleportPos = target.Center + new Vector2((float)Math.Cos(warningAngle) * warningDistance, (float)Math.Sin(warningAngle) * warningDistance);
                    
                    // Make sure it's in a valid location
                    if (!Collision.SolidCollision(warningTeleportPos, NPC.width, NPC.height))
                    {
                        // Create warning dust effect at future teleport location
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Dust.NewDustDirect(
                                warningTeleportPos - new Vector2(NPC.width/2, NPC.height/2),
                                NPC.width, NPC.height, 
                                DustID.PurpleTorch, 0f, 0f, 0, default, 2f);
                            dust.noGravity = true;
                            dust.velocity *= 0.5f;
                        }
                    }
                }
                
                // Create vanishing effect
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 2f);
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
                
                // Play teleport sound
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                
                // Choose teleport position (somewhere near the player but not too close)
                Vector2 teleportPos;
                float distance = Main.rand.Next(200, 350);
                float angle = Main.rand.NextFloat() * MathHelper.TwoPi;
                teleportPos = target.Center + new Vector2((float)Math.Cos(angle) * distance, (float)Math.Sin(angle) * distance);
                
                // Make sure it's in a valid location
                if (!Collision.SolidCollision(teleportPos, NPC.width, NPC.height))
                {
                    NPC.Center = teleportPos;
                }
                
                // Create appearing effect
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 2f);
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
                
                // Stop velocity after teleport
                NPC.velocity = Vector2.Zero;
                teleportCount++;
            }
            else if (attackCounter % teleportInterval == 20)
            {
                // Dash towards player after a delay
                Vector2 toPlayer = target.Center - NPC.Center;
                toPlayer.Normalize();
                NPC.velocity = toPlayer * 12f;
                
                // Play dash sound
                SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                
                // Create dash effect
                for (int i = 0; i < 10; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, NPC.velocity.X * 0.4f, NPC.velocity.Y * 0.4f, 0, default, 1.5f);
                    dust.noGravity = true;
                }
            }
            else if (attackCounter % teleportInterval > 40)
            {
                // Slow down after dash
                NPC.velocity *= 0.95f;
            }
            
            // After a certain number of teleports, end this attack early
            if (teleportCount >= 3)
            {
                attackDuration = 1; // End soon
            }
        }
        
        private void VoidSphereAttack(Player target)
        {
            // Float in one place and summon void spheres that orbit before converging
            
            // Slowly move to a position above the player
            Vector2 hoverPos = target.Center - new Vector2(0, 150);
            Vector2 toHoverPos = hoverPos - NPC.Center;
            float distToHover = toHoverPos.Length();
            
            if (distToHover > 50)
            {
                toHoverPos.Normalize();
                NPC.velocity = toHoverPos * Math.Min(distToHover / 20f, 6f);
            }
            else
            {
                NPC.velocity *= 0.9f;
            }
            
            // Channeling visual effect
            if (attackCounter < 90)
            {
                if (attackCounter % 15 == 0)
                {
                    // Create pulsating effect while charging
                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(40f, 40f);
                        Vector2 dustVel = (NPC.Center - dustPos) * 0.1f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, dustVel, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                }
            }
            else if (attackCounter == 90)
            {
                // After charging, summon void spheres that orbit
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int sphereCount = 6;
                    int damage = NPC.damage / 4;
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                    
                    // Create a ring of orbiting projectiles
                    for (int i = 0; i < sphereCount; i++)
                    {
                        float angle = MathHelper.TwoPi * i / sphereCount;
                        Vector2 offset = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 150f;
                        Vector2 velocity = offset.RotatedBy(MathHelper.PiOver2) * 0.05f; // Make them orbit
                        
                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            NPC.Center + offset,
                            velocity,
                            projectileType,
                            damage,
                            0f,
                            Main.myPlayer,
                            ai0: 1f, // Special AI flag for orbital behavior
                            ai1: 0f
                        );
                    }
                    
                    // Play a sound effect
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                }
            }
            else if (attackCounter == 150)
            {
                // After orbiting, make the spheres converge on the player
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // Find all the orbiting projectiles and redirect them
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile proj = Main.projectile[i];
                        if (proj.active && proj.type == projectileType && proj.ai[0] == 1f)
                        {
                            // Redirect to the player
                            Vector2 toPlayer = target.Center - proj.Center;
                            toPlayer.Normalize();
                            proj.velocity = toPlayer * 8f;
                            proj.ai[0] = 2f; // Change state to homing
                            
                            // Create a trail effect
                            for (int d = 0; d < 5; d++)
                            {
                                Dust dust = Dust.NewDustDirect(proj.position, proj.width, proj.height, DustID.PurpleTorch, 0f, 0f, 0, default, 1.5f);
                                dust.noGravity = true;
                                dust.velocity = -proj.velocity * 0.5f;
                            }
                        }
                    }
                    
                    // Play a sound effect
                    SoundEngine.PlaySound(SoundID.Item9, NPC.position);
                }
            }
        }
        
        private void PursuitAttack(Player target)
        {
            // Aggressively chase the player while periodically firing projectiles
            
            Vector2 toPlayer = target.Center - NPC.Center;
            float distance = toPlayer.Length();
            toPlayer.Normalize();
            
            // Move faster when farther away
            float speed = Math.Min(distance / 40f, 7f);
            NPC.velocity = (NPC.velocity * 10f + toPlayer * speed) / 11f;
            
            // Periodic attacks while pursuing
            if (attackCounter % 40 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int damage = NPC.damage / 4;
                int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                
                // Fire a volley of projectiles in the player's direction
                int projectileCount = 3;
                float spreadAngle = MathHelper.ToRadians(30);
                float startAngle = -spreadAngle / 2;
                
                for (int i = 0; i < projectileCount; i++)
                {
                    float angle = startAngle + (spreadAngle / (projectileCount - 1)) * i;
                    Vector2 velocity = toPlayer.RotatedBy(angle) * 7f;
                    
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        NPC.Center,
                        velocity,
                        projectileType,
                        damage,
                        0f,
                        Main.myPlayer
                    );
                }
                
                // Visual effect
                for (int i = 0; i < 15; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, toPlayer.X * 2f, toPlayer.Y * 2f, 0, default, 1.2f);
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                }
                
                // Play sound
                SoundEngine.PlaySound(SoundID.Item17, NPC.position);
            }
            
            // If we get too close to the player, back off a bit
            if (distance < 100)
            {
                NPC.velocity *= 0.8f;
            }
        }
        
        private void VoidBatSummonAttack(Player target)
        {
            // First phase summons 3 void bats
            
            // First, move to a safe position above the player
            if (attackCounter < 80)
            {
                Vector2 hoverPos = target.Center - new Vector2(0, 200);
                Vector2 toHoverPos = hoverPos - NPC.Center;
                float distToHover = toHoverPos.Length();
                
                if (distToHover > 30)
                {
                    toHoverPos.Normalize();
                    NPC.velocity = toHoverPos * Math.Min(distToHover / 20f, 6f);
                }
                else
                {
                    NPC.velocity *= 0.9f;
                }
                
                // Charging effect
                if (attackCounter % 15 == 0)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(60f, 60f);
                        Vector2 dustVel = (NPC.Center - dustPos) * 0.08f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, dustVel, 0, default, 1.8f);
                        dust.noGravity = true;
                    }
                }
            }
            else if (attackCounter == 80)
            {
                // Summon 3 void bats with warning
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        // Spawn bats at different positions around the boss
                        float angle = MathHelper.TwoPi * i / 3f;
                        Vector2 spawnOffset = new Vector2(
                            (float)Math.Cos(angle) * 120f,
                            (float)Math.Sin(angle) * 80f
                        );
                        Vector2 spawnPos = NPC.Center + spawnOffset;
                        
                        // Ensure spawn position is valid
                        if (!Collision.SolidCollision(spawnPos, 32, 32))
                        {
                            int voidBat = NPC.NewNPC(NPC.GetSource_FromAI(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<VoidBat>());
                            
                            if (voidBat < Main.maxNPCs)
                            {
                                Main.npc[voidBat].target = NPC.target;
                                Main.npc[voidBat].netUpdate = true;
                            }
                        }
                        
                        // Create spawn effect
                        for (int d = 0; d < 25; d++)
                        {
                            Dust dust = Dust.NewDustDirect(spawnPos - new Vector2(16, 16), 32, 32, 
                                DustID.PurpleTorch, 0f, 0f, 0, default, 2f);
                            dust.noGravity = true;
                            dust.velocity = Main.rand.NextVector2Circular(4f, 4f);
                        }
                    }
                    
                    // Play summoning sound
                    SoundEngine.PlaySound(SoundID.NPCDeath52, NPC.position);
                }
                
                // Create dramatic summoning effect
                for (int i = 0; i < 50; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(8f, 8f);
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                        DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 2.5f);
                    dust.noGravity = true;
                }
            }
            else
            {
                // After summoning, retreat and move defensively
                Vector2 retreatPos = target.Center + new Vector2(
                    Main.rand.Next(-300, 300),
                    Main.rand.Next(-250, -150)
                );
                
                Vector2 toRetreat = retreatPos - NPC.Center;
                float distance = toRetreat.Length();
                
                if (distance > 50)
                {
                    toRetreat.Normalize();
                    NPC.velocity = toRetreat * Math.Min(distance / 30f, 5f);
                }
                else
                {
                    NPC.velocity *= 0.95f;
                }
                
                // Occasionally fire defensive projectiles
                if (attackCounter % 60 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toPlayer = target.Center - NPC.Center;
                    toPlayer.Normalize();
                    
                    int damage = NPC.damage / 6; // Reduced damage for defensive shots
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                    
                    // Fire 2 projectiles in a spread
                    float spread = MathHelper.ToRadians(25);
                    Vector2 velocity1 = toPlayer.RotatedBy(spread) * 5f;
                    Vector2 velocity2 = toPlayer.RotatedBy(-spread) * 5f;
                    
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity1, projectileType, damage, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity2, projectileType, damage, 0f, Main.myPlayer);
                    
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                }
            }
        }
        
        private void EnhancedVoidBatSummonAttack(Player target)
        {
            // Second phase summons 4 void bats with enhanced effects
            
            // First, move to a safe position above the player
            if (attackCounter < 70)
            {
                Vector2 hoverPos = target.Center - new Vector2(0, 220);
                Vector2 toHoverPos = hoverPos - NPC.Center;
                float distToHover = toHoverPos.Length();
                
                if (distToHover > 30)
                {
                    toHoverPos.Normalize();
                    NPC.velocity = toHoverPos * Math.Min(distToHover / 18f, 7f);
                }
                else
                {
                    NPC.velocity *= 0.9f;
                }
                
                // Enhanced charging effect
                if (attackCounter % 12 == 0)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(80f, 80f);
                        Vector2 dustVel = (NPC.Center - dustPos) * 0.1f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, dustVel, 0, default, 2.2f);
                        dust.noGravity = true;
                    }
                }
            }
            else if (attackCounter == 70)
            {
                // Summon 4 void bats with enhanced warning
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        // Spawn bats at different positions around the boss
                        float angle = MathHelper.TwoPi * i / 4f;
                        Vector2 spawnOffset = new Vector2(
                            (float)Math.Cos(angle) * 140f,
                            (float)Math.Sin(angle) * 90f
                        );
                        Vector2 spawnPos = NPC.Center + spawnOffset;
                        
                        // Ensure spawn position is valid
                        if (!Collision.SolidCollision(spawnPos, 32, 32))
                        {
                            int voidBat = NPC.NewNPC(NPC.GetSource_FromAI(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<VoidBat>());
                            
                            if (voidBat < Main.maxNPCs)
                            {
                                Main.npc[voidBat].target = NPC.target;
                                Main.npc[voidBat].netUpdate = true;
                                // In phase 2, give bats slightly more health
                                Main.npc[voidBat].lifeMax = (int)(Main.npc[voidBat].lifeMax * 1.2f);
                                Main.npc[voidBat].life = Main.npc[voidBat].lifeMax;
                            }
                        }
                        
                        // Create enhanced spawn effect
                        for (int d = 0; d < 30; d++)
                        {
                            Dust dust = Dust.NewDustDirect(spawnPos - new Vector2(16, 16), 32, 32, 
                                DustID.PurpleTorch, 0f, 0f, 0, default, 2.3f);
                            dust.noGravity = true;
                            dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
                        }
                    }
                    
                    // Play enhanced summoning sound
                    SoundEngine.PlaySound(SoundID.Roar, NPC.position);
                }
                
                // Create dramatic enhanced summoning effect
                for (int i = 0; i < 70; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(10f, 10f);
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, 
                        DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 3f);
                    dust.noGravity = true;
                }
            }
            else
            {
                // After summoning, more aggressive movement and attacks
                Vector2 retreatPos = target.Center + new Vector2(
                    Main.rand.Next(-350, 350),
                    Main.rand.Next(-280, -180)
                );
                
                Vector2 toRetreat = retreatPos - NPC.Center;
                float distance = toRetreat.Length();
                
                if (distance > 40)
                {
                    toRetreat.Normalize();
                    NPC.velocity = toRetreat * Math.Min(distance / 25f, 6f);
                }
                else
                {
                    NPC.velocity *= 0.95f;
                }
                
                // More frequent and aggressive defensive projectiles
                if (attackCounter % 45 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toPlayer = target.Center - NPC.Center;
                    toPlayer.Normalize();
                    
                    int damage = NPC.damage / 5; // Slightly higher damage in phase 2
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                    
                    // Fire 3 projectiles in a spread
                    float spread = MathHelper.ToRadians(30);
                    Vector2 velocity1 = toPlayer.RotatedBy(spread) * 6f;
                    Vector2 velocity2 = toPlayer * 6f;
                    Vector2 velocity3 = toPlayer.RotatedBy(-spread) * 6f;
                    
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity1, projectileType, damage, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity2, projectileType, damage, 0f, Main.myPlayer);
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity3, projectileType, damage, 0f, Main.myPlayer);
                    
                    SoundEngine.PlaySound(SoundID.Item9, NPC.position);
                }
            }
        }        private void SecondPhaseAttacks(Player target)
        {
            // Second phase uses a cycle of 6 different attack patterns, more aggressive than first phase
            if (attackDuration <= 0)
            {
                // Time to switch to the next attack pattern
                currentAttack = (currentAttack + 1) % 6;
                
                // Set up the new attack pattern
                switch (currentAttack)
                {                case 0: // Void storm attack
                        attackDuration = 240; // Increased from 180
                        break;
                        
                    case 1: // Multi-teleport barrage
                        attackDuration = 270; // Increased from 210
                        teleportCount = 0;
                        break;
                        
                    case 2: // Dimensional rift attack
                        attackDuration = 300; // Increased from 240
                        NPC.velocity = Vector2.Zero;
                        break;
                        
                    case 3: // Void implosion/explosion
                        attackDuration = 240; // Increased from 180
                        break;
                        
                    case 4: // Enhanced void bat summoning attack
                        attackDuration = 300; // Long duration for summoning
                        NPC.velocity = Vector2.Zero;
                        break;
                        
                    case 5: // Shadow dash combo
                        attackDuration = 260; // Increased from 200
                        targetPosition = target.Center;
                        break;
                }
                
                // Reset counter for the new attack
                attackCounter = 0;
            }
            
            // Execute the current attack pattern
            switch (currentAttack)
            {
                case 0: // Void storm attack
                    VoidStormAttack(target);
                    break;
                    
                case 1: // Multi-teleport barrage
                    MultiTeleportBarrage(target);
                    break;
                    
                case 2: // Dimensional rift attack
                    DimensionalRiftAttack(target);
                    break;
                    
                case 3: // Void implosion/explosion
                    VoidImplosionAttack(target);
                    break;
                    
                case 4: // Enhanced void bat summoning attack
                    EnhancedVoidBatSummonAttack(target);
                    break;
                    
                case 5: // Shadow dash combo
                    ShadowDashCombo(target);
                    break;
            }
            
            // Apply advanced mechanics in second phase
            MarkOfTheVoid(target);
            HandleVoidZones();
            GravityManipulation();
            
            // Decrease the attack duration
            attackDuration--;
        }
        
        private void VoidStormAttack(Player target)
        {
            // Move erratically while firing volleys of projectiles
            
            // First phase: move above player and charge
            if (attackCounter < 60)
            {
                // Move to position above player
                Vector2 hoverPos = target.Center - new Vector2(0, 150);
                Vector2 toHoverPos = hoverPos - NPC.Center;
                float distToHover = toHoverPos.Length();
                
                if (distToHover > 20)
                {
                    toHoverPos.Normalize();
                    NPC.velocity = toHoverPos * Math.Min(distToHover / 15f, 8f);
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
                
                // Charging effect
                if (attackCounter % 10 == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(40f, 40f);
                        Vector2 dustVel = (NPC.Center - dustPos) * 0.1f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, dustVel, 0, default, 1.8f);
                        dust.noGravity = true;
                    }
                }
            }
            // Second phase: unleash the void storm
            else
            {
                // Move in a figure-8 pattern around the player
                float speed = 6f;
                float angle = attackCounter * 0.04f;
                Vector2 figureEightPos = target.Center + new Vector2(
                    (float)Math.Sin(angle) * 250f,
                    (float)Math.Sin(angle * 2) * 100f
                );
                
                Vector2 moveDirection = figureEightPos - NPC.Center;
                float distance = moveDirection.Length();
                
                if (distance > 20f)
                {
                    moveDirection.Normalize();
                    NPC.velocity = moveDirection * speed;
                }
                else
                {
                    NPC.velocity *= 0.9f;
                }
                  // Periodic projectile bursts
                if (attackCounter % 30 == 0 && Main.netMode != NetmodeID.MultiplayerClient) // Changed from 20 to 30 to reduce frequency
                {
                    int damage = NPC.damage / 5; // Reduced damage for storm attacks due to quantity
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                    
                    // Fire projectiles in multiple directions
                    int projectileCount = 4; // Reduced from 6
                    for (int i = 0; i < projectileCount; i++)
                    {
                        float projAngle = MathHelper.TwoPi * i / projectileCount;
                        Vector2 velocity = new Vector2((float)Math.Cos(projAngle), (float)Math.Sin(projAngle)) * 5f; // Reduced from 6f
                        
                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            NPC.Center,
                            velocity,
                            projectileType,
                            damage,
                            0f,
                            Main.myPlayer
                        );
                    }
                    
                    // Create visual effects
                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 0f, 0f, 0, default, 1.5f);
                        dust.noGravity = true;
                        dust.velocity = Main.rand.NextVector2Circular(5f, 5f);
                    }
                    
                    SoundEngine.PlaySound(SoundID.Item9, NPC.position);
                }
            }
        }
          private void MultiTeleportBarrage(Player target)
        {
            // Rapid teleportation with projectile barrages after each teleport
            int teleportInterval = 60; // Increased from 40 to slow down teleport frequency
              // Create teleport warning dust 1 second before teleporting
            if (attackCounter % teleportInterval == 30 && teleportCount < 5)
            {
                Vector2 warningTeleportPos;
                float warningDistance = Main.rand.Next(200, 300);
                float warningAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                warningTeleportPos = target.Center + new Vector2((float)Math.Cos(warningAngle) * warningDistance, (float)Math.Sin(warningAngle) * warningDistance);
                
                // Make sure it's in a valid location
                if (!Collision.SolidCollision(warningTeleportPos, NPC.width, NPC.height))
                {
                    // Create warning dust effect at future teleport location
                    for (int i = 0; i < 20; i++)
                    {
                        Dust dust = Dust.NewDustDirect(
                            warningTeleportPos - new Vector2(NPC.width/2, NPC.height/2),
                            NPC.width, NPC.height, 
                            DustID.PurpleTorch, 0f, 0f, 0, default, 2f);
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
            }
            
            if (attackCounter % teleportInterval == 0)
            {
                // Create vanishing effect
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 2f);
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
                
                // Play teleport sound
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                
                // Choose teleport position (random positions around the player)
                Vector2 teleportPos;
                float distance = Main.rand.Next(200, 300);
                float angle = Main.rand.NextFloat() * MathHelper.TwoPi;
                teleportPos = target.Center + new Vector2((float)Math.Cos(angle) * distance, (float)Math.Sin(angle) * distance);
                
                // Make sure it's in a valid location
                if (!Collision.SolidCollision(teleportPos, NPC.width, NPC.height))
                {
                    NPC.Center = teleportPos;
                }
                
                // Create appearing effect
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 2f);
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
                
                // Stop velocity after teleport
                NPC.velocity = Vector2.Zero;
                teleportCount++;
                
                // Fire a barrage of projectiles after each teleport
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toPlayer = target.Center - NPC.Center;
                    toPlayer.Normalize();
                    int damage = NPC.damage / 5;
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                      // Fire projectiles in a cone towards the player
                    int projectileCount = 3 + teleportCount/2; // Fewer projectiles with each teleport (reduced from 3 + teleportCount)
                    float spreadAngle = MathHelper.ToRadians(45);
                    float startAngle = -spreadAngle / 2;
                    
                    for (int i = 0; i < projectileCount; i++)
                    {
                        float projAngle = startAngle + (spreadAngle / (projectileCount - 1)) * i;
                        Vector2 velocity = toPlayer.RotatedBy(projAngle) * 7f; // Reduced speed from 9f to 7f
                        
                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            NPC.Center,
                            velocity,
                            projectileType,
                            damage,
                            0f,
                            Main.myPlayer
                        );
                    }
                    
                    // Sound effect
                    SoundEngine.PlaySound(SoundID.Item43, NPC.position);
                }
            }
            
            // After a certain number of teleports, end this attack early
            if (teleportCount >= 5)
            {
                attackDuration = 1; // End soon
            }
        }
        
        private void DimensionalRiftAttack(Player target)
        {
            // Creates a stationary dimensional rift that fires projectiles
            
            // First phase: move to position and create the rift
            if (attackCounter < 60)
            {
                // Move to position above player
                Vector2 hoverPos = target.Center - new Vector2(0, 200);
                Vector2 toHoverPos = hoverPos - NPC.Center;
                float distToHover = toHoverPos.Length();
                
                if (distToHover > 20)
                {
                    toHoverPos.Normalize();
                    NPC.velocity = toHoverPos * Math.Min(distToHover / 15f, 8f);
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
                
                // Charging effect
                if (attackCounter % 10 == 0)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(50f, 50f);
                        Vector2 dustVel = (NPC.Center - dustPos) * 0.1f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, dustVel, 0, default, 2f);
                        dust.noGravity = true;
                    }
                }
            }
            else if (attackCounter == 60)
            {
                // Create dimensional rift at current position
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    targetPosition = NPC.Center; // Store rift position
                    
                    // Visual effect for rift creation
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2Circular(10f, 10f);
                        Dust dust = Dust.NewDustDirect(NPC.Center - new Vector2(25, 25), 50, 50, DustID.PurpleCrystalShard, velocity.X, velocity.Y, 0, default, 2f);
                        dust.noGravity = true;
                    }
                    
                    // Sound effect
                    SoundEngine.PlaySound(SoundID.Item94, NPC.position);
                }
            }
            else
            {
                // Second phase: move away from rift while it fires projectiles
                
                // Move in a circle around the rift
                float orbitSpeed = 0.05f;
                float orbitRadius = 300f;
                
                // Calculate orbital position
                float angle = orbitSpeed * (attackCounter - 60);
                Vector2 orbitPosition = targetPosition + new Vector2(
                    (float)Math.Cos(angle) * orbitRadius,
                    (float)Math.Sin(angle) * orbitRadius
                );
                
                // Move smoothly to the orbit position
                Vector2 moveDirection = orbitPosition - NPC.Center;
                float distance = moveDirection.Length();
                
                if (distance > 20f)
                {
                    moveDirection.Normalize();
                    float speed = Math.Min(distance / 10f, 10f);
                    NPC.velocity = moveDirection * speed;
                }
                else
                {
                    NPC.velocity *= 0.9f;
                }
                
                // Rift periodically fires projectiles at the player
                if ((attackCounter - 60) % 30 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // Get direction to player from the rift
                    Vector2 toPlayer = target.Center - targetPosition;
                    toPlayer.Normalize();
                    
                    int damage = NPC.damage / 5;
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                    
                    // Create tracking projectiles from the rift
                    int projectileCount = 4;
                    float spreadAngle = MathHelper.ToRadians(60);
                    float startAngle = -spreadAngle / 2;
                    
                    for (int i = 0; i < projectileCount; i++)
                    {
                        float projAngle = startAngle + (spreadAngle / (projectileCount - 1)) * i;
                        Vector2 velocity = toPlayer.RotatedBy(projAngle) * 6f;
                        
                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            targetPosition,
                            velocity,
                            projectileType,
                            damage,
                            0f,
                            Main.myPlayer,
                            ai0: 2f, // Homing behavior
                            ai1: 0f
                        );
                    }
                    
                    // Create rift pulse effect
                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 dustVel = Main.rand.NextVector2CircularEdge(3f, 3f);
                        Dust dust = Dust.NewDustPerfect(targetPosition, DustID.PurpleTorch, dustVel, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    
                    // Sound effect
                    SoundEngine.PlaySound(SoundID.Item8, targetPosition);
                }
                
                // NPC also fires projectiles occasionally
                if ((attackCounter - 60) % 90 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toPlayer = target.Center - NPC.Center;
                    toPlayer.Normalize();
                    
                    int damage = NPC.damage / 4;
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                    
                    // Fire 3 projectiles in a spread
                    float spread = MathHelper.ToRadians(20);
                    Vector2 velocity = toPlayer * 8f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, projectileType, damage, 0f, Main.myPlayer);
                    
                    Vector2 velocity2 = toPlayer.RotatedBy(spread) * 8f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity2, projectileType, damage, 0f, Main.myPlayer);
                    
                    Vector2 velocity3 = toPlayer.RotatedBy(-spread) * 8f;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity3, projectileType, damage, 0f, Main.myPlayer);
                    
                    // Sound effect
                    SoundEngine.PlaySound(SoundID.Item12, NPC.position);
                }
                
                // Maintain rift visual effects
                if ((attackCounter - 60) % 5 == 0)
                {
                    // Create pulsating rift effect
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 dustOffset = Main.rand.NextVector2CircularEdge(20f, 20f);
                        Dust dust = Dust.NewDustPerfect(targetPosition + dustOffset, DustID.PurpleCrystalShard, Vector2.Zero, 0, default, 1.5f);
                        dust.noGravity = true;
                        dust.velocity = dustOffset * -0.05f;
                    }
                }
            }
        }
        
        private void VoidImplosionAttack(Player target)
        {
            // Create void projectiles that implode and then explode
            
            // First phase: move to position and charge
            if (attackCounter < 60)
            {
                // Move to position near player
                Vector2 targetPos = target.Center - new Vector2(0, 100);
                Vector2 toTarget = targetPos - NPC.Center;
                float distance = toTarget.Length();
                
                if (distance > 20)
                {
                    toTarget.Normalize();
                    NPC.velocity = toTarget * Math.Min(distance / 10f, 10f);
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
                
                // Charging effect
                if (attackCounter % 10 == 0)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(50f, 50f);
                        Vector2 dustVel = (NPC.Center - dustPos) * 0.1f;
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, dustVel, 0, default, 2f);
                        dust.noGravity = true;
                    }
                }
            }            else if (attackCounter == 60)
            {
                // Create implosion effect
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // Create ring of void projectiles that implode
                    int projectileCount = 12; // Reduced from 16 for VoidImplosionAttack
                    for (int i = 0; i < projectileCount; i++)
                    {
                        float rotation = MathHelper.TwoPi * i / projectileCount;
                        Vector2 spawnPos = NPC.Center + new Vector2(
                            (float)Math.Cos(rotation) * 400,
                            (float)Math.Sin(rotation) * 400
                        );
                        
                        Vector2 velocity = NPC.Center - spawnPos;
                        velocity.Normalize();
                        velocity *= 6f; // Reduced from 8f
                        
                        int damage = NPC.damage / 5;
                        int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                        
                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            spawnPos,
                            velocity,
                            projectileType,
                            damage,
                            0f,
                            Main.myPlayer,
                            ai0: 3f, // Special flag for implosion behavior
                            ai1: 0f
                        );
                    }
                    
                    // Sound effect
                    SoundEngine.PlaySound(SoundID.Item15, NPC.position);
                }
                
                // Store position for explosion phase
                targetPosition = NPC.Center;
                
                // Create teleport warning dust (15 frames before teleporting)
                Vector2 teleportPos;
                float teleDistance = 300f;
                float teleAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                teleportPos = target.Center + new Vector2((float)Math.Cos(teleAngle) * teleDistance, (float)Math.Sin(teleAngle) * teleDistance);
                
                // Make sure it's in a valid location
                if (!Collision.SolidCollision(teleportPos, NPC.width, NPC.height))
                {
                    // Create warning dust effect at future teleport location
                    for (int i = 0; i < 25; i++)
                    {
                        Dust dust = Dust.NewDustDirect(
                            teleportPos - new Vector2(NPC.width/2, NPC.height/2),
                            NPC.width, NPC.height, 
                            DustID.PurpleTorch, 0f, 0f, 0, default, 2f);
                        dust.noGravity = true;
                        dust.velocity *= 0.5f;
                    }
                }
                
                // Actual teleport happens 15 frames later
            }
            else if (attackCounter == 75) // Added a delay before teleporting
            {
                // Teleport away
                Vector2 teleportPos;
                float teleDistance = 300f;
                float teleAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                teleportPos = target.Center + new Vector2((float)Math.Cos(teleAngle) * teleDistance, (float)Math.Sin(teleAngle) * teleDistance);
                
                // Create vanishing effect
                for (int i = 0; i < 30; i++)
                {
                    Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 2f);
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                }
                
                // Make sure it's in a valid location
                if (!Collision.SolidCollision(teleportPos, NPC.width, NPC.height))
                {
                    NPC.Center = teleportPos;
                    
                    // Create teleport dust effect
                    for (int i = 0; i < 30; i++)
                    {
                        Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 2f);
                        dust.noGravity = true;
                        dust.velocity *= 2f;
                    }
                }
            }
            else if (attackCounter == 120)
            {
                // Create explosion effect
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // Create ring of void projectiles that explode outward
                    int projectileCount = 16; // Reduced from 24 for explosion
                    for (int i = 0; i < projectileCount; i++)
                    {
                        float rotation = MathHelper.TwoPi * i / projectileCount;
                        Vector2 velocity = new Vector2(
                            (float)Math.Cos(rotation),
                            (float)Math.Sin(rotation)
                        );
                        velocity *= 8f; // Reduced speed from 10f
                        
                        int damage = NPC.damage / 5;
                        int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                        
                        Projectile.NewProjectile(
                            NPC.GetSource_FromAI(),
                            targetPosition,
                            velocity,
                            projectileType,
                            damage,
                            0f,
                            Main.myPlayer
                        );
                    }
                    
                    // Visual effect
                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 dustVel = Main.rand.NextVector2Circular(10f, 10f);
                        Dust dust = Dust.NewDustPerfect(targetPosition, DustID.PurpleCrystalShard, dustVel, 0, default, 2f);
                        dust.noGravity = true;
                    }
                    
                    // Sound effect
                    SoundEngine.PlaySound(SoundID.Item14, targetPosition);
                }
            }
            else
            {
                // After explosion, move erratically
                if (attackCounter % 60 == 0)
                {
                    // Choose new random target position
                    Vector2 newPos;
                    float posDistance = Main.rand.Next(200, 350);
                    float posAngle = Main.rand.NextFloat() * MathHelper.TwoPi;
                    newPos = target.Center + new Vector2((float)Math.Cos(posAngle) * posDistance, (float)Math.Sin(posAngle) * posDistance);
                    
                    // Make sure it's in a valid location
                    if (!Collision.SolidCollision(newPos, NPC.width, NPC.height))
                    {
                        targetPosition = newPos;
                    }
                }
                
                // Move towards target position
                Vector2 toTarget = targetPosition - NPC.Center;
                float distance = toTarget.Length();
                
                if (distance > 20)
                {
                    toTarget.Normalize();
                    NPC.velocity = toTarget * Math.Min(distance / 10f, 12f);
                }
                else
                {
                    NPC.velocity *= 0.8f;
                }
                
                // Occasional projectiles
                if (attackCounter % 30 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Vector2 toPlayer = target.Center - NPC.Center;
                    toPlayer.Normalize();
                    
                    int damage = NPC.damage / 4;
                    int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                    
                    // Fire a projectile at the player
                    Projectile.NewProjectile(
                        NPC.GetSource_FromAI(),
                        NPC.Center,
                        toPlayer * 8f,
                        projectileType,
                        damage,
                        0f,
                        Main.myPlayer
                    );
                }
            }
        }
          private void ShadowDashCombo(Player target)
        {
            // Perform a series of dashes with shadow afterimages
            
            // Cycle through the combo pattern
            int cycleDuration = 60; // Increased from 40 for a slower, more predictable pattern
            int cyclePhase = (attackCounter / cycleDuration) % 3;
            int cycleProgress = attackCounter % cycleDuration;
            
            switch (cyclePhase)
            {
                case 0: // Teleport to position
                    if (cycleProgress == 0)
                    {
                        // Create vanishing effect
                        for (int i = 0; i < 30; i++)
                        {
                            Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 2f);
                            dust.noGravity = true;
                            dust.velocity *= 2f;
                        }
                        
                        // Play teleport sound
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                        
                        // Choose teleport position (above the player)
                        Vector2 teleportPos = target.Center - new Vector2(0, 250);
                        
                        // Make sure it's in a valid location
                        if (!Collision.SolidCollision(teleportPos, NPC.width, NPC.height))
                        {
                            NPC.Center = teleportPos;
                        }
                        
                        // Create appearing effect
                        for (int i = 0; i < 30; i++)
                        {
                            Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 2f);
                            dust.noGravity = true;
                            dust.velocity *= 2f;
                        }
                        
                        // Stop velocity
                        NPC.velocity = Vector2.Zero;
                    }
                    else
                    {
                        // Hover in place while charging
                        NPC.velocity *= 0.9f;
                        
                        // Create charging effect
                        if (cycleProgress % 5 == 0)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 0f, 0f, 0, default, 1.5f);
                                dust.noGravity = true;
                                dust.velocity = Main.rand.NextVector2Circular(2f, 2f);
                            }
                        }
                        
                        // Add warning indicator before dash (at 80% through this phase)
                        if (cycleProgress == 45) // Warning appears 15 ticks before dash
                        {
                            // Create line of dust from boss to player showing dash path
                            Vector2 toPlayer = target.Center - NPC.Center;
                            toPlayer.Normalize();
                            
                            for (int i = 0; i < 30; i++)
                            {
                                Vector2 dustPos = NPC.Center + toPlayer * 20f * i;
                                Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleTorch, Vector2.Zero, 0, default, 1.5f);
                                dust.noGravity = true;
                            }
                        }
                    }
                    break;
                    
                case 1: // Dash down to player
                    if (cycleProgress == 0)
                    {
                        // Calculate direction to player
                        Vector2 toPlayer = target.Center - NPC.Center;
                        toPlayer.Normalize();
                        
                        // Set dash velocity
                        NPC.velocity = toPlayer * 12f; // Reduced from 16f
                        
                        // Play dash sound
                        SoundEngine.PlaySound(SoundID.Item74, NPC.position);
                        
                        // Create dash effect
                        for (int i = 0; i < 20; i++)
                        {
                            Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, -NPC.velocity.X * 0.5f, -NPC.velocity.Y * 0.5f, 0, default, 2f);
                            dust.noGravity = true;
                        }
                    }
                    else
                    {
                        // Create shadow afterimages as we dash
                        if (cycleProgress % 5 == 0)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Shadowflame, 0f, 0f, 0, default, 1.5f);
                                dust.noGravity = true;
                                dust.velocity *= 0.1f;
                            }
                        }
                        
                        // Slowly reduce velocity
                        NPC.velocity *= 0.95f;
                    }
                    break;
                    
                case 2: // Radial projectile attack
                    if (cycleProgress == 0)
                    {
                        // Stop movement
                        NPC.velocity *= 0.5f;
                        
                        // Create warning effect for projectile burst
                        for (int i = 0; i < 20; i++)
                        {
                            float angle = MathHelper.TwoPi * i / 20;
                            Vector2 dustOffset = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 40;
                            Dust dust = Dust.NewDustPerfect(NPC.Center + dustOffset, DustID.PurpleTorch, Vector2.Zero, 0, default, 1.5f);
                            dust.noGravity = true;
                            dust.velocity = dustOffset * 0.1f;
                        }
                    }
                    else if (cycleProgress == 30) // Delay the attack to give players time to react
                    {
                        // Fire radial projectiles
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            int damage = NPC.damage / 5;
                            int projectileType = ModContent.ProjectileType<VoidHarbingerBossProjectile>();
                            
                            // Fire projectiles in multiple directions
                            int projectileCount = 8; // Reduced from 10
                            for (int i = 0; i < projectileCount; i++)
                            {
                                float projAngle = MathHelper.TwoPi * i / projectileCount;
                                Vector2 velocity = new Vector2((float)Math.Cos(projAngle), (float)Math.Sin(projAngle)) * 6f; // Reduced from 8f
                                
                                Projectile.NewProjectile(
                                    NPC.GetSource_FromAI(),
                                    NPC.Center,
                                    velocity,
                                    projectileType,
                                    damage,
                                    0f,
                                    Main.myPlayer
                                );
                            }
                            
                            // Sound effect
                            SoundEngine.PlaySound(SoundID.Item73, NPC.position);
                        }
                        
                        // Create burst effect
                        for (int i = 0; i < 30; i++)
                        {
                            Vector2 dustVel = Main.rand.NextVector2CircularEdge(10f, 10f);
                            Dust dust = Dust.NewDustPerfect(NPC.Center, DustID.PurpleTorch, dustVel, 0, default, 1.5f);
                            dust.noGravity = true;
                        }
                    }
                    else
                    {
                        // Move back slowly to prepare for next combo
                        Vector2 toTarget = target.Center - new Vector2(Main.rand.Next(-200, 200), 200) - NPC.Center;
                        float distance = toTarget.Length();
                        
                        if (distance > 20)
                        {
                            toTarget.Normalize();
                            NPC.velocity = toTarget * 4f;
                        }
                        else
                        {
                            NPC.velocity *= 0.9f;
                        }
                    }
                    break;
            }        }        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {   
            // Always drop Void Essence (for crafting other void-themed items)
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Materials.VoidEssence>(), 1, 25, 35));

            // 100% chance to drop some souls (a mix of them) - increased amounts
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofSight, 1, 15, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofNight, 1, 15, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofLight, 1, 15, 25));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofFright, 1, 8, 15));
            npcLoot.Add(ItemDropRule.Common(ItemID.SoulofMight, 1, 8, 15));

            // 50% chance to drop Hallowed Bars (increased chance and amount)
            npcLoot.Add(ItemDropRule.Common(ItemID.HallowedBar, 2, 15, 25));

            // Money drops (Guaranteed platinum coins for defeating a boss)
            npcLoot.Add(ItemDropRule.Common(ItemID.PlatinumCoin, 1, 3, 6));

            // Trophy drop (10% chance)
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Placeables.VoidHarbingerTrophy>(), 10));

            // Weapon drops - one guaranteed weapon from this pool
            IItemDropRule weaponPool = new OneFromOptionsNotScaledWithLuckDropRule(1, 1,
                ModContent.ItemType<Content.Items.Weapons.TheGrimWraith>(),
                ModContent.ItemType<Content.Items.Weapons.VoidResonator>()
            );
            npcLoot.Add(weaponPool);

            // Rare materials (15% chance each)
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentNebula, 7, 3, 8));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentSolar, 7, 3, 8));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentStardust, 7, 3, 8));
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentVortex, 7, 3, 8));

            // Consumable rewards (guaranteed)
            npcLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 10, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.GreaterManaPotion, 1, 10, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.EndurancePotion, 1, 3, 8));
            npcLoot.Add(ItemDropRule.Common(ItemID.LifeforcePotion, 1, 3, 8));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            // Create void dust when hit
            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(5f, 5f);
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, velocity.X, velocity.Y);
            }

            // Create more void dust/gores on death
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 40; i++)
                {
                    Vector2 velocity = Main.rand.NextVector2Circular(8f, 8f);
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, velocity.X, velocity.Y, 0, default, 1.5f);
                }
                
                // Add a death effect - dimensional rift closing
                for (int i = 0; i < 30; i++)
                {
                    Vector2 position = NPC.Center + Main.rand.NextVector2Circular(50f, 50f);
                    Vector2 velocity = (NPC.Center - position) * 0.1f;
                    Dust dust = Dust.NewDustPerfect(position, DustID.PurpleCrystalShard, velocity, 0, default, 1.5f);
                    dust.noGravity = true;
                }
            }
        }
        
        private void HandleVoidZones()
        {
            // Process each active void zone
            for (int i = voidZones.Count - 1; i >= 0; i--)
            {
                Vector2 zonePos = voidZones[i];
                
                // Create visual effects for the zone
                if (Main.rand.NextBool(6)) // Less frequent to reduce visual clutter
                {
                    Vector2 dustPos = zonePos + Main.rand.NextVector2Circular(40f, 40f);
                    Vector2 dustVel = (zonePos - dustPos) * 0.05f;
                    Dust dust = Dust.NewDustPerfect(dustPos, DustID.PurpleCrystalShard, dustVel, 0, default, 1.5f);
                    dust.noGravity = true;
                }
                
                // Damage players who enter the zone
                foreach (Player player in Main.player.Where(p => p.active && !p.dead))
                {
                    if (Vector2.Distance(player.Center, zonePos) < 70f)
                    {
                        // Apply damage to player
                        if (Main.rand.NextBool(30)) // Only damage every half second or so
                        {
                            player.Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage / 8, 0);
                            
                            // Apply a debuff if you want
                            // player.AddBuff(ModContent.BuffType<VoidCorruption>(), 60);
                            
                            // Visual effect for damage
                            for (int j = 0; j < 5; j++)
                            {
                                Dust.NewDust(player.position, player.width, player.height, DustID.PurpleTorch, 0f, 0f, 0, default, 1.2f);
                            }
                        }
                    }
                }
                
                // Remove old zones (after ~10 seconds)
                if (Main.GameUpdateCount % 600 == 0)
                {
                    voidZones.RemoveAt(i);
                }
            }
        }
        
        private void GravityManipulation()
        {
            // Only activate in second phase and when below 50% health
            if (secondPhase && NPC.life < PhaseTwoMaxLife / 2)
            {
                // Toggle gravity control every 10 seconds
                if (!gravitationActive && Main.GameUpdateCount % 600 == 0)
                {
                    gravitationActive = true;
                    gravityFlipTimer = 300; // 5 seconds of gravity control
                    
                    // Create visual effect for gravity distortion
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(200f, 200f);
                        Vector2 dustVel = Vector2.UnitY * Main.rand.NextFloat(-3f, 3f);
                        Dust dust = Dust.NewDustPerfect(dustPos, DustID.Vortex, dustVel, 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    
                    // Play sound for gravity distortion
                    SoundEngine.PlaySound(SoundID.Item82, NPC.Center);
                    
                    // Apply gravitation buff to all players within range
                    foreach (Player player in Main.player.Where(p => p.active && !p.dead && Vector2.Distance(p.Center, NPC.Center) < 1000f))
                    {
                        player.AddBuff(BuffID.Gravitation, gravityFlipTimer);
                    }
                }
                
                if (gravitationActive)
                {
                    // Create visual effects while gravity manipulation is active
                    if (Main.GameUpdateCount % 10 == 0)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 dustPos = NPC.Center + Main.rand.NextVector2Circular(150f, 150f);
                            Dust dust = Dust.NewDustPerfect(dustPos, DustID.Vortex, Vector2.Zero, 0, default, 1.2f);
                            dust.noGravity = true;
                        }
                    }
                    
                    gravityFlipTimer--;
                    
                    if (gravityFlipTimer <= 0)
                    {
                        gravitationActive = false;
                    }
                }
            }
        }
          private void MarkOfTheVoid(Player target)
        {
            // Only apply mark in second phase
            if (!secondPhase) return;

            // Check if player is already marked
            if (!playerMarked)
            {
                // Attempt to mark player every 6 seconds
                if (attackCounter % 360 == 0)
                {
                    playerMarked = true;
                    markTimer = 300; // 5 seconds of void zone following
                    markedPosition = target.Center;
                    
                    // Visual effect for marking
                    for (int i = 0; i < 30; i++)
                    {
                        Vector2 dustPos = target.Center + Main.rand.NextVector2Circular(30f, 30f);
                        Vector2 toCenter = target.Center - dustPos;
                        toCenter.Normalize();
                        
                        Dust dust = Dust.NewDustPerfect(
                            dustPos,
                            DustID.PurpleCrystalShard,
                            toCenter * 5f,
                            0,
                            default,
                            1.5f
                        );
                        dust.noGravity = true;
                    }
                    
                    // Play marking sound
                    SoundEngine.PlaySound(SoundID.Item100, target.position);
                }
            }
            else
            {
                // Update mark timer
                markTimer--;
                
                // Gradually move the void zone towards the player
                Vector2 toPlayer = target.Center - markedPosition;
                if (toPlayer.Length() > 5f)
                {
                    toPlayer.Normalize();
                    markedPosition += toPlayer * 6f; // Zone follows at 6 pixels per frame
                }
                
                // Create visual effects for the void zone
                if (Main.GameUpdateCount % 2 == 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 dustPos = markedPosition + Main.rand.NextVector2Circular(50f, 50f);
                        Vector2 dustVel = (markedPosition - dustPos) * 0.05f;
                        Dust dust = Dust.NewDustPerfect(
                            dustPos,
                            DustID.PurpleTorch,
                            dustVel,
                            0,
                            default,
                            1.2f
                        );
                        dust.noGravity = true;
                    }
                }
                
                // Damage player if they're in the void zone
                if (Vector2.Distance(target.Center, markedPosition) < 70f)
                {
                    // Apply damage every 30 ticks (half second)
                    if (Main.GameUpdateCount % 30 == 0)
                    {
                        target.Hurt(PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage / 6, 0);
                        
                        // Damage effect
                        for (int i = 0; i < 10; i++)
                        {
                            Dust.NewDust(target.position, target.width, target.height,
                                DustID.PurpleCrystalShard,
                                Main.rand.NextFloat(-2f, 2f),
                                Main.rand.NextFloat(-2f, 2f),
                                0, default, 1.5f);
                        }
                        
                        // Add the position to void zones list for lingering effect
                        voidZones.Add(markedPosition);
                    }
                }
                
                // When mark expires
                if (markTimer <= 0)
                {
                    playerMarked = false;
                    
                    // Final void effect
                    for (int i = 0; i < 50; i++)
                    {
                        Vector2 velocity = Main.rand.NextVector2CircularEdge(8f, 8f);
                        Dust dust = Dust.NewDustPerfect(
                            markedPosition,
                            DustID.PurpleCrystalShard,
                            velocity,
                            0,
                            default,
                            2f
                        );
                        dust.noGravity = true;
                    }
                    
                    SoundEngine.PlaySound(SoundID.Item82, markedPosition);
                }
            }
        }
    }
}