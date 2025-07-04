using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Consumables
{
    public class Poutine : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 1);
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 28800; // 8 minutes (480 seconds)
        }

        public override bool? UseItem(Player player)
        {
            // Add Plenty Satisfied buff for 8 minutes
            player.AddBuff(BuffID.WellFed3, 28800); // Using WellFed3 as "Plenty Satisfied"
            return true;
        }
    }
}
