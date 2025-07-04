using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Spiritrum.Content.Projectiles.Weapons;

namespace Spiritrum.Content.Items.Weapons
{
    public class Twinarang : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true; // Enables right-click usage
        }

        public override void SetDefaults()
        {
            // General weapon properties
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.Pink; // Same as The Twins
            Item.value = Item.sellPrice(gold: 5);

            // Combat properties
            Item.damage = 55;
            Item.knockBack = 3f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true; // Doesn't hit enemies directly
            Item.noUseGraphic = true; // Doesn't show the item when used
            
            // Projectile properties
            Item.shoot = ModContent.ProjectileType<TwinarangProjectile>();
            Item.shootSpeed = 16f;
            
            // Sound effects
            Item.UseSound = SoundID.Item1;
            
            // Enable autoswing
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true; // Enable right-click functionality
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                // Right-click: Spazmatism mode - laser boomerang
                type = ModContent.ProjectileType<TwinarangLaserProjectile>();
                Item.UseSound = SoundID.Item33; // Laser sound
                
                // Create the projectile with increased attack speed
                var proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
                // Faster attack speed for right-click
                proj.timeLeft = (int)(proj.timeLeft * 0.7f);
            }
            else
            {
                // Left-click: Retinazer mode - flame boomerang
                type = ModContent.ProjectileType<TwinarangFlameProjectile>();
                Item.UseSound = SoundID.Item34; // Fire sound
                
                // Create the projectile
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }
            
            return false; // Don't shoot the default projectile
        }

        // Add the item to The Twins' loot pool
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // Twins drop logic - would typically be in an NPCLoot method
            // This is a placeholder as the actual implementation would be in a GlobalNPC
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            // Add custom tooltips
            tooltips.Add(new TooltipLine(Mod, "TwinarangUsage1", "Left-click: Throws a boomerang that emits cursed flames in four directions"));
            tooltips.Add(new TooltipLine(Mod, "TwinarangUsage2", "Right-click: Throws a boomerang that fires rapid lasers in two directions"));
            tooltips.Add(new TooltipLine(Mod, "TwinarangPierce", "Infinite pierce"));
        }
    }
}
