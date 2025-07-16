using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class GoogMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            // ...existing code...
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.White;
            Item.defense = 1;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.04f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<GoogBody>() && legs.type == ModContent.ItemType<GoogPaws>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+1 summon slot, +1 sentry slot\nReplaces hurt sound with Goog!";
            player.maxMinions += 1;
            player.maxTurrets += 1;

            // Custom hurt sound logic
            player.GetModPlayer<GoogArmorPlayer>().hasGoogSet = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "GoogMask", "4% increased summon damage"));
        }
    }
}
