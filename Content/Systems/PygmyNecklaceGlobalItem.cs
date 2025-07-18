using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Spiritrum.Content.Systems
{
    public class PygmyNecklaceGlobalItem : GlobalItem
    {
        public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
        {
            if (item.type == ItemID.PygmyNecklace)
            {
                for (int i = 0; i < player.armor.Length; i++)
                {
                    Item acc = player.armor[i];
                    if (acc != null && !acc.IsAir)
                    {
                        if (acc.type == ModContent.ItemType<Spiritrum.Content.Items.Accessories.WitchLocket>() || acc.type == ModContent.ItemType<Spiritrum.Content.Items.Accessories.VoidPendant>())
                        {
                            if (player.whoAmI == Main.myPlayer)
                                Main.NewText("You cannot equip the Pygmy Necklace while Witch Locket or Void Pendant is equipped.", 255, 255, 255);
                            return false;
                        }
                    }
                }
            }
            return base.CanEquipAccessory(item, player, slot, modded);
        }
    }
}
