using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{
    public class StealthNodachi : ModItem
    {
        public override void SetDefaults()
        {
            // Dimensions
            Item.width = 64;
            Item.height = 64;

            // Usage
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 23;
            Item.useAnimation = 23;
            Item.autoReuse = true;

            // Combat
            Item.DamageType = DamageClass.Melee;
            Item.damage = 23;
            Item.knockBack = 6;
            Item.ArmorPenetration = 4;
            Item.ChangePlayerDirectionOnShoot = true;
            Item.crit = 15;
            Item.scale = 1.3f;

            // Value
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-100f, 20f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add a tooltip about armor penetration
            tooltips.Add(new TooltipLine(Mod, "Projectile", "Creates armor piercing slashes"));
            
            // Add a blank line for spacing
            tooltips.Add(new TooltipLine(Mod, "Spacing", "")
            {
                OverrideColor = new Color(255, 255, 255)
            });
        }
    }
}

