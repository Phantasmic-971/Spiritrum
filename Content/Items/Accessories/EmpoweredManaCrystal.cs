using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Spiritrum.Content.Items.Other;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Accessories
{
    public class EmpoweredManaCrystal : ModItem
    {
        public static readonly int MaxManaIncrease = 40;
        public override void SetDefaults()
        {
            Item.width = 32; // Hitbox width of the item.
            Item.height = 32; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.value = 10;
            Item.maxStack = 1;
            Item.accessory = true;
        }
        public override void UpdateEquip(Player player)
        {



            player.statManaMax2 += MaxManaIncrease;

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "A mana crystal infused with extra fallen stars");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Increases Mana by 40")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddIngredient(ItemID.FallenStar, 4);
            recipe.AddIngredient<LunarGem>(1);
            recipe.Register();
        }

    }
}
