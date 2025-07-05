using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for TileType<T>()
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Placeables
{
	public class BromiumBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Optional: Display name and tooltip can go in localization .hjson files
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 9999; // Increased stack size to 9999
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Orange;

			// Properties to make the item placeable as a tile
			Item.useStyle = ItemUseStyleID.Swing; // The animation when using the item
			Item.useTurn = true; // Allows the player to turn while using
			Item.useAnimation = 15; // How long the animation plays
			Item.useTime = 10; // How fast the item can be used (placement speed)
			Item.autoReuse = true; // Allows holding the use button to place continuously
			Item.consumable = true; // The item is consumed when a tile is placed

			// Link this item to the tile it should place
			// You NEED to create a ModTile class named BromiumBarTile
			Item.createTile = TileType<Tiles.BromiumBarTile>(); // Assuming your tile class is in Content/Tiles
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<BromiumOre>(), 4); // Requires 4 Bromium Ore
            recipe.AddTile(TileID.Hellforge); // Crafted at a Hellforge
			recipe.Register();
		}
	}
}
