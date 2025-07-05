using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Items; // For GoogBlast and BromiumBar
using Spiritrum.Content.Projectiles; // For GoogBlast_Projectile
using Spiritrum.Content.Items.Placeables;

namespace Spiritrum.Content.Items.Weapons // Updated namespace
{
    public class GoogBlaster : ModItem
    {
        // Localization for DisplayName and Tooltip should be added to your .hjson files.
        // Example: 
        // "Mods.Spiritrum.Items.GoogBlaster.DisplayName": "Goog Blaster",
        // "Mods.Spiritrum.Items.GoogBlaster.Tooltip": "An upgraded version of the Goog Blast\nSlightly more damage and faster!"

        public override void SetDefaults()
        {
            // Item Stats
            Item.damage = 47; // Slightly more than GoogBlast (assuming GoogBlast is around 20-22)
            Item.DamageType = DamageClass.Ranged;
            Item.width = 40; // Placeholder width, adjust with sprite
            Item.height = 20; // Placeholder height, adjust with sprite
            Item.useTime = 20; // Faster than GoogBlast (assuming GoogBlast is around 22-25)
            Item.useAnimation = 20; // Faster than GoogBlast
            Item.useStyle = ItemUseStyleID.Shoot; // Standard shooting style
            Item.noMelee = true; // Doesn't deal contact damage
            Item.knockBack = 7f;
            Item.value = Item.sellPrice(gold: 2); // Adjusted value based on ingredients
            Item.rare = ItemRarityID.Orange; // Or ItemRarityID.LightRed, depending on progression
            Item.UseSound = SoundID.Item11; // Standard gun sound, adjust if needed
            Item.autoReuse = true; // Can hold down mouse to fire
            Item.scale = 0.6f; // Adjusted scale for visual consistency
            // Weapon Functionality
            Item.shoot = ModContent.ProjectileType<GoogBlast_Projectile>(); 
            Item.shootSpeed = 12f; // Slightly faster projectile speed
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                // GoogBlast is likely in Spiritrum.Content.Items, so ModContent.ItemType<GoogBlast>() should work with the using statement.
                .AddIngredient(ModContent.ItemType<GoogBlast>(), 1) 
                // BromiumBar is likely in Spiritrum.Content.Items, so ModContent.ItemType<BromiumBar>() should work.
                .AddIngredient(ModContent.ItemType<BromiumBar>(), 10)
                .AddTile(TileID.Anvils) // Crafted at an Anvil
                .Register();
        }
    }
}
