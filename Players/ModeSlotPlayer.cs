using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using Terraria.GameContent.UI;
using Terraria.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Players
{
    public class ModeSlotPlayer : ModPlayer
    {
        // The custom slot's item
        public Item modeSlotItem = new Item();

        public override void Initialize()
        {
            modeSlotItem = new Item();
            modeSlotItem.TurnToAir();
        }

        public override void SaveData(TagCompound tag)
        {
            tag["modeSlotItem"] = modeSlotItem.Clone(); // Store a clone of the item
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("modeSlotItem"))
            {
                modeSlotItem = tag.Get<Item>("modeSlotItem");
            }
        }

        public override void UpdateEquips()
        {
            if (!modeSlotItem.IsAir && modeSlotItem.accessory && IsModeItem(modeSlotItem))
            {
                if (modeSlotItem.ModItem != null)
                {
                    modeSlotItem.ModItem.UpdateAccessory(Player, false);
                }
            }
        }

        private bool IsModeItem(Item item)
        {
            // Accepts any item whose ModItem is in the Modes folder
            if (item.ModItem != null)
            {
                string modItemFullName = item.ModItem.GetType().FullName;
                // Looks for .Items.Modes. in the namespace path
                return modItemFullName != null && modItemFullName.Contains(".Items.Modes.");
            }
            return false;
        }
    }
}
