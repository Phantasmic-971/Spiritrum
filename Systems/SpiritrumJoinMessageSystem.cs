using Terraria.ModLoader;
using Terraria;
using System;

namespace Spiritrum.Systems
{
    public class SpiritrumJoinMessageSystem : ModSystem
    {
        private double joinTime = -1;
        private bool messageShown = false;

        public override void OnWorldLoad()
        {
            joinTime = Main.time;
            messageShown = false;
        }

        public override void PreUpdateWorld()
        {
            if (!messageShown && joinTime >= 0 && Main.time - joinTime >= 60) // 60 ticks = 1 second
            {
                Main.NewText("Spiritrum is made to be played individually without any mod that adds content that affects gameplay. Quality of life such as magic storage and Recipe Browser are recommended to be used.", 200, 100, 255);
                messageShown = true;
            }
        }
    }
}
