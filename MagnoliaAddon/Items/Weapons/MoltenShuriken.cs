using Spiritrum.MagnoliaAddon.Projectiles;
using Microsoft.Build.Evaluation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.MagnoliaAddon.Items.Weapons
{
    public class MoltenShuriken : ModItem
    {
        public override void SetDefaults()
        {
            // Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools

            // Common Properties
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 5);
            Item.maxStack = 9999;

            // Use Properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.consumable = true;

            // Weapon Properties			
            Item.damage = 16;
            Item.knockBack = 5f;
            Item.noUseGraphic = true; // The item should not be visible when used
            Item.noMelee = true; // The projectile will do the damage and not the item
            Item.DamageType = DamageClass.Ranged;

            // Projectile Properties
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<MoltenShurikenProjectile>(); // The projectile that will be thrown
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(250);
            recipe.AddIngredient<SteelShuriken>(250);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
    }
}

