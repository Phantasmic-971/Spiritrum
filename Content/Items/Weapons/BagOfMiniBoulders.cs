using Spiritrum.Content.Projectiles;
using Microsoft.Build.Evaluation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{
    public class BagOfMiniBoulders : ModItem
    {
        public override void SetDefaults()
        {
            // Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools

            // Common Properties
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(copper: 5);
            Item.maxStack = 9999;

            // Use Properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = false;

            // Weapon Properties			
            Item.damage = 33;
            Item.knockBack = 5f;
            Item.noUseGraphic = true; // The item should not be visible when used
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.DamageType = DamageClass.Ranged;

            // Projectile Properties
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<MiniBoulderThrowableProjectile>(); // The projectile that will be thrown
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<Items.Weapons.MiniThrowableBoulder>(1000);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}

