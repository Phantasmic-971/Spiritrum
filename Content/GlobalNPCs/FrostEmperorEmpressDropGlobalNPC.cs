using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.GlobalNPCs
{
    public class FrostEmperorEmpressDropGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            // Ice Queen has NPC ID 345
            if (npc.type == 345 && Main.rand.NextFloat() < 0.20f) // 20% chance
            {
                Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ItemType<Items.Consumables.RoyalFrozenGem>());
            }
        }
    }
}
