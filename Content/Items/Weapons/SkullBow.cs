using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Weapons
{
    public class SkullBow : ModItem
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
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 5f; // Average knockback
            Item.crit = 4; // 4% critical chance
            
            // Usage properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item5; // Bow shooting sound
            // Projectile properties
            Item.shoot = ProjectileID.BoneGloveProj;
            Item.shootSpeed = 11f; // Velocity of 11
            Item.consumable = false; // The weapon itself is not consumed
            Item.useAmmo = AmmoID.Arrow; // Uses arrows as ammo
            Item.noMelee = true; // Doesn't deal melee damage when swung
        }
      
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Create a bone projectile
            Projectile.NewProjectile(source, position, velocity, ProjectileID.BoneGloveProj, damage, knockback, player.whoAmI);
            
            // Return false to prevent the default projectile from being shot
            return false;
        }

        public override void AddRecipes()
        {
            // Create a recipe for the SkullBow
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 30); // 50 bones
            recipe.AddTile(TileID.Anvils); // Crafted at an anvil
            recipe.Register();
        }

        // Optional: Add visual effects when using the bow
        public override Vector2? HoldoutOffset()
        {
            // This creates an offset when holding the weapon
            // (X, Y) offset from the player's hand
            return new Vector2(-2, 0);
        }
    }
}
