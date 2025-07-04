using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{ 
	public class BromiumSlayer : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Spiritrum.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 42; // Increased damage
			Item.DamageType = DamageClass.Melee;
			Item.width = 20; // Reduced width
			Item.height = 20; // Reduced height
			Item.useTime = 17;
			Item.useAnimation = 17;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5; // Increased knockback
			Item.value = Item.buyPrice(gold: 10); // Increased value
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<BromiumBar>(), 15); // Requires 15 Bromium Bars
			recipe.AddIngredient(ItemID.Muramasa, 1);
            recipe.AddIngredient(ItemID.BladeofGrass, 1);
			recipe.AddIngredient(ItemID.FieryGreatsword, 1);
			recipe.AddTile(TileID.Anvils); // Crafted at an Anvil
			recipe.Register();
		}
	}
}
