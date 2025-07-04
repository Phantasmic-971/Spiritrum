using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Spiritrum.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)] // Load the spritesheet as a shield for the player when equipped
    public class IrisCrossShield : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip should be set in localization files
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.buyPrice(gold: 10);
            Item.defense = 15;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.noKnockback = true;
            // Paladin's Shield effect for nearby allies
            if (player.whoAmI == Main.myPlayer && player.team != 0)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player other = Main.player[i];
                    if (other.active && other.team == player.team && other.whoAmI != player.whoAmI && !other.dead && Vector2.Distance(player.Center, other.Center) < 800f)
                    {
                        other.hasPaladinShield = true;
                    }
                }
            }
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.Dazed] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[BuffID.OgreSpit] = true;
            player.buffImmune[BuffID.Daybreak] = true;
            player.buffImmune[BuffID.MoonLeech] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.buffImmune[BuffID.Lovestruck] = true;
            player.buffImmune[BuffID.Stinky] = true;
            player.buffImmune[BuffID.Horrified] = true;
            player.buffImmune[BuffID.Midas] = true;
            player.buffImmune[BuffID.WindPushed] = true;
            player.buffImmune[BuffID.Slimed] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.OnFire3] = true;
            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.buffImmune[BuffID.Dazed] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[BuffID.OgreSpit] = true;
            player.buffImmune[BuffID.Daybreak] = true;
            player.buffImmune[BuffID.WitheredArmor] = true;
            player.buffImmune[BuffID.WitheredWeapon] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.buffImmune[BuffID.Lovestruck] = true;
            player.buffImmune[BuffID.Stinky] = true;
            player.buffImmune[BuffID.Horrified] = true;
            player.buffImmune[BuffID.Midas] = true;
            player.buffImmune[BuffID.WindPushed] = true;
            player.buffImmune[BuffID.Slimed] = true;
            player.buffImmune[BuffID.Silenced] = true;
      }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "IrisCrossShieldImmunity", "Gain immunity to almost all debuffs"));
            tooltips.Add(new TooltipLine(Mod, "IrisCrossShieldPaladinsShield", "Grants all nearby players on their team the Paladin's Shield buff,"));
            tooltips.Add(new TooltipLine(Mod, "IrisCrossShieldPaladinsShield2", "which causes 25% of any damage inflicted on them to transfer to the wearer of the shield instead."));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PaladinsShield)
                .AddIngredient(ItemID.AnkhShield)
                .AddIngredient(ItemID.FragmentSolar, 20)
                .AddIngredient(ItemID.FragmentVortex, 20)
                .AddIngredient(ItemID.FragmentNebula, 20)
                .AddIngredient(ItemID.FragmentStardust, 20)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
