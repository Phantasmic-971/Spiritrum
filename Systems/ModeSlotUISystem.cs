using System;
using Terraria; // Provides access to core Terraria classes and game state, like Main, Player, Item, etc.
using Terraria.ModLoader; // Essential for creating mods, provides ModSystem, ModPlayer, ModItem, etc.
using Terraria.UI; // Provides UIState, UserInterface, UIElement for building custom game interfaces.
using System.Collections.Generic; // Provides List<T> for working with collections, used here for the list of interface layers.
using Spiritrum.UI; // Imports the namespace containing the custom ModeSlotUI class.
using Spiritrum.Config; // For accessing mod configuration

namespace Spiritrum.Systems
{
    // This ModSystem class is responsible for managing the custom Mode Slot UI.
    // It handles the initialization, drawing, and unloading of the UI,
    // integrating it seamlessly into Terraria's existing interface structure.
    public class ModeSlotUISystem : ModSystem
    {
        // A UserInterface instance is a tModLoader construct that manages a UIState.
        // It handles the updating and drawing of the UI elements contained within its current UIState.
        internal UserInterface modeSlotInterface;
        // This is a reference to our custom UIState, which defines the layout and behavior of the Mode Slot.
        internal ModeSlotUI modeSlotUI;

        // The Load method is called when the mod is loaded into the game.
        // It's the ideal place to initialize resources that the system will use.
        public override void Load()
        {
            // UI elements should only be created on the client-side.
            // Main.dedServ is true if the code is running on a dedicated server, which has no graphical interface.
            if (!Main.dedServ)
            {                try
                {
                    // Log setup start
                    ModContent.GetInstance<SpiritrumMod>()?.Logger.Info("ModeSlotUI - Starting initialization");
                    
                    // Check if ModeSlotUI type exists
                    Type modeSlotUIType = typeof(Spiritrum.UI.ModeSlotUI);
                    if (modeSlotUIType != null)
                    {
                        ModContent.GetInstance<SpiritrumMod>()?.Logger.Info($"ModeSlotUI - Type found: {modeSlotUIType.FullName}");
                    }
                    
                    // Instantiate UserInterface first (it has fewer dependencies)
                    modeSlotInterface = new UserInterface();
                    ModContent.GetInstance<SpiritrumMod>()?.Logger.Info("ModeSlotUI - UserInterface created");
                    
                    // Create the UI state
                    modeSlotUI = new Spiritrum.UI.ModeSlotUI();
                    ModContent.GetInstance<SpiritrumMod>()?.Logger.Info("ModeSlotUI - ModeSlotUI instance created");
                    
                    // Activate the UIState
                    modeSlotUI.Activate();
                    ModContent.GetInstance<SpiritrumMod>()?.Logger.Info("ModeSlotUI - ModeSlotUI activated");
                    
                    // Set the state
                    modeSlotInterface.SetState(modeSlotUI);
                    ModContent.GetInstance<SpiritrumMod>()?.Logger.Info("ModeSlotUI - State set in UserInterface");
                    
                    // Register with mod instance
                    var modInstance = ModContent.GetInstance<SpiritrumMod>();
                    if (modInstance != null)
                    {
                        modInstance.RegisterUISystem(this);
                        ModContent.GetInstance<SpiritrumMod>()?.Logger.Info("ModeSlotUI - UI system registered with mod");
                    }
                    
                    ModContent.GetInstance<SpiritrumMod>()?.Logger.Info("ModeSlotUI - Initialization complete");
                }
                catch (Exception ex)
                {
                    // Log the full exception de
                    // tails to better diagnose the issue
                    ModContent.GetInstance<SpiritrumMod>()?.Logger.Error($"Error initializing ModeSlotUI: {ex.Message}");
                    ModContent.GetInstance<SpiritrumMod>()?.Logger.Error($"Stack trace: {ex.StackTrace}");
                }
            }
        }
        
        // Method to update UI position from config
        internal void UpdatePositionsFromConfig()
        {
            if (modeSlotUI != null && !Main.dedServ)
            {
                var config = ModContent.GetInstance<SpiritrumConfig>();
                if (config != null)
                {
                    // Update the UI position from config
                    modeSlotUI.UpdatePosition(config.ModeSlotXPosition);
                }
            }
        }

        // ModifyInterfaceLayers is a hook that allows mods to add, remove, or reorder
        // Terraria's UI drawing layers. Each layer is drawn sequentially.
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            // Find the index of the vanilla inventory layer.
            // We want to draw our Mode Slot UI relative to the inventory.
            // GameInterfaceLayer.Name is a string identifier for each layer (e.g., "Vanilla: Inventory", "Vanilla: Hotbar").
            int inventoryLayerIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            
            // If the inventory layer was found (index is not -1), proceed.
            if (inventoryLayerIndex != -1)
            {
                // Insert our custom UI layer into the list.
                // We insert it at inventoryLayerIndex + 1 to make it draw immediately *after* the vanilla inventory,
                // effectively appearing on top of it but potentially below other UI elements like the map or settings.
                layers.Insert(inventoryLayerIndex + 1, new LegacyGameInterfaceLayer(
                    "Spiritrum: ModeSlotUI", // A unique name for our custom interface layer.
                    delegate // This anonymous method (delegate) is the drawing function for our layer.
                    {
                        // Check if the modeSlotInterface and its CurrentState (our modeSlotUI) are valid.
                        if (modeSlotInterface?.CurrentState != null)
                        {
                            // Update the UI. Main._drawInterfaceGameTime is the specific GameTime instance
                            // that should be used for UI logic to ensure it's synchronized with the game's interface updates.
                            modeSlotInterface.Update(Main._drawInterfaceGameTime);
                            // Draw the UI. Main.spriteBatch is the SpriteBatch instance used for drawing game interface elements.
                            modeSlotInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                        }
                        return true; // Return true to indicate that the layer was drawn successfully.
                    },
                    InterfaceScaleType.UI) // Specifies that this layer should scale with the game's UI scale setting.
                );
            }
        }

        // The Unload method is called when the mod is unloaded from the game.
        // It's crucial for cleaning up resources to prevent memory leaks or errors if the mod is reloaded.
        public override void Unload()
        {
            // Nullify references to our UI objects.
            // This allows the garbage collector to reclaim the memory they were using,
            // and ensures that old instances don't persist if the mod is reloaded.
            modeSlotUI = null;
            modeSlotInterface = null;
        }
    }
}
