using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Systems;

namespace Spiritrum.Content.Global
{
    public class PenumbriumOreGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                ModContent.GetInstance<PenumbriumWorldGenSystem>().GeneratePenumbriumOre();
            }
        }
    }
}
