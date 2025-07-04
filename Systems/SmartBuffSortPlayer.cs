using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace Spiritrum.Systems
{
    public class SmartBuffSortPlayer : ModPlayer
    {
        public override void PostUpdateBuffs()
        {
            // Get current buffs and their times
            var buffs = Player.buffType
                .Select((type, i) => new { type, time = Player.buffTime[i] })
                .Where(b => b.type > 0)
                .OrderBy(b => Lang.GetBuffName(b.type))
                .ToList();

            // Clear all buffs
            for (int i = 0; i < Player.buffType.Length; i++)
            {
                Player.buffType[i] = 0;
                Player.buffTime[i] = 0;
            }

            // Re-add sorted buffs
            for (int i = 0; i < buffs.Count; i++)
            {
                Player.buffType[i] = buffs[i].type;
                Player.buffTime[i] = buffs[i].time;
            }
        }
    }
}
