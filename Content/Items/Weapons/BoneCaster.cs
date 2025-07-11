using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Spiritrum.Content.Items.Weapons
{
    public class BoneCaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bone Caster");
            // Tooltip.SetDefault("Shoots a spread of bones");
        }

        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 18;
            Item.width = 38;
            Item.height = 18;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.Bone;
            Item.shootSpeed = 12f;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-3, 1);

        public override bool? UseItem(Player player)
        {
            int numProjectiles = 8;
            float spread = MathHelper.ToRadians(18f);
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * Item.shootSpeed;
            for (int i = 0; i < numProjectiles; i++)
            {
                float rotation = MathHelper.Lerp(-spread, spread, i / (numProjectiles - 1f));
                Vector2 perturbedSpeed = velocity.RotatedBy(rotation) * 1f;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center, perturbedSpeed, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            }
            return true;
        }
    }
}
