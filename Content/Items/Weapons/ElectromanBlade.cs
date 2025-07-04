using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Weapons
{
    public class ElectromanBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.damage = 30; // Low base damage
            Item.DamageType = DamageClass.Melee;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 18; // Very fast attack speed (lower numbers mean faster)
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3f; // Low knockback
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow; // Blue rarity (uncommon)
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; // Can hold to attack continuously
            Item.noUseGraphic = false; // Show the item when used
            Item.noMelee = false; // The item's hitbox will be used
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
           tooltips.Add(new TooltipLine(Mod, "Story", "The blade of the dimension police"));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Starfury, 1);
            recipe.AddIngredient(ItemID.MeteoriteBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}