using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class VoidHunterLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 14);
            Item.rare = ItemRarityID.Red;
            Item.defense = 22; // Average defense for legs
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidHunterLeggingsInfo", "14% increased movement speed and 5% increased damage"));
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.14f;
            player.GetDamage(DamageClass.Generic) += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 22)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
