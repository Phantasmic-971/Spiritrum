using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Spiritrum.Content.Items.Misc;

namespace Spiritrum.Players
{
    public class SpiritrumPlayer : ModPlayer
    {
        // Flag to track if this is a new character
        public bool isNewPlayer = true;

        public override void SaveData(TagCompound tag)
        {
            // Save the flag so we know this is not a new player anymore
            tag["isNewPlayer"] = false;
        }

        public override void LoadData(TagCompound tag)
        {
            // Load the flag - if it exists, this is not a new player
            isNewPlayer = tag.ContainsKey("isNewPlayer") ? (bool)tag["isNewPlayer"] : true;
        }

        public override void OnEnterWorld()
        {
            // If this is a new character and the mod is enabled, give them a LoreStone
            if (isNewPlayer)
            {
                // Set to false to avoid giving more in future
                isNewPlayer = false;
                
                // Create the LoreStone and give it to the player
                Item loreStone = new Item();
                loreStone.SetDefaults(ModContent.ItemType<LoreStone>());
                Player.QuickSpawnItem(Player.GetSource_GiftOrReward("StartingItem"), loreStone);
            }
        }
    }
}
