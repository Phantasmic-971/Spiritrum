using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Accessories
{
    public class VolcanicSyringe : ModItem
    {        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 3);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.OnFire3] = true;
            player.GetModPlayer<Spiritrum.Players.VolcanicSyringePlayer>().volcanicSyringeEquipped = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddIngredient(ItemID.LavaBucket, 1)
                .AddIngredient(ItemID.ObsidianRose, 1)
                .AddTile(TileID.Hellforge)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "VolcanicSyringe", "Grants immunity to fire debuffs"));
            tooltips.Add(new TooltipLine(Mod, "VolcanicSyringeEffect", "Releases flaming embers when damaged"));
        }
    }
}
