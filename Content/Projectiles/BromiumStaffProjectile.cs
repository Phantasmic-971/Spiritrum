using Microsoft.Xna.Framework; // Needed for Vector2
using Terraria;
using Terraria.ID; // Needed for ProjectileID and ProjectileAIStyleID, DustID, BuffID
using Terraria.ModLoader;
using static Terraria.Lighting; // Use 'using static' for the Lighting class

namespace Spiritrum.Content.Projectiles // Make sure this matches your folder structure
{
	public class BromiumStaffProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// Optional: Display name can go in localization .hjson files
			// DisplayName.SetDefault("Bromium Bolt"); // Uncomment to set display name directly

			// Sets the projectile's glowmask. Requires you to create a texture file named YourProjectileName_Glow.png
			// ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // For trails
			// ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // For trails
		}

		public override void SetDefaults()
		{
			// Common projectile properties
			Projectile.width = 10; // Adjust sprite width
			Projectile.height = 10; // Adjust sprite height
			// Set aiStyle to -1 for custom AI in the AI() method
			Projectile.aiStyle = -1; // Use -1 for custom AI
			Projectile.friendly = true; // Can hit enemies
			Projectile.hostile = false; // Cannot hit players
			Projectile.DamageType = DamageClass.Magic; // Magic damage type
			Projectile.penetrate = 3; // How many enemies it can hit (-1 for infinite)
			Projectile.timeLeft = 500; // How long the projectile lasts in frames (changed to 500)
			Projectile.ignoreWater = true; // Ignores water
			Projectile.tileCollide = true; // Collides with tiles
			Projectile.extraUpdates = 1; // Runs AI an extra time per frame for smoother movement

			// Projectile specific properties
			// Projectile.light = 0.5f; // Emit light (can also be done in AI)
			// Projectile.alpha = 0; // Transparency (0 is fully visible)
		}

		// Custom AI logic goes here when aiStyle is -1
		public override void AI()
		{
			// Add light emission using the static method from the Lighting class
			AddLight(Projectile.position, 0.2f, 0.2f, 0.6f);

			// --- Custom AI logic ---

			// REMOVED: Gravity effect
			// Projectile.velocity.Y += 0.1f; // Example of adding gravity effect

			// Add dust particles (IDs 158, 57, 55)
			// Spawn a few dust particles each frame for a trail effect
			if (Main.rand.NextBool(2)) // Adjust frequency of dust spawning (e.g., 1 in 2 frames)
			{
				// Use the specified dust IDs: 158, 57 (Enchanted_Gold), and 55 (Pixie)
				int dustType = Main.rand.Next(new int[] { 158, DustID.Enchanted_Gold, DustID.Pixie }); // Randomly pick from dust IDs 158, 57, 55
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
				dust.noGravity = true; // Make the dust float
				dust.velocity *= 0.5f; // Reduce dust velocity
			}

			// You can add other custom movement or behavior here
		}

		// What happens when the projectile hits an NPC
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Apply Ichor debuff for 3 seconds (3 * 60 = 180 frames)
			target.AddBuff(BuffID.Ichor, 180);

			// Apply On Fire! debuff for 6 seconds (6 * 60 = 360 frames)
			target.AddBuff(BuffID.OnFire, 360);

			// You can add other effects here, like spawning particles or playing sounds
		}

		// Optional: What happens when the projectile dies (e.g., hits a tile or time runs out)
		// public override void Kill(int timeLeft)
		// {
		//     // Example: Create dust particles on death
		//     // for (int i = 0; i < 10; i++)
		//     // {
		//     //     Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror);
		//     // }
		//     // Example: Play a sound effect
		//     // SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		// }
	}
}
