using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Global
{
    public class CryoCothGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            // Check if the killed NPC is the Nurse and player is in the Overworld
            if (npc.type == NPCID.Nurse && 
                Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneOverworldHeight)
            {
                // Drop the CryoCoth accessory with 100% chance
                Item.NewItem(
                    npc.GetSource_Loot(), 
                    npc.getRect(), 
                    ModContent.ItemType<Items.Accessories.CryoCoth>()
                );
            }
        }
    }
}
