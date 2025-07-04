using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Spiritrum.Content.Items.Ammo.Bolts
{
    public class KrakenBolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Kraken Bolt");
            // Tooltip.SetDefault("Penetrates armor");
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 32;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 0, 0, 10);
            Item.rare = ItemRarityID.Orange;
            Item.ammo = ModContent.ItemType<Bolt>(); // The ammo type. If this uses a custom ModAmmo class, use AmmoID.Bullet + ModContent.AmmoType<MyAmmo>() instead.
            Item.shoot = ProjectileID.ExplosiveBullet; // The projectile the ammo fires.
            Item.shootSpeed = 3.5f; // The velocity of the projectile.
            Item.damage = 25;
            Item.knockBack = 2.5f;
            Item.DamageType = DamageClass.Ranged;
            Item.ArmorPenetration = 20; // Base armor penetration of 20
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.EmptyBullet, 25);
            recipe.AddIngredient(ItemID.IronBar, 3); // Iron bar as material
            recipe.AddIngredient(ItemID.ExplosivePowder, 3); 
            recipe.AddIngredient(ItemID.SharkFin, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}