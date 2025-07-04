using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Ideology
{
    public class Environmentalism : ModItem
    {
        public override void SetStaticDefaults() { }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(gold: 6);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.10f;
            player.jumpSpeedBoost += 2f;
            player.manaCost -= 0.08f; // 8% less mana usage
            player.GetModPlayer<IdeologySlotPlayer>().natureRegen = true;

            // If the player is standing on grass, increase life regen by 2
            int tileX = (int)(player.Center.X / 16f);
            int tileY = (int)((player.position.Y + player.height + 8f) / 16f);
            if (WorldGen.InWorld(tileX, tileY))
            {
                ushort tileType = Main.tile[tileX, tileY].TileType;
                if (tileType == TileID.Grass || tileType == TileID.JungleGrass || tileType == TileID.MushroomGrass)
                {
                    player.lifeRegen += 2;
                }
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "EnvironmentalismTip1", "+10% movement speed, +2 jump speed"));
            tooltips.Add(new TooltipLine(Mod, "EnvironmentalismTip2", "Standing on grass or near water grants rapid health regen"));
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            return player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.NaturesGift);
            recipe.AddIngredient(ItemID.JungleRose);
            recipe.AddIngredient(ItemID.BottledWater, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
