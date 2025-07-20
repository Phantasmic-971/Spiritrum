using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; // Needed for List<TooltipLine>
using Terraria.Localization; // Needed for TooltipLine

namespace Spiritrum.Content.Items.Accessories
{
    public class SkywarePanel : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width     = 28;
            Item.height    = 28;
            Item.accessory = true; 		// mark as accessory
            Item.value     = Item.sellPrice(gold: 1);
            Item.rare      = ItemRarityID.Green;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic)  += 0.04f; // +4% damage
            player.statManaMax2 += 20; 			// +20 max mana
            player.manaCost     -= 0.04f; 		// –4% mana usage
        }

        // --- Add the ModifyTooltips method ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Cost", "-4% mana cost"));
            tooltips.Add(new TooltipLine(Mod, "Damage", "+4% magic damage"));
            tooltips.Add(new TooltipLine(Mod, "Mana", "+20 max mana"));

        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SunplateBlock, 50);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddTile(TileID.Anvils); // Crafted at an Anvil
            recipe.Register();
        }
    }
}