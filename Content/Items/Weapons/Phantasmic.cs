using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using System; // Needed for MathHelper.ToRadians
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
	public class Phantasmic : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip is now handled directly via ModifyTooltips
		}

		public override void SetDefaults()
		{
			Item.damage = 90; // Base damage of the bow
			Item.DamageType = DamageClass.Ranged; // Ranged weapon
			Item.width = 30; // Reduced sprite width
			Item.height = 20; // Reduced sprite height
			Item.useTime = 12; // Fast use time (5 shots per second)
			Item.useAnimation = 12; // Matches use time
			Item.useStyle = ItemUseStyleID.Shoot; // Bow style
			Item.noMelee = true; // Does not deal melee damage
			Item.knockBack = 2; // Low knockback
			Item.value = Item.buyPrice(gold: 50); // Value in coins
			Item.rare = ItemRarityID.Cyan; // Post-Golem rarity (though requires Luminite Bars and Phantasm)
			Item.UseSound = SoundID.Item5; // Bow sound
			Item.autoReuse = true; // Automatically reuses
			Item.shoot = ModContent.ProjectileType<PhantomArrowProjectile>(); // Shoots arrows
			Item.shootSpeed = 25f; // Arrow speed
			Item.scale = 0.76f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			const int NumProjectiles = 5; // The number of projectiles that this bow will shoot.

			for (int i = 0; i < NumProjectiles; i++) {
				// Rotate the velocity randomly by 12 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
			}
			return false; // Return false because we don't want tModLoader to shoot the default projectile
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(1, 0); // Adjust the sprite position to be more inside the player
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			var devLine = new TooltipLine(Mod, "PhantasmicTipDeveloper", "Developer weapon")
			{
				OverrideColor = new Color(255, 200, 50)
			};
			tooltips.Add(devLine);
			tooltips.Add(new TooltipLine(Mod, "PhantasmicTipShoot", "Shoots 5 fast arrows with slight inaccuracy"));

			// Add a tooltip line for the ammo consumption chance, which is present in the code
			tooltips.Add(new TooltipLine(Mod, "PhantasmicTipAmmo", "75% chance to not consume ammo")); // Added line for ammo effect

			// You can add more lines or modify existing ones here
		}


		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Phantasm, 1); // Requires 1 Phantasm bow
			recipe.AddIngredient(ItemID.LunarBar, 10); // Requires 10 Luminite Bars
			recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			recipe.Register();
		}

		// This method gives this bow a 75% chance to not consume ammo
		public override bool CanConsumeAmmo(Item ammo, Player player) {
			return Main.rand.NextFloat() >= 0.75f; // This means 25% chance to consume, or 75% chance NOT to consume
		}
	}
}