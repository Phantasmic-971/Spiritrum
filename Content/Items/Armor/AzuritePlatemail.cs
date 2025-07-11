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
    public class AzuritePlatemail : ModItem
    {

        public static LocalizedText SetBonusText { get; private set; }

        public override void SetStaticDefaults()
        {
            // Set display name and tooltip directly in code
            // DisplayName.SetDefault("Azurite Platemail");
            // Tooltip.SetDefault("Magical armor infused with azurite crystals");

            // SetBonusText is not needed unless using localization. Set bonus will be hardcoded in UpdateArmorSet.
        }

        public override void SetDefaults()
        {
            Item.width = 32; // Width of the item
            Item.height = 28; // Height of the item
            Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
            Item.rare = ItemRarityID.Orange; // The rarity of the item
            Item.defense = 6; // The amount of defense the item will give when equipped
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add any custom tooltip lines here if needed in the future
        }
        // IsArmorSet determines what armor pieces are needed for the setbonus to take effect
        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AzuriteBar>(20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}


