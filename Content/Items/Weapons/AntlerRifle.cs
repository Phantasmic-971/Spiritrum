using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Weapons
{
    public class AntlerRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 11f;
            Item.damage = 31;
            Item.crit = 7;
            Item.knockBack = 6f;
            Item.DamageType = DamageClass.Ranged;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item11;
            Item.useAmmo = AmmoID.Bullet; 
            Item.noMelee = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-24, 2); // Adjust as needed for best fit
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Musket, 1);
            recipe.AddIngredient(ItemID.AntlionMandible, 6); // Using Antlion Mandible as a stand-in for Antler
            recipe.AddIngredient(ItemID.Sandstone, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
