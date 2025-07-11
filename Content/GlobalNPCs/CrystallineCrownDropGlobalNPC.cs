using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace Spiritrum.Content.GlobalNPCs
{
    public class CrystallineCrownDropGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.QueenSlimeBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Spiritrum.Content.Items.Accessories.CrystallineCrown>(), 20)); // 5% chance
            }
        }
    }
}
