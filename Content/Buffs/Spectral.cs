using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Buffs
{
    public class Spectral : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // Don't save buff when exiting
            Main.buffNoTimeDisplay[Type] = false; // Display time remaining
            
            // DisplayName.SetDefault("Spectral Form");
            // Description.SetDefault("You can pass through enemies and emit a damaging aura");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Allow passing through enemies
            player.noKnockback = true;
            
            // Create a damaging aura effect
            float auraRadius = 120f;
            int auraDamage = 30;
        
            // Visual effect - ghostly dust particles
            if (Main.rand.NextBool(3))
            {
                Vector2 dustPosition = player.Center + Main.rand.NextVector2CircularEdge(auraRadius, auraRadius);
                Vector2 dustVelocity = (player.Center - dustPosition) * 0.03f;
                
                Dust dust = Dust.NewDustPerfect(
                    dustPosition,
                    DustID.ShadowbeamStaff,
                    dustVelocity,
                    100,
                    Color.White * 0.7f,
                    Main.rand.NextFloat(0.8f, 1.2f));
                dust.noGravity = true;
                dust.fadeIn = 0.5f;
            }
            
            // Damage nearby enemies
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && !npc.dontTakeDamage && !npc.townNPC && Vector2.Distance(player.Center, npc.Center) < auraRadius)
                {
                    // Apply damage every 20 frames (~3 times per second)
                    if (Main.GameUpdateCount % 20 == 0)
                    {
                        // Create a damaging dust effect 
                        for (int d = 0; d < 3; d++)
                        {
                            Vector2 dustPos = npc.Center + Main.rand.NextVector2Circular(npc.width / 2, npc.height / 2);
                            Vector2 dustVel = (player.Center - dustPos) * 0.1f;
                            
                            Dust.NewDustPerfect(
                                dustPos,
                                DustID.ShadowbeamStaff,
                                dustVel,
                                100,
                                Color.Purple,
                                Main.rand.NextFloat(1.0f, 1.5f)).noGravity = true;
                        }
                        
                        // Deal damage to the NPC
                        npc.SimpleStrikeNPC(auraDamage, 0);
                    }
                }
            }
        }
    }
}