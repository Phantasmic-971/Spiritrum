using Microsoft.Build.Evaluation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Ammo
{
    // This example is similar to the Wooden Arrow item
    public class FeatherArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 36;

            Item.damage = 9; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
            Item.DamageType = DamageClass.Ranged;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = Item.sellPrice(copper: 30);
            Item.shoot = ModContent.ProjectileType<FeatherArrowProjectile>(); // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 0.12f; // The speed of the projectile.
            Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddIngredient(ItemID.WoodenArrow, 20);
            recipe.AddIngredient(ItemID.Feather, 1);
            recipe.Register();
        }

    }
}
