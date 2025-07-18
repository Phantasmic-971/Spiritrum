using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Items.Accessories
{
    public class VoidClaws : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in ModifyTooltips for localization safety
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
            player.kbGlove = true; // Fire Gauntlet effect
            player.autoReuseGlove = true; // Fire Gauntlet effect
            player.meleeScaleGlove = true; // Fire Gauntlet effect
            player.GetDamage(DamageClass.Melee) += 0.15f; // +15% melee damage
            player.GetAttackSpeed(DamageClass.Melee) += 0.12f; // +12% melee speed
            player.GetKnockback(DamageClass.Melee) += 1.0f; // +100% knockback
            player.GetModPlayer<VoidClawsPlayer>().voidClaws = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidClawsEffect1", "Melee weapons inflict VoidFlames"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidClawsEffect2", "Melee knockback increased"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidClawsEffect3", "+15% melee damage"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidClawsEffect4", "+12% melee speed"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidClawsEffect5", "Enables autoswing for melee weapons and whips"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "VoidClawsEffect6", "+10% melee size"));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FireGauntlet)
                .AddIngredient(ModContent.ItemType<VoidEssence>(), 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }

    public class VoidClawsPlayer : ModPlayer
    {
        public bool voidClaws;

        public override void ResetEffects()
        {
            voidClaws = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (voidClaws && hit.DamageType == DamageClass.Melee)
            {
                target.AddBuff(ModContent.BuffType<Content.Debuffs.VoidFlames>(), 300); // 5 seconds
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (voidClaws && proj.DamageType == DamageClass.Melee)
            {
                target.AddBuff(ModContent.BuffType<Content.Debuffs.VoidFlames>(), 300); // 5 seconds
            }
        }

        public override void PostUpdate()
        {
            if (voidClaws && Player.HeldItem.DamageType == DamageClass.Melee)
            {
                // Emit shadowflame dust around the player when holding melee weapons
                if (Main.rand.NextBool(3))
                {
                    Vector2 dustPos = Player.Center + new Vector2(Main.rand.Next(-20, 21), Main.rand.Next(-20, 21));
                    Dust dust = Dust.NewDustDirect(dustPos, 0, 0, DustID.Shadowflame, 0f, 0f, 100, default(Color), 0.8f);
                    dust.noGravity = true;
                    dust.velocity *= 0.5f;
                }
            }
        }
    }

    public class VoidClawsGlobalItem : GlobalItem
    {
        public override void HoldItem(Item item, Player player)
        {
            if (player.GetModPlayer<VoidClawsPlayer>().voidClaws && item.DamageType == DamageClass.Melee)
            {
                // Enable autoswing
                item.autoReuse = true;

                // Emit shadowflame dust from held melee weapons
                if (player.HeldItem == item && Main.rand.NextBool(5))
                {
                    Vector2 dustPos = player.Center + new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    Dust dust = Dust.NewDustDirect(dustPos, 0, 0, DustID.Shadowflame, 0f, 0f, 100, default(Color), 0.6f);
                    dust.noGravity = true;
                    dust.velocity *= 0.3f;
                }
            }
        }


    }

    public class VoidClawsGlobalProjectile : GlobalProjectile
    {
        public override void PostAI(Projectile projectile)
        {
            // Emit shadowflame dust from melee projectiles when VoidClaws is equipped
            if (projectile.DamageType == DamageClass.Melee)
            {
                Player owner = Main.player[projectile.owner];
                if (owner != null && owner.GetModPlayer<VoidClawsPlayer>().voidClaws)
                {
                    if (Main.rand.NextBool(4))
                    {
                        Vector2 dustPos = projectile.Center + new Vector2(Main.rand.Next(-8, 9), Main.rand.Next(-8, 9));
                        Dust dust = Dust.NewDustDirect(dustPos, 0, 0, DustID.Shadowflame, 0f, 0f, 100, default(Color), 0.7f);
                        dust.noGravity = true;
                        dust.velocity *= 0.4f;
                    }
                }
            }
        }
    }
}
