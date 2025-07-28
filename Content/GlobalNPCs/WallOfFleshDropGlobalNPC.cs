using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;

namespace Spiritrum.Content.GlobalNPCs
{
    public class WallOfFleshDropGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Spiritrum.Content.Items.Misc.NoteAboutHardMode>(), 1));
            }
        }
    }
}
