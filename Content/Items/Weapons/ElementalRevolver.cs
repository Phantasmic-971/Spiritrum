using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
    public class ElementalRevolver : ModItem
    {
        // 0 = Fire, 1 = Ice, 2 = Lightning
        private int selectedElement = 0;
        private string[] elementNames = new string[] { "Fire", "Ice", "Lightning" };

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elemental Revolver");
            Item.ResearchUnlockCount = 1;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ElementalRevolverElement", $"Current Element: {elementNames[selectedElement]} (Right click to cycle)"));
            tooltips.Add(new TooltipLine(Mod, "ElementalRevolverEffect", GetElementTooltip(selectedElement)));
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name.StartsWith("ElementalRevolver"))
                {
                    line.OverrideColor = new Color(255, 210, 120);
                }
            }
        }

        private string GetElementTooltip(int element)
        {
            switch (element)
            {
                case 0: return "Fires a Flamelash";
                case 1: return "Fires a Frozo Flake";
                case 2: return "Fires a Lightning Bolt";
                default: return "";
            }
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.scale = 0.5f;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 73;
            Item.knockBack = 3f;
            Item.crit = 8;
            Item.useTime = 11;
            Item.useAnimation = 11;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 14;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item41;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                // Right click: cycle element
                return true;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                selectedElement = (selectedElement + 1) % 3;
                if (Main.myPlayer == player.whoAmI)
                {
                    Main.NewText($"Elemental Revolver: {elementNames[selectedElement]} mode", 255, 210, 120);
                }
                return true;
            }
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                // Don't shoot on right click
                return false;
            }
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 30f;
            if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
                muzzleOffset.Y *= 0.5f;
            else
                muzzleOffset.X *= 0.5f;
            position += muzzleOffset;

            switch (selectedElement)
            {
                case 0: // Fire (Flamelash)
                    int FlamelashDamage = (int)(damage * 0.8f);
                    Vector2 targetPos = Main.MouseWorld;
                    Vector2 direction = targetPos - position;
                    direction.Normalize();
                    direction *= velocity.Length();
                    Projectile.NewProjectile(source, position, direction, ProjectileID.Flamelash, FlamelashDamage, knockback, player.whoAmI);
                    break;
                case 1: // Ice (Frozo Flake)
                    int frozoDamage = (int)(damage * 1.2f);
                    int proj = Projectile.NewProjectile(source, position, velocity, ProjectileType<FrozoFlake>(), frozoDamage, knockback, player.whoAmI);
                    Main.projectile[proj].friendly = true;
                    Main.projectile[proj].hostile = false;
                    break;
                case 2: // Lightning (Heat Ray)
                    int heatRayDamage = (int)(damage * 2.3f);
                    Projectile.NewProjectile(source, position, velocity, ProjectileID.HeatRay, heatRayDamage, knockback, player.whoAmI);
                    break;
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Revolver, 1)
                .AddIngredient(ItemID.HeatRay, 1)
                .AddIngredient(ItemID.Flamelash, 1)
                .AddIngredient(ItemType<Placeables.FrozoniteBar>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, 2);
        }
    }
}
