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
           Main.npcFrameCount[NPC.type] = 28;
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
            return Main.rand.Next(8) switch
            {
                0 => "I wonder if my father will arrive some day. He is the [UNKNOWN DATA]",
                1 => "There is this weird story that happened before my arrival. I do wonder what was it.",
                2 => "You don't know about the KAS council? They are the 3 creators. Konetrum, Anlatrum and Spiritrum.",
                3 => "Wrath of the Gods? Do you mean Wrath of the Creators?",
                4 => "Some may say, my father is a nameless deity, but he is something else.",
                5 => "Did you know, that before you came to this world, it was a total calamity.",
                6 => "Shimmer is a very powerful tool that I need access.",
                _ => "This dimension is the best out of all 11 as most say, but I am not sure."
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
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                // Use boss category instead of biome categories               // It's a boss
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
                // Updated flavor text to reflect boss status
                new FlavorTextBestiaryInfoElement("No one knows from where he comes from... except him and himself, but doesn't want to tell.")
            });
        }
    }
}