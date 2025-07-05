using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using System; // Needed for MathHelper.ToRadians
using Spiritrum.Content.Items.Weapons;
using Spiritrum.Content.Items.Materials;
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Weapons
{
    public class BromeBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bromium Pistol");
            // Tooltip.SetDefault("A sleek pistol forged from Bromium. Fires a single shot.");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet; // Uses vanilla bullet
            Item.shootSpeed = 20f;
            Item.damage = 33;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Ranged; // Use DamageType for compatibility
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item41; // Sound of firing
            Item.useAmmo = AmmoID.Bullet; // Uses standard bullets as ammo
            Item.scale = 0.68f; // Adjust scale to make the sprite more centered
            Item.noMelee = true; // This item does not deal melee damage
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			const int NumProjectiles = 2; // The number of projectiles that this bow will shoot.

			for (int i = 0; i < NumProjectiles; i++) {
				// Rotate the velocity randomly by 12 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));

				// Decrease velocity randomly for nicer visuals (optional, removed from original code)
				// newVelocity *= 1f - Main.rand.NextFloat(0.1f);

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
			}
			return false; // Return false because we don't want tModLoader to shoot the default projectile

        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 1); // Adjust the sprite position to be more inside the player
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<BromiumBar>(), 15); // Requires 15 Bromium Bars
            recipe.AddIngredient(ItemID.PhoenixBlaster, 1);
            recipe.AddIngredient(ItemID.Boomstick, 1);
            recipe.AddIngredient(ItemID.QuadBarrelShotgun, 1);
            recipe.AddTile(TileID.Anvils); // Crafted at an Anvil
            recipe.Register();
        }
    }
}
