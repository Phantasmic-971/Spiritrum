using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using System; // Needed for MathHelper.ToRadians

namespace Spiritrum.Content.Items.Weapons
{
    public class HoneyPot : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Laser Blaster");
            // Tooltip.SetDefault("A post-Plantera magic gun, upgraded from the Laser Gun.");
        }

        public override void SetDefaults()
        {
            Item.damage = 7; // Base damage of the Laser Blaster
            Item.DamageType = DamageClass.Magic; // Magic weapon
            Item.mana = 7; // Mana cost per use
            Item.width = 20; // Reduced sprite width
            Item.height = 10; // Reduced sprite height
            Item.useTime = 16; // Faster speed of use
            Item.useAnimation = 16; // Faster animation speed
            Item.useStyle = ItemUseStyleID.Shoot; // Gun style
            Item.noMelee = true; // Does not deal melee damage
            Item.knockBack = 1; // Knockback
            Item.value = Item.buyPrice(gold: 2); // Value in coins
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11; // Laser sound
            Item.autoReuse = true; // Automatically reuses
            Item.shoot = ProjectileID.Bee; // Shoots purple lasers
            Item.shootSpeed = 8f; // Increased speed of the lasers
            Item.scale = 0.3f; // Adjust scale to make the sprite more centered
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			const int NumProjectiles = 3; // The number of projectiles that this bow will shoot.

			for (int i = 0; i < NumProjectiles; i++) {
				// Rotate the velocity randomly by 12 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));

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
            recipe.AddIngredient(ItemID.BottledHoney, 10); // Requires 1 Laser Rifle
            recipe.AddIngredient(ItemID.HoneyBlock, 10);
            recipe.AddTile(TileID.Anvils); // Crafted at a Mythril or Orichalcum Anvil
            recipe.Register();
        }
    }
}
