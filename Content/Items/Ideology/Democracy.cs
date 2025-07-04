using Spiritrum.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic; // Needed for List<TooltipLine>

namespace Spiritrum.Content.Items.Ideology
{
    public class Democracy : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Democracy"); // Keep this if you want a default name

            // --- Remove the Tooltip.SetDefault line ---
            // Tooltip.SetDefault(...) // DELETE THIS LINE
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(gold: 10);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 2;
            player.moveSpeed += 0.05f;
            player.endurance += 0.05f;
            player.lifeRegen += 2;
            player.GetCritChance(DamageClass.Generic) += 3;
        }

        // --- Add the ModifyTooltips method ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we create TooltipLine objects for each line we want to add.
            // The first argument is the Mod instance, the second is a unique identifier name for the line,
            // and the third is the text to display.

            TooltipLine line1 = new TooltipLine(Mod, "DemocracyTip1", "Increases defense by 2");
            tooltips.Add(line1); // Add the line to the list

            TooltipLine line2 = new TooltipLine(Mod, "DemocracyTip2", "Increases movement speed by 5%");
            tooltips.Add(line2);

            TooltipLine line3 = new TooltipLine(Mod, "DemocracyTip3", "Reduces damage taken by 5%");
            tooltips.Add(line3);

            TooltipLine line4 = new TooltipLine(Mod, "DemocracyTip4", "Increases life regeneration by 2");
            tooltips.Add(line4);

            TooltipLine line5 = new TooltipLine(Mod, "DemocracyTip5", "Increases critical strike chance by 3%");
            tooltips.Add(line5);

            // You can add more lines or logic here if needed.
            // For example, you could change the color of a line:
            // line1.OverrideColor = Colors.RarityGreen;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 20);
            recipe.AddIngredient(ItemID.Silk, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item)
                return true;
            return false;
        }
    }
}