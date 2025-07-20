using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Accessories
{
    public class SpiderSyringe : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in ModifyTooltips for localization safety
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = Item.buyPrice(gold: 8);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.GetModPlayer<SpiderSyringePlayer>().spiderSyringe = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "SpiderSyringeEffect1", "Grants immunity to poison and venom"));
            tooltips.Add(new Terraria.ModLoader.TooltipLine(Mod, "SpiderSyringeEffect2", "Spreads toxic clouds that damage enemies"));
        }
    }

    public class SpiderSyringePlayer : ModPlayer
    {
        public bool spiderSyringe;
        private int toxicCloudTimer;

        public override void ResetEffects()
        {
            spiderSyringe = false;
        }

        public override void PostUpdate()
        {
            if (spiderSyringe)
            {
                toxicCloudTimer++;
                
                // Spawn toxic clouds every 120 ticks (2 seconds)
                if (toxicCloudTimer >= 120)
                {
                    toxicCloudTimer = 0;
                    
                    // Only spawn clouds if there are enemies nearby
                    bool enemiesNearby = false;
                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.active && !npc.friendly && npc.Distance(Player.Center) < 400f)
                        {
                            enemiesNearby = true;
                            break;
                        }
                    }
                    
                    if (enemiesNearby && Main.myPlayer == Player.whoAmI)
                    {
                        // Spawn 1-3 toxic clouds around the player
                        int cloudCount = Main.rand.Next(1, 4);
                        for (int i = 0; i < cloudCount; i++)
                        {
                            Vector2 spawnPos = Player.Center + new Vector2(Main.rand.Next(-150, 151), Main.rand.Next(-100, 101));
                            
                            // Choose random toxic cloud type (511, 512, or 513)
                            int cloudType = Main.rand.Next(3) switch
                            {
                                0 => ProjectileID.ToxicCloud,
                                1 => ProjectileID.ToxicCloud2,
                                _ => ProjectileID.ToxicCloud3
                            };
                            
                            Projectile.NewProjectile(
                                Player.GetSource_Accessory(Player.GetModPlayer<SpiderSyringePlayer>().Player.HeldItem),
                                spawnPos,
                                Vector2.Zero,
                                cloudType,
                                25, // 25 damage
                                0f,
                                Player.whoAmI
                            );
                        }
                    }
                }
            }
        }
    }

    public class SpiderSyringeGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // If toxic cloud hits an enemy and the owner has Spider Syringe, inflict venom
            if ((projectile.type == ProjectileID.ToxicCloud || 
                 projectile.type == ProjectileID.ToxicCloud2 || 
                 projectile.type == ProjectileID.ToxicCloud3) &&
                Main.player[projectile.owner].GetModPlayer<SpiderSyringePlayer>().spiderSyringe)
            {
                target.AddBuff(BuffID.Venom, 60); // 1 second of venom
            }
        }
    }
}
