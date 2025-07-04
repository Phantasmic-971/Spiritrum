using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Spiritrum.UI;
using Spiritrum.Config;

namespace Spiritrum.Systems
{
    public class IdeologySlotUISystem : ModSystem
    {
        internal UserInterface ideologySlotInterface;
        internal IdeologySlotUI ideologySlotUI;
          public override void Load()
        {
            if (!Main.dedServ)
            {
                try
                {
                    // Create and initialize the UI elements
                    ideologySlotUI = new IdeologySlotUI();
                    ideologySlotUI.Activate();
                    ideologySlotInterface = new UserInterface();
                    ideologySlotInterface.SetState(ideologySlotUI);
                    
                    // Register this system with the mod instance for config updates
                    var modInstance = ModContent.GetInstance<SpiritrumMod>();
                    if (modInstance != null)
                    {
                        modInstance.RegisterUISystem(this);
                    }
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occur during initialization
                    ModContent.GetInstance<SpiritrumMod>().Logger.Error($"Error initializing IdeologySlotUI: {ex.Message}");
                }
            }
        }
        
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryLayerIndex != -1)
            {
                layers.Insert(inventoryLayerIndex + 1, new LegacyGameInterfaceLayer(
                    "Spiritrum: IdeologySlotUI",
                    () =>
                    {
                        if (ideologySlotInterface?.CurrentState != null)
                        {
                            ideologySlotInterface.Update(Main._drawInterfaceGameTime);
                            ideologySlotInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }        }

        // Method to update UI position from config
        internal void UpdatePositionsFromConfig()
        {
            if (ideologySlotUI != null && !Main.dedServ)
            {
                var config = ModContent.GetInstance<SpiritrumConfig>();
                if (config != null)
                {
                    // Update the UI position from config
                    ideologySlotUI.UpdatePosition(config.IdeologySlotXPosition);
                }
            }
        }

        public override void Unload()
        {
            ideologySlotUI = null;
            ideologySlotInterface = null;
        }
    }
}
