using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Accessories
{
    public class BromiumCube : ModItem
    {


        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 26; // Hitbox width of the item.
            Item.height = 26; // Hitbox height of the item.
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
            Item.value = 500;
            Item.maxStack = 1;
            Item.accessory = true;
            Item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxFallSpeed = player.maxFallSpeed * 5f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "This costs $50");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Speed", "Quintiples your fall speed");
            line = new TooltipLine(Mod, "Face", "Now with 50% more Bromium")
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

            // Another method of hiding can be done if you want to hide just one line.
            // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<Items.Materials.CubicMold>(1);
            recipe.AddIngredient<Content.Items.Placeables.BromiumBar>(12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
