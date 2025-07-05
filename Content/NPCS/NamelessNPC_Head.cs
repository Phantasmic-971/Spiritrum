using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Spiritrum.Content.Items.Accessories;                  // EchoCharm, NamelessEmblem
using Spiritrum.Content.Items.Consumables;      // ShimmeringDust
using Spiritrum.Content.Items.Weapons;
using Spiritrum.Content.Items.Materials;          // WhisperingBlade
using Spiritrum.Content.Projectiles;  
using Spiritrum.Content.Items.Other;        // WhisperingBladeProjectile
using Terraria.Localization; // Needed for Language.GetTextValue
using Microsoft.Xna.Framework; // Needed if you add custom buttons with textures

namespace Spiritrum.Content.NPCS
{
    [AutoloadHead]
    public class NamelessNPC : ModNPC
    {
        // ... (Keep your SetStaticDefaults and SetDefaults methods as they are) ...
        public override void SetStaticDefaults()
        {
           Main.npcFrameCount[NPC.type] = 25;
           // Consider adding display name and head map data here if not done elsewhere
           // DisplayName.SetDefault("Nameless One"); // Example display name
           // NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0) { Velocity = 1f };
           // NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
           NPC.townNPC       = true;
           NPC.friendly      = true;
           NPC.width         = 18;
           NPC.height        = 40;
           NPC.aiStyle       = 7; // Use Guide's AI
           AnimationType     = NPCID.Guide;
           NPC.lifeMax       = 250;
           NPC.defense       = 15;
           NPC.damage        = 10;
           NPC.knockBackResist= 0.5f;
           NPC.HitSound      = SoundID.NPCHit1;
           NPC.DeathSound    = SoundID.NPCDeath1;
           // Add town NPC profile if desired
           // TownNPCProfile = new Profiles.NamelessNPCProfile();
        }


        public override void AddShops()
        {
            var shop = new NPCShop(Type, "Shop"); // "Shop" is the internal name

            // Echo Charm — after Skeletron
            shop.Add<EchoCharm>(
                new Condition("Conditions.DownedSkeletron", () => NPC.downedBoss3)
            );

            // Shimmering Dust — Hardmode + in Aether (shimmer) zone
            shop.Add<ShimmeringDust>(Condition.InAether);

            // Nameless Emblem — any Mechanical Boss defeated
            shop.Add<NamelessEmblem>(
                new Condition("Conditions.DownedAnyMechBoss", () => NPC.downedMechBossAny)
            );

            // Whispering Blade — after Plantera
            shop.Add<WhisperingBlade>(
                new Condition("Conditions.DownedPlantera", () => NPC.downedPlantBoss)
            );

            // Whispering Blade — after Plantera
            shop.Add<BrokenBlaster>(
                new Condition("Conditions.DownedPlantera", () => NPC.downedPlantBoss)
            );

            // Phantasmo — after Eye of Cthulhu defeated
            shop.Add<Phantasmo>(
                new Condition("Conditions.DownedEyeOfCthulhu", () => NPC.downedBoss1)
            ); // 10 gold (100000 copper)

            // Echoing Lancer — after Skeletron
            shop.Add<EchoingLancer>(
                new Condition("DownedSkeletron", () => NPC.downedBoss3)
            );

            // Nameless Codex — Hardmode + Aether
            shop.Add<NamelessCodex>(
                new Condition("HardmodeAether", () => Main.hardMode && Main.LocalPlayer.ZoneShimmer)
            );

            // Anthrax — after Plantera
            shop.Add<Anthrax>(
                new Condition("Conditions.DownedPlantera", () => NPC.downedPlantBoss)
            );

            shop.Register();
        }

        public override string GetChat()
        {
            return Main.rand.Next(3) switch
            {
                0 => "Greetings. I deal in things beyond your wildest imaginings.",
                1 => "My wares change as the world does.",
                _ => "Power lies in the shadows—and I have plenty."
            };
        }

        // --- ADD THIS METHOD ---
        public override void SetChatButtons(ref string button, ref string button2)
        {
            // This adds the "Shop" button text.
            button = Language.GetTextValue("LegacyInterface.28"); // Standard "Shop" text
            // button2 can be used for a second button if needed.
        }
        // --- ADD THIS METHOD ---


        // --- AND ADD THIS METHOD ---
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                // This refers back to the name you gave the shop in AddShops()
                shopName = "Shop";
            }
        }
        // --- AND ADD THIS METHOD ---


        public override bool CanTownNPCSpawn(int numTownNPCs) => true;

        // Optional: Add other Town NPC methods like TownNPCName, CanGoToStatue, etc.
        // public override string TownNPCName() => "Nameless One"; // Example

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 38; // Echoing Lancer base damage
            knockback = 2.5f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30; // Increase cooldown to avoid spamming
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<EchoingLancerArrow>();
            attackDelay = 10; // Give a small delay to allow animation
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 1.1f; // Slightly faster than default
            gravityCorrection = 0f;
            randomOffset = 0f;
        }
    }
}