using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Spiritrum.Content.Projectiles.Minions;
using Spiritrum.Players;

namespace Spiritrum.Content.Buffs
{
    public class NamelessParasiteBuff : ModBuff
    {        public override void SetStaticDefaults()
        {
            // DisplayName and Description are handled by .hjson localization files
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
            
            // These settings are important for minion buffs - using the proper BuffID.Sets properties
            Main.buffNoTimeDisplay[Type] = false; // The buff shouldn't display a timer
            Main.vanityPet[Type] = false; // This is not a vanity pet buff
            Main.lightPet[Type] = false; // This is not a light pet buff
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
        }        public override void Update(Player player, ref int buffIndex)
        {
            // This code ensures the minion stays alive and links it to the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<NamelessParasiteMinion>()] > 60)
            {
                player.buffTime[buffIndex] = 60; // Resetting buff time keeps the buff active
            }
            else
            {
                // If the player has this buff but not the minion, remove the buff
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
