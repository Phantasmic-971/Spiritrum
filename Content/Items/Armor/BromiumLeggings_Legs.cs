using Microsoft.Xna.Framework; // Often needed when working with tooltips or colors
using Terraria;
using Terraria.ID;
using Terraria.Localization; // Needed for TooltipLine
using Terraria.ModLoader;
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent; // Needed for ItemType<T>() and EquipLoader
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Armor // Make sure this matches your folder structure
{
    [AutoloadEquip(EquipType.Legs)] // This attribute links the item to a visual legs texture
    public class BromiumLeggings : ModItem // Class name is correct
    {

        public static readonly float MoveSpeedBonus = 0.20f; // 20% increased movement speed
        public static readonly float RangedDamageBonus = 0.15f; // 15% increased ranged damage

        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults() {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth (Adjusted to 1 gold as per your code)
            Item.rare = ItemRarityID.Orange; // The rarity of the item (Adjusted to Green as per your code)
            Item.defense = 9; // The amount of defense the item will give when equipped (Adjusted to 7 as per your code)

        }

        // --- Add the ModifyTooltips method ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add tooltip lines using TooltipLine objects
            // The first argument is the Mod instance, the second is a unique name for the line, the third is the text

            // Movement Speed Tooltip
            TooltipLine moveSpeedLine = new TooltipLine(Mod, "BromiumLeggingsMoveSpeed", $"+{(int)(MoveSpeedBonus * 100)}% movement speed")
            {
                IsModifier = true // This makes the text green for positive bonuses
            };
            tooltips.Add(moveSpeedLine);

            // Ranged Damage Tooltip
            TooltipLine rangedDamageLine = new TooltipLine(Mod, "BromiumLeggingsRangedDamage", $"+{(int)(RangedDamageBonus * 100)}% ranged damage")
            {
                IsModifier = true // This makes the text green for positive bonuses
            };
            tooltips.Add(rangedDamageLine);
        }


        // UpdateEquip allows you to give passive effects to the armor while equipped.
        public override void UpdateEquip(Player player) {
            player.moveSpeed += MoveSpeedBonus; // Increase the movement speed of the player by 10%
            player.GetDamage(DamageClass.Ranged) += RangedDamageBonus; // Increase ranged damage by 10%
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
            // Requires 15 Bromium Bars (as per your code)
            recipe.AddIngredient(ModContent.ItemType<BromiumBar>(), 15);
            recipe.AddIngredient(ItemID.BeeGreaves, 1);
            recipe.AddIngredient(ItemID.NecroGreaves, 1);
            recipe.AddIngredient(ItemID.MoltenGreaves, 1);
            recipe.AddIngredient(ItemID.JunglePants, 1);
            recipe.AddTile(TileID.Anvils); // Crafted at an Anvil
            recipe.Register();
        }
    }
}