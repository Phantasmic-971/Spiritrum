using Terraria;
using Terraria.ModLoader;

namespace Spiritrum.Players
{
    // This class is used to track the count of various minions for display in buff tooltips
    public class MinionCounterPlayer : ModPlayer
    {
        public int namelessParasiteCount;

        public override void ResetEffects()
        {
            // Reset the minion count at the beginning of each update
            namelessParasiteCount = 0;
        }
    }
}
