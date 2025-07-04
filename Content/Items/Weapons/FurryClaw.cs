using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Weapons
{
    public class FurryClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip are handled by localization file
        }

        public override void SetDefaults()
        {
            Item.damage = 8; // Low base damage
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 6; // Very fast attack speed (lower numbers mean faster)
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1.5f; // Low knockback
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Blue; // Blue rarity (uncommon)
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; // Can hold to attack continuously
            
            // Extra visual effect for the small claw
            Item.noUseGraphic = false; // Show the item when used
            Item.noMelee = false; // The item's hitbox will be used
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // You could add custom tooltips here if needed
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Create dust to enhance the weapon's visual effects
            if (Main.rand.NextBool(3)) // One-third chance per frame
            {
                int dustType = 16; // Yellow dust
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, dustType);
            }
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IronBar, 5);
            recipe.AddIngredient(ItemID.Leather, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
            // Alternative recipe with lead
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.LeadBar, 5);
            recipe2.AddIngredient(ItemID.Leather, 2);
            recipe2.AddTile(TileID.Anvils);
            recipe2.Register();
        }
    }
}
