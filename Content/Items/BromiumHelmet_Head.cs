using Microsoft.Xna.Framework; // Often needed when working with tooltips or colors
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; // Needed for List<TooltipLine>
// using Terraria.Localization; // No longer needed for SetBonusText if we define it directly
using static Terraria.ModLoader.ModContent; // This line is crucial for ItemType<T>()

namespace Spiritrum.Content.Items // Make sure this matches your folder structure
{
    [AutoloadEquip(EquipType.Head)]
    public class BromiumHelmet : ModItem
    {
        // Constants for the helmet's individual bonuses
        public const int MeleeSpeedBonus = 15; // 10%
        public const int MeleeDamageBonus = 10;  // 5%

        // Constant for the set bonus
        public const int SetBonusCritChance = 15; // 10%

        // REMOVED: LocalizedText for Set Bonus - we'll define it directly in UpdateArmorSet
        // public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Basic tooltip info if needed"); // Optional: Basic description

            // Armor rendering settings
            // Keep head hidden since it's a full helmet
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;

            // REMOVED: SetBonusText initialization using localization
            // SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(SetBonusCritChance);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 50); // Value: 50 silver
            Item.rare = ItemRarityID.Orange; // Rarity: Orange
            Item.defense = 9; // Defense: 6
        }

        // Apply individual item bonuses when equipped
        public override void UpdateEquip(Player player)
        {
            // Increase melee speed by 15%
            player.GetAttackSpeed(DamageClass.Melee) += MeleeSpeedBonus / 100f;
            // Increase melee damage by 10%
            player.GetDamage(DamageClass.Melee) += MeleeDamageBonus / 100f;
        }

        // Add tooltip lines for individual bonuses
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Melee Speed Tooltip
            TooltipLine meleeSpeedLine = new TooltipLine(Mod, "BromiumHelmetMeleeSpeed", $"Increases melee speed by {MeleeSpeedBonus}%") // Added a unique name for the line
            {
                IsModifier = true // Optional: Adds color based on bonus/penalty
            };
            tooltips.Add(meleeSpeedLine);

            // Melee Damage Tooltip
            TooltipLine meleeDamageLine = new TooltipLine(Mod, "BromiumHelmetMeleeDamage", $"Increases melee damage by {MeleeDamageBonus}%") // Added a unique name for the line
            {
                IsModifier = true // Optional: Adds color based on bonus/penalty
            };
            tooltips.Add(meleeDamageLine);
        }


        // Check if the full armor set is equipped
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            // Ensure correct class names (case-sensitive)
            // This line is causing the error, likely due to the file naming mismatch
            return head.type == Type && // Check if the equipped head is this item
                   body.type == ItemType<BromiumChestplate>() &&
                   legs.type == ItemType<BromiumLeggings>();
        }

        // Apply the set bonus effects
        public override void UpdateArmorSet(Player player)
        {
            // Set the tooltip displayed below the armor items
            player.setBonus = $"Set Bonus: Increases critical strike chance by {SetBonusCritChance}%";

            // Apply the actual bonus: +10% critical strike chance for all damage types
            // Note: Crit chance is typically added as a whole number percentage
            player.GetCritChance(DamageClass.Generic) += SetBonusCritChance;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            // Assuming BromiumBar exists
            recipe.AddIngredient(ItemType<BromiumBar>(), 10);
            recipe.AddIngredient(ItemID.BeeHeadgear, 1);
            recipe.AddIngredient(ItemID.NecroHelmet, 1); // Assuming you want to use the Necro Helmet as a base
            recipe.AddIngredient(ItemID.MoltenHelmet, 1);
            recipe.AddIngredient(ItemID.JungleHat, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}