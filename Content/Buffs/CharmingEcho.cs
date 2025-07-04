using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Spiritrum.Content.Items;

namespace Spiritrum.Content.Buffs // Using a more standard namespace for buffs
{    // This class defines the buff applied by the Echo Charm
    public class CharmingEcho : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // Set buff properties
            Main.buffNoSave[Type] = true; // Buff doesn't save on logout
            Main.debuff[Type] = false; // Not a debuff
            Main.buffNoTimeDisplay[Type] = true; // Buff time is not displayed
        }

        // This method is called every tick while the player has the buff
        public override void Update(Player player, ref int buffIndex)
        {
            // Visual indicator that the buff is active
            if (Main.rand.NextBool(20))
            {
                Dust dust = Dust.NewDustDirect(
                    player.position, 
                    player.width, 
                    player.height, 
                    DustID.Shadowflame, 
                    0f, 0f, 100, default, 1f);
                dust.noGravity = true;
                dust.velocity *= 0.1f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }
            
            // The actual dodge functionality is implemented in the EchoPlayer.PreHurt method
            // When this buff is active, the player has a 15% chance to dodge any attack
            // and create a shadowy burst effect that damages nearby enemies
        }
    }
}

    // The conceptual YourModPlayer class for dodge logic and effect
    // This should be in its own file (e.g., YourModPlayer.cs) if you implement it.
    /*
    using Terraria;
    using Terraria.ModLoader;
    using Microsoft.Xna.Framework;
    using System;
    using Terraria.DataStructures; // Required for PlayerDeathReason

    namespace Spiritrum.Players // Example namespace for players
    {
        public class YourModPlayer : ModPlayer
        {
            // This method is called before the player takes damage
            public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageReason)
            {
                // Check if the player has the Echo Charm buff (meaning the accessory is equipped)
                // Ensure EchoCharmBuff is in its correct namespace (Spiritrum.Content.Buffs)
                if (Player.HasBuff(ModContent.BuffType<Spiritrum.Content.Buffs.EchoCharmBuff>()))
                {
                    // 15% chance to dodge
                    if (Main.rand.NextFloat() < 0.15f) // 0.15f = 15%
                    {
                        // Successful dodge!
                        playSound = false; // Prevent the default hit sound
                        genGore = false; // Prevent gore generation
                        // You might want to play a custom dodge sound here
                        // Example: Terraria.Audio.SoundEngine.PlaySound(SoundID.Dodge, Player.position);

                        // Trigger the shadowy burst effect
                        SpawnShadowyBurst(Player.Center);

                        // Cancel the damage
                        return false; // Returning false cancels the Hurt method
                    }
                }

                // If not dodged or buff not present, allow the damage to proceed
                return true;
            }

            // Method to spawn the shadowy burst effect
            private void SpawnShadowyBurst(Vector2 position)
            {
                // This is a placeholder for creating the visual and damage effect
                // You could spawn a custom projectile, create dust, or apply a debuff to nearby enemies.

                // Example: Spawn some shadowy dust
                // Note: If DustID.Shadowflame causes a CS0117 error, the ID might be different in your tModLoader version.
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(position - new Vector2(10, 10), 20, 20, DustID.Shadowflame, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), 0, default(Color), 1.5f);
                }

                // Example: Apply damage to nearby enemies
                float burstRadius = 150f; // Radius of the burst
                int burstDamage = 30; // Damage of the burst (adjust)
                // Iterate through active NPCs
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    // Check if the NPC is active, not town NPC, not friendly, and within the burst radius
                    if (npc.active && !npc.townNPC && !npc.friendly && Vector2.Distance(position, npc.Center) < burstRadius)
                    {
                        // Deal damage to the NPC
                        // This applies damage as if from the player
                        int damage = burstDamage; // Base damage
                        float knockback = 2f; // Knockback (adjust)
                        bool crit = false; // Whether the hit is a crit (adjust if needed)
                        // Use Player.StrikeNPC to deal damage from the player
                        Player.StrikeNPC(npc, damage, knockback, Player.direction, crit);
                    }
                }
            }
        }
    }
    */