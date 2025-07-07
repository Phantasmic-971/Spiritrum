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
    public class ArmBlasterV3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Laser Blaster");
            // Tooltip.SetDefault("A post-Plantera magic gun, upgraded from the Laser Gun.");
        }

        public override void SetDefaults()
        {
            Item.damage = 45; // Swapped: now uses Blaster's stats
            Item.DamageType = DamageClass.Magic;
            Item.crit = 4;
            Item.mana = 12;
            Item.width = 20;
            Item.height = 10;
            Item.useTime = 16;
            Item.useAnimation = 16;
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
            return new Vector2(-20, 2); // Adjust the sprite position to be more inside the player
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AdamantiteBar, 15); // Requires 1 Laser Rifle
             recipe.AddIngredient(ModContent.ItemType<ArmBlasterV2>(), 1);
            recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
            recipe.Register();
        }
    }
}
