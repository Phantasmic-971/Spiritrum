using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization; // Needed for tooltips
using System.Collections.Generic; // Needed for List<TooltipLine>
using Spiritrum.Content.Items.Weapons;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Weapons
{
	public class LMG : ModItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip is now handled directly via ModifyTooltips
		}

		public override void SetDefaults()
		{
			Item.damage = 65; // Base damage of the LMG
			Item.crit = 5;
			Item.DamageType = DamageClass.Ranged; // Ranged weapon
			Item.width = 30; // Reduced sprite width
			Item.height = 15; // Reduced sprite height
			Item.useTime = 5; // Very fast use time
			Item.useAnimation = 5; // Matches use time
			Item.useStyle = ItemUseStyleID.Shoot; // Gun style
			Item.noMelee = true; // Does not deal melee damage
			Item.knockBack = 2; // Low knockback
			Item.value = Item.buyPrice(gold: 20); // Value in coins
			Item.rare = ItemRarityID.Cyan; // Post-Golem rarity
			Item.UseSound = SoundID.Item11; // Gunfire sound
			Item.autoReuse = true; // Automatically reuses
			Item.shoot = ProjectileID.Bullet; // Shoots bullets
			Item.shootSpeed = 15f; // Bullet speed
			Item.useAmmo = AmmoID.Bullet; // Uses bullets as ammo
			Item.scale = 0.5f; // Adjust scale to make the sprite more centered
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-50, 0); // Adjust the sprite position to be more inside the player
		}

		// --- Add the ModifyTooltips method ---
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			// Add your custom tooltip lines based on the provided text
			tooltips.Add(new TooltipLine(Mod, "LMGTipAmmo", "75% chance to not consume ammo"));
			tooltips.Add(new TooltipLine(Mod, "LMGTipCost", "It costs 210 copper coins to fire this weapon for 12 seconds")); // Note: This is a calculation based on ammo
			tooltips.Add(new TooltipLine(Mod, "LMGTipOrigin", "*Lethal Mega Shark*")); // Flavor text

			// You can add more lines or modify existing ones here
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Megashark, 1); // Requires 1 Megashark
			recipe.AddIngredient(ItemID.FragmentVortex, 10); // Requires 10 Vortex Fragments
			recipe.AddIngredient(ModContent.ItemType<BrokenBlaster>(), 1);
			recipe.AddTile(TileID.MythrilAnvil); // Crafted at a Mythril or Orichalcum Anvil
			recipe.Register();
			if (ModLoader.TryGetMod("gunrightsmod", out Mod TerMerica) && TerMerica.TryFind("CyberneticGunParts", out ModItem CyberneticGunParts))
    recipe.AddIngredient(CyberneticGunParts.Type);
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= 0.75f; // This means 25% chance to consume, or 75% chance NOT to consume
		}
	}
}