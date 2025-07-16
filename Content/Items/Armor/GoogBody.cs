using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class GoogBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            // ...existing code...
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 40);
            Item.rare = ItemRarityID.White;
            Item.defense = 2;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.04f;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "GoogBody", "4% increased summon damage"));
        }
    }
}
