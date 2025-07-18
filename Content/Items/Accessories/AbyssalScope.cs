using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Accessories
{
        public class AbyssalScope : ModItem
        {
            public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
            {
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "AbyssalScopeEffect1", "15% increased ranged damage"));
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "AbyssalScopeEffect2", "12% increased ranged critical strike chance"));
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "AbyssalScopeEffect3", "2% increased ranged attack speed"));
                tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "AbyssalScopeEffect4", "Right click while holding a gun to zoom out"));
            }
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 20);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.GetCritChance(DamageClass.Ranged) += 12f;
            player.GetAttackSpeed(DamageClass.Ranged) += 0.02f;
            player.GetModPlayer<AbyssalScopePlayer>().abyssalScope = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SniperScope)
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }

    public class AbyssalScopePlayer : ModPlayer
    {
        public bool abyssalScope;
        public override void ResetEffects()
        {
            abyssalScope = false;
        }
        public override void PostUpdate()
        {
            if (abyssalScope && Player.HeldItem.DamageType == DamageClass.Ranged && Player.HeldItem.useAmmo == AmmoID.Bullet)
            {
                if (Main.LocalPlayer.controlUseTile)
                {
                    Player.scope = true;
                }
            }
        }
    }
}
