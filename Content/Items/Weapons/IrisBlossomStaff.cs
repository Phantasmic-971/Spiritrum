using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Weapons
{
    public class IrisBlossomStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip should be set in localization files
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Magic;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<IrisBlossomStaffProjectile>();
            Item.shootSpeed = 10f;
            Item.mana = 7;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Daybloom, 5)
                .AddIngredient(ItemID.Wood, 10)
                .AddIngredient(ItemID.FallenStar, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
