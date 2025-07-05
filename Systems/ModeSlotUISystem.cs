using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System.Collections.Generic;
using Spiritrum.UI;
using Spiritrum.Config;

namespace Spiritrum.Systems
{
    public class ModeSlotUISystem : ModSystem
    {
        internal UserInterface modeSlotInterface;
        internal ModeSlotUI modeSlotUI;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                try
                {
                    // Create and initialize the UI elements
                    modeSlotInterface = new UserInterface();
                    modeSlotUI = new ModeSlotUI();
                    modeSlotUI.Activate();
                    modeSlotInterface.SetState(modeSlotUI);
                    
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
                    ModContent.GetInstance<SpiritrumMod>().Logger.Error($"Error initializing ModeSlotUI: {ex.Message}");
                }
            }
        }
        
        internal void UpdatePositionsFromConfig()
        {
            if (modeSlotUI != null && !Main.dedServ)
            {
                var config = ModContent.GetInstance<SpiritrumConfig>();
                if (config != null)
                {
                    // Calculate position based on percentage of screen width
                    float screenPosition = Main.screenWidth * (config.ModeSlotXPositionPercent / 100f);
                    
                    // Update the UI position from calculated value
                    modeSlotUI.UpdatePosition((int)screenPosition);
                }
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryLayerIndex != -1)
            {
                layers.Insert(inventoryLayerIndex + 1, new LegacyGameInterfaceLayer(
                    "Spiritrum: ModeSlotUI",
                    () =>
                    {
                        if (modeSlotInterface?.CurrentState != null)
                        {
                            modeSlotInterface.Update(Main._drawInterfaceGameTime);
                            modeSlotInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }

        public override void Unload()
        {
            modeSlotUI = null;
            modeSlotInterface = null;
        }
    }
}
