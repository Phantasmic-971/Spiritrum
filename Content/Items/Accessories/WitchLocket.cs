using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Accessories
{
        public class WitchLocket : ModItem
        {
            public override bool CanEquipAccessory(Player player, int slot, bool modded)
            {
                // Prevent equipping if Void Pendant or Pygmy Necklace is equipped
                for (int i = 0; i < player.armor.Length; i++)
                {
                    Item acc = player.armor[i];
                    if (acc != null && !acc.IsAir)
                    {
                        if (acc.type == ModContent.ItemType<VoidPendant>() || acc.type == ItemID.PygmyNecklace)
                            return false;
                    }
                }
                return base.CanEquipAccessory(player, slot, modded);
            }
        public override void SetStaticDefaults()
        {
            // Tooltip handled in ModifyTooltips for localization safety
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.buyPrice(gold: 12);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 2;
            player.GetDamage(DamageClass.Summon) += 0.15f;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "WitchLocketEffect1", "+2 max minions"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "WitchLocketEffect2", "+15% summon damage"));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PygmyNecklace)
                .AddIngredient(ItemID.AvengerEmblem)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
