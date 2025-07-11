using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Consumables
{
    public class MapleSyrup : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(copper: 50);
            Item.healLife = 75;
            Item.potion = true; // This makes it trigger potion sickness
            Item.ammo = Item.type; // Makes this item count as its own ammo type
        }

        public override bool? UseItem(Player player)
        {
            // Add Sugar Rush buff for 30 seconds (1800 ticks)
            player.AddBuff(192, 1800);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Acorn, 5)
                .AddTile(TileID.Campfire)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "MapleSyrup", "Gives Sugar Rush for 30 seconds")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
    }
}
