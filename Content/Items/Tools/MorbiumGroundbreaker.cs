using Spiritrum.Content.Items.Placeables;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Tools
{
    public class MorbiumGroundbreaker : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 76;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; // Automatically re-swing/re-use this item after its swinging animation is over.

            Item.hammer = 100; // How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(10))
            { // This creates a 1/10 chance that a dust will spawn every frame that this item is in its 'Swinging' animation.
              // Creates a dust at the hitbox rectangle, following the rules of our 'if' conditional.
            }
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<MorbiumBar>(18);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();





        }
    }
}
