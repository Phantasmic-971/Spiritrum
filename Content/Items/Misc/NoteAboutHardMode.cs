using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Spiritrum.Content.Items.Misc
{
    public class NoteAboutHardMode : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Note About Hardmode");
            // Tooltip.SetDefault("Rejoin for extra ores");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = 0;
            Item.rare = ItemRarityID.Red;
        }
        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "NoteAboutHardModeTooltip", "Killing Wall of Flesh will generate Penumbrium in the world"));
        }
    }
}
