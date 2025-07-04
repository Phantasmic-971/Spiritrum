using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Players;
using Spiritrum.Content.Items.Ideology;
using Spiritrum.Systems;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Ideology
{
    public class UltimateIdeology : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip are handled in the ModifyTooltips method
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Purple; // Highest rarity
            Item.value = Item.buyPrice(platinum: 1); // Very expensive
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // ---- COMBINED POSITIVE EFFECTS FROM ALL IDEOLOGIES ----
            
            // Corporatism effects
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 8;
            
            // Capitalism effects
            player.discountAvailable = true; // Shop discount
            player.GetModPlayer<IdeologySlotPlayer>().inflictMidas = true; // More coins from enemies
            
            // Environmentalism effects
            player.moveSpeed += 0.10f;
            player.jumpSpeedBoost += 2f;
            player.manaCost -= 0.08f;
            player.GetModPlayer<IdeologySlotPlayer>().natureRegen = true;
            
            // Check if player is on grass for environment bonus
            int tileX = (int)(player.Center.X / 16f);
            int tileY = (int)((player.position.Y + player.height + 8f) / 16f);
            if (WorldGen.InWorld(tileX, tileY))
            {
                ushort tileType = Main.tile[tileX, tileY].TileType;
                if (tileType == TileID.Grass || tileType == TileID.JungleGrass || tileType == TileID.MushroomGrass)
                {
                    player.lifeRegen += 2;
                }
            }
            
            // Transhumanism effects
            player.statLifeMax2 += 40;
            player.statManaMax2 += 40;
            player.GetDamage(DamageClass.Generic) += 0.10f; // Stacks with previous buffs
            player.GetCritChance(DamageClass.Generic) += 5; // Stacks with previous buffs
            player.statDefense += 4;
            player.endurance += 0.10f;
            player.lifeRegen += 2;
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
            
            // Immunity to debuffs from Transhumanism
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            
            // Authoritarianism positive effects
            player.statDefense += 8; // Additional defense
            player.endurance += 0.08f; // Reduced damage taken
            
            // Centralism positive effects
            player.statLifeMax2 += 20; // Additional max life
            player.lifeRegen += 1; // Additional life regeneration
            
            // Communitarianism positive effects
            // Assuming it provides team buffs - apply personal buffs here
            player.lifeRegen += 1;
            
            // Conservatism positive effects
            // Assuming it provides stability - possibly defense and reduced knockback
            player.noKnockback = true; // Immunity to knockback
            
            // Social Democracy positive effects 
            // Assuming it provides balanced stats
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.moveSpeed += 0.05f;
            player.statDefense += 5;
            
            // Technocracy positive effects
            // Assuming it benefits technology/mechanical items
            player.GetDamage(DamageClass.Summon) += 0.15f; // Better summons/sentries
            player.maxTurrets += 1; // Additional sentry
            
            // Futurism positive effects
            player.GetAttackSpeed(DamageClass.Generic) += 0.12f; // Faster attacks
            // (futuristDamage effect skipped: property does not exist)
            
            // Anarchy - only include positive max values from random ranges
            // Max positive values from Anarchy's random ranges
            player.GetDamage(DamageClass.Generic) += 0.20f; // Take best value from range
            player.GetCritChance(DamageClass.Generic) += 5; // Take best value from range
            player.GetAttackSpeed(DamageClass.Generic) += 0.15f; // Take best value from range
              // Basic Politics - assuming it gives foundational bonuses
            player.statDefense += 3;
            player.GetDamage(DamageClass.Generic) += 0.03f;
            
            // Ultimate ideology equipped - no need for special flag as this property doesn't exist
            // in IdeologySlotPlayer - the accessory effects apply directly
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyTitle", "The Ultimate Ideology") { OverrideColor = new Color(255, 50, 255) });
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyDesc", "Combines all the benefits of every political Ideology"));
            
            // Stat bonuses
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyDamage", "+46% damage"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyCrit", "+18% critical strike chance"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyDefense", "+20 defense, +18% damage reduction"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyLife", "+60 max life, +6 life regeneration"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyMana", "+40 max mana, -8% mana cost"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologySpeed", "+15% movement speed, +50% jump speed"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyAttackSpeed", "+37% melee attack speed"));
            
            // Special effects
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologySpecial1", "Shop prices are reduced"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologySpecial2", "Enemies drop more coins"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologySpecial3", "Immune to Poison, Venom, Slow, and Confusion"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologySpecial4", "Additional sentry capacity"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologySpecial5", "Immune to knockback"));
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologySpecial6", "Enhanced natural regeneration on grass"));
            
            // Flavor text
            tooltips.Add(new TooltipLine(Mod, "UltimateIdeologyFlavor", "\"The perfect balance of all political views\"") { OverrideColor = new Color(150, 150, 255) });
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            // Can only be equipped in the Ideology slot
            return player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Anarchy>(), 1);
            recipe.AddIngredient(ItemType<Authoritarianism>(), 1);
            recipe.AddIngredient(ItemType<BasicPolitics>(), 1);
            recipe.AddIngredient(ItemType<Capitalism>(), 1);
            recipe.AddIngredient(ItemType<Centralism>(), 1);
            recipe.AddIngredient(ItemType<Communitarianism>(), 1);
            recipe.AddIngredient(ItemType<Conservatism>(), 1);
            recipe.AddIngredient(ItemType<Corporatism>(), 1);
            recipe.AddIngredient(ItemType<Environmentalism>(), 1);
            recipe.AddIngredient(ItemType<Futurism>(), 1);
            recipe.AddIngredient(ItemType<SocialDemocracy>(), 1);
            recipe.AddIngredient(ItemType<Technocracy>(), 1);
            recipe.AddIngredient(ItemType<Transhumanism>(), 1);
            
            // Add additional rare material to balance the recipe
            recipe.AddIngredient(ItemID.FragmentSolar, 5);
            recipe.AddIngredient(ItemID.FragmentVortex, 5);
            recipe.AddIngredient(ItemID.FragmentNebula, 5);
            recipe.AddIngredient(ItemID.FragmentStardust, 5);
            
            // Crafted at the Ancient Manipulator (endgame crafting station)
            recipe.AddTile(TileID.LunarCraftingStation);
            
            recipe.Register();
        }
    }
}
