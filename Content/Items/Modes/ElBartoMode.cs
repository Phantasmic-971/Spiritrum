using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using static Terraria.ModLoader.ModContent;
using Spiritrum.Players;
using System;

namespace Spiritrum.Content.Items.Modes
{
    public class ElBartoMode : ModItem
    {
        // Track active buffs and debuffs to prevent stacking too many
        private int activeBonusBuffs = 0;
        private int activeDebuffs = 0;
        private int maxBonusBuffs = 3;
        private int maxDebuffs = 2;
          // Timer for cycling effects
        private int effectTimer = 0;
        private int effectCycleTime = 600; // 10 seconds total cycle time
        private int phaseTime = 200; // Each phase lasts about 3.3 seconds
        private int currentPhase = 1; // Track the current phase (1, 2, or 3)
        
        public override void SetStaticDefaults()
        {
            // DisplayName handled in localization
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.Red;
            Item.value = Item.buyPrice(gold: 20);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Permanent bonuses (these are always active)
            player.statLifeMax2 += 150;
            player.statManaMax2 += 150;
            player.lifeRegen += 3;
            
            // Base penalties that are always active but not too severe
            player.moveSpeed -= 0.15f;
              // CHAOS MECHANICS
            // Effects cycle every 10 seconds with automatic phase transitions
            effectTimer++;
            
            // Check if we need to move to the next phase
            if (effectTimer % phaseTime == 0)
            {
                // Transition to next phase
                currentPhase++;
                if (currentPhase > 3)
                {
                    currentPhase = 1;
                    // Reset the cycle when we complete all phases
                    effectTimer = 0;
                    
                    // Reset buff counters when cycle completes
                    activeBonusBuffs = 0;
                    activeDebuffs = 0;
                    
                    // Clear certain buffs/debuffs when cycle completes
                    for (int i = 0; i < player.buffTime.Length; i++)
                    {
                        // Only clear buffs added by El Barto
                        if (IsElBartoEffect(player.buffType[i]))
                        {
                            player.ClearBuff(player.buffType[i]);
                        }
                    }
                    
                    // Visual effect for cycle reset
                    for (int i = 0; i < 20; i++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.WhiteTorch, 0, -2f, 0, default, 2f);
                    }
                }
                else
                {
                    // Visual effect for phase change
                    Color phaseColor = currentPhase == 1 ? new Color(50, 255, 50) : 
                                      (currentPhase == 2 ? new Color(255, 255, 50) : new Color(255, 50, 50));
                    
                    for (int i = 0; i < 15; i++)
                    {
                        Dust dust = Dust.NewDustDirect(player.Center, 10, 10, DustID.Clentaminator_Cyan);
                        dust.noGravity = true;
                        dust.scale = 1.5f;
                        dust.velocity = new Vector2(Main.rand.NextFloat(-3, 3), Main.rand.NextFloat(-3, 3));
                        dust.color = phaseColor;
                    }
                }
            }
            
            // Handle the current phase based on currentPhase value rather than calculating phase from timer
            
            // Phase 1: Mostly buffs
            if (currentPhase == 1 && activeBonusBuffs < maxBonusBuffs && Main.GameUpdateCount % 120 == 0)
            {
                ApplyRandomBuff(player);
                activeBonusBuffs++;
            }
            
            // Phase 2: Mixed effects
            else if (currentPhase == 2 && Main.GameUpdateCount % 180 == 0)
            {
                // 70% chance for buff, 30% for debuff
                if (Main.rand.NextFloat() < 0.7f && activeBonusBuffs < maxBonusBuffs)
                {
                    ApplyRandomBuff(player);
                    activeBonusBuffs++;
                }
                else if (activeDebuffs < maxDebuffs)
                {
                    ApplyRandomDebuff(player, false); // mild debuffs
                    activeDebuffs++;
                }
            }
            
            // Phase 3: Mostly debuffs
            else if (currentPhase == 3 && activeDebuffs < maxDebuffs && Main.GameUpdateCount % 150 == 0)
            {
                // Apply stronger debuffs in final phase
                ApplyRandomDebuff(player, effectTimer % phaseTime > phaseTime * 0.6f); // Strong debuffs in latter part of phase 3
                activeDebuffs++;
            }
              // Visual effect: dust particles that change color based on cycle phase
            if (Main.rand.NextBool(5) && !hideVisual)
            {
                Color dustColor = GetCycleColor(currentPhase);
                Vector2 dustPosition = player.Center + new Vector2(
                    Main.rand.NextFloat(-20, 20),
                    Main.rand.NextFloat(-30, 10));
                
                Dust dust = Dust.NewDustDirect(dustPosition, 10, 10, DustID.Clentaminator_Cyan);
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity = new Vector2(0, -1f);
                dust.color = dustColor;
            }
            
            // Add resource regeneration based on chaos level
            float chaosBonus = CalculateChaosBonus(player);
            player.GetDamage(DamageClass.Generic) += chaosBonus * 0.1f; // Up to 10% damage
            player.statDefense += (int)(chaosBonus * 5); // Up to 5 defense
        }

