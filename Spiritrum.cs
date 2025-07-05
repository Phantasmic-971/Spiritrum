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
        internal static SpiritrumMod Instance;
        internal IdeologySlotUISystem IdeologySlotUI;
        internal ModeSlotUISystem ModeSlotUI;
        public static bool downedVoidHarbinger;
        
        public SpiritrumMod()
        {
            // Store instance reference immediately upon construction
            Instance = this;
        }

        public override void Unload()
        {
            Logger.Info("Spiritrum mod: Unloading...");
            
            // Reset boss flags
            downedVoidHarbinger = false;
            
            // Clear instance reference
            Instance = null;
        }

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
        
        public bool GetDownedVoidHarbinger()
        {
            return downedVoidHarbinger;
        }
    }

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
