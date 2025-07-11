using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;
using Spiritrum.Players;
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
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            // Defensive save - only save if there's actual data to save
            // Currently no persistent data needed for this GlobalItem
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            // Defensive load - gracefully handle missing data
            // Use TryGet to avoid KeyNotFoundException
            
            // Example of safe key reading:
            // if (tag.TryGet("someKey", out string value))
            // {
            //     // Handle the value safely
            // }
            
            // Remove any problematic legacy keys
            if (tag.ContainsKey("legacyData"))
            {
                tag.Remove("legacyData");
            }
        }

        public override bool InstancePerEntity => false; // Prevent unnecessary instances
    }
}
