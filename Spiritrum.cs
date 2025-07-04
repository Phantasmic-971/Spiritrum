using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using System.Collections.Generic;
using Spiritrum.Content.Global;
using System.IO;
using Terraria.ModLoader.IO;
using Spiritrum.Systems;
using Spiritrum.Config;

namespace Spiritrum
{
    public class SpiritrumMod : Mod
    {
        // Static reference to the mod instance for easier access
        // Initialize with the constructor to ensure it's available earlier
        internal static SpiritrumMod Instance;
        
        // UI Systems references for position updates
        internal IdeologySlotUISystem IdeologySlotUI;
        internal ModeSlotUISystem ModeSlotUI;
        
        // Boss flags
        public static bool downedVoidHarbinger;
        
        public SpiritrumMod()
        {
            // Store instance reference immediately upon construction
            Instance = this;
        }

        public override void Unload()
        {
            // Nothing special to unload
            Logger.Info("Spiritrum mod: Unloading Time prefixes and reforging system...");
            
            // Reset boss flags
            downedVoidHarbinger = false;
            
            // Clear instance reference
            Instance = null;
        }
        
        /// <summary>
        /// Reloads UI positions from config when settings change
        /// </summary>
        internal void ReloadUIPositions()
        {
            if (!Main.dedServ)
            {
                // Only update if UI systems are initialized
                if (IdeologySlotUI?.ideologySlotUI != null)
                {
                    IdeologySlotUI.UpdatePositionsFromConfig();
                }
                
                if (ModeSlotUI?.modeSlotUI != null)
                {
                    ModeSlotUI.UpdatePositionsFromConfig();
                }
            }
        }
        
        /// <summary>
        /// Returns whether the Void Harbinger boss has been defeated
        /// </summary>
        public bool GetDownedVoidHarbinger()
        {
            return downedVoidHarbinger;
        }
        
        /// <summary>
        /// Registers a UI system with the mod instance
        /// </summary>
        public void RegisterUISystem(object uiSystem)
        {
            if (uiSystem is IdeologySlotUISystem ideologySystem)
            {
                IdeologySlotUI = ideologySystem;
            }
            else if (uiSystem is ModeSlotUISystem modeSystem)
            {
                ModeSlotUI = modeSystem;
            }
        }
    }
      // WorldData class to handle saving and loading boss flags
    public class SpiritrunWorldData : ModSystem
    {
        public override void SaveWorldData(TagCompound tag)
        {
            // Save boss flags
            tag["downedVoidHarbinger"] = SpiritrumMod.downedVoidHarbinger;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            // Load boss flags
            SpiritrumMod.downedVoidHarbinger = tag.ContainsKey("downedVoidHarbinger") && tag.GetBool("downedVoidHarbinger");
        }
    }
}
