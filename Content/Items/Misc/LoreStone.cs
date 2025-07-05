using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Misc
{
    public class LoreStone : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lore Stone");
            // Tooltip.SetDefault("A mysterious stone containing the knowledge of world creation");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(gold: 1);
            Item.maxStack = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Clear default tooltip
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Name == "Tooltip0")
                    tooltips.RemoveAt(i--);
            }

            // Add lore tooltips
            tooltips.Add(new TooltipLine(Mod, "LoreTitle", "World Creation Lore"));
            
            // First paragraph
            tooltips.Add(new TooltipLine(Mod, "Lore1", "The subspace, or so called edge, allows all 11 dimensions"));
            tooltips.Add(new TooltipLine(Mod, "Lore2", "to stay in place and away from each other."));
            
            // Second paragraph - dimensions
            tooltips.Add(new TooltipLine(Mod, "Lore3", "The 1st, is the world of creators. The 2nd, is Terraria."));
            tooltips.Add(new TooltipLine(Mod, "Lore4", "The 3rd, is life. The 4th, is death. The 5th, are the elements."));
            tooltips.Add(new TooltipLine(Mod, "Lore5", "The 6th, is materialisation. The 7th, is the world of immortals."));
            tooltips.Add(new TooltipLine(Mod, "Lore6", "The 8th, is knowledge. The 9th, is power. The 10th, are the ideas."));
            tooltips.Add(new TooltipLine(Mod, "Lore7", "The 11th, are. [UNKNOWN DATA]."));
            
            // Third paragraph - creation days
            tooltips.Add(new TooltipLine(Mod, "Lore8", "Day 1, the foundation of the world was formed."));
            tooltips.Add(new TooltipLine(Mod, "Lore9", "Day 2, resources were formed."));
            tooltips.Add(new TooltipLine(Mod, "Lore10", "Day 3, balance between death and life was implemented."));
            tooltips.Add(new TooltipLine(Mod, "Lore11", "Day 4, the first signs of intelligence and ideas appeared."));
            tooltips.Add(new TooltipLine(Mod, "Lore12", "Day 5, [UNKNOWN]."));
            tooltips.Add(new TooltipLine(Mod, "Lore13", "Day 6, the first terrarians came to be, Bromus, [No identity] and Phantus."));
            tooltips.Add(new TooltipLine(Mod, "Lore14", "Day 7, the initial world was created and copied multiple times"));
            tooltips.Add(new TooltipLine(Mod, "Lore15", "using some godly seeds. There was no event on day 8 and 9."));
            
            // Fourth paragraph - events
            tooltips.Add(new TooltipLine(Mod, "Lore16", "On day 10, [No identity] learned to restore life to the skin. However,"));
            tooltips.Add(new TooltipLine(Mod, "Lore17", "the exercise of such power introduced some side effects to the body,"));
            tooltips.Add(new TooltipLine(Mod, "Lore18", "like loss of DNA. But this DNA was transferred to the earth"));
            tooltips.Add(new TooltipLine(Mod, "Lore19", "and formed new materials. Phantus did not participate in the experiment."));
            tooltips.Add(new TooltipLine(Mod, "Lore20", "These minerals were both named after Bromus and [No identity]."));
            tooltips.Add(new TooltipLine(Mod, "Lore21", "They were named Bromium and..."));
            
            // Final paragraph
            tooltips.Add(new TooltipLine(Mod, "Lore22", "The creators were upset. Therefore, they sent the [UNKNOWN]"));
            tooltips.Add(new TooltipLine(Mod, "Lore23", "to exterminate them. Phantus, as a present, was turned into a plushie"));
            tooltips.Add(new TooltipLine(Mod, "Lore24", "and sent to the 7th dimension, the world of immortals,"));
            tooltips.Add(new TooltipLine(Mod, "Lore25", "to offer to the inhabitants of this world who passed the TEST."));
            tooltips.Add(new TooltipLine(Mod, "Lore26", "As a present, the 2nd dimension receives a visit from the 11th dimension."));
            
            // Style the title line differently
            foreach (TooltipLine line in tooltips)
            {
                if (line.Name == "LoreTitle")
                {
                    line.OverrideColor = new Microsoft.Xna.Framework.Color(255, 215, 0); // Gold color
                }
            }
        }
    }
}
