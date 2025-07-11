using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
    public class AbyssalSniper : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Sniper");
            // Tooltip.SetDefault("Shoots streams of piercing shadows");
        }

        public override void SetDefaults()
        {
            Item.damage = 230;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 18;
            Item.useTime = 20;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = false;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(gold: 6);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.AbyssalBallProjectile>();
            Item.shootSpeed = 55f;
            Item.crit = 25;

        }

        public override Vector2? HoldoutOffset() => new Vector2(-6, 0);

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Spiritrum.Content.Items.Materials.VoidEssence>(), 12);
            recipe.AddIngredient(ItemID.SniperRifle, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
