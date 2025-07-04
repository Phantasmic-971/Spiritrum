using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Spiritrum.Content.Items.Modes;

namespace Spiritrum.Content.Global
{
    // This global item class prevents mana stars from working and minions from being summoned when GeotMode is equipped
    public class GeotModeGlobalItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            // Check if GeotMode is equipped in any accessory slot
            bool geotModeEquipped = player.armor.Any(a => a.active && a.type == ModContent.ItemType<GeotMode>());
            
            // If GeotMode is equipped, prevent using minion summoning items
            if (geotModeEquipped && item.DamageType == DamageClass.Summon)
            {
                return false;
            }
            
            // If GeotMode is equipped, prevent using mana-related items
            if (geotModeEquipped && item.mana > 0)
            {
                return false;
            }
            
            return base.CanUseItem(item, player);
        }
        
        public override bool OnPickup(Item item, Player player)
        {
            // Check if GeotMode is equipped
            bool geotModeEquipped = player.armor.Any(a => a.active && a.type == ModContent.ItemType<GeotMode>());
            
            // If GeotMode is equipped, prevent mana stars from being collected
            if (geotModeEquipped && (item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum))
            {
                return false;
            }
            
            return base.OnPickup(item, player);
        }
    }
}
