using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Players;

namespace Spiritrum.Content.Global
{
    public class SpiritrumGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[projectile.owner];
            if (owner.GetModPlayer<IdeologySlotPlayer>().inflictMidas)
            {
                target.AddBuff(BuffID.Midas, 180);
            }
        }
    }
}
