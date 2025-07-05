using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Players;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Global
{
    public class SpiritrumGlobalItem : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<IdeologySlotPlayer>().inflictMidas)
            {
                target.AddBuff(BuffID.Midas, 180);
            }
        }
    }
}
