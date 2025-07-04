using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Mounts
{
    public class SkiDooMount : ModMount
    {
        public override void SetStaticDefaults()
        {
            // Basic mount settings
            MountData.buff = ModContent.BuffType<Content.Buffs.SkiDooBuff>();
            MountData.heightBoost = 20;
            MountData.fallDamage = 0.4f;
            MountData.runSpeed = 7f;
            MountData.dashSpeed = 9f;
            MountData.flightTimeMax = 0;
            MountData.fatigueMax = 0;
            MountData.jumpHeight = 10;
            MountData.acceleration = 0.2f;
            MountData.jumpSpeed = 3f;
            MountData.blockExtraJumps = false;
            
            // Frame data and player offsets - single frame for 91x141 texture
            MountData.totalFrames = 1;
            MountData.playerYOffsets = new int[] { 20 };
            MountData.xOffset = -4;
            MountData.yOffset = 9;
            MountData.bodyFrame = 3;
            MountData.playerHeadOffset = 31;
            
            // Standing animation - single frame
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            
            // Running animation - single frame
            MountData.runningFrameCount = 1;
            MountData.runningFrameDelay = 12;
            MountData.runningFrameStart = 0;
            
            // In-air animation - single frame
            MountData.inAirFrameCount = 1;
            MountData.inAirFrameDelay = 12;
            MountData.inAirFrameStart = 0;
            
            // Idle animation - single frame
            MountData.idleFrameCount = 1;
            MountData.idleFrameDelay = 12;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = true;
            
            MountData.spawnDust = DustID.Snow;
            
            // Texture dimensions - try both front and back textures like ExampleMount
            if (!Main.dedServ)
            {
                if (MountData.backTexture != null)
                {
                    MountData.textureWidth = MountData.backTexture.Width();
                    MountData.textureHeight = MountData.backTexture.Height();
                }
                else if (MountData.frontTexture != null)
                {
                    MountData.textureWidth = MountData.frontTexture.Width();
                    MountData.textureHeight = MountData.frontTexture.Height();
                }
            }
        }
        public override void UpdateEffects(Player player)
        {
            bool onSnow = false;
            Tile tile = Framing.GetTileSafely(player.Center.ToTileCoordinates());
            ushort type = tile.TileType;
            if (type == TileID.SnowBlock || type == TileID.IceBlock || 
                type == TileID.BreakableIce || type == TileID.Slush)
            {
                onSnow = true;
            }
            if (onSnow)
            {
                player.runAcceleration += 0.5f;
                player.maxRunSpeed += 4f;
            }
            else
            {
                player.runAcceleration += 0.0f;
                player.maxRunSpeed += 0f;
            }
        }
    }
}