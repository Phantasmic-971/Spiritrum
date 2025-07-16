using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Accessories
{
    public class CrystallineCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 5);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<CrystallineCrownPlayer>().crownEquipped = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "CrystallineSpikes", "Summon Crystalline Spikes that spins around you"));
        }
    }

    public class CrystallineCrownPlayer : ModPlayer
    {
        public bool crownEquipped;
        public override void ResetEffects() => crownEquipped = false;

        public override void PostUpdate()
        {
            if (crownEquipped && Player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.CrystallineCrownSpike>()] < 10)
            {
                // Spawn 6 spikes in a circle if not already present
                for (int i = 0; i < 10; i++)
                {
                    float angle = MathHelper.TwoPi / 10 * i;
                    Vector2 offset = new Vector2(70, 0).RotatedBy(angle);
                    Projectile.NewProjectile(Player.GetSource_Misc("CrystallineCrown"), Player.Center + offset, Vector2.Zero, ModContent.ProjectileType<Projectiles.CrystallineCrownSpike>(), 45, 4f, Player.whoAmI, i);
                }
            }
        }
    }
}
