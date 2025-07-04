using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items
{
    public class VoidEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Essence");
            // Tooltip.SetDefault("'Crystallized fragments of the void'\nUsed to craft void-infused items");
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            
            // Register a material
            ItemID.Sets.ItemIconPulse[Item.type] = true; // Makes the item pulsate in the inventory
            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item float when thrown
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(silver: 25); // Each is worth 25 silver
            Item.rare = ItemRarityID.Pink;
        }

        public override void PostUpdate()
        {
            // Create dust effect when the item is dropped in the world
            Lighting.AddLight(Item.Center, 0.3f, 0.1f, 0.4f); // Emit purple light
            
            if (Main.rand.NextBool(10))
            {
                Dust dust = Dust.NewDustDirect(
                    Item.position,
                    Item.width,
                    Item.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add a lore tooltip
            TooltipLine line = new TooltipLine(Mod, "VoidEssenceLore", "A mysterious material torn from the spaces between dimensions")
            {
                OverrideColor = new Color(150, 80, 255)
            };
            tooltips.Add(line);
        }

        // No crafting recipe - this is obtained by slaying Void Harbingers
    }
}
