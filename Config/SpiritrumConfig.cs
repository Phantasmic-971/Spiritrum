using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Spiritrum.Config
{
    public class SpiritrumConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;        
        
        [Header("UIPositioning")]
        [Label("Ideology Slot X Position (% of screen width)")]
        [Range(10, 100)]
        [DefaultValue(88)]
        [Increment(5)]
        [DrawTicks]
        [SliderColor(32, 110, 160)]
        public int IdeologySlotXPositionPercent;
        
        [Label("Mode Slot X Position (% of screen width)")]
        [Range(10, 100)]
        [DefaultValue(88)]
        [Increment(5)]
        [DrawTicks]
        [SliderColor(32, 110, 160)]
        public int ModeSlotXPositionPercent;
        
        public override void OnChanged()
        {
            if (SpiritrumMod.Instance != null)
            {
                SpiritrumMod.Instance.ReloadUIPositions();
            }
        }
    }
}
