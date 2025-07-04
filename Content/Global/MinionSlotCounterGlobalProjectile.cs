using Terraria;
using Terraria.ModLoader;

namespace Spiritrum.Content.Global
{
    public class MinionSlotCounterGlobalProjectile : GlobalProjectile
    {
        public override void PostAI(Projectile projectile)
        {
            // Only apply to our mod's minion projectiles
            if (projectile.minion && projectile.type == ModContent.ProjectileType<Projectiles.SnowyOwlStaffProjectile>())
            {
                Player player = Main.player[projectile.owner];
                
                // Count existing minions of this type
                int existingMinions = 0;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.owner == player.whoAmI && proj.type == projectile.type)
                    {
                        existingMinions++;
                    }
                }
                
                // If we have too many minions for our available slots, kill the oldest ones
                while (existingMinions > player.maxMinions)
                {
                    for (int i = 0; i < Main.maxProjectiles; i++)
                    {
                        Projectile proj = Main.projectile[i];
                        if (proj.active && proj.owner == player.whoAmI && proj.type == projectile.type)
                        {
                            proj.Kill();
                            existingMinions--;
                            break;
                        }
                    }
                }
            }
        }
    }
}
