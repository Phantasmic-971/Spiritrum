using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Projectiles.Weapons
{
    public abstract class BaseTwinarangProjectile : ModProjectile
    {
        // AI Variables
        protected float MaxTravelDistance = 800f; // Maximum travel distance
        protected bool Returning; // Whether the boomerang is returning or not
        protected int OwnerProjIndex = -1; // This will track the projectile index for the owner
        protected Vector2 OriginalVelocity; // To store the initial velocity for the return trajectory
        protected Vector2 OriginalPosition; // To store the initial position for distance calculation

        // For drawing the trail
        protected int TrailLength = 8;
        protected float TrailScale = 0.75f;
        protected Color TrailColor = Color.White;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = TrailLength;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = -1; // Custom AI
            Projectile.friendly = true;
            Projectile.penetrate = -1; // Infinite pierce
            Projectile.tileCollide = false; // Pass through tiles
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 300; // 5 seconds
            
            // Independent immunity frames for the boomerang itself
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15; // Base boomerang hit cooldown
            
            // Light emission
            Projectile.light = 0.5f;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            // Set the owner of this projectile
            if (OwnerProjIndex == -1 && Projectile.owner == Main.myPlayer)
            {
                OwnerProjIndex = Projectile.whoAmI;
                OriginalVelocity = Projectile.velocity;
                OriginalPosition = Projectile.Center;
            }

            // Handle the return mechanism
            float distanceTravelled = Vector2.Distance(OriginalPosition, Projectile.Center);
            if (distanceTravelled > MaxTravelDistance || Returning)
            {
                Returning = true;
                
                // Change velocity towards owner
                Vector2 directionToOwner = owner.Center - Projectile.Center;
                float distanceToOwner = directionToOwner.Length();

                if (distanceToOwner > 20f)
                {
                    directionToOwner.Normalize();
                    float speed = Math.Min(18f, distanceToOwner / 10f);
                    Projectile.velocity = directionToOwner * speed;
                }
                else
                {
                    // Return to the owner
                    Projectile.Kill();
                }
            }

            // Rotation
            Projectile.rotation += 0.3f;

            // Custom AI for subclasses
            CustomAI();

            // Check if owner is still alive
            if (owner.dead)
            {
                Projectile.Kill();
            }
        }

        // This method will be overridden by subclasses
        protected virtual void CustomAI()
        {
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Drawing the trail
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);

            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin;
                Color color = Projectile.GetAlpha(TrailColor) * ((float)(Projectile.oldPos.Length - k) / Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, TrailScale * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length), SpriteEffects.None, 0);
            }

            return true;
        }
    }
}
