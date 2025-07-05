using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class FrozoniteSkates : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frozonite Skates");
            // Tooltip.SetDefault handled in localization
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 3, silver: 50);
            Item.rare = ItemRarityID.Yellow; // Post-Golem rarity (Yellow)
            Item.defense = 17; // Keeping the defense stat as requested
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "FrozoniteSkatesBonus", "+25% movement speed"));;
            tooltips.Add(new TooltipLine(Mod, "FrozoniteSkatesBonus3", "Ice skating and water walking"));
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.25f; // Increased from 14% to 25% movement speed for post-Golem
            player.waterWalk = true; // Water walking
            player.iceSkate = true; // Ice walking
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Placeables.FrozoniteBar>(), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
