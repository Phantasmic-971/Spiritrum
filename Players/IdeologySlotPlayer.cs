using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Spiritrum.Players
{
    public class IdeologySlotPlayer : ModPlayer
    {
        public Item ideologySlotItem = new Item();
        public bool extraMoneyDrop;
        public bool corpoBuff;
        public bool teamBuff;
        public bool natureRegen;
        public bool shareBuff;
        public bool inflictMidas;
        public float rangedVelocity;

        public override void Initialize()
        {
            ideologySlotItem = new Item();
            ideologySlotItem.TurnToAir();
        }

        public override void SaveData(TagCompound tag)
        {
            tag["ideologySlotItem"] = ideologySlotItem.Clone();
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("ideologySlotItem"))
                ideologySlotItem = tag.Get<Item>("ideologySlotItem");
        }

        public override void UpdateEquips()
        {
            if (!ideologySlotItem.IsAir && ideologySlotItem.accessory && IsIdeologyItem(ideologySlotItem))
            {
                if (ideologySlotItem.ModItem != null)
                {
                    ideologySlotItem.ModItem.UpdateAccessory(Player, false);
                }
            }
        }        public override void ResetEffects()
        {
            extraMoneyDrop = false;
            corpoBuff = false;
            teamBuff = false;
            natureRegen = false;
            shareBuff = false;
            inflictMidas = false;
            rangedVelocity = 0f;
        }

        public void InflictMidasOnHit(NPC target)
        {
            if (inflictMidas)
            {
                target.AddBuff(Terraria.ID.BuffID.Midas, 180);
            }
        }

        private bool IsIdeologyItem(Item item)
        {
            if (item.ModItem != null)
            {
                string modItemFullName = item.ModItem.GetType().FullName;
                return modItemFullName != null && modItemFullName.Contains(".Items.Ideology.");
            }
            return false;
        }
    }
}
