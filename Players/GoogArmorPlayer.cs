using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Spiritrum.Players
{
    public class GoogArmorPlayer : ModPlayer
    {
        public bool hasGoogSet;

        public override void ResetEffects()
        {
            hasGoogSet = false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (hasGoogSet)
            {
                SoundEngine.PlaySound(new SoundStyle("Spiritrum/Sounds/Goog"), Player.position);
            }
        }
    }
}
