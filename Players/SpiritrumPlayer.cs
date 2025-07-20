using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ID;

namespace Spiritrum.Players
{
    public class SpiritrumPlayer : ModPlayer
    {
        // Flag to track if this is a new character
        public bool isNewPlayer = true;

        // Morbium set bonus flag
        public bool morbiumSetBonus = false;

        public override void ResetEffects()
        {
            morbiumSetBonus = false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["isNewPlayer"] = false;
        }

        public override void LoadData(TagCompound tag)
        {
            // Load the flag - if it exists, this is not a new player
            isNewPlayer = tag.ContainsKey("isNewPlayer") ? tag.Get<bool>("isNewPlayer") : true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Apply venom if using morbium set bonus and it was a melee or whip attack
            if (morbiumSetBonus && (hit.DamageType == DamageClass.Melee || hit.DamageType == DamageClass.MeleeNoSpeed))
            {
                target.AddBuff(BuffID.Venom, 300); // 5 seconds of venom
            }
        }
    }
}
