using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{
    /// <summary>
    /// Steel Longsword - A mid-tier melee weapon with armor penetration
    /// </summary>
    public class SteelLongsword : ModItem
    {
        #region Properties and Fields

        // No fields needed for this class

        #endregion

        #region Initialization

        /// <summary>
        /// Sets the default properties of this item
        /// </summary>
        public override void SetDefaults()
        {
            // Dimensions
            Item.width = 36;
            Item.height = 36;

            // Usage
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.autoReuse = true;

            // Combat
            Item.DamageType = DamageClass.Melee;
            Item.damage = 19;
            Item.knockBack = 6;
            Item.ArmorPenetration = 6;
            Item.ChangePlayerDirectionOnShoot = true;

            // Value
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
        }

        #endregion

        #region Visuals

        /// <summary>
        /// Determines the color of the item in low light conditions
        /// </summary>
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        /// <summary>
        /// Modifies the tooltips shown for this item
        /// </summary>
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add a tooltip about armor penetration
            tooltips.Add(new TooltipLine(Mod, "ArmorPenetration", "It does 6 armor penetration"));
            
            // Add a blank line for spacing
            tooltips.Add(new TooltipLine(Mod, "Spacing", "")
            {
                OverrideColor = new Color(255, 255, 255)
            });
        }

        #endregion

        #region Crafting

        /// <summary>
        /// Adds crafting recipes for this item
        /// </summary>
        public override void AddRecipes()
        {
            // Craft from 12 steel bars
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<Items.Placeables.SteelBar>(12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
            // Upgrade Gold Broadsword
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBroadsword, 1);
            recipe.AddIngredient<Items.Placeables.SteelBar>(4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            
            // Upgrade Platinum Broadsword
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PlatinumBroadsword, 1);
            recipe.AddIngredient<Items.Placeables.SteelBar>(4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        #endregion
    }
}

