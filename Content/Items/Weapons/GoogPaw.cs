using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Weapons
{
    public class GoogPaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Enhanced stats from Furry Claw
            Item.damage = 14; // Increased from 8
            Item.DamageType = DamageClass.Melee;
            Item.width = 5;
            Item.height = 5;
            Item.useTime = 8; // Faster use time for smoother animation
            Item.useAnimation = 8; // Should be the same as useTime for proper behavior
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.0f; // Increased knockback from 1.5f
            Item.value = Item.buyPrice(gold: 1); // More valuable
            Item.rare = ItemRarityID.Green; // Green rarity (slightly better than blue)
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 0.5f; 
            
            // Force direct hit detection
            Item.DamageType = DamageClass.Melee;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "GoogPawDesc", "Goog... is life"));
            
            // Color the effect tooltip line
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "GoogPawEffect")
                {
                    line.OverrideColor = new Color(150, 220, 150); // Light green color for effect
                }
            }
        }
        
        public override Vector2? HoldoutOffset()
        {
            // Adjust visual positioning when held
            return new Vector2(-10f, -2f); // Better positioning for Arkhalis-style weapon
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemType<FurryClaw>(), 1) // Requires Furry Claw
                .AddIngredient(ItemType<Materials.Googling>(), 10) // 10 Googling
                .AddTile(TileID.Anvils) // Crafted at an anvil
                .Register();
        }
    }
}
