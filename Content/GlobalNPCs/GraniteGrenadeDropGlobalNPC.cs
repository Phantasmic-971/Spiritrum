using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace Spiritrum.Content.GlobalNPCs
{
    public class GraniteGrenadeDropGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Granite Elemental (ID: 483) and Granite Golem (ID: 482)
            if (npc.type == 483 || npc.type == 482)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Spiritrum.Content.Items.Weapons.GraniteGrenade>(), 10, 1, 1)); // 10% chance, 1 drop
            }
        }
    }
}
