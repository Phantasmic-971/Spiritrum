using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Armor
{    [AutoloadEquip(EquipType.Head)]
    public class FrozoniteHeadgear : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.Yellow; 
            Item.defense = 18; 
        }        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "FrozoniteHeadgearBonus", "+10% critical strike chance"));
            tooltips.Add(new TooltipLine(Mod, "FrozoniteHeadgearBonus2", "+10% damage"));
        }
        
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 10f; // Increased from 4% to 10% for post-Golem
            player.GetDamage(DamageClass.Generic) += 0.10f;
        }
        
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<FrozoniteChestplate>() && legs.type == ItemType<FrozoniteSkates>();
        }
        
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "30% chance to not consume ammo\n+20% all damage\n+80 max mana and +2 summon slots\nGrants the Ice Barrier buff when below 50% health";
            
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.GetCritChance(DamageClass.Generic) += 10f;
            player.maxMinions += 2;
            player.statManaMax2 += 40;
            player.ammoCost80 = true;
            
            // Add ice barrier effect when health is low
            if (player.statLife <= player.statLifeMax2 / 2)
            {
                player.AddBuff(BuffID.IceBarrier, 5); // 5 is just to keep the buff active
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Placeables.FrozoniteBar>(), 12);
            recipe.AddIngredient(ItemID.FrostCore, 1); // Add Frost Core for ice theme
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
