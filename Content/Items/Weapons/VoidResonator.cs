using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;
using Spiritrum.Content.Projectiles;
using Spiritrum.Content.Items.Materials; // Add reference to the Projectiles namespace

namespace Spiritrum.Content.Items.Weapons
{
    public class VoidResonator : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Resonator");
            // Tooltip.SetDefault("'Echoes from the abyss'\nFires a spread of void bolts that pierce through enemies");
            
            Item.staff[Item.type] = true; // This makes the item swing like a staff
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 8);
            
            // Combat properties
            Item.DamageType = DamageClass.Magic;
            Item.damage = 80;
            Item.knockBack = 4f;
            Item.mana = 14;
            Item.crit = 4;
            
            // Use properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20; 
            Item.useTime = 20;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item20;
            
            // Shooting properties
            Item.shoot = ModContent.ProjectileType<VoidResonatorProjectile>();
            Item.shootSpeed = 14f;
            Item.noMelee = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Create a spread of void bolts
            int numProjectiles = 3;
            float spread = 20f;
            
            for (int i = 0; i < numProjectiles; i++)
            {
                // Calculate spread angle
                float rotation = MathHelper.ToRadians(-spread / 2 + spread * i / (numProjectiles - 1));
                
                // Create the projectile with the rotated velocity
                Vector2 newVelocity = velocity.RotatedBy(rotation);
                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            
            return false; // Don't fire the original projectile
            
        }        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add a lore tooltip
            TooltipLine lore = new TooltipLine(Mod, "VoidResonatorLore", "A powerful staff crafted from the essence of a defeated Void Harbinger")
            {
                OverrideColor = new Color(150, 80, 255)
            };
            tooltips.Add(lore);
        }        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            // Alternative crafting recipe if you can't find the Void Harbinger
            recipe.AddIngredient(ModContent.ItemType<VoidEssence>(), 12);
            recipe.AddIngredient(ItemID.SoulofSight, 8);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddIngredient(ItemID.SpellTome, 1);
            recipe.AddTile(TileID.MythrilAnvil);            recipe.Register();
        }
    }
}
