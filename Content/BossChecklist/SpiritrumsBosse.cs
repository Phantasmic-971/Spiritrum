using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.ItemDropRules;
using Spiritrum;
using Spiritrum.Content.NPCS;
using Spiritrum.Content.Items;
using Spiritrum.Content.Items.Placeables;
using Spiritrum.Content.Items.Consumables;

namespace Spiritrum.Content.BossChecklist
{
    public class SpiritrumsBosses : ModSystem
    {
        // The vanilla bosses have values like 1, 2, 3, etc.
        // Boss Checklist itself uses increments of 0.1, implying that its bosses should have progression values like 1.5, 3.3, etc.
        // Most modders use increments of 0.01, making values like 2.06, 7.4, etc. 
        
        // The Void Harbinger is intended to be fought after the mechanical bosses (Destroyer, Twins, Skeletron Prime)
        // But before Plantera, so somewhere around the 7.5 mark
        private const float VoidHarbingerBossValue = 7.5f;
        
        public override void PostSetupContent()
        {
            // Boss Checklist Integration
            // Check if Boss Checklist mod is loaded
            if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
                return;

            // Register the Void Harbinger
            AddVoidHarbingerToBossChecklist(bossChecklist);
        }

        private void AddVoidHarbingerToBossChecklist(Mod bossChecklist)
        {
            // Boss Checklist API integration
            bossChecklist.Call(
                "AddBoss", // Method
                VoidHarbingerBossValue, // Boss progression value
                ModContent.NPCType<VoidHarbinger>(), // NPC type reference                this, // ModSystem reference
                "Void Harbinger", // Boss name
                (Func<bool>)(() => ModContent.GetInstance<Spiritrum.SpiritrumMod>().GetDownedVoidHarbinger()), // Using method via ModContent
                ModContent.ItemType<VoidCatalyst>(), // Spawn item// Collection of boss drops
                new List<int> {
                    ModContent.ItemType<Items.Weapons.VoidResonator>(),
                    ModContent.ItemType<Content.Items.Materials.VoidEssence>(),
                    ModContent.ItemType<Items.Placeables.VoidHarbingerTrophy>()
                },
                
                // Custom boss head texture
                "Spiritrum/Assets/Textures/UI/BossHeads/VoidHarbingerBossHead",
                
                // Boss spawn message
                "Use the [i:" + ModContent.ItemType<VoidCatalyst>() + "] at night",
                
                // Custom boss painting texture
                "Spiritrum/Assets/Textures/UI/BossPaintings/VoidHarbingerPainting",
                
                // Custom lore text
                "A cosmic entity from the void between dimensions, the Void Harbinger seeks to drain our world of its life energy. Summoned using the [i:" + ModContent.ItemType<VoidCatalyst>() + "] only at night."
            );
        }
    }
}
