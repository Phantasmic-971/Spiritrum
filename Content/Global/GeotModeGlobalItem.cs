using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Linq;
using Spiritrum.Content.Items.Modes;

namespace Spiritrum.Content.Global
{
    public class GeotModeGlobalItem : GlobalItem
    {
        public override bool CanUseItem(Item item, Player player)
        {
            // Check if GeotMode is equipped in any accessory slot
            bool geotModeEquipped = IsGeotModeEquipped(player);
            
            // When GeotMode is equipped:
            if (geotModeEquipped)
            {
                // Prevent using minion summoning items
                if (item.DamageType == DamageClass.Summon)
                {
                    return false;
                }
                
                // Prevent using mana-consuming items
                if (item.mana > 0)
                {
                    return false;
                }
            }
            
            // Allow normal usage for all other cases
            return base.CanUseItem(item, player);
        }
        
        public override bool OnPickup(Item item, Player player)
        {
            // Check if GeotMode is equipped
            bool geotModeEquipped = IsGeotModeEquipped(player);
            
            // When GeotMode is equipped:
            if (geotModeEquipped)
            {
                // Prevent picking up mana regeneration items
                if (item.type == ItemID.Star || item.type == ItemID.SoulCake || item.type == ItemID.SugarPlum)
                {
                    return false;
                }
            }
            
            // Allow normal pickup for all other cases
            return base.OnPickup(item, player);
        }
        
        private bool IsGeotModeEquipped(Player player)
        {
            return player.armor.Any(a => a.active && a.type == ModContent.ItemType<GeotMode>());
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            // Defensive save - only save if there's actual data to save
            // Currently no persistent data needed for this GlobalItem
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            // Defensive load - gracefully handle missing data
            // Use TryGet to avoid KeyNotFoundException
            
            // Remove any problematic legacy keys
            if (tag.ContainsKey("geotModeData"))
            {
                tag.Remove("geotModeData");
            }
        }

        public override bool InstancePerEntity => false; // Prevent unnecessary instances
    }
}
