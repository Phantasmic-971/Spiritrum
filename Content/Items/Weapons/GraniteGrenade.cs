using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Weapons
{
    public class GraniteGrenade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }


        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(silver: 2);
            Item.rare = ItemRarityID.Blue;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.consumable = true;
            Item.noUseGraphic = false;
            Item.noMelee = true;
            Item.shoot = ProjectileType<Projectiles.GraniteGrenadeProjectile>();
            Item.shootSpeed = 5f;
            Item.damage = 68;
            Item.knockBack = 6f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
        }
        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "GraniteGrenadeTooltip", "A heavy grenade that creates a lightning explosion\nCannot hurt the thrower"));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.GraniteBlock, 2);
            recipe.AddIngredient(ItemID.Grenade, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
