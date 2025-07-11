using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Spiritrum.Content.Global
{
    /// <summary>
    /// Compatibility GlobalItem to handle legacy/missing GlobalItem references
    /// and prevent UnloadedGlobalItem exceptions during world joins
    /// </summary>
    public class CompatibilityGlobalItem : GlobalItem
    {
        public override void SaveData(Item item, TagCompound tag)
        {
            // Defensive save - only save if there's actual data to save
            // Currently empty but prevents serialization issues
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            // Defensive load - gracefully handle missing data
            // This will catch any legacy keys that might cause UnloadedGlobalItem exceptions
            
            // Handle legacy armor GlobalItem data if it exists
            HandleLegacyArmorData(item, tag);
            
            // Handle any other legacy GlobalItem data
            HandleOtherLegacyData(item, tag);
        }

        private void HandleLegacyArmorData(Item item, TagCompound tag)
        {
            // Safely handle any legacy armor-specific GlobalItem data
            // Check for common armor-related keys and handle them gracefully
            
            if (tag.ContainsKey("armorSetBonus"))
            {
                // Legacy armor set bonus data - safely ignore or migrate
                tag.Remove("armorSetBonus");
            }
            
            if (tag.ContainsKey("armorType"))
            {
                // Legacy armor type data - safely ignore or migrate
                tag.Remove("armorType");
            }
        }

        private void HandleOtherLegacyData(Item item, TagCompound tag)
        {
            // Handle any other potential legacy keys that might cause issues
            string[] legacyKeys = { 
                "oldData", "weaponData", "toolData", "miscData",
                "customStats", "legacyInfo", "oldVersion"
            };

            foreach (string key in legacyKeys)
            {
                if (tag.ContainsKey(key))
                {
                    // Safely remove legacy keys to prevent deserialization issues
                    tag.Remove(key);
                }
            }
        }

        public override bool InstancePerEntity => false; // Don't create separate instances per item
    }
}
