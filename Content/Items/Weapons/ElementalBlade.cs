using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Weapons
{
    public class ElementalBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.damage = 64; // Low base damage
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 12; // Very fast attack speed (lower numbers mean faster)
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f; // Low knockback
            Item.value = Item.buyPrice(gold: 12);
            Item.rare = ItemRarityID.Yellow; // Blue rarity (uncommon)
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; // Can hold to attack continuously
            Item.noUseGraphic = false; // Show the item when used
            Item.noMelee = false; // The item's hitbox will be used
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
           tooltips.Add(new TooltipLine(Mod, "Ability", "Fires fire waves with left click and ice waves with right click"));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FieryGreatsword, 1);
            recipe.AddIngredient(ItemID.Frostbrand, 1);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ItemID.LivingFireBlock, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}