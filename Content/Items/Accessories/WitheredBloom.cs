using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Accessories
{
    public class WitheredBloom : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in ModifyTooltips for localization safety
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 24);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= 0.10f;
            player.manaFlower = true;
            player.GetDamage(DamageClass.Magic) += 0.20f;
            player.starCloakItem = Item; // For star pickup range
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "WitheredBloomEffect1", "Reduces mana usage by 10%"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "WitheredBloomEffect2", "Automatically uses Mana Potions when needed"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "WitheredBloomEffect3", "Increases pickup range for Stars by 18.75 tiles"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "WitheredBloomEffect4", "+20% magic damage"));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MagnetFlower)
                .AddIngredient(ItemID.AvengerEmblem)
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.ManaFlower)
                .AddIngredient(ItemID.CelestialEmblem)
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
