using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace Spiritrum.Content.Projectiles
{
    public class MorbiumArrowProj : ModProjectile
    {
        

        public override void SetDefaults()
        {
            Projectile.width = 1; // The width of projectile hitbox
            Projectile.height = 1; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = 1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 11; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            
            Projectile.light = 0f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 0; // Set to above 0 if you want the projectile to update multiple time in a frame

            AIType = ProjectileID.WoodenArrowFriendly; // Act exactly like default Bullet
        }


        public override void AI()
        {

            Projectile.velocity = Projectile.velocity* 0.225f;


        }




        public override void OnKill(int timeLeft)
        {
           


                Vector2 velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(4.5f));
                Vector2 spawn = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawn, velocity,
                    ModContent.ProjectileType<MorbeamRanged>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner);
                Vector2 velocity2 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(1.5f));
                Vector2 spawn2 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawn2, velocity2,
                ModContent.ProjectileType<MorbeamRanged>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner);

                Vector2 velocity3 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(-1.5f));
                Vector2 spawn3 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawn3, velocity3,
                ModContent.ProjectileType<MorbeamRanged>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner);
                Vector2 velocity4 = Projectile.velocity.RotatedBy(MathHelper.ToRadians(-4.5f));
                Vector2 spawn4 = Projectile.Center - new Vector2(Main.rand.NextFloat(0, 0));
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawn4, velocity4,
                ModContent.ProjectileType<MorbeamRanged>(), (int)(Projectile.damage * 0.75f), Projectile.knockBack, Projectile.owner);
               

            
          
        }

    }

}



