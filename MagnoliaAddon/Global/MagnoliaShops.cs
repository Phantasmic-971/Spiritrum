using Spiritrum.MagnoliaAddon.Items.Equippable;
using Spiritrum.MagnoliaAddon.Items.Placeables;
using Spiritrum.MagnoliaAddon.Items.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.MagnoliaAddon.Global
{
    internal class MericaNPCShops : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                // Adding an item to a vanilla NPC is easy:
                // This item sells for the normal price.
                shop.Add<MoltenShuriken>(condition: Terraria.Condition.Hardmode);

            }
            if (shop.NpcType == NPCID.GoblinTinkerer)
            {
                // Adding an item to a vanilla NPC is easy:
                // This item sells for the normal price.
                shop.Add<SteelBar>(condition: Terraria.Condition.Hardmode);

            }
            if (shop.NpcType == NPCID.Cyborg)
            {
                // Adding an item to a vanilla NPC is easy:
                // This item sells for the normal price.
                shop.Add<MorbiumBar>(condition: Terraria.Condition.DownedCultist);


            }
        }
    }
}