using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace Spiritrum.Content.GlobalNPCs
{
    public class WeaponDropsGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Gelatic Crown: 5% from King Slime (ID 50)
            if (npc.type == NPCID.KingSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Spiritrum.Content.Items.Accessories.GelaticCrown>(), 20));
            }
            // BoneCaster: 10% from Dark Caster (ID 32)
            if (npc.type == NPCID.DarkCaster)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Spiritrum.Content.Items.Weapons.BoneCaster>(), 10));
            }
            // Nut Cracker: 5% from Nutcracker (ID 349)
            if (npc.type == 349) // Nutcracker
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Spiritrum.Content.Items.Weapons.NutCracker>(), 20));
            }
        }
    }
}
