using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Microsoft.Xna.Framework;
using Spiritrum.Content.Items.Materials; // Assuming Googling will be in this namespace
using Terraria.Audio; // Added for SoundEngine

namespace Spiritrum.Content.NPCS
{
    public class EvilGoog : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Zombie]; // Use Zombie's frame count
            //DisplayName.SetDefault("Evil Goog"); // Not needed for tModLoader 1.4.4+
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 25; // Weak damage
            NPC.defense = 6;  // Weak defense
            NPC.lifeMax = 45; // Weak health
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 25f; // Low value
            NPC.knockBackResist = 0.9f; // Slightly less knockback resistant than a zombie
            NPC.aiStyle = 3; // Use Zombie AI
            AIType = NPCID.Zombie; // Mimic Zombie behavior
            AnimationType = NPCID.Zombie; // Use Zombie's animation
            NPC.buffImmune[BuffID.Confused] = true;
            //Banner = Item.NPCtoBanner(NPCID.Zombie); // Consider creating a banner if desired
            //BannerItem = Item.BannerToItem(Banner);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Spawn at night, on surface, not in town, not during events like blood moon for now
            if (spawnInfo.Player.ZoneOverworldHeight && !Main.dayTime && !spawnInfo.PlayerInTown && !Main.eclipse && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon)
            {
                return 0.08f; // Adjust spawn chance as needed
            }
            return 0f;
        }

        public override void AI()
        {
            base.AI(); // Run the base AI (Zombie AI)
            // Randomly play sound
            if (Main.rand.NextBool(300)) // Adjust frequency as desired, 300 is roughly every 5 seconds
            {
                SoundEngine.PlaySound(new SoundStyle("Spiritrum/Sounds/EvilGoog") { Volume = 1.5f }, NPC.position);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Drop 1 Googling material
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Googling>(), 1, 1, 1)); // Always drops 1
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement("A mischievous creature of the night, drawn to the glow of screens and the allure of endless information. Or maybe it just wants to bite you.")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            // Add gore/dust effects on hit if desired
            if (NPC.life <= 0)
            {
                // Spawn gore on death
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Blood, 2.5f * hit.HitDirection, -2.5f, 0, default, 0.7f);
                }
                // Example of custom gore, if you have it
                // Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("EvilGoogGore1").Type, 1f);
                // Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("EvilGoogGore2").Type, 1f);
            }
        }
    }
}
