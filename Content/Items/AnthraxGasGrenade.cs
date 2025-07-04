using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Spiritrum.Content.Items
{
    public class AnthraxGasGrenade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in localization or ModifyTooltips
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 9999;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 20;
            Item.crit = 15;
            Item.value = Item.buyPrice(silver: 2);
            Item.rare = ItemRarityID.Green;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.AnthraxProjectile>();
            Item.shootSpeed = 15f;
            Item.ammo = Item.type;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "AnthraxTip1", "Can be thrown or used with the Anthrax, sold by the Nameless NPC after killing Plantera"));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20); // Makes 1 at a time
            recipe.AddIngredient(ItemID.Grenade, 4);
            recipe.AddIngredient(ItemID.VialofVenom, 1);
            recipe.AddIngredient(ItemID.Ichor, 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();


      if (ModLoader.TryGetMod("gunrightsmod", out Mod TerMerica) && TerMerica.TryFind<ModItem>("AnthraxRocket", out ModItem AnthraxRocket))
      {
          recipe = CreateRecipe(20);

          recipe.AddIngredient(AnthraxRocket.Type, 10);
          recipe.AddIngredient(ItemID.Ichor, 2);

          recipe.AddTile(TileID.MythrilAnvil);
          recipe.Register();
        }

}
}
}
