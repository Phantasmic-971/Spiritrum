using Terraria;
using Terraria.ModLoader;
using System.Linq;
using Spiritrum.Content.Items.Modes;

namespace Spiritrum.Content.Global
{
    public class GeotModeGlobalPlayer : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            // Check if GeotMode is equipped in any accessory slot
            bool geotModeEquipped = IsGeotModeEquipped();
            
            // Apply restrictions if GeotMode is equipped
            if (geotModeEquipped)
            {
                ApplyGeotModeRestrictions();
            }
        }
        
        private bool IsGeotModeEquipped()
        {
            return Player.armor.Any(a => a.active && a.type == ModContent.ItemType<GeotMode>());
        }
        
        private void ApplyGeotModeRestrictions()
        {
            // Disable mana
            Player.statMana = 0;            // Current mana
            Player.statManaMax2 = 0;        // Maximum mana (after buffs/equipment)
            
            // Disable summons
            Player.numMinions = 0;          // Current minion count
            Player.maxMinions = 0;          // Maximum minion slots
            
            // Prevent mana regeneration
            Player.manaRegenDelay = 60;     // Keep regeneration on cooldown
        }
    }
}
