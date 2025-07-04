using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Spiritrum.Content.Items.Ammo.Bolts
{
    public class Bolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip are handled by .hjson localization files
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.consumable = true; // This marks the item as consumable when used
            Item.value = Item.buyPrice(copper: 10);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ProjectileID.ExplosiveBullet; // The projectile the ammo fires
            Item.shootSpeed = 5f; // The speed of the projectile
            Item.ammo = ModContent.ItemType<Bolt>(); // Custom ammo type for Bolter
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 25; // Base damage
        }

        // Modify the ammo state based on conditions, such as player effects
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Make the shot slightly more accurate
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));
        }

        // Add crafting recipes
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25); // Creates 25 Bolts per craft
            recipe.AddIngredient(ItemID.EmptyBullet, 25);
            recipe.AddIngredient(ItemID.IronBar, 1); // Iron bar as material
            recipe.AddIngredient(ItemID.ExplosivePowder, 1); // Explosive component
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

            // Alternative recipe with Lead
            Recipe recipe2 = CreateRecipe(25);
            recipe2.AddIngredient(ItemID.EmptyBullet, 25);
            recipe2.AddIngredient(ItemID.LeadBar, 1); // Lead bar as material
            recipe2.AddIngredient(ItemID.ExplosivePowder, 1); // Explosive component
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}
