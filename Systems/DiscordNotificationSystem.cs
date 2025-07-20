using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Systems
{
    public class DiscordNotificationSystem : ModSystem
    {
        private int notificationTimer = 0;
        private bool shouldShowNotification = false;

        public override void OnWorldLoad()
        {
            shouldShowNotification = true;
            notificationTimer = 0;
        }

        public override void PostUpdateTime()
        {
            if (shouldShowNotification)
            {
                notificationTimer++;
                // 3 seconds = 180 ticks (60 ticks per second)
                if (notificationTimer >= 180)
                {
                    Main.NewText("Make sure to join the discord server: https://discord.gg/mNwZWRDJrr", Color.LightBlue);
                    shouldShowNotification = false;
                    notificationTimer = 0;
                }
            }
        }
    }
}
