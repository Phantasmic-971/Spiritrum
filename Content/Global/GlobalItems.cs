using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Spiritrum.Content.Global
{
    // This class is for modifications to vanilla items
    // Currently empty but with defensive serialization to prevent UnloadedGlobalItem errors
    public class GlobalItems : GlobalItem
    {
        public override void SaveData(Terraria.Item item, TagCompound tag)
        {
            // Defensive save - only save if there's actual data to save
            // Currently no persistent data needed for this GlobalItem
        }

        public override void LoadData(Terraria.Item item, TagCompound tag)
        {
            // Defensive load - gracefully handle missing data
            // Use TryGet to avoid KeyNotFoundException
            
            // Remove any problematic legacy keys
            if (tag.ContainsKey("globalItemData"))
            {
                tag.Remove("globalItemData");
            }
        }

        public override bool InstancePerEntity => false; // Prevent unnecessary instances
    }
}