using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for TooltipLine
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent; // Needed for ItemType<T>() and ProjectileType<T>()
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Weapons // Make sure this matches your folder structure
{
	public class BromiumStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Optional: Display name can go in localization .hjson files
			// DisplayName.SetDefault("Bromium Staff"); // Uncomment to set display name directly

			// Tooltip is now handled directly via ModifyTooltips.
			// Removed the comment about localization handling the tooltip.

			// Sets the item's glowmask. Requires you to create a texture file named YourItemName_Glow.png
			// Item.GlowMask = Request<Texture2D>(Texture + "_Glow").Value;
		}

		public override void SetDefaults()
		{
			// Common item properties
			Item.width = 40; // Adjust sprite width
			Item.height = 40; // Adjust sprite height
			Item.scale = 1f; // Adjust scale if needed
			Item.rare = ItemRarityID.Orange; // Set rarity (adjust as needed for Bromium tier)
			Item.value = Item.sellPrice(gold: 5); // Set value

			// Weapon properties
			Item.useStyle = ItemUseStyleID.Shoot; // Staff shooting style
			Item.useTime = 18; // How fast the staff is used
			Item.useAnimation = 18; // How long the animation plays
			Item.autoReuse = true; // Can hold to continuously use
			Item.UseSound = SoundID.Item43; // Example magic staff sound
			Item.noMelee = true; // This item does not deal melee damage

			// Magic weapon properties
			Item.mana = 10; // Mana cost per shot
			Item.damage = 45; // Base magic damage
			Item.DamageType = DamageClass.Magic; // Magic damage type

			// Projectile properties
			Item.shoot = ProjectileType<Projectiles.BromiumStaffProjectile>(); // The projectile this staff shoots
			Item.shootSpeed = 10f; // The speed of the projectile
			// Item.useAmmo = AmmoID.None; // Staves typically don't use ammo, but you can set this if needed
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add the specified tooltip line
			tooltips.Add(new TooltipLine(Mod, "BromiumStaffTipDebuffs", "Inflicts ichor and on fire on hit"));

			// You can add more lines or modify existing ones here
			// For example, if you uncommented the default tooltip in SetStaticDefaults, you could find and modify it here.
			// TooltipLine defaultTooltip = tooltips.Find(x => x.Name == "Tooltip0");
			// if (defaultTooltip != null)
			// {
			//     defaultTooltip.Text += "\nShoots a spectral bolt."; // Add the default text if you want
			// }
		}


		// You can override Shoot if you need custom projectile spawning logic (e.g., multiple projectiles, spread)
		// public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		// {
		//     // Example: Shoot 3 projectiles in a slight spread
		//     // float spread = MathHelper.ToRadians(10); // 10 degree spread
		//     // float baseSpeed = velocity.Length();
		//     // float startAngle = velocity.ToRotation() - spread / 2;
		//     // for (int i = 0; i < 3; i++)
		//     // {
		//     //     Vector2 newVelocity = new Vector2(baseSpeed, 0).RotatedBy(startAngle + spread * i);
		//     //     Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
		//     // }
		//     // return false; // Return false because we handled the projectile spawning manually
		//     return base.Shoot(player, source, position, velocity, type, damage, knockback); // Use default projectile spawning
		// }


		public override void AddRecipes()
		{
			// Create a recipe for the Bromium Staff
			Recipe recipe = CreateRecipe();

			// Add ingredients
			recipe.AddIngredient(ItemType<BromiumBar>(), 15); // Requires 15 of your Bromium Bar item
			recipe.AddIngredient(ItemID.MagicMissile, 1);
			recipe.AddIngredient(ItemID.FlowerofFire, 1);
			recipe.AddIngredient(ItemID.AquaScepter, 1);
			// Add other ingredients if needed (e.g., mana crystals, souls)
			// recipe.AddIngredient(ItemID.ManaCrystal, 1);

			// Specify the crafting station
			recipe.AddTile(TileID.Anvils); // Crafted at any Anvil (adjust as needed)

			// Register the recipe
			recipe.Register();
		}
	}
}
