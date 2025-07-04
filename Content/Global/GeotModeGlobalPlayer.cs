using Terraria;
using Terraria.ModLoader;
using System.Linq;
using Spiritrum.Content.Items.Modes;

namespace Spiritrum.Content.Global
{
    // This global player class forces mana and summons to 0 when GeotMode is equipped
    public class GeotModeGlobalPlayer : ModPlayer
    {
        public override void PostUpdateEquips()
        {
            // Check if GeotMode is equipped in any accessory slot
            bool geotModeEquipped = Player.armor.Any(a => a.active && a.type == ModContent.ItemType<GeotMode>());
            
            if (geotModeEquipped)
            {
                // Force values to be exactly 0 at the end of each update
                Player.statMana = 0;
                Player.statManaMax2 = 0;
                Player.numMinions = 0;
                Player.maxMinions = 0;
                
                // Prevent any mana regeneration
                Player.manaRegenDelay = 60; // Keep mana regen on cooldown
            }
        }
    }
}
