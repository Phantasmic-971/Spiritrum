using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent; // Needed for ItemType
using System.Collections.Generic; // Needed for List<T>
using Spiritrum.Content.Items.Placeable;

namespace Spiritrum.Content.Items.Tools // Recommended namespace for weapons/tools
{
    public class ReborniumHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32; // Item sprite width
            Item.height = 28; // Item sprite height
            Item.useStyle = ItemUseStyleID.Swing; // The item swings
            Item.useTime = 15; // How fast the item is used
            Item.useAnimation = 15; // The animation time
            Item.autoReuse = true; // Can hold down to use
            Item.useTurn = true; // Player can turn while using
            Item.hammer = 50;
            Item.damage = 8; // Moderate melee damage for its tier
            Item.knockBack = 5f; // Moderate knockback
            Item.DamageType = DamageClass.Melee; // Specify that this item deals melee damage
            Item.value = Item.sellPrice(gold: 2); // Example value: 2 gold coins
            Item.rare = ItemRarityID.White; // Match Bromium armor rarity
            Item.UseSound = SoundID.Item1; // Standard swing sound
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<ReborniumBar>(), 8); // Example: Requires 12 Bromium Bars
            recipe.AddTile(TileID.Anvils); // Crafted at an Anvil (or a different crafting station if desired)
            recipe.Register();
        }
    }
}
