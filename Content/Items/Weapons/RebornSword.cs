using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Weapons
{
    public class RebornSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip handled in the localization file
            ItemID.Sets.Yoyo[Item.type] = false; // This is not a yoyo but including this to prevent issues
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // Allows full screen use range on gamepads
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            // Basic sword properties
            Item.damage = 18;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ItemRarityID.White; // Common rarity (pre-boss)
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 1.2f; // Slightly larger than normal swords
            
            // Since this is an early game sword, it has no projectiles
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Create slight dust effect when swinging the sword
            if (Main.rand.NextBool(3))
            {
                // Create a dust effect similar to Rebornium's color (bright lime green)
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 
                    DustID.GreenTorch, 0f, 0f, 0, default, 1f);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add special tooltip for the RebornSword
            tooltips.Add(new TooltipLine(Mod, "RebornSwordTooltip", 
                "A sword forged from the strange ore that has the power of rebirth"));
            
            // Add a second tooltip line for lore
            tooltips.Add(new TooltipLine(Mod, "RebornSwordLore", 
                "The bright green blade pulses with energy, as if alive"));
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            // Requires Rebornium Bars for crafting
            recipe.AddIngredient(ItemType<Placeable.ReborniumBar>(), 12);
            // Crafted at a basic anvil (early game)
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
