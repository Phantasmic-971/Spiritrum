using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Other
{
    public class Phantasmo : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Phantasmo");
            // Tooltip.SetDefault("Restores 1 HP on use. 1% chance to deal 2000 damage to yourself!");
        }

        public override void SetDefaults()
        {
            Item.width = 24; // 40% of 24 is ~10
            Item.height = 24; // 40% of 24 is ~10
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 24; // 0.4 seconds at 60 FPS
            Item.useTime = 24;      // 0.4 seconds at 60 FPS
            Item.useTurn = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(silver: 50);
            Item.consumable = false;
            Item.autoReuse = true;
            Item.maxStack = 1;
            Item.scale = 1f; // Adjust scale to make the sprite more centered
        }

        public override Vector2? HoldoutOffset()
        {
            // Offset to center the small item sprite on the player
            return new Vector2(50, -10); // Adjust X as needed for best centering
        }

        public override bool? UseItem(Player player)
        {
            if (Main.rand.NextFloat() < 0.03f)
            {
                if (Main.myPlayer == player.whoAmI)
                {                    SoundEngine.PlaySound(new SoundStyle("Spiritrum/Audio/PhantasmoKill", SoundType.Sound) { Volume = 3f }, player.Center);
                }
                player.Hurt(PlayerDeathReason.ByCustomReason(Terraria.Localization.NetworkText.FromLiteral($"{player.name}, I hate you. You are worthless. Why did you cuddle me? Now I am mad >:( ")), 9999, 0);
            }
            else
            {
                player.statLife += 5;
                if (player.statLife > player.statLifeMax2)
                    player.statLife = player.statLifeMax2;
                player.HealEffect(5, true);
                if (Main.myPlayer == player.whoAmI)
                {
                    SoundEngine.PlaySound(new SoundStyle("Spiritrum/Audio/PhantasmoHeal", SoundType.Sound) { Volume = 3f }, player.Center);
                }
            }
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "PhantasmoTooltip1", "A mysterious plushie that seems oddly eager to help... or not."));
            tooltips.Add(new TooltipLine(Mod, "PhantasmoTooltip2", "Sometimes, a little hope comes with a big risk."));
            tooltips.Add(new TooltipLine(Mod, "PhantasmoTooltip3", "Use at your own peril (and amusement)."));
            tooltips.Add(new TooltipLine(Mod, "PhantasmoTooltip4", "Caution: May cause unexpected outcomes."));
            tooltips.Add(new TooltipLine(Mod, "PhantasmoTooltip5", "A label mentions: Made in Spiritrum"));
            tooltips.Add(new TooltipLine(Mod, "PhantasmoTooltip6", "Developer Item"));
        }
    }
}
