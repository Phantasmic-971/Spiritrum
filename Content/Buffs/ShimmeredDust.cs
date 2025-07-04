using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Items.Consumables;

namespace Spiritrum.Content.Buffs
{
    public class ShimmeredDust : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // Names/tooltips handled via .hjson localization
            // Alternatively, you can hardcode them here (less recommended):
            // DisplayName.SetDefault("Shimmering Dust");
            // Description.SetDefault("Increased movement speed and jump height.\nLeaving a sparkling trail.");

            Main.buffNoSave[Type] = false;   // persists through death
            Main.debuff[Type]     = false;   // not a debuff
            // BuffID.Sets.NoTimeDisplay does not exist in 1.4 and is removed
            // Main.buffNoTimeDisplay[Type] = true; // Uncomment if you don't want the timer shown
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // +20% movement speed
            player.moveSpeed += 0.20f;
            // +1 jump speed
            player.jumpSpeedBoost += 1.00f;

            // Sparkling dust effect
            if (Main.rand.NextBool(3))
            {
                int d = Dust.NewDust(
                    player.position,
                    player.width,
                    player.height,
                    DustID.GoldFlame
                );
                Main.dust[d].velocity *= 0.3f;
            }
        }
    }
}