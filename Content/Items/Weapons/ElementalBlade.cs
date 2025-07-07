using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
    public class ElementalBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Indicates this is a summoning weapon
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            
            // Link the buff and projectile for this minion weapon
            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1;
        }
        
        public override void SetDefaults()
        {
            Item.damage = 23; // Summoning damage
            Item.DamageType = DamageClass.Summon;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f;
            Item.value = Item.buyPrice(gold: 15);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item82; // Summoning sound
            Item.autoReuse = true;
            Item.noUseGraphic = false;
            Item.noMelee = true; // No melee damage from the item itself
            Item.shoot = ModContent.ProjectileType<ElementalBladeProjectile>();
            Item.shootSpeed = 16f;
            Item.buffType = ModContent.BuffType<Spiritrum.Content.Buffs.ElementalBlades>(); // Set the buff type!
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
           tooltips.Add(new TooltipLine(Mod, "Ability", "Summons elemental blades to fight for you"));
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.LivingFireBlock, 25);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
        
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Apply the buff to the player
            player.AddBuff(Item.buffType, 2);
            
            // Spawn the minion at the mouse position - let the game handle minion slot limits
            Vector2 spawnPos = Main.MouseWorld;
            var projectile = Projectile.NewProjectileDirect(source, spawnPos, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;
            
            return false; // Prevent vanilla projectile
        }
    }
}