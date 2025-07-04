using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for ItemType<T>()
using Spiritrum.Content.Items.Placeable;

namespace Spiritrum.Content.Items.Tools // Make sure this matches your folder structure
{
	public class ReborniumPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Optional: Display name and tooltip can go in localization .hjson files
			// DisplayName.SetDefault("Bromium Pickaxe"); // Uncomment to set display name directly
			// Tooltip.SetDefault("Has 100% pickaxe power."); // Uncomment to set tooltip directly
		}

		public override void SetDefaults()
		{
			// Common item properties
			Item.width = 32; // Adjust sprite width
			Item.height = 32; // Adjust sprite height
			Item.scale = 1f; // Adjust scale if needed
			Item.rare = ItemRarityID.White; // Set rarity
			Item.value = Item.sellPrice(gold: 1); // Set value (e.g., 1 gold)

			// Tool/Weapon properties
			Item.useStyle = ItemUseStyleID.Swing; // Swing animation
			Item.useTime = 10; // How fast the tool is used
			Item.useAnimation = 15; // How long the animation plays
			Item.autoReuse = true; // Can hold to continuously use
			Item.UseSound = SoundID.Item1; // Sound when used
			Item.damage = 5; // Damage (pickaxes deal damage, but usually not their main purpose)
			Item.DamageType = DamageClass.Melee; // Tools use melee damage type
			Item.knockBack = 2; // Knockback
            Item.scale = 0.9f; // Adjust scale to make the sprite more centered

			// Pickaxe specific property
			Item.pick = 59;
		}

		public override void AddRecipes()
		{
			// Create a recipe for the Bromium Pickaxe
			Recipe recipe = CreateRecipe();

			// Add ingredients
			recipe.AddIngredient(ItemType<ReborniumBar>(), 20); // Requires 20 of your Bromium Bar item

			// Specify the crafting station
			recipe.AddTile(TileID.Anvils); // Crafted at any Anvil

			// Register the recipe
			recipe.Register();
		}
	}
}
