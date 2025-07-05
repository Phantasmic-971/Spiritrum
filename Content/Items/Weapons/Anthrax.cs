using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Weapons
{
    public class Anthrax : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in localization or ModifyTooltips
        }
        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 36;
            Item.height = 18;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(gold: 50);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item61;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.AnthraxProjectile>();
            Item.shootSpeed = 12f;
            Item.useAmmo = ModContent.ItemType<Ammo.AnthraxGasGrenade>();
            Item.scale = 0.6f;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-45, 0);
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "AnthraxTip1", "Fires Anthrax Gas Grenades"));
            tooltips.Add(new TooltipLine(Mod, "AnthraxTip2", "Explosion inflicts Venom and Broken Armor"));
            tooltips.Add(new TooltipLine(Mod, "AnthraxTip3", "Explosion deals splash damage (50% of base) to all enemies in range"));
        }
        public override void AddRecipes()
        {
            // Add your recipe here if needed
        }
    }
}
