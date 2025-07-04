using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader.IO;
using System.IO;

namespace Spiritrum.Content.Items.Weapons
{
    public class Equinox : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip should be set in localization files
            ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our item as a spear
        }

        public override void SetDefaults()
        {
            // Spear properties only
            Item.damage = 80; // High base damage for hardmode spear
            Item.DamageType = DamageClass.Melee;
            Item.width = 54;
            Item.height = 54;
            Item.useTime = 30; // The length of the item's use time in ticks (like ExampleSpear)
            Item.useAnimation = 30; // The length of the item's use animation in ticks (like ExampleSpear)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (like ExampleSpear)
            Item.knockBack = 5f; // Strong knockback
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink; // Hardmode tier
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true; // Projectile does the damage
            Item.noUseGraphic = true; // Don't show the item when used
            Item.shoot = ModContent.ProjectileType<Content.Projectiles.EquinoxSpear>();
            Item.shootSpeed = 13f; // Vanilla spear reach
            Item.channel = false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true; // Enable right-click functionality
        }

        public override bool CanUseItem(Player player)
        {
            // Left click: normal spear, right click: dash
            if (player.altFunctionUse == 2) // Right click
            {
                // Only allow one dash at a time
                return player.ownedProjectileCounts[ModContent.ProjectileType<Content.Projectiles.EquinoxDash>()] < 1;
            }
            else // Left click
            {
                return player.ownedProjectileCounts[Item.shoot] < 1;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2) // Right click
            {
                // Dash: fire EquinoxDash projectile horizontally in the direction the player is facing
                Vector2 dashVel = new Vector2(player.direction * 16f, 0f); // Reduced from 24f to 16f
                Projectile.NewProjectile(source, player.Center, dashVel, ModContent.ProjectileType<Content.Projectiles.EquinoxDash>(), damage, knockback, player.whoAmI);
                return false; // Don't fire normal spear
            }
            // Let vanilla spear logic handle projectile spawning and velocity
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CobaltBar, 12)
                .AddIngredient(ItemID.MythrilBar, 8)
                .AddIngredient(ItemID.AdamantiteBar, 6)
                .AddIngredient(ItemID.HallowedBar, 4)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
