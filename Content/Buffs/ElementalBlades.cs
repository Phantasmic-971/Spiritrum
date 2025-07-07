using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Spiritrum.Content.Buffs
{
    public class ElementalBlades : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elemental Blade");
            // Description.SetDefault("The Elemental Blade will fight for you");
            
            // Match the Snowy Owl Staff buff settings
            Main.buffNoTimeDisplay[Type] = false;
            Main.buffNoSave[Type] = true;
        }

        // We don't need an Update method at all - let the normal buff mechanics work
        // The projectile will refresh the buff as needed
    }
}
