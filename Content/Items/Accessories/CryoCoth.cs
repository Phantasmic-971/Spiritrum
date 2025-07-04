using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Accessories
{
    public class CryoCoth : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffID.Warmth, 2); // Constantly reapplies Warmth buff
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frostburn2] = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "CryoCothTipWarmth", "Grants the Warmth buff"));
            tooltips.Add(new TooltipLine(Mod, "CryoCothTipImmune", "Immunity to Chilled, Frozen, Frostburn, and Frostbite"));
            tooltips.Add(new TooltipLine(Mod, "CryoCothTipDrop", "100% drop from the Nurse"));
        }
    }
}
