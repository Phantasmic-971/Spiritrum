using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Localization;

namespace Spiritrum.Content.Items.Placeables
{
    public class VoidHarbingerTrophy : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Harbinger Trophy");
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // General properties
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.VoidHarbingerTrophyTile>());
            
            Item.width = 32;   
            Item.height = 32;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 0, 0); // 1 gold
            Item.master = false; // Not master mode exclusive
        }
    }
}
