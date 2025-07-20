using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; // Needed for List<TooltipLine>
using Terraria.Localization; // Needed for TooltipLine

namespace Spiritrum.Content.Items.Accessories
{
    public class CommanderManual : ModItem
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
            Item.rare      = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxTurrets += 2;
            player.GetDamage(DamageClass.Summon) += 0.08f;
        }

        // --- Add the ModifyTooltips method ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Sentryslots", "+2 sentry slots"));
            tooltips.Add(new TooltipLine(Mod, "Damage", "+8% summon damage"));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DefenderMedal, 25);
            recipe.AddIngredient(ItemID.Book, 1);
            recipe.AddTile(TileID.Anvils); // Crafted at an Anvil
            recipe.Register();
        }
    }
}