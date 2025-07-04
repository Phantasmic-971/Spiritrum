using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for ItemType<T>()

namespace Spiritrum.Content.Items.Tools // Make sure this matches your folder structure
{
	public class BromiumPickaxe : ModItem
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
			Item.rare = ItemRarityID.Orange; // Set rarity
			Item.value = Item.sellPrice(gold: 1); // Set value (e.g., 1 gold)

			// Tool/Weapon properties
			Item.useStyle = ItemUseStyleID.Swing; // Swing animation
			Item.useTime = 10; // How fast the tool is used
			Item.useAnimation = 15; // How long the animation plays
			Item.autoReuse = true; // Can hold to continuously use
			Item.UseSound = SoundID.Item1; // Sound when used
			Item.damage = 10; // Damage (pickaxes deal damage, but usually not their main purpose)
			Item.DamageType = DamageClass.Melee; // Tools use melee damage type
			Item.knockBack = 3; // Knockback
            Item.scale = 0.7f; // Adjust scale to make the sprite more centered

			// Pickaxe specific property
			Item.pick = 115; // 115% pickaxe power
		}

		public override void AddRecipes()
		{
			// Create a recipe for the Bromium Pickaxe
			Recipe recipe = CreateRecipe();

			// Add ingredients
			recipe.AddIngredient(ItemType<BromiumBar>(), 20); // Requires 20 of your Bromium Bar item
			recipe.AddIngredient(ItemID.MoltenPickaxe, 1); // Requires a Molten Pickaxe as a base

			// Specify the crafting station
			recipe.AddTile(TileID.Anvils); // Crafted at any Anvil

			// Register the recipe
			recipe.Register();
		}
	}
}
