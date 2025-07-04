using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Spiritrum.Content.Projectiles;
using System.Collections.Generic; // Needed for List<TooltipLine>
using Terraria.Localization; // Needed for TooltipLine
using Terraria.DataStructures;

namespace Spiritrum.Content.Items.Weapons // Using the namespace you provided
{
    // This class defines the Whispering Blade melee weapon
    public class WhisperingBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.damage = 100; // Base damage (adjust for Hardmode, Post-Plantera)
            Item.DamageType = DamageClass.Melee; // Set damage type to melee
            Item.width = 40; // Item texture width (adjust)
            Item.height = 40; // Item texture height (adjust)
            Item.useTime = 15; // Use time (how fast it swings)
            Item.useAnimation = 15; // Use animation time
            Item.useStyle = ItemUseStyleID.Swing; // Use a swinging animation
            Item.knockBack = 6; // Knockback value
            Item.crit = 4; // Base critical strike chance
            Item.rare = ItemRarityID.Lime; // Set rarity (Lime is typically Post-Plantera)
            Item.value = Item.sellPrice(gold: 20); // Set sell price (adjust)
            Item.UseSound = SoundID.Item1; // Sound when used (swinging sound)
            Item.autoReuse = true; // Can hold down to swing continuously
            Item.scale = 1.4f; // Adjust scale to make the sprite more centered

            // Projectile properties
            // Ensure WhisperingBladeProjectile is in its own file and has the correct namespace and using directives
            Item.shoot = ModContent.ProjectileType<WhisperingBladeProjectile>(); // Set the projectile type to shoot
            Item.shootSpeed = 10f; // Speed of the projectile (adjust)
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-50, 10); // Adjust the sprite position to be more inside the player
        }
        // This method is called when the weapon hits an NPC
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // 40% chance to inflict Confused debuff (adjust chance)
            if (Main.rand.NextFloat() < 0.40f) // 0.40f = 40% chance
            {
                target.AddBuff(BuffID.Confused, 180); // Apply Confused debuff for 3 seconds (180 ticks)
            }
        }

        // This method is called when the weapon is used, allowing custom projectile spawning
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
        	const int NumProjectiles = 1; // The number of projectiles that this bow will shoot.
        {
            // Occasionally release a shadowy projectile (e.g., 10% chance)
            if (Main.rand.NextFloat() < 0.10f) // 0.10f = 10% chance
            {
                // Spawn the projectile
                // Make sure the projectile type is correct (it should be the 'type' parameter passed in, which is ModContent.ProjectileType<WhisperingBladeProjectile>())
                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            }

            }
			for (int i = 0; i < NumProjectiles; i++) {
				// Rotate the velocity randomly by 12 degrees at max.
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(7));

				// Decrease velocity randomly for nicer visuals (optional, removed from original code)
				// newVelocity *= 1f - Main.rand.NextFloat(0.1f);

				// Create a projectile.
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
			}
			return false; // Return false because we don't want tModLoader to shoot the default projectile
            // Return true to allow the default projectile (if Item.shoot is set) to also fire,
            // or return false if you only want your custom projectile to fire.
            // Since the projectile is occasional, we return false here to only fire it on the chance.
            return false;
        }

        // --- Add the ModifyTooltips method ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add your custom tooltip lines
            tooltips.Add(new TooltipLine(Mod, "WhisperingBladeTooltip1", "Each strike has 40% chance to inflict confusion."));
            tooltips.Add(new TooltipLine(Mod, "WhisperingBladeTooltip2", "Releases a piercing shadowy projectile that inflicts Shadowflame."));
        }
           public override void AddRecipes()
  {
      Recipe recipe = CreateRecipe();

      if (ModLoader.TryGetMod("gunrightsmod", out Mod TerMerica) && TerMerica.TryFind<ModItem>("DeliriantDagger", out ModItem DeliriantDagger))
      {
          recipe = CreateRecipe();

          recipe.AddIngredient(DeliriantDagger.Type);

          recipe.AddTile(TileID.MythrilAnvil);
          recipe.Register();

      }
      else
      {


      }

  }
}
}

