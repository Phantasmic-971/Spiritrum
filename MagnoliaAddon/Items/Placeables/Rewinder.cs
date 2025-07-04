using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Spiritrum.MagnoliaAddon.Items.Placeables
{
    public class Rewinder : ModItem
    {
        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 20; // Hitbox width of the item.
            Item.height = 20; // Hitbox height of the item.
            Item.rare = ItemRarityID.White; // The color that the item's name will be in-game.
            Item.value = 7500;
            Item.maxStack = 9999;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            // Commenting out missing tile reference - you'll need to create this tile later
            // Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Furniture.RewinderPlaced>());
            // Temporarily use a vanilla tile instead
            Item.createTile = TileID.WorkBenches;

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Converts some items and blocks to their old textures");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



            // Here we will hide all tooltips whose title end with ':RemoveMe'
            // One like that is added at the start of this method
            foreach (var l in tooltips)
            {
                if (l.Name.EndsWith(":RemoveMe"))
                {
                    l.Hide();
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient<Items.Other.LunarGem>(6);
            recipe.AddIngredient(ItemID.GoldBar, 2);
            recipe.AddIngredient(ItemID.Glass, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            recipe = CreateRecipe(1);
            recipe.AddIngredient<Items.Other.LunarGem>(6);
            recipe.AddIngredient(ItemID.PlatinumBar, 2);
            recipe.AddIngredient(ItemID.Glass, 12);
            recipe.AddTile(TileID.Anvils);
        }

    }
}
