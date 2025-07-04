using Microsoft.Build.Evaluation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.MagnoliaAddon.Projectiles;
using Spiritrum.MagnoliaAddon.Items.Placeables;

namespace Spiritrum.MagnoliaAddon.Items.Weapons
{
    // This example is similar to the Wooden Arrow item
    public class AzuriteArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 36;

            Item.damage = 6; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
            Item.DamageType = DamageClass.Ranged;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = Item.sellPrice(silver: 2);
            Item.shoot = ModContent.ProjectileType<AzuriteArrowProjectile>(); // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 0.5f; // The speed of the projectile.
            Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
            Item.rare = ItemRarityID.Blue;
            Item.ArmorPenetration = 5;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient<AzuriteBar>(1);
            recipe.Register();
        }

    }
}

