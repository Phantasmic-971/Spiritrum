using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Projectiles.Minions;
using Spiritrum.Content.Buffs;

namespace Spiritrum.Content.Items.Weapons
{
    public class NamelessParasite : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip handled by .hjson localization files
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
            // Sets the defaults for how this minion will attach to the player
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
            
            // Specifies that this item will be a staff that creates a minion when used.
            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f;
        }        public override void SetDefaults()
        {
            Item.damage = 18; // The damage for the projectile shot by your weapon
            Item.knockBack = 2.2f;
            Item.mana = 9; // Mana cost
            Item.width = 32; // The item texture's width
            Item.height = 32; // The item texture's height
            Item.useTime = 36; // The time that the item takes to use
            Item.useAnimation = 36; // The time that the animation takes
            Item.useStyle = ItemUseStyleID.Swing; // Standard style for summoning staffs
            Item.value = Item.sellPrice(silver: 30); // How much the item sells for
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.UseSound = SoundID.Item44; // The sound when the item is used
            Item.autoReuse = false; // Disable auto-reuse for summon weapons
            Item.noMelee = true; // Whether the item's animation doesn't deal damage
            Item.noUseGraphic = false; // Show the item when used
            Item.scale = 0.5f;
            
            // These properties relate to the minion properties
            Item.DamageType = DamageClass.Summon; // Specifies the damage type
            
            // The projectile shot when used
            Item.shoot = ModContent.ProjectileType<NamelessParasiteMinion>();
        }        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(ModContent.BuffType<NamelessParasiteBuff>(), 60);
            
            // Calculating spawn position - offset from the player for better visuals
            position = player.Center - new Vector2(0, 20);
            
            // Add slight randomness to position when spawning multiple minions
            position.X += Main.rand.NextFloat(-20f, 20f);
            
            // Spawn the minion directly
            var projectileID = Projectile.NewProjectile(
                source,
                position,
                Vector2.Zero,
                type,
                damage,
                knockback,
                player.whoAmI
            );
            
            return false; // Return false because we don't want vanilla to spawn additional projectiles
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.VilePowder, 8);
            recipe.AddIngredient(ItemID.RottenChunk, 5); // Will automatically accept Vertebrae as alternative
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
