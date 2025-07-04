using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Weapons
{
    public class HuotRifle : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 15f;
            Item.damage = 13;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Ranged;
            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item11;
            Item.useAmmo = AmmoID.Bullet;
            Item.noMelee = true;
            Item.scale = 0.40f; // Adjust scale for visual effect
            Item.crit = 4; // Set critical hit chance
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int NumProjectiles = 1;
            for (int i = 0; i < NumProjectiles; i++)
            {
                // Rotate the velocity randomly by 8 degrees at max.
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false; // Prevent default projectile
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-40, 1); // Adjust as needed
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RossRifle>(), 1);
            recipe.AddIngredient(ItemID.Minishark, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
