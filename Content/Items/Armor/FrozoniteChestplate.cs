using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class FrozoniteChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frozonite Chestplate");
            // Tooltip.SetDefault handled in localization
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Yellow; // Post-Golem rarity (Yellow)
            Item.defense = 22; // Keeping the defense stat as requested
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "FrozoniteChestplateBonus", "+8% damage"));
            tooltips.Add(new TooltipLine(Mod, "FrozoniteChestplateBonus2", "+20 max mana"));
            tooltips.Add(new TooltipLine(Mod, "FrozoniteChestplateBonus3", "8% reduced mana cost"));
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.statManaMax2 += 20; // +20 Max mana
            player.manaCost -= 0.08f; // 8% reduced mana cost
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Placeables.FrozoniteBar>(), 18);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
