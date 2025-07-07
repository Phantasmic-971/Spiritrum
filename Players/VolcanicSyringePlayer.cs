using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Players
{
    public class VolcanicSyringePlayer : ModPlayer
    {
        public bool volcanicSyringeEquipped;
        public override void ResetEffects()
        {
            volcanicSyringeEquipped = false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (volcanicSyringeEquipped && info.Damage > 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    Vector2 velocity = new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-6f, -2f));
                    int proj = Projectile.NewProjectile(Player.GetSource_Misc("VolcanicSyringe"), Player.Center, velocity, Terraria.ID.ProjectileID.GreekFire1, 40, 7f, Player.whoAmI);
                    Main.projectile[proj].friendly = true;
                    Main.projectile[proj].hostile = false;
                    Main.projectile[proj].timeLeft = 60;
                }
            }
        }
    }
}
