using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace Spiritrum.Content.Items.Materials
{
    public class Googling : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("The essence of a thousand searches");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(copper: 50);
            Item.rare = ItemRarityID.White;
        }
    }
}
