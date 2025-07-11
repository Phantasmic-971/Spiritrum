using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Consumables
{
    public class PeanutButter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 2);
            Item.healLife = 100;
            Item.potion = true; // This makes it trigger potion sickness
        }

        public override bool? UseItem(Player player)
        {
            // Add Ironskin and Endurance buffs for 30 seconds (1800 ticks)
            player.AddBuff(BuffID.Ironskin, 1800);
            player.AddBuff(BuffID.Endurance, 1800);
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var line = new TooltipLine(Mod, "PeanutButter", "Provides Ironskin and Endurance for 30 seconds")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);
        }
    }
}
