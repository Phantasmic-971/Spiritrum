using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.MagnoliaAddon.Projectiles
{
    // ExampleFlail and ExampleFlailProjectile show the minimum amount of code needed for a flail using the existing vanilla code and behavior. ExampleAdvancedFlail and ExampleAdvancedFlailProjectile need to be consulted if more advanced customization is desired, or if you want to learn more advanced modding techniques.
    // ExampleFlailProjectile is a copy of the Sunfury flail projectile.
    internal class FrozenMaceProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true; // This ensures that the projectile is synced when other players join the world.
            Projectile.width = 22; // The width of your projectile
            Projectile.height = 22; // The height of your projectile
            Projectile.friendly = true; // Deals damage to enemies
            Projectile.penetrate = -1; // Infinite pierce
            Projectile.DamageType = DamageClass.Melee; // Deals melee damage
            Projectile.scale = 0.8f;
            Projectile.usesLocalNPCImmunity = true; // Used for hit cooldown changes in the ai hook
            Projectile.localNPCHitCooldown = 10; // This facilitates custom hit cooldown logic

            // Here we reuse the flail projectile aistyle and set the aitype to the Sunfury. These lines will get our projectile to behave exactly like Sunfury would. This only affects the AI code, you'll need to adapt other code for the other behaviors you wish to use.
            Projectile.aiStyle = ProjAIStyleID.Flail;
            AIType = ProjectileID.Mace;

            // These help center the projectile as it rotates since its hitbox and scale doesn't match the actual texture size
            DrawOffsetX = -6;
            DrawOriginOffsetY = -6;
        }

        // All of the following methods are additional behaviors of Sunfury that are not automatically inherited by ExampleFlailProjectile through the use of Projectile.aiStyle and AIType. You'll need to find corresponding code in the decompiled source code if you wish to clone a different vanilla projectile as a starting point.

        // Draw the projectile in full brightness, ignoring lighting conditions.
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        // Another thing that won't automatically be inherited by using Projectile.aiStyle and AIType are effects that happen when the projectile hits something. Here we see the code responsible for applying the OnFire debuff to players and enemies.
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Main.rand.NextBool(4))
            {
                target.AddBuff(BuffID.Frostburn, 180, quiet: false);
            }
        }
    }
}
