using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System; // Needed for MathHelper.ToRadians
using Spiritrum.Content.Items.Weapons;

namespace Spiritrum.Content.Items.Weapons
{
    public class BromiumRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 35;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet; // Uses vanilla bullet
            Item.shootSpeed = 25f;
            Item.damage = 50;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Ranged; // Use DamageType for compatibility
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item11; // Sound of firing
            Item.useAmmo = AmmoID.Bullet; // Uses standard bullets as ammo
            Item.scale = 0.8f; // Adjust scale to make the sprite more centered
            Item.noMelee = true; // This item does not deal melee damage
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			const int NumProjectiles = 2; // The number of projectiles that this bow will shoot.

			for (int i = 0; i < NumProjectiles; i++) {
				// Rotate the velocity randomly by 12 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(6));

				// Decrease velocity randomly for nicer visuals (optional, removed from original code)
				// newVelocity *= 1f - Main.rand.NextFloat(0.1f);

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
			}
			return false; // Return false because we don't want tModLoader to shoot the default projectile

        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 1); // Adjust the sprite position to be more inside the player
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BromeBlaster>(), 1); // Requires 1 Bromium Blaster
            recipe.AddIngredient(ModContent.ItemType<BrokenBlaster>(), 1);
            recipe.AddTile(TileID.MythrilAnvil); // Crafted at an Anvil
            recipe.Register();
        }
    }
}
