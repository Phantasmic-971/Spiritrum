using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Spiritrum.Content.Items.Placeable;

namespace Spiritrum.Content.Items.Weapons
{
    public class RebornBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip are handled by the localization file
        }

        public override void SetDefaults()
        {
            // Basic item properties
            Item.width = 16;
            Item.height = 40;
            Item.rare = ItemRarityID.Orange; // Rarity level 3 (Orange)
            Item.value = Item.sellPrice(gold: 2); // Sells for 2 gold
            
            // Combat properties
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 3f; // Average knockback
            // Usage properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item5; // Bow shooting sound
            // Projectile properties
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 9f; // Velocity of 11
            Item.consumable = false; // The weapon itself is not consumed
            Item.useAmmo = AmmoID.Arrow; // Uses arrows as ammo
            Item.noMelee = true; // Doesn't deal melee damage when swung
        }

        public override void AddRecipes()
        {
            // Create a recipe for the SkullBow
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ReborniumBar>(), 8); // Requires 15 Bromium Bars
            recipe.AddTile(TileID.Anvils); // Crafted at an anvil
            recipe.Register();
        }

        // Optional: Add visual effects when using the bow
        public override Vector2? HoldoutOffset()
        {
            // This creates an offset when holding the weapon
            // (X, Y) offset from the player's hand
            return new Vector2(2, 0);
        }
    }
}
