using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Spiritrum.Config
{
    /// <summary>
    /// Configuration class for Spiritrum mod settings
    /// </summary>
    public class SpiritrumConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;        [Header("UIPositioning")]
        // X coordinate for Ideology Slot (auto-labeled in localization)
        [Range(0, 3840)] // Supports up to 4K resolution
        [DefaultValue(1665)]
        public int IdeologySlotXPosition;
        
        // X coordinate for Mode Slot (auto-labeled in localization)
        [Range(0, 3840)] // Supports up to 4K resolution
        [DefaultValue(1665)]
        public int ModeSlotXPosition;
        
        public override void OnChanged()
        {
            // This will be called whenever settings are changed in-game
            // We need to check if the mod instance is available since config might load before mod
            if (SpiritrumMod.Instance != null)
            {
                // We'll handle the actual UI updates in the UI systems
                SpiritrumMod.Instance.ReloadUIPositions();
            }
        }
    }
}
