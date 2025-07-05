using Microsoft.Build.Evaluation;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Ammo
{
    // This example is similar to the Wooden Arrow item
    public class BloodshotArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 36;

            Item.damage = 12; // Keep in mind that the arrow's final damage is combined with the bow weapon damage.
            Item.DamageType = DamageClass.Ranged;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(copper: 8);
            Item.shoot = ModContent.ProjectileType<BloodshotArrowProjectile>(); // The projectile that weapons fire when using this item as ammunition.
            Item.shootSpeed = 3.4f; // The speed of the projectile.
            Item.ammo = AmmoID.Arrow; // The ammo class this ammo belongs to.
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddIngredient(ItemID.CrimtaneBar, 1);
            recipe.Register();
            recipe = CreateRecipe(20);
            recipe.AddIngredient(ItemID.DemoniteBar, 1);
            recipe.Register();
        }

    }
}
