using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Items.Consumables;

namespace Spiritrum.Content.Items.Weapons
{
    public class MapleSyrupBurner : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Maple Syrup Burner");
            // Tooltip.SetDefault("A strong pre-boss flamethrower.");
        }

        public override void SetDefaults()
        {
            Item.damage = 4; // Base damage of the flamethrower
            Item.DamageType = DamageClass.Ranged; // Ranged weapon
            Item.width = 40; // Sprite width
            Item.height = 20; // Sprite height
            Item.useTime = 30; // Speed of use
            Item.useAnimation = 30; // Animation speed
            Item.useStyle = ItemUseStyleID.Shoot; // Flamethrower style
            Item.noMelee = true; // Does not deal melee damage
            Item.knockBack = 0; // Knockback
            Item.value = Item.buyPrice(silver: 50); // Value in coins
            Item.rare = ItemRarityID.Blue; // Rarity
            Item.UseSound = SoundID.Item34; // Flamethrower sound
            Item.autoReuse = true; // Automatically reuses
            Item.shoot = ProjectileID.Flames; // Shoots flames
            Item.shootSpeed = 4f; // Speed of the flames
            Item.useAmmo = ModContent.ItemType<MapleSyrup>(); // Uses maple syrup as ammo
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 20); // Requires 20 wood
            recipe.AddIngredient(ItemID.Gel, 10); // Requires 10 gel
            recipe.AddTile(TileID.WorkBenches); // Crafted at a workbench
            recipe.Register();
        }
    }
}
