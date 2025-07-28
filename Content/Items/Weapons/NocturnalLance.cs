using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Spiritrum.Content.Items.Materials;
using Spiritrum.Content.Projectiles;
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Weapons
{
    public class NocturnalLance : ModItem
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
            Item.damage = 185;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 10f; // Average knockback
            Item.crit = 6; // 4% critical chance
            
            // Usage properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item5; // Bow shooting sound
            Item.shootSpeed = 55f; // Velocity of 55
            Item.shoot = ModContent.ProjectileType<Projectiles.NocturnalLance>();
            Item.useAmmo = 0; // No ammo
            Item.consumable = false; // The weapon itself is not consumed
            Item.noMelee = true; // Doesn't deal melee damage when swung
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            velocity *= 3f;
            var proj = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Projectiles.NocturnalLance>(), damage, knockback, player.whoAmI);
            if (proj != null) proj.scale = 2f;
            return false; // Return false because we don't want tModLoader to shoot the default projectile
        }

        public override void AddRecipes()
        {
            // Create a recipe for the SkullBow
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Placeables.Penumbrium>(), 30); // 30 Penumbrium
            recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril Anvil
            recipe.Register();
        }

        // Optional: Add visual effects when using the bow
        public override Vector2? HoldoutOffset()
        {
            // This creates an offset when holding the weapon
            // (X, Y) offset from the player's hand
            return new Vector2(-2, 0);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "SkullBow", "Shoots high power arrows")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
    }
}
