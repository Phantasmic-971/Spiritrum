using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization; // Needed for localization
using static Terraria.ModLoader.ModContent; // For ModContent

namespace Spiritrum.Content.Items
{
    public class Autism : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("While not moving, increases critical chance by 15%.\n" +
                       "Grants immunity to Confused and detects nearby rare creatures."); */

            if (ModLoader.TryGetMod("TerMerica", out Mod terMerica))
            {
            Item.material = true; // Makes the item a material when TerMerica is enabled
            }
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink; // Hardmode rarity
            Item.value = Item.buyPrice(gold: 10);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Check if the player is not moving
            if (player.velocity.X == 0 && player.velocity.Y == 0)
            player.GetCritChance(DamageClass.Generic) += 15; // Increase critical chance by 15%
            player.AddBuff(BuffID.Hunter, 2); // Grants the effect of the Lifeform Analyzer
            player.buffImmune[BuffID.Confused] = true; // Immunity to Confused debuff
            player.detectCreature = true; // Detects nearby rare creatures
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
	    {
			// Add your custom tooltip lines here
			tooltips.Add(new TooltipLine(Mod, "AutismTipCrit", "Increases critical chance by 15% while not moving"));
			tooltips.Add(new TooltipLine(Mod, "AutismTipImmunity", "Grants immunity to Confusion and detects nearby rare creatures"));

			// You can add more lines or modify existing ones here
	    }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LifeformAnalyzer, 1); // Requires Lifeform Analyzer
            recipe.AddIngredient(ItemID.TrifoldMap, 1); // Requires Trifold Map
            recipe.AddIngredient(ItemID.SoulofLight, 10); // Requires 10 Souls of Light
            recipe.AddTile(TileID.TinkerersWorkbench); // Crafted at the Tinkerer's Workshop
            recipe.Register();
        }
    }
}