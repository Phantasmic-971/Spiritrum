using Spiritrum.Content.Items.Placeables;
using Spiritrum.Content.Projectiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{
   
    public class MorbiumSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.autoReuse = true;
            Item.scale = 1.2f;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 124;
            Item.knockBack = 9.25f;


            Item.value = Item.buyPrice(gold: 15);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;

            Item.shoot = ModContent.ProjectileType<MorbiumBolt>(); 
            Item.shootSpeed = 11.25f; // Speed of the projectiles the sword will shoot

            // If you want melee speed to only affect the swing speed of the weapon and not the shoot speed (not recommended)
            // Item.attackSpeedOnlyAffectsWeaponAnimation = true;

            // Normally shooting a projectile makes the player face the projectile, but if you don't want that (like the beam sword) use this line of code
            // Item.ChangePlayerDirectionOnShoot = false;
        }
       


       
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Shoots a bolt of Morbium energy");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "")
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
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
         
            recipe.AddIngredient<MorbiumBar>(15);
          
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}
