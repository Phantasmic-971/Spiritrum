using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Players;
using static Terraria.ModLoader.ModContent;

namespace Spiritrum.Content.Items.Ideology
{
    public class WorstIdeology : ModItem
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
            Item.rare = ItemRarityID.Red; // High rarity despite negative effects
            Item.value = Item.buyPrice(platinum: 1); // Expensive but terrible
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // ---- COMBINED NEGATIVE EFFECTS FROM ALL IDEOLOGIES ----
            
            // Anarchy negative effects
            player.statDefense -= 5; // Reduced defense
            player.endurance -= 0.05f; // Increased damage taken
            // Worst case of random effects from Anarchy
            player.GetDamage(DamageClass.Generic) -= 0.2f; // Minimum damage
            player.moveSpeed -= 0.1f; // Minimum movement speed
            player.GetCritChance(DamageClass.Generic) -= 15; // Minimum crit chance
            player.GetAttackSpeed(DamageClass.Generic) -= 0.3f; // Minimum attack speed
            
            // Affect nearby allies (handled in the ally effect section below)
            
            // Authoritarianism negative effects
            player.GetDamage(DamageClass.Generic) -= 0.10f; 
            player.moveSpeed -= 0.10f;
            
            // Capitalism negative effects (assumed from its nature - tax on coins)
            player.coinLuck -= 1.0f; // Reduced coin drops
            
            // Centralism negative effects (assumed from centralized control)
            player.GetAttackSpeed(DamageClass.Generic) -= 0.15f;
            player.GetKnockback(DamageClass.Generic) -= 0.2f; // Reduced knockback
            
            // Communitarianism negative effects (assumed from collective cost)
            player.manaCost += 0.15f; // Increased mana costs
            player.statManaMax2 -= 20; // Reduced max mana
            
            // Conservatism negative effects (assumed from resistance to change)
            player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
            player.moveSpeed -= 0.05f;
            
            // Corporatism negative effects (assumed from corporate restrictions)
            player.GetKnockback(DamageClass.Generic) -= 0.25f;
            player.pickSpeed += 0.2f; // Mining is slower
            
            // Environmentalism negative effects (assumed from limitations)
            player.GetDamage(DamageClass.Melee) -= 0.1f; // Reduced melee damage to avoid harm
            player.statDefense -= 5; // More vulnerable
            
            // Futurism negative effects (assumptions based on over-reliance on technology)
            player.statLifeMax2 -= 20; // Reduced max life
            player.lifeRegen -= 1; // Reduced life regeneration
            
            // Social Democracy negative effects (assumed from tax burden)
            player.moveSpeed -= 0.05f; 
            player.GetDamage(DamageClass.Generic) -= 0.05f;
            
            // Socialism negative effects 
            player.GetDamage(DamageClass.Generic) -= 0.05f; // From actual Socialism implementation
            
            // Technocracy negative effects (assumed from overspecialization)
            player.statDefense -= 8; // Very reduced defense
            player.lifeRegen -= 2; // Significantly reduced life regen
            
            // Transhumanism negative effects (assumed from biological drawbacks)
            player.statDefense -= 10; // Extremely reduced defense
            player.manaCost += 0.25f; // Significantly increased mana cost
            
            // Basic Politics negative effects (assumed from bureaucracy)
            player.moveSpeed -= 0.05f; 
            player.GetAttackSpeed(DamageClass.Generic) -= 0.05f;
            
            // Apply negative effects to allies in range
            foreach (Player ally in Main.player)
            {
                if (ally.active && ally != player && Vector2.Distance(player.Center, ally.Center) < 800f)
                {
                    ally.statDefense -= 3; // Reduced defense of nearby allies (from Anarchy)
                    ally.GetDamage(DamageClass.Generic) -= 0.05f; // Minimum damage from Anarchy's range
                    ally.moveSpeed -= 0.05f; // Minimum movement speed from Anarchy's range
                }
            }
            
            // Add various environmental debuffs reflecting the terrible ideological mix
            if (Main.rand.NextBool(600)) // Approximately once every 10 seconds
            {
                player.AddBuff(BuffID.Confused, 300); // 5 seconds of confusion
            }
            
            // Vulnerability to debuffs
            player.buffImmune[BuffID.Poisoned] = false;
            player.buffImmune[BuffID.OnFire] = false;
            player.buffImmune[BuffID.Confused] = false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyTitle", "The Worst Ideology") { OverrideColor = new Color(255, 50, 50) });
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyDesc", "Combines all the drawbacks of every political Ideology"));
            
            // Stat penalties
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyDamage", "-45% damage"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyCrit", "-15% critical strike chance"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyDefense", "-28 defense, -5% damage reduction"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyLife", "-20 max life, -3 life regeneration"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyMana", "-20 max mana, +40% mana cost"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologySpeed", "-30% movement speed, -60% attack speed"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyMining", "-20% mining speed"));
            
            // Special penalties
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologySpecial1", "Reduced coin drops"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologySpecial2", "May randomly confuse you"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologySpecial3", "No immunity to common debuffs"));
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologySpecial4", "Weakens nearby allies"));
            
            // Flavor text
            tooltips.Add(new TooltipLine(Mod, "WorstIdeologyFlavor", "\"This combination should never exist\"") { OverrideColor = new Color(150, 50, 50) });
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            // Can only be equipped in the Ideology slot
            return player.GetModPlayer<IdeologySlotPlayer>().ideologySlotItem == Item;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
              // Add all ideology items as ingredients
            recipe.AddIngredient(ItemType<Anarchy>(), 1);
            recipe.AddIngredient(ItemType<Authoritarianism>(), 1);
            recipe.AddIngredient(ItemType<BasicPolitics>(), 1);
            recipe.AddIngredient(ItemType<Capitalism>(), 1);
            recipe.AddIngredient(ItemType<Centralism>(), 1);
            recipe.AddIngredient(ItemType<Communitarianism>(), 1);
            recipe.AddIngredient(ItemType<Communism>(), 1);
            recipe.AddIngredient(ItemType<Conservatism>(), 1);
            recipe.AddIngredient(ItemType<Corporatism>(), 1);
            recipe.AddIngredient(ItemType<Democracy>(), 1);
            recipe.AddIngredient(ItemType<Environmentalism>(), 1);
            recipe.AddIngredient(ItemType<Fascism>(), 1);
            recipe.AddIngredient(ItemType<Futurism>(), 1);
            recipe.AddIngredient(ItemType<Liberalism>(), 1);
            recipe.AddIngredient(ItemType<Libertarianism>(), 1);
            recipe.AddIngredient(ItemType<SocialDemocracy>(), 1);
            recipe.AddIngredient(ItemType<Socialism>(), 1);
            recipe.AddIngredient(ItemType<Technocracy>(), 1);
            recipe.AddIngredient(ItemType<Transhumanism>(), 1);
            
            // Add additional evil material to balance the recipe
            recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 25);
            
            // Crafted at the Ancient Manipulator (endgame crafting station)
            recipe.AddTile(TileID.DemonAltar); // Crafted at a demon altar for thematic reasons
            
            recipe.Register();
        }
    }
}
