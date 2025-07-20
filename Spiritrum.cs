using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Localization;
using System.Collections.Generic;
using Spiritrum.Content.Global;
using System.IO;
using Terraria.ModLoader.IO;
using Spiritrum.Systems;
using Spiritrum.Config;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Bestiary;

namespace Spiritrum
{
    public class SpiritrumMod : Mod
    {
        internal static SpiritrumMod Instance;
        internal IdeologySlotUISystem IdeologySlotUI;
        internal ModeSlotUISystem ModeSlotUI;
        public static bool downedVoidHarbinger;
        
        public SpiritrumMod()
        {
            // Store instance reference immediately upon construction
            Instance = this;
        }

        public override void PostSetupContent()
        {
            // Register critical localization keys to prevent them from being removed
            try
            {
                RegisterLocalizationKeys();
            }
            catch (System.Exception ex)
            {
                Logger.Error("Failed to register localization keys: " + ex.Message);
                // Continue execution even if localization registration fails
            }
        }

        private void RegisterLocalizationKeys()
        {
            // Register armor set bonus keys
            string[] armorSets = { "SteelHelmet", "SteelBreastplate", "SteelLeggings",
                                 "AzuriteHat", "AzuritePlatemail", "AzuriteGreaves",
                                 "MintalHelmet", "MintalBreastplate", "MintalLeggings",
                                 "MorbiumMask", "MorbiumPlatemail", "MorbiumGreaves",
                                 "BromiumHelmet", "BromiumChestplate", "BromiumLeggings" };

            foreach (string armorPiece in armorSets)
            {
                // Register DisplayName
                Language.GetOrRegister($"Mods.{Name}.Items.{armorPiece}.DisplayName");
                // Register Tooltip
                Language.GetOrRegister($"Mods.{Name}.Items.{armorPiece}.Tooltip");
                // Register SetBonus
                Language.GetOrRegister($"Mods.{Name}.Items.{armorPiece}.SetBonus");
            }

            // Register other important item keys
            string[] importantItems = { 
                "TheGrimWraith", "Twinarang", "NamelessParasite", "FurryClaw", "SkullBow",
                "RoPro9000", "Googling", "GoogBlast", "TheMag", "BromeBlaster", "ElectromanBlade",
                "SnowyOwlStaff", "GoogBlaster", "IrisBlossomStaff", "CrystallineCrown", 
                "IrisCrossShield", "NamelessEmblem", "VoidEssence", "VoidResonator",
                "ShimmeringDust", "SpectralBrew", "VoidCatalyst", "BromiumStaff",
                "LaserBlaster", "MapleSyrupBurner", "Equinox", "BottledShimmer",
                "BromeMode", "GEoTMode", "TheyStoleEverythingFromUsMode"
            };

            foreach (string item in importantItems)
            {
                Language.GetOrRegister($"Mods.{Name}.Items.{item}.DisplayName");
                Language.GetOrRegister($"Mods.{Name}.Items.{item}.Tooltip");
                // Some items may have SetBonus
                Language.GetOrRegister($"Mods.{Name}.Items.{item}.SetBonus");
            }

            // Register buff localization
            string[] buffs = { 
                "NamelessParasiteBuff", "SkiDooBuff", "BulletEvidence", "CharmingEcho",
                "ElementalBlades", "ShimmeredDust", "SnowyOwlStaffBuff", "Spectral"
            };

            foreach (string buff in buffs)
            {
                Language.GetOrRegister($"Mods.{Name}.Buffs.{buff}.DisplayName");
                Language.GetOrRegister($"Mods.{Name}.Buffs.{buff}.Description");
            }

            // Register NPC localization
            string[] npcs = { "EvilGoog", "VoidHarbinger", "NamelessNPC" };
            foreach (string npc in npcs)
            {
                Language.GetOrRegister($"Mods.{Name}.NPCs.{npc}.DisplayName");
            }
        }

        public override void Unload()
        {
            Logger.Info("Spiritrum mod: Unloading...");
            
            // Reset boss flags
            downedVoidHarbinger = false;
            
            // Clear instance reference
            Instance = null;
        }

        internal void ReloadUIPositions()
        {
            if (!Main.dedServ)
            {
                // Only update if UI systems are initialized
                if (IdeologySlotUI?.ideologySlotUI != null)
                {
                    IdeologySlotUI.UpdatePositionsFromConfig();
                }
                
                if (ModeSlotUI?.modeSlotUI != null)
                {
                    ModeSlotUI.UpdatePositionsFromConfig();
                }
            }
        }
        
        public void RegisterUISystem(object uiSystem)
        {
            if (uiSystem is IdeologySlotUISystem ideologySystem)
            {
                IdeologySlotUI = ideologySystem;
            }
            else if (uiSystem is ModeSlotUISystem modeSystem)
            {
                ModeSlotUI = modeSystem;
            }
        }
        
        public bool GetDownedVoidHarbinger()
        {
            return downedVoidHarbinger;
        }

        public static string BestiaryFilterKey => "Mods.Spiritrum.BestiaryFilter.Spiritrum";
        public static string BestiaryIconPath => "Spiritrum/icon_small";

        public override void Load()
        {
            // Load the mod icon for UI and bestiary
            if (!Main.dedServ)
            {
                ModContent.Request<Texture2D>(BestiaryIconPath, AssetRequestMode.ImmediateLoad);
                // You can now use BestiaryFilterKey and BestiaryIconPath in your NPCs' SetBestiary methods
            }
        }
    }

    public class SpiritrunWorldData : ModSystem
    {
        public override void SaveWorldData(TagCompound tag)
        {
            // Save boss flags
            tag["downedVoidHarbinger"] = SpiritrumMod.downedVoidHarbinger;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            // Load boss flags
            SpiritrumMod.downedVoidHarbinger = tag.ContainsKey("downedVoidHarbinger") && tag.GetBool("downedVoidHarbinger");
        }
    }
}
