using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{
    public class Hockarang : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 25;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(silver: 80);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
              Item.shoot = ModContent.ProjectileType<Content.Projectiles.Hockarang_Projectile>();
            Item.shootSpeed = 16f;
        }        public override bool CanUseItem(Player player)
        {
            // Allow up to 3 Hockarang projectiles to be active
            return player.ownedProjectileCounts[Item.shoot] < 3;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IceBlock, 50)
                .AddIngredient(ItemID.Shiverthorn, 4)
                .AddIngredient(ItemID.IceBoomerang, 1)
                .AddTile(TileID.IceMachine)
                .Register();
        }
    }
}
