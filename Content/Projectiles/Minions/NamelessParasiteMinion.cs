using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Buffs;
using Spiritrum.Content.Projectiles.Misc;
using Spiritrum.Players;

namespace Spiritrum.Content.Projectiles.Minions
{
    public class NamelessParasiteMinion : ModProjectile
    {
        // Constants for the minion's behavior
        private const float ORBIT_DISTANCE = 32f; // 2 tiles
        private const float ATTACK_INTERVAL = 78f; // 1.3 seconds at 60fps
        private const float MAX_ATTACK_RANGE = 500f; // How far the minion will target enemies
        private const float IDLE_ROTATION_SPEED = 0.03f; // How fast the minion orbits when idle
        private const float HOMING_STRENGTH = 0.08f; // How strongly the projectiles home in on enemies
        
        // Variables to track the minion's state
        private float attackCooldown;
        private float orbitAngle;
        private NPC targetNPC;

        public override void SetStaticDefaults()
        {
            // This makes the projectile shoot through tiles and counts as a minion
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed for right-click to despawn it
            Main.projFrames[Projectile.type] = 4; // The number of frames in your minion's sprite sheet
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.tileCollide = false; // Makes your minion go through tiles
            Projectile.friendly = true; // Deals damage to enemies but not to friendly npcs            Projectile.minion = true; // Is a minion
            Projectile.DamageType = DamageClass.Summon; // Deals summon damage
            Projectile.minionSlots = 1f; // Takes 1 minion slot
            Projectile.originalDamage = Projectile.damage; // Store the original damage value
            Projectile.penetrate = -1; // Infinite penetration
            Projectile.timeLeft = 2; // 5 minutes, will be refreshed by the buff
            Projectile.netImportant = true; // This guarantees client gets update from server
            
            // Initialize variables
            attackCooldown = 0f;
            orbitAngle = Main.rand.NextFloat() * MathHelper.TwoPi; // Random starting angle
        }

        public override bool? CanCutTiles()
        {
            return false; // Minion can't cut tiles (like grass)
        }

        public override bool MinionContactDamage()
        {
            return false; // The minion can't deal contact damage
        }        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            
            // Check if player is alive and has the buff, kill minion if not
            if (!owner.active || owner.dead || !owner.HasBuff(ModContent.BuffType<NamelessParasiteBuff>()))
            {
                Projectile.Kill();
                return;
            }

            // Refresh the minion's timer
            Projectile.timeLeft = 2;
            
            // This is the crucial part for summon weapons
            // This counter is used for the buff tooltip to show how many minions are active
            owner.GetModPlayer<MinionCounterPlayer>().namelessParasiteCount++;

            // Animate the minion
            Animate();

            // Find a target
            FindTarget();

            // Handle attack cooldown
            attackCooldown -= 1f;

            // Attack if we have a target and cooldown is ready
            if (targetNPC != null && targetNPC.active && !targetNPC.dontTakeDamage && attackCooldown <= 0)
            {
                Attack();
                attackCooldown = ATTACK_INTERVAL;
            }

            // Move the minion in orbit around the player
            UpdatePosition(owner);
        }

        private void Animate()
        {
            // Implement animation frames here
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10) // Adjust this value for animation speed
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
            }
            
            // Makes the minion always face the way it's moving
            Projectile.rotation = Projectile.velocity.X * 0.05f;
        }

        private void FindTarget()
        {
            // Reset target if it's no longer valid
            if (targetNPC != null && (!targetNPC.active || targetNPC.friendly || targetNPC.dontTakeDamage))
            {
                targetNPC = null;
            }

            // Look for a new target if we don't have one
            if (targetNPC == null)
            {
                float closestDistance = MAX_ATTACK_RANGE;
                
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    
                    // Skip invalid targets
                    if (!npc.active || npc.friendly || npc.dontTakeDamage || npc.lifeMax <= 5)
                        continue;
                        
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    
                    // Check if this NPC is closer than our current target
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetNPC = npc;
                    }
                }
            }
        }

        private void Attack()
        {
            if (targetNPC == null || !targetNPC.active || Main.myPlayer != Projectile.owner)
                return;
                
            // Calculate direction to target
            Vector2 direction = targetNPC.Center - Projectile.Center;
            direction.Normalize();
            direction *= 8f; // Projectile speed
                  // Create a tiny crimson dart projectile
            int projectileID = Projectile.NewProjectile(
                Projectile.GetSource_FromAI(),
                Projectile.Center,
                direction,
                ModContent.ProjectileType<CrimsonDart>(), // Using our custom CrimsonDart
                Projectile.damage,
                Projectile.knockBack,
                Projectile.owner,
                ai0: targetNPC.whoAmI, // Store target for homing
                ai1: HOMING_STRENGTH // Store homing strength
            );
                
            // Set the projectile to home in
            if (Main.projectile.IndexInRange(projectileID))
            {
                Main.projectile[projectileID].friendly = true;
                Main.projectile[projectileID].hostile = false;
                Main.projectile[projectileID].tileCollide = true;
                Main.projectile[projectileID].timeLeft = 180; // 3 seconds lifetime
                Main.projectile[projectileID].netUpdate = true;
            }
              // Play sound effect
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item17, Projectile.position);
        }

        private void UpdatePosition(Player owner)
        {
            // Update orbit angle
            orbitAngle += IDLE_ROTATION_SPEED;
            if (orbitAngle > MathHelper.TwoPi)
                orbitAngle -= MathHelper.TwoPi;

            // Calculate position around player
            Vector2 orbitPosition = owner.Center + new Vector2(
                (float)Math.Cos(orbitAngle) * ORBIT_DISTANCE,
                (float)Math.Sin(orbitAngle) * ORBIT_DISTANCE
            );
            
            // Move towards orbit position
            Vector2 direction = orbitPosition - Projectile.Center;
            float speed = 8f; // Adjust speed as needed
            
            if (direction.Length() > speed)
            {
                direction.Normalize();
                Projectile.velocity = direction * speed;
            }
            else
            {
                Projectile.velocity = direction; // Move directly to position if close enough
            }
            
            // Dust effects for visual flair
            if (Main.rand.NextBool(20))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0, 0, 0, default, 0.8f);
            }
        }
    }
}
