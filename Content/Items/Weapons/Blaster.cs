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
    public class Blaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Laser Blaster");
            // Tooltip.SetDefault("A post-Plantera magic gun, upgraded from the Laser Gun.");
        }

        public override void SetDefaults()
        {
            Item.damage = 50; // Swapped: now uses ArmBlasterV3's stats
            Item.DamageType = DamageClass.Magic;
            Item.crit = 4;
            Item.mana = 12;
            Item.width = 20;
            Item.height = 10;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item33;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.GreenLaser;
            Item.shootSpeed = 12f;
            Item.scale = 0.6f;

        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-45, 0); // Adjust the sprite position to be more inside the player
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PalladiumBar, 15); // Requires 1 Laser Rifle
            
            recipe.AddTile(TileID.Anvils); // Crafted at a Mythril or Orichalcum Anvil
            recipe.Register();
        }
    }
}
