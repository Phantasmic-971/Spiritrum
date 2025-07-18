using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Accessories
{
    public class VoidPendant : ModItem
    {
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            // Prevent equipping if Witch Locket or Pygmy Necklace is equipped
            for (int i = 0; i < player.armor.Length; i++)
            {
                Item acc = player.armor[i];
                if (acc != null && !acc.IsAir)
                {
                    if (acc.type == ModContent.ItemType<WitchLocket>() || acc.type == ItemID.PygmyNecklace)
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
            Item.width = 30;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 24);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Prevent stacking with WitchLocket and Pygmy Necklace
            if (!player.GetModPlayer<VoidPendantPlayer>().voidPendantEquipped)
            {
                player.maxMinions += 3;
                player.maxTurrets += 2;
                player.GetDamage(DamageClass.Summon) += 0.20f;
                player.GetModPlayer<VoidPendantPlayer>().voidPendantEquipped = true;
            }
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidPendantEffect1", "+3 max minions"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidPendantEffect2", "+2 max sentries"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidPendantEffect3", "+20% summon damage"));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WitchLocket>())
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}

    public class VoidPendantPlayer : ModPlayer
    {
        public bool voidPendantEquipped;
        public override void ResetEffects()
        {
            voidPendantEquipped = false;
        }
    }
