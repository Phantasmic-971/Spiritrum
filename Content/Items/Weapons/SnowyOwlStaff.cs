using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Projectiles;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Weapons
{
    public class SnowyOwlStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip should be set in localization files
            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f;
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Summon;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<SnowyOwlStaffProjectile>();
            Item.shootSpeed = 10f;
            Item.mana = 10;
            Item.buffType = ModContent.BuffType<Spiritrum.Content.Buffs.SnowyOwlStaffBuff>();
            // Item.summon = true; // Not needed in tModLoader 1.4+, use Item.DamageType = DamageClass.Summon;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FlinxFur, 8)
                .AddIngredient(ItemID.BorealWood, 20)
                .AddIngredient(ItemID.Owl, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Apply the buff to the player
            player.AddBuff(Item.buffType, 2);
            
            // Spawn the minion at the mouse position
            Vector2 spawnPos = Main.MouseWorld;
            var projectile = Terraria.Projectile.NewProjectileDirect(source, spawnPos, velocity, type, damage, knockback, player.whoAmI);
            projectile.originalDamage = Item.damage;
            
            return false;
        }
    }
}
