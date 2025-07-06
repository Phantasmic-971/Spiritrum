using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using static Terraria.ModLoader.ModContent;
using System; // Needed for MathHelper.ToRadians

namespace Spiritrum.Content.Items.Weapons
{
    public class BlasterV3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Laser Blaster");
            // Tooltip.SetDefault("A post-Plantera magic gun, upgraded from the Laser Gun.");
        }

        public override void SetDefaults()
        {
            Item.damage = 60; // Base damage of the Laser Blaster
            Item.DamageType = DamageClass.Magic; // Magic weapon
            Item.crit = 6;
            Item.mana = 10; // Mana cost per use
            Item.width = 20; // Reduced sprite width
            Item.height = 10; // Reduced sprite height
            Item.useTime = 11; // Faster speed of use
            Item.useAnimation = 11; // Faster animation speed
            Item.useStyle = ItemUseStyleID.Shoot; // Gun style
            Item.noMelee = true; // Does not deal melee damage
            Item.knockBack = 1; // Knockback
            Item.value = Item.buyPrice(gold: 2); // Value in coins
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item33; // Laser sound
            Item.autoReuse = true; // Automatically reuses
            Item.shoot = ProjectileID.GreenLaser; // Shoots purple lasers
            Item.shootSpeed = 15f; // Increased speed of the lasers
            Item.scale = 0.6f; // Adjust scale to make the sprite more centered

        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-45, 0); // Adjust the sprite position to be more inside the player
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitaniumBar, 15); // Requires 1 Laser Rifle
         
            recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
            recipe.Register();
        }
    }
}
