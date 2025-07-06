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
    public class ArmBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Laser Blaster");
            // Tooltip.SetDefault("A post-Plantera magic gun, upgraded from the Laser Gun.");
        }

        public override void SetDefaults()
        {
            Item.damage = 35; // Base damage of the Laser Blaster
            Item.DamageType = DamageClass.Magic; // Magic weapon
            Item.mana = 10; // Mana cost per use
            Item.width = 20; // Reduced sprite width
            Item.height = 10; // Reduced sprite height
            Item.useTime = 13; // Faster speed of use
            Item.useAnimation = 13; // Faster animation speed
            Item.useStyle = ItemUseStyleID.Shoot; // Gun style
            Item.noMelee = true; // Does not deal melee damage
            Item.knockBack = 1; // Knockback
            Item.value = Item.buyPrice(gold: 2); // Value in coins
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item33; // Laser sound
            Item.autoReuse = true; // Automatically reuses
            Item.shoot = ProjectileID.GreenLaser; // Shoots purple lasers
            Item.shootSpeed = 8f; // Increased speed of the lasers
            Item.scale = 0.6f; // Adjust scale to make the sprite more centered

        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 2); // Adjust the sprite position to be more inside the player
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CobaltBar, 15); // Requires 1 Laser Rifle
            recipe.AddTile(TileID.Anvils); // Crafted at a Mythril or Orichalcum Anvil
            recipe.Register();
        }
    }
}
