using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Content.NPCS;
using Spiritrum.Content.Items;
using Spiritrum.Content.Items.Weapons;
using Spiritrum.Content.Items.Consumables;
using Spiritrum.Content.Items.Mounts;

namespace Spiritrum.Content.Global
{
    public class SpiritrumGlobalNPC : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add<Poutine>();
            }
        }

        public override void OnKill(NPC npc)
        {
            // Handle VoidHarbinger kill
            if (npc.type == ModContent.NPCType<VoidHarbinger>())
            {
                SpiritrumMod.downedVoidHarbinger = true;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
              // King Slime drops GelaticCrown with 10% chance
            if (npc.type == NPCID.KingSlime)
            {
                if (Main.rand.NextFloat() < 0.1f) // 10% chance
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<GelaticCrown>());
                }
            }
            
            // The Twins drop Twinarang with 5% chance
            if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
            {
                // Only drop when both twins are dead
                bool bothTwinsDead = false;
                
                if (npc.type == NPCID.Retinazer)
                {
                    // Check if Spazmatism is dead
                    bothTwinsDead = !NPC.AnyNPCs(NPCID.Spazmatism);
                }
                else if (npc.type == NPCID.Spazmatism)
                {
                    // Check if Retinazer is dead
                    bothTwinsDead = !NPC.AnyNPCs(NPCID.Retinazer);
                }
                
                if (bothTwinsDead && Main.rand.NextFloat() < 0.05f) // 5% chance
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<Twinarang>());
                }
            }

            // Deerclops drops Ski-dooKey with 15% chance
            if (npc.type == NPCID.Deerclops)
            {
                if (Main.rand.NextFloat() < 0.15f) // 15% chance
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SkiDooKey>());
                }
            }
        }
    }
}
