using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Spiritrum.Content.Buffs;

namespace Spiritrum.Content.Items.Consumables
{
    public class SpectralBrew : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spectral Brew");
            // Tooltip.SetDefault("Temporarily allows you to pass through enemies\nEmits an aura that damages nearby foes");
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 10);
            Item.buffType = ModContent.BuffType<Spectral>(); // Create this buff
            Item.buffTime = 600; // 10 seconds
        }

        public override bool? UseItem(Player player)
        {
            // Visual effects on use
            for (int i = 0; i < 20; i++)
            {
                Vector2 position = player.Center + Main.rand.NextVector2Circular(24f, 24f);
                Vector2 velocity = (position - player.Center) * 0.1f;
                
                Dust dust = Dust.NewDustPerfect(
                    position,
                    DustID.ShadowbeamStaff,
                    velocity,
                    0,
                    Color.White,
                    Main.rand.NextFloat(1.0f, 1.5f));
                dust.noGravity = true;
                dust.fadeIn = 1.1f;
            }
            
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.GlowingMushroom, 2);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}
