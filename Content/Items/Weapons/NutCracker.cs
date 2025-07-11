using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{
    public class NutCracker : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nut Cracker");
            // Tooltip.SetDefault("Releases nut shrapnel with each attack");
        }

        public override void SetDefaults()
        {
            Item.damage = 105;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 9f;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.scale = 1.8f; // Slightly larger scale for visual effect
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Release 4-6 nut shrapnel projectiles in a spread
            int numShrapnel = Main.rand.Next(4, 7);
            for (int i = 0; i < numShrapnel; i++)
            {
                float speed = Main.rand.NextFloat(4f, 6f);
                float angle = MathHelper.ToRadians(Main.rand.Next(-15, 18));
                Vector2 velocity = Vector2.UnitX.RotatedBy(angle) * speed * player.direction;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, velocity, ModContent.ProjectileType<Projectiles.NutShrapnel>(), Item.damage / 2, 2f, player.whoAmI);
            }
        }
    }
}
