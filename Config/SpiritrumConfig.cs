using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Spiritrum.Config
{
    public class SpiritrumConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;        [Header("UIPositioning")]
        [Range(0, 3840)]
        [DefaultValue(1665)]
        public int IdeologySlotXPosition;
        
        [Range(0, 3840)]
        [DefaultValue(1665)]
        public int ModeSlotXPosition;
        
        public override void OnChanged()
        {
            if (SpiritrumMod.Instance != null)
            {
                SpiritrumMod.Instance.ReloadUIPositions();
            }
        }
    }
}
