using Terraria;
using Terraria.ModLoader;

namespace Spiritrum.Content.Buffs
{
    public class SnowyOwlStaffBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Description should be set in localization files
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
        }
    }
}
