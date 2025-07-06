
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Spiritrum.Content.Items.Placeables;
using Spiritrum.Content.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{
   
    public class MorbeamStaff : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
        }
        public override void SetDefaults()
        {
            Item.width = 33;
            Item.height = 33;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.autoReuse = true;
        
            Item.mana = 12;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 84;
            Item.knockBack = 3f;
            Item.noMelee = true;
            Item.ArmorPenetration = 10;
            Item.value = 175000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item72;

            Item.shoot = ModContent.ProjectileType<Morbeam>(); // ID of the projectiles the sword will shoot
            Item.shootSpeed = 12.5f; // Speed of the projectiles the sword will shoot

            // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
            // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

            // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
            // Item.ChangePlayerDirectionOnShoot = false;
        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<Morbeam>();
           

           

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {

            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 newVelocity = velocity.RotatedBy(MathHelper.ToRadians(1.5f));



            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);

            // Rotate the velocity randomly by 30 degrees at max.
            Vector2 new2Velocity = velocity.RotatedBy(MathHelper.ToRadians(-1.5f));



            // Create a projectile.
            Projectile.NewProjectileDirect(source, position, new2Velocity, type, damage, knockback, player.whoAmI);


            return false; // Return false because we don't want tModLoader to shoot projectile
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Shoots two bouncing morbium beams");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Ignores 10 enemy defense")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



            // Here we will hide all tooltips whose title end with ':RemoveMe'
            // One like that is added at the start of this method
            foreach (var l in tooltips)
            {
                if (l.Name.EndsWith(":RemoveMe"))
                {
                    l.Hide();
                }
            }

            // Another method of hiding can be done if you want to hide just one line.
            // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(3f, -7f);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.ShadowbeamStaff);
            recipe.AddIngredient<MorbiumBar>(12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}
