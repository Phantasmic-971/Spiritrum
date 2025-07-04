using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
    public class NamelessCodex : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip handled in localization or ModifyTooltips
        }
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 36;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(gold: 100);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<NamelessCodexProjectile>();
            Item.shootSpeed = 60f;
            Item.mana = 25;
        }
        public override Vector2? HoldoutOffset() => new Vector2(-6, 0);
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "NamelessCodexTip1", "Fires a cultist fireball that explodes on hit and on expiration"));
        }
    }
}
