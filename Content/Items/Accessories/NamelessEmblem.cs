using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; // Needed for List<TooltipLine>
using Terraria.Localization; // Needed for TooltipLine

namespace Spiritrum.Content.Items.Accessories
{
    public class NamelessEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Localization via .hjson; no code-based SetDefault calls
            // Example .hjson entries:
            // Mods: {
            // 	Spiritrum: {
            // 		Items: {
            // 			NamelessEmblem: {
            // 				DisplayName: "Nameless Emblem"
            // 				Tooltip: "Placeholder tooltip line 1\nPlaceholder tooltip line 2" // You can override this with ModifyTooltips
            // 			}
            // 		}
            // 	}
            // }
        }

        public override void SetDefaults()
        {
            Item.width     = 28;
            Item.height    = 28;
            Item.accessory = true; 		// mark as accessory
            Item.value     = Item.sellPrice(gold: 20);
            Item.rare      = ItemRarityID.Purple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic)  += 0.10f; // +10% damage
            player.GetCritChance(DamageClass.Generic) += 8;  // +8% crit
            player.maxMinions   += 1; 			// +1 minion slot
            player.maxTurrets   += 1; 			// +1 sentry slot
            player.statLifeMax2 += 10; 			// +10 max life
            player.lifeRegen    += 1; 			// +1 life/sec
            player.statManaMax2 += 20; 			// +20 max mana
            player.manaCost     -= 0.05f; 		// –5% mana usage
        }

        // --- Add the ModifyTooltips method ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add your custom tooltip lines describing the item's effects
            // Use standard Terraria tooltip formats where applicable (e.g., "+10% generic damage")
            // Add damage/crit/mana lines first, as they often appear near the top
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipDamage", "+10% generic damage"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipCrit", "+8% generic critical strike chance"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipManaCost", "-5% mana usage")); // Note: Negative value shows as a reduction
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipMana", "+20 maximum mana"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipLife", "+10 maximum life"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipLifeRegen", "+1 life regeneration")); // This usually appears as a hidden stat unless boosted significantly or from specific items
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipMinions", "+1 maximum number of minions"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipSentries", "+1 maximum number of sentries"));


            // You can add other flavour text lines as well
            // tooltips.Add(new TooltipLine(Mod, "NamelessEmblemFlavour", "'Its origins are lost to time.'"));

            // Optional: Remove the default tooltip line if you want to fully customize
            // For example, if you had a default tooltip set in SetStaticDefaults or localization that you want to replace:
            /*
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Mod == "Spiritrum" && tooltips[i].Name == "Tooltip") // Assuming your default tooltip line is named "Tooltip"
                {
                    tooltips.RemoveAt(i);
                    i--; // Decrement index because removing shifts elements
                }
            }
            */
        }
    }
}