        private bool IsElBartoEffect(int buffType)
        {
            // List of buffs that were added by El Barto and should be cleared on cycle end
            int[] elBartoEffects = {
                BuffID.Swiftness, BuffID.Ironskin, BuffID.Regeneration, BuffID.Shine,
                BuffID.NightOwl, BuffID.Battle, BuffID.Thorns, BuffID.WaterWalking,
                BuffID.Archery, BuffID.Hunter, BuffID.Endurance, BuffID.Rage, BuffID.Wrath,
                BuffID.Cursed, BuffID.Darkness, BuffID.Silenced, BuffID.BrokenArmor,
                BuffID.Confused, BuffID.Slow, BuffID.Weak, BuffID.Chilled
            };
            
            foreach (int effect in elBartoEffects)
            {
                if (buffType == effect)
                    return true;
            }
            
            return false;
        }
        
        private float CalculateChaosBonus(Player player)
        {
            // Calculate a bonus based on how many different effects are active
            int effectCount = 0;
            for (int i = 0; i < player.buffType.Length; i++)
            {
                if (player.buffType[i] > 0 && IsElBartoEffect(player.buffType[i]))
                {
                    effectCount++;
                }
            }
            
            // Cap the bonus at 1.0 (100%)
            return Math.Min(effectCount * 0.1f, 1.0f);
        }
          private Color GetCycleColor(int phase)
        {
            switch(phase)
            {
                case 1:
                    return new Color(50, 255, 50); // Green for buff phase
                case 2:
                    return new Color(255, 255, 50); // Yellow for mixed phase
                default:
                    return new Color(255, 50, 50);  // Red for debuff phase
            }
        }
        
        private void ApplyRandomBuff(Player player)
        {
            int[] buffs = { 
                BuffID.Swiftness,      // +25% movement speed
                BuffID.Ironskin,       // +8 defense
                BuffID.Regeneration,   // Life regeneration
                BuffID.Shine,          // Light
                BuffID.NightOwl,       // Increased night vision
                BuffID.Battle,         // Increased spawn rate
                BuffID.Thorns,         // Reflects damage
                BuffID.WaterWalking,   // Walk on liquids
                BuffID.Archery,        // +20% arrow speed & damage
                BuffID.Hunter,         // Shows enemies
                BuffID.Endurance,      // -10% damage taken
                BuffID.Rage,           // +10% critical hit chance
                BuffID.Wrath           // +10% damage
            };
            
            int buffDuration = Main.rand.Next(300, 900); // 5-15 seconds
            int buffChoice = Main.rand.Next(buffs.Length);
            player.AddBuff(buffs[buffChoice], buffDuration);
            
            // Visual effect
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.GreenTorch, 0, -2f, 0, default, 1.5f);
            }
        }
        
        private void ApplyRandomDebuff(Player player, bool strongDebuffs)
        {
            // Mild debuffs for earlier phases
            int[] mildDebuffs = {
                BuffID.Darkness,     // Reduced vision
                BuffID.Slow,         // -20% movement speed
                BuffID.Weak,         // -50% damage
                BuffID.Chilled,      // -20% movement speed
                BuffID.BrokenArmor   // -20 defense
            };
            
            // Strong debuffs for final phase
            int[] strongDebuffList = {
                BuffID.Cursed,      // Can't use items
                BuffID.Silenced,    // Can't use magic
                BuffID.Confused     // Reversed controls
            };
            
            int[] debuffPool = strongDebuffs ? strongDebuffList : mildDebuffs;
            int debuffDuration = Main.rand.Next(180, 360); // 3-6 seconds
            
            // Strong debuffs have shorter duration
            if (strongDebuffs)
                debuffDuration = Main.rand.Next(120, 240); // 2-4 seconds
                
            int debuffChoice = Main.rand.Next(debuffPool.Length);
            player.AddBuff(debuffPool[debuffChoice], debuffDuration);
            
            // Visual effect
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.RedTorch, 0, -2f, 0, default, 1.5f);
            }
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            // Only allow equipping in the custom mode slot
            var modeSlotPlayer = player.GetModPlayer<ModeSlotPlayer>();
            return modded && slot == 0 && modeSlotPlayer != null && modeSlotPlayer.modeSlotItem.type == Item.type;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {            tooltips.Add(new TooltipLine(Mod, "ElBartoStat1", "+150 max life and mana"));
            tooltips.Add(new TooltipLine(Mod, "ElBartoStat2", "Automatically cycles through 3 phases"));
            tooltips.Add(new TooltipLine(Mod, "ElBartoStat3", "Phase 1 (Green): Mostly beneficial effects"));
            tooltips.Add(new TooltipLine(Mod, "ElBartoStat4", "Phase 2 (Yellow): Mixed effects"));
            tooltips.Add(new TooltipLine(Mod, "ElBartoStat5", "Phase 3 (Red): Mostly negative effects"));
            tooltips.Add(new TooltipLine(Mod, "ElBartoStat6", "Gain bonuses based on chaos level"));
            
            tooltips.Add(new TooltipLine(Mod, "ElBartoFlavor1", "The ultimate gamble: fortune and disaster in one slot!"));
            tooltips.Add(new TooltipLine(Mod, "ElBartoFlavor2", "Ride the chaos wave for power... if you can handle it."));
            tooltips.Add(new TooltipLine(Mod, "ElBartoFlavor3", "El Barto was here!"));
        }
    public override void AddRecipes()
		{
			// CreateRecipe() implicitly creates a recipe for this item (BromeMode)
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 1); // Requires 1 dirt block
			recipe.AddTile(TileID.WorkBenches); // Crafted at any workbench
			recipe.Register();
		}
    }
}