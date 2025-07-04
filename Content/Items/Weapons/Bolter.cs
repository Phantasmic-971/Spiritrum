using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Spiritrum.Content.Items.Ammo.Bolts;

namespace Spiritrum.Content.Items.Weapons
{
    public class Bolter : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in localization or ModifyTooltips
        }
        public override void SetDefaults()
        {
            Item.damage = 105;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 44;
            Item.height = 18;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(gold: 12);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.ExplosiveBullet;
            Item.shootSpeed = 18f;
            Item.useAmmo = ModContent.ItemType<Bolt>(); // Only use Bolt ammo
            Item.scale = 0.78f;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-8, 0);
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Use Bolt projectile with increased damage
            Projectile.NewProjectile(source, position, velocity * 1.2f, type, (int)(damage * 1.2f), knockback * 1.2f, player.whoAmI);
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "BolterTip1", "Consumes Bolts as ammunition"));
            tooltips.Add(new TooltipLine(Mod, "BolterTip2", "Very effective against armored targets and crowds"));
        }
        public override void AddRecipes()
        {
            // Mythril variant
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentVortex, 12);
            recipe.AddIngredient(ItemID.MythrilBar, 10);
            recipe.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            // Orichalcum variant
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.FragmentVortex, 12);
            recipe2.AddIngredient(ItemID.OrichalcumBar, 10);
            recipe2.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe2.AddIngredient(ItemID.SoulofMight, 5);
            recipe2.AddTile(TileID.LunarCraftingStation);
            recipe2.Register();
        }
    }
}
