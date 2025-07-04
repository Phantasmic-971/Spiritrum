using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
    public class EchoingLancer : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in localization or ModifyTooltips
        }
        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 34;
            Item.height = 60;
            Item.useTime = 18;
            Item.useAnimation = 53;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(gold: 60);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EchoingLancerArrow>();
            Item.shootSpeed = 18f;
            Item.useAmmo = AmmoID.Arrow;
            Item.reuseDelay = 15; // Add a small delay between bursts to ensure proper burst fire
        }
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Fire 3 custom arrows, do not spawn default ammo projectiles
            for (int i = 0; i < 3; i++)
            {
                int proj = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<EchoingLancerArrow>(), damage, knockback, player.whoAmI, 0);
                if (proj >= 0)
                {
                    Main.projectile[proj].timeLeft -= i * 2;
                }
            }
            return false; // Prevent vanilla arrow from being fired
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            // 60% chance to not consume ammo
            return Main.rand.NextFloat() >= 0.6f;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-4, 0);
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "EchoingLancerTip1", "Fires a burst of 3 spectral arrows per shot"));
            tooltips.Add(new TooltipLine(Mod, "EchoingLancerTip2", "60% chance to not consume arrows"));
        }
    }
}
