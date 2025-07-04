using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Spiritrum.Content.Items.Consumables;

namespace Spiritrum.Content.Items.Mounts
{
    public class SkiDooKey : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item79; // Mount sound
            Item.noMelee = true;
            Item.mountType = ModContent.MountType<Content.Mounts.SkiDooMount>();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "KeyTip1", "Allows the player to drive a snow bike"));
            tooltips.Add(new TooltipLine(Mod, "KeyTip2", "Ski-Doo is the original name"));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1); // Makes 1 at a time
            recipe.AddIngredient(ItemID.IronBar, 25);
            recipe.AddIngredient(ModContent.ItemType<Poutine>(), 4);
            recipe.AddIngredient(ModContent.ItemType<MapleSyrup>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}