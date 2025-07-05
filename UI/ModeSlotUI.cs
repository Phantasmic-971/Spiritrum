using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Spiritrum.Players;
using Spiritrum.Config;

namespace Spiritrum.UI
{
    public class ModeSlotUI : UIState
    {
        private ModeSlotItemSlot modeSlotItemSlot;
        private Vector2 slotPosition = new Vector2(1665f, 700f); // Default position

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.RemoveAllChildren();
            if (Main.playerInventory)
            {
                // Get the configured percentage
                var config = ModContent.GetInstance<SpiritrumConfig>();
                float percentage = config != null ? config.ModeSlotXPositionPercent / 100f : 0.88f;
                
                // Calculate position based on screen size
                float xPosition = Main.screenWidth * percentage;
                
                // Update position each frame to handle resolution changes
                modeSlotItemSlot.Left.Set(xPosition, 0f);
                // Append to UI
                Append(modeSlotItemSlot);
            }
        }

        public override void OnInitialize()
        {
            // Create the item slot element
            modeSlotItemSlot = new ModeSlotItemSlot(this);
            
            // Get the configured percentage
            var config = ModContent.GetInstance<SpiritrumConfig>();
            float percentage = config != null ? config.ModeSlotXPositionPercent / 100f : 0.88f;
            
            // Calculate position based on screen size
            float xPosition = Main.screenWidth * percentage;
            
            // Set its initial position
            modeSlotItemSlot.Left.Set(xPosition, 0f);
            modeSlotItemSlot.Top.Set(slotPosition.Y, 0f);
        }
        
        public void UpdatePosition(int xPosition)
        {
            if (modeSlotItemSlot != null)
            {
                // Use the provided position value from the config
                modeSlotItemSlot.Left.Set(xPosition, 0f);
                
                // Keep the original Y position
                modeSlotItemSlot.Top.Set(slotPosition.Y, 0f);
                
                // Force a recalculation of the UI element
                Recalculate();
            }
        }
    }

    public class ModeSlotItemSlot : UIElement
    {
        private readonly ModeSlotUI parentUI;

        public ModeSlotItemSlot(ModeSlotUI parent)
        {
            parentUI = parent;
            Width.Set(52f, 0f);
            Height.Set(52f, 0f);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<ModeSlotPlayer>();
            CalculatedStyle dimensions = GetDimensions();
            float slotX = dimensions.X;
            float slotY = dimensions.Y;

            // Draw slot background
            spriteBatch.Draw(TextureAssets.InventoryBack.Value, new Vector2(slotX, slotY), Color.White);

            // Draw item if present
            if (!modPlayer.modeSlotItem.IsAir)
            {
                Texture2D itemTexture = TextureAssets.Item[modPlayer.modeSlotItem.type].Value;
                float scale = 1f;
                int slotSize = 52;
                if (itemTexture.Width > 0 && itemTexture.Height > 0)
                {
                    scale = Math.Min((float)slotSize / itemTexture.Width, (float)slotSize / itemTexture.Height) * 0.8f;
                }
                Vector2 iconPos = new Vector2(
                    slotX + (slotSize - itemTexture.Width * scale) / 2f,
                    slotY + (slotSize - itemTexture.Height * scale) / 2f
                );
                spriteBatch.Draw(itemTexture, iconPos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }

            // Handle mouse interaction
            if (IsMouseHovering)
            {
                Main.hoverItemName = modPlayer.modeSlotItem.IsAir ? "Mode Accessory Slot" : modPlayer.modeSlotItem.Name;
                
                // Handle item swap
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    if (Main.mouseItem.IsAir || (Main.mouseItem.accessory && Main.mouseItem.Name.Contains("Mode")))
                    {
                        Utils.Swap(ref modPlayer.modeSlotItem, ref Main.mouseItem);
                    }
                }
            }
        }

        public override int CompareTo(object obj) => 0;
    }
}
