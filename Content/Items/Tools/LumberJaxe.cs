using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Tools
{
    public class LumberJaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 45;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.axe = 40; // Hardmode axe power
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.LucyTheAxe, 1)
                .AddIngredient(ItemID.Pearlwood, 25)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        // Optional: Add special tree-chopping effects
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
        }
    }
}
