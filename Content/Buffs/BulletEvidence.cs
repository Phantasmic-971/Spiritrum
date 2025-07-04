using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Spiritrum.Content.Buffs
{
    public class BulletEvidence : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Description are now set via localization files in tModLoader 1.4+
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Item heldItem = player.HeldItem;
            if (heldItem != null && heldItem.damage > 0 && heldItem.DamageType == DamageClass.Ranged)
            {
                int ammoType = heldItem.useAmmo;
                if (ammoType == AmmoID.Bullet || ammoType == AmmoID.Rocket)
                {
                    player.GetCritChance(DamageClass.Ranged) += 8f;
                    player.ammoBox = true; // 20% chance to not consume ammo
                }
            }
        }
    }
}