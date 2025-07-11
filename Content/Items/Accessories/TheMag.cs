using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Spiritrum.Content.Items.Accessories
{
    
    public class TheMag : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 28; // Placeholder width, adjust if you have a sprite for this item
            Item.height = 28; // Placeholder height, adjust if you have a sprite for this item
            Item.accessory = true; // Marks this item as an accessory
            Item.value = Item.sellPrice(gold: 1, silver: 50); // Example monetary value, adjust as needed
            Item.rare = ItemRarityID.LightRed; // Example rarity (e.g., Hardmode, pre-mechanical bosses), adjust as needed
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Item heldItem = player.HeldItem;

            // Check if the player is holding a valid item that uses bullets as ammunition
            if (heldItem != null && !heldItem.IsAir && heldItem.useAmmo == AmmoID.Bullet)
            {
                player.GetAttackSpeed(DamageClass.Ranged) += 0.15f;

                player.GetDamage(DamageClass.Ranged) -= 0.05f;

                player.GetCritChance(DamageClass.Ranged) -= 4;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "TheMagTip1", "Increases gun attack speed by 15%"));
            tooltips.Add(new TooltipLine(Mod, "TheMagTip2", "Decreases gun damage by 5%"));
            tooltips.Add(new TooltipLine(Mod, "TheMagTip3", "Decreases gun critical strike chance by 4%"));
            tooltips.Add(new TooltipLine(Mod, "TheMagTip4", "Juicy Uranium"));
        }

        // Optional: Add a recipe for crafting this accessory if you wish.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe()
                .AddIngredient(ItemID.CobaltBar, 2)       // Cobalt bars for durability
                .AddIngredient(ItemID.MythrilBar, 4)      // Mythril bars for advanced crafting
                .AddIngredient(ItemID.SoulofNight, 5)     // Soul of Night for special properties
                .AddTile(TileID.MythrilAnvil);            // Requires a Mythril or Orichalcum Anvil
                
            if (ModLoader.TryGetMod("gunrightsmod", out Mod TerMerica) && TerMerica.TryFind("UraniumBar", out ModItem UraniumBar))
            {
                recipe.AddIngredient(UraniumBar.Type, 10);
            }
            
            recipe.Register();
        }
    }
}
