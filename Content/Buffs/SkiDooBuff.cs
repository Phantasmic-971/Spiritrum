using Terraria;
using Terraria.ModLoader;

namespace Spiritrum.Content.Buffs
{
    public class SkiDooBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Description are now set via localization files in tModLoader 1.4+
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.mount.SetMount(ModContent.MountType<Content.Mounts.SkiDooMount>(), player);
            player.buffTime[buffIndex] = 10;
        }
    }
}
