using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Players;
using Spiritrum.Content.Items.Armor;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Global
{
    public class SpiritrumGlobalItem : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (player.GetModPlayer<IdeologySlotPlayer>().inflictMidas)
            {
                target.AddBuff(BuffID.Midas, 180);
            }
            
            // Check if player is wearing the full Rebornium armor set
            bool wearingReborniumSet = 
                player.armor[0].type == ModContent.ItemType<ReborniumMask>() &&
                player.armor[1].type == ModContent.ItemType<ReborniumChestplate>() &&
                player.armor[2].type == ModContent.ItemType<ReborniumLeggings>();
                
            // 5% chance to heal 1 HP when hitting an enemy with the Rebornium set
            if (wearingReborniumSet && Main.rand.NextBool(20))
            {
                player.Heal(1);
                // Show a healing effect
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(player.position, player.width, player.height, DustID.GreenTorch, 0f, -2f, 0, default, 1.5f);
                }
            }
        }
    }
}
