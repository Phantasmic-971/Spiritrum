using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Terraria.DataStructures;
using Terraria.Enums;
using Spiritrum.Content.Buffs;
using Terraria.Audio;

namespace Spiritrum.Content.Tiles
{
    public class IBISTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(120, 120, 200), CreateMapEntryName());
            DustType = DustID.Iron;
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            int buffType = ModContent.BuffType<BulletEvidence>();
            int buffTime = 60 * 60 * 60; // 1 hour in-game (3600 seconds)
            player.AddBuff(buffType, buffTime);
            // Optionally, play a sound or show a message
            SoundEngine.PlaySound(SoundID.Item4, player.position);
            return true;
        }
    }
}
