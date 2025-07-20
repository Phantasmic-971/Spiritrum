using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic; // Needed for List<TooltipLine>
using Terraria.Localization; // Needed for TooltipLine
using Spiritrum.Content.Items.Materials; // Assuming VoidEssence is in this namespace

namespace Spiritrum.Content.Items.Accessories
{
    public class AbyssalEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width     = 28;
            Item.height    = 28;
            Item.accessory = true; 		// mark as accessory
            Item.value     = Item.sellPrice(gold: 50);
            Item.rare      = ItemRarityID.Purple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic)  += 0.20f; // +10% damage
            player.GetCritChance(DamageClass.Generic) += 16;  // +8% crit
            player.maxMinions   += 2; 			// +1 minion slot
            player.maxTurrets   += 2; 			// +1 sentry slot
            player.statLifeMax2 += 20; 			// +10 max life
            player.lifeRegen    += 2; 			// +1 life/sec
            player.statManaMax2 += 40; 			// +20 max mana
            player.manaCost     -= 0.10f; 		// –5% mana usage
        }

        // --- Add the ModifyTooltips method ---
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipDamage", "+20% generic damage"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipCrit", "+16% generic critical strike chance"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipManaCost", "-10% mana usage")); // Note: Negative value shows as a reduction
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipMana", "+40 maximum mana"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipLife", "+20 maximum life"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipLifeRegen", "+2 life regeneration")); // This usually appears as a hidden stat unless boosted significantly or from specific items
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipMinions", "+2 maximum number of minions"));
            tooltips.Add(new TooltipLine(Mod, "NamelessEmblemTooltipSentries", "+2 maximum number of sentries"));

        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<NamelessEmblem>(), 3)
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 50)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}