using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Spiritrum.Content.Projectiles
{
public class EquinoxSpear : ModProjectile
{
    // Tracks the number of bonus stacks (max 3 for 15%)
    private int bonusStacks = 0;
    // Offset from the spear's center to the tip (end) in local coordinates (adjust as needed)
    private static readonly Vector2 TipOffset = new Vector2(48f, 0f); // 48 pixels along the spear's axis
        // Offset from the spear's center to the tip (end) in local coordinates (adjust as needed)
        public override void SetDefaults()
        {
            Projectile.width = (int)(31f * 1.5f);
            Projectile.height = (int)(18f * 1.5f);
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.aiStyle = 19; // Use vanilla spear AI
            AIType = ProjectileID.Spear;
            bonusStacks = 0;
        }

        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 96f;

        // Increase damage by 5% per hit, up to 15%, reset on kill
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (bonusStacks < 3)
                bonusStacks++;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FinalDamage *= 1f + 0.05f * bonusStacks;
        }

        [System.Obsolete]
        public override void OnKill(int timeLeft)
        {
            bonusStacks = 0;
        }
    }
}