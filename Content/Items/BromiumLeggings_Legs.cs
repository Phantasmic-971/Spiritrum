using Microsoft.Xna.Framework; // Often needed when working with tooltips or colors
using Terraria;
using Terraria.ID;
using Terraria.Localization; // Needed for TooltipLine
using Terraria.ModLoader;
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent; // Needed for ItemType<T>() and EquipLoader

// CORRECTED: Namespace should be Spiritrum.Content.Items
namespace Spiritrum.Content.Items // Make sure this matches your folder structure
{
    [AutoloadEquip(EquipType.Legs)] // This attribute links the item to a visual legs texture
    public class BromiumLeggings : ModItem // Class name is correct
    {
        // Static fields for bonuses, represented as floats for direct addition to player stats
        // Terraria typically uses 0.10f for a 10% increase
        public static readonly float MoveSpeedBonus = 0.20f; // 20% increased movement speed
        public static readonly float RangedDamageBonus = 0.15f; // 15% increased ranged damage

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bromium Leggings"); // Use this if not using localization for names
            // Tooltip is handled in ModifyTooltips
        }

        public override void SetDefaults() {
            Item.width = 18; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth (Adjusted to 1 gold as per your code)
            Item.rare = ItemRarityID.Orange; // The rarity of the item (Adjusted to Green as per your code)
            Item.defense = 9; // The amount of defense the item will give when equipped (Adjusted to 7 as per your code)

            // Armor specific properties
            // If your legs sprite sheet has feet/boots, you might set legSlot
            // Item.legSlot = EquipLoader.GetEquipSlot(Mod, "BromiumLeggings", EquipType.Legs); // This is set implicitly by AutoloadEquip
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

            // You can optionally modify existing lines here if needed
            // For example, finding the defense line and changing its color:
            /*
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "Defense" && line.Mod == "Terraria") // Terraria's default tooltips have Mod set to "Terraria"
                {
                    line.OverrideColor = Color.Cyan; // Make the defense text cyan
                    break; // Exit the loop once found
                }
            }
            */
        }


        // UpdateEquip allows you to give passive effects to the armor while equipped.
        public override void UpdateEquip(Player player) {
            player.moveSpeed += MoveSpeedBonus; // Increase the movement speed of the player by 10%
            player.GetDamage(DamageClass.Ranged) += RangedDamageBonus; // Increase ranged damage by 10%
        }

        // The ArmorSet hooks (IsArmorSet and UpdateArmorSet) are typically only on ONE piece
        // of the set, usually the helmet or chestplate. We put them on the BromiumHelmet.
        // Remove them from here.

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