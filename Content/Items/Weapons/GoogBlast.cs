using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Items.Materials;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
    public class GoogBlast : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Unleashes a blast of concentrated search results");
        }

        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40;
            Item.height = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<GoogBlast_Projectile>();
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.None; // Or a custom ammo type if you prefer
            Item.UseSound = SoundID.Item11; // Sound for shooting
            Item.scale = 0f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Googling>(), 15)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
