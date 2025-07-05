using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Spiritrum.Content.Buffs;

namespace Spiritrum.Players
{
    public class EchoPlayer : ModPlayer
    {
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (Player.HasBuff(ModContent.BuffType<CharmingEcho>()))
            {
                if (Main.rand.NextFloat() < 0.15f)
                {
                    modifiers.FinalDamage.Base = 0; // Negate the damage
                    modifiers.DisableSound();
                    Player.immuneTime = 180; // 3 seconds of invincibility
                    SoundEngine.PlaySound(SoundID.Item163, Player.position);
                    SpawnShadowyBurst();
                    CombatText.NewText(Player.getRect(), Color.Cyan, "Dodged!");
                }
            }
        }
        
        private void SpawnShadowyBurst()
        {
            // Spawn shadowy dust
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Player.Center + new Vector2(Main.rand.Next(-20, 21), Main.rand.Next(-20, 21));
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));
                
                Dust dust = Dust.NewDustDirect(position, 10, 10, DustID.Shadowflame, velocity.X, velocity.Y);
                dust.noGravity = true;
                dust.scale = Main.rand.NextFloat(1.0f, 2.0f);
                dust.fadeIn = 1.5f;
            }
            
            // Apply a brief glow effect to the player
            Player.AddBuff(BuffID.Shine, 30); // 0.5 seconds of shine
            
            // Damage nearby enemies
            float burstRadius = 120f; // Radius of the burst
            int burstDamage = 20; // Damage of the burst
            
            // Iterate through active NPCs
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.townNPC && !npc.friendly && Vector2.Distance(Player.Center, npc.Center) < burstRadius)
                {
                    // Deal damage to the NPC using the correct method
                    npc.SimpleStrikeNPC(burstDamage, Player.direction, false);
                    
                    // Create a dust trail between player and enemy
                    CreateDustTrail(Player.Center, npc.Center);
                }
            }
        }
        
        private void CreateDustTrail(Vector2 start, Vector2 end)
        {
            int numPoints = 10;
            for (int i = 0; i < numPoints; i++)
            {
                Vector2 position = Vector2.Lerp(start, end, (float)i / numPoints);
                Dust dust = Dust.NewDustPerfect(position, DustID.Shadowflame);
                dust.noGravity = true;
                dust.scale = 1.2f;
            }
        }
    }
}
