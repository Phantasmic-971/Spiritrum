using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{ 
	public class Bromecalibur : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Spiritrum.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 85; // Increased damage
			Item.DamageType = DamageClass.Melee;
			Item.width = 20; // Reduced width
			Item.height = 20; // Reduced height
			Item.useTime = 14;
			Item.useAnimation = 14;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8; // Increased knockback
			Item.value = Item.buyPrice(gold: 22); // Increased value
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.scale = 1.55f; // Adjust scale to make the sprite more centered
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<BromiumSlayer>(), 1); // Requires 1 Bromium Slayer
            recipe.AddIngredient(ItemID.SoulofFright, 10);
            recipe.AddIngredient(ItemID.SoulofMight, 10);
            recipe.AddIngredient(ItemID.SoulofSight, 10);
            recipe.AddIngredient(ItemID.Excalibur, 1);
            recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			recipe.Register();
		}
	}
}
