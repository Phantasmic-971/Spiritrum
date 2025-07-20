using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class VoidHunterPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 16);
            Item.rare = ItemRarityID.Red;
            Item.defense = 34; // Average defense for body
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidHunterPlateInfo", "8% increased damage reduction and +20 max life"));
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.08f;
            player.statLifeMax2 += 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 28)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
