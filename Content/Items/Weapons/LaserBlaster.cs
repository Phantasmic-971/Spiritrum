using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Weapons
{
    public class LaserBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Laser Blaster");
            // Tooltip.SetDefault("A post-Plantera magic gun, upgraded from the Laser Gun.");
        }

        public override void SetDefaults()
        {
            Item.damage = 50; // Base damage of the Laser Blaster
            Item.DamageType = DamageClass.Magic; // Magic weapon
            Item.mana = 5; // Mana cost per use
            Item.width = 20; // Reduced sprite width
            Item.height = 10; // Reduced sprite height
            Item.useTime = 10; // Faster speed of use
            Item.useAnimation = 10; // Faster animation speed
            Item.useStyle = ItemUseStyleID.Shoot; // Gun style
            Item.noMelee = true; // Does not deal melee damage
            Item.knockBack = 3; // Knockback
            Item.value = Item.buyPrice(gold: 10); // Value in coins
            Item.rare = ItemRarityID.Lime; // Post-Plantera rarity
            Item.UseSound = SoundID.Item33; // Laser sound
            Item.autoReuse = true; // Automatically reuses
            Item.shoot = ProjectileID.PurpleLaser; // Shoots purple lasers
            Item.shootSpeed = 12f; // Increased speed of the lasers
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 1); // Adjust the sprite position to be more inside the player
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LaserRifle, 1); // Requires 1 Laser Rifle
            recipe.AddIngredient(ModContent.ItemType<Spectralite>(), 10); // Requires 10 Spectralite instead of Spectre Bars
            recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
            recipe.Register();
        }
    }
}
