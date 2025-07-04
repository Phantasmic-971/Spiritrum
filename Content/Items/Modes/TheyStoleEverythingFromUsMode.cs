using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Spiritrum.Players;

namespace Spiritrum.Content.Items.Modes
{
    public class TheyStoleEverythingFromUsMode : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip should be set in localization files
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = Item.buyPrice(gold: 5);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 1); // Requires 1 dirt block
			recipe.AddTile(TileID.WorkBenches); // Crafted at any workbench
			recipe.Register();
		}
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            // Only allow equipping in the custom mode slot
            var modeSlotPlayer = player.GetModPlayer<ModeSlotPlayer>();
            return modded && slot == 0 && modeSlotPlayer != null && modeSlotPlayer.modeSlotItem.type == Item.type;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Big Buff: Québec’s Wrath
            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.moveSpeed += 0.3f;
            player.GetCritChance(DamageClass.Generic) += 20f;
            player.maxMinions += 2;
            player.maxTurrets += 2;

            // Big Debuff: Canadian Guilt
            player.endurance -= 0.5f;
            player.lifeRegen = (int)(player.lifeRegen * 0.75f);
            player.pickSpeed += 0.5f;
            player.accRunSpeed *= 0.5f;
            player.runAcceleration *= 0.33f;
            player.runSlowdown *= 0.33f;
        }
        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "TSEUMModeBuff", "Québec’s Wrath: +50% damage, +30% movement speed, +20% crit chance, +2 minion/sentry slots"));
            tooltips.Add(new TooltipLine(Mod, "TSEUMModeDebuff", "Canadian Guilt: Take 50% more damage, -25% life regen, -50% mining speed, -67% acceleration/deacceleration"));
        }
	}
}