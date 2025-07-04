using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using System.IO;
namespace Spiritrum.Systems
{
    // This system generates placeholder textures for various mod elements
    public class TextureGenerationSystem : ModSystem
    {
        public override void Load()
        {
            // Don't try to do anything on server
            if (Main.netMode == Terraria.ID.NetmodeID.Server)
                return;
            // DO NOT generate textures during Load - it must be done on the main thread
        }
        public override void PostSetupContent()
        {
            if (Main.netMode == Terraria.ID.NetmodeID.Server)
                return;
            // Queue texture generation on the main thread
            Main.QueueMainThreadAction(GenerateFusionTableTextures);
        }
        private void GenerateFusionTableTextures()
        {
            try
            {
                // Generate a texture for the Material Fusion Table item
                GenerateItemTexture(
                    "Spiritrum/Content/Items/Placeable/MaterialFusionTable", 
                    new Color(70, 40, 120), // Dark purple base
                    new Color(140, 100, 220) // Light purple highlight
                );
                // Generate a texture for the Material Fusion Table tile
                GenerateTileTexture(
                    "Spiritrum/Content/Tiles/MaterialFusionTableTile", 
                    new Color(70, 40, 120), // Dark purple base
                    new Color(140, 100, 220), // Light purple highlight
                    new Color(20, 10, 30) // Shadow color
                );
            }
            catch (System.Exception ex)
            {
                Mod mod = ModContent.GetInstance<SpiritrumMod>();
                mod.Logger.Error("Error generating textures: " + ex.Message);
            }
        }
        private void GenerateItemTexture(string texturePath, Color baseColor, Color highlightColor)
        {
            // Check if texture already exists on disk
            string filePath = Path.Combine(ModLoader.ModPath, "Spiritrum", texturePath + ".png");
            if (File.Exists(filePath))
                return;
            // Item texture dimensions
            int width = 32;
            int height = 32;
            Texture2D texture = new Texture2D(Main.instance.GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            // Fill with transparent pixels initially
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;
            // Draw a table-like shape
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Draw table top
                    if (y >= 10 && y < 15)
                    {
                        if (x >= 3 && x < width - 3)
                        {
                            data[y * width + x] = highlightColor;
                            // Add some details to the table top
                            if ((x + y) % 5 == 0)
                                data[y * width + x] = Color.Lerp(highlightColor, Color.White, 0.3f);
                        }
                    }
                    // Draw table legs
                    if (y >= 15 && y < height - 2)
                    {
                        if ((x >= 5 && x < 9) || (x >= width - 9 && x < width - 5))
                            data[y * width + x] = baseColor;
                    }
                    // Draw some magical elements on the table
                    if (y >= 5 && y < 10)
                    {
                        if (x >= 10 && x < width - 10)
                        {
                            // Create a glowing orb in the center
                            float distanceFromCenter = Vector2.Distance(
                                new Vector2(x, y), 
                                new Vector2(width / 2, 7)
                            ) / 5f;
                            if (distanceFromCenter < 1f)
                            {
                                float glow = 1f - distanceFromCenter;
                                data[y * width + x] = Color.Lerp(
                                    Color.Transparent, 
                                    Color.Lerp(Color.Purple, Color.White, glow * 0.7f), 
                                    glow
                                );
                            }
                        }
                    }
                }
            }
            texture.SetData(data);
            // Save texture to file
            SaveTextureToFile(texture, texturePath);
        }
        private void GenerateTileTexture(string texturePath, Color baseColor, Color highlightColor, Color shadowColor)
        {
            // Check if texture already exists on disk
            string filePath = Path.Combine(ModLoader.ModPath, "Spiritrum", texturePath + ".png");
            if (File.Exists(filePath))
                return;
            // Tile texture dimensions
            int width = 16;
            int height = 16;
            Texture2D texture = new Texture2D(Main.instance.GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            // Fill with transparent pixels initially
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;
            // Draw a workbench-like tile
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Draw table top
                    if (y >= 4 && y < 6)
                    {
                        if (x >= 0 && x < width)
                        {
                            data[y * width + x] = highlightColor;
                            // Add some details to the table top
                            if (x % 3 == 0)
                                data[y * width + x] = Color.Lerp(highlightColor, Color.White, 0.3f);
                        }
                    }
                    // Draw table legs and structure
                    if (y >= 6 && y < height)
                    {
                        if ((x >= 2 && x < 4) || (x >= width - 4 && x < width - 2))
                            data[y * width + x] = baseColor;
                        // Draw center console
                        if (y >= 6 && y < 12 && x >= 6 && x < 10)
                            data[y * width + x] = Color.Lerp(baseColor, highlightColor, 0.5f);
                    }
                    // Draw glowing elements
                    if (y >= 2 && y < 4 && x >= 7 && x < 9)
                    {
                        data[y * width + x] = Color.Lerp(Color.Purple, Color.White, 0.7f);
                    }
                }
            }
            texture.SetData(data);
            // Save texture to file
            SaveTextureToFile(texture, texturePath);
        }
        private void SaveTextureToFile(Texture2D texture, string texturePath)
        {
            string fullPath = Path.Combine(ModLoader.ModPath, "Spiritrum", texturePath + ".png");
            // Ensure directory exists
            string directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            // Save texture as PNG file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                texture.SaveAsPng(stream, texture.Width, texture.Height);
            }
        }
    }
}
