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
{    public class IdeologySlotUI : UIState
    {
        private IdeologySlotItemSlot ideologySlotItemSlot;
        private Vector2 slotPosition = new Vector2(1665f, 775f); // Default position

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.RemoveAllChildren();
            if (Main.playerInventory)
            {
                // Slot position is updated from config
                Append(ideologySlotItemSlot);
            }
        }

        public override void OnInitialize()
        {
            ideologySlotItemSlot = new IdeologySlotItemSlot(this);
            
            // Try to load position from config, fall back to default if config not available
            float xPosition = 1665f; // Default
            var config = ModContent.GetInstance<SpiritrumConfig>();
            if (config != null)
            {
                xPosition = config.IdeologySlotXPosition;
            }
            
            ideologySlotItemSlot.Left.Set(xPosition, 0f);
            ideologySlotItemSlot.Top.Set(slotPosition.Y, 0f);
        }
        
        /// <summary>
        /// Updates the UI position based on the config value
        /// </summary>
        /// <param name="xPosition">New X position for the slot</param>
        public void UpdatePosition(int xPosition)
        {
            if (ideologySlotItemSlot != null)
            {
                // Update the slot's X position
                ideologySlotItemSlot.Left.Set(xPosition, 0f);
                
                // Keep the original Y position
                ideologySlotItemSlot.Top.Set(slotPosition.Y, 0f);
                
                // Force a recalculation of the UI element
                Recalculate();
            }
        }
    }    public class IdeologySlotItemSlot : UIElement
    {
        private readonly IdeologySlotUI parentUI;

        public IdeologySlotItemSlot(IdeologySlotUI parent)
        {
            parentUI = parent;
            Width.Set(52f, 0f);
            Height.Set(52f, 0f);
        }        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<IdeologySlotPlayer>();
            CalculatedStyle dimensions = GetDimensions();
            float slotX = dimensions.X;
            float slotY = dimensions.Y;

            // Draw dark green slot background
            Color darkGreen = new Color(20, 80, 20, 255);
            spriteBatch.Draw(TextureAssets.InventoryBack.Value, new Vector2(slotX, slotY), darkGreen);

            // Draw item if present
            if (!modPlayer.ideologySlotItem.IsAir)
            {
                Texture2D itemTexture = Terraria.GameContent.TextureAssets.Item[modPlayer.ideologySlotItem.type].Value;
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

            // Handle mouse interaction (dragging removed)
            if (IsMouseHovering)
            {
                Main.hoverItemName = modPlayer.ideologySlotItem.IsAir ? "Ideology Accessory Slot" : modPlayer.ideologySlotItem.Name;
                
                // Handle item swap
                if (Main.mouseLeftRelease && Main.mouseLeft)
                {
                    if (Main.mouseItem.IsAir || (Main.mouseItem.accessory && IsIdeologyItem(Main.mouseItem)))
                    {
                        Utils.Swap(ref modPlayer.ideologySlotItem, ref Main.mouseItem);
                    }
                }
            }
        }

        private bool IsIdeologyItem(Item item)
        {
            if (item.ModItem != null)
            {
                string modItemFullName = item.ModItem.GetType().FullName;
                return modItemFullName != null && modItemFullName.Contains(".Items.Ideology.");
            }
            return false;
        }

        public override int CompareTo(object obj) => 0;
    }
}
