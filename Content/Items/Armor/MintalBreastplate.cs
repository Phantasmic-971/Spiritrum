using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Body)]
    public class MintalBreastplate : ModItem
    {
        public static readonly int RangedDamageBonus = 11;
        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            // Set display name and tooltip directly in code
            // DisplayName.SetDefault("Mintal Breastplate");
            // Tooltip.SetDefault("+11% Ranged Damage");

            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs();
        }

        public override void SetDefaults()
        {
            Item.width = 32; // Width of the item
            Item.height = 28; // Height of the item
            Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
            Item.rare = ItemRarityID.LightRed; // The rarity of the item
            Item.defense = 11; // The amount of defense the item will give when equipped
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.11f; // 11% ranged damage bonus
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add any custom tooltip lines here if needed in the future
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<Items.Placeables.Mintal>(20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}

