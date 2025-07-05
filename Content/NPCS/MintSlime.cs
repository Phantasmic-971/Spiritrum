using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Spiritrum.Content.Items.Placeables;
using Spiritrum.Content.Items.Placeables.Banners;

namespace Spiritrum.Content.NPCS
{
    /// <summary>
    /// Mint Slime - A hallow variant slime that drops Mintal Ore
    /// </summary>
    public class MintSlime : ModNPC
    {
        #region Initialization
        
        /// <summary>
        /// Sets the static defaults for this NPC
        /// </summary>
        public override void SetStaticDefaults()
        {
            // Animation frames
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];

            // Shimmer transformation
            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.ShimmerSlime;

            // Bestiary display settings
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { 
                Velocity = 1f // Draws the NPC in the bestiary as if it's moving +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        /// <summary>
        /// Sets the defaults for this NPC instance
        /// </summary>
        public override void SetDefaults()
        {
            // Dimensions
            NPC.width = 34;
            NPC.height = 24;
            
            // Combat stats
            NPC.damage = 55;
            NPC.defense = 25;
            NPC.lifeMax = 170;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 500f;
            NPC.knockBackResist = 0.7f;
            
            // AI behavior
            NPC.aiStyle = 1; // Slime AI
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.GreenSlime;
            
            // Banner
            Banner = Type;
            BannerItem = ModContent.ItemType<MintSlimeBanner>();
        }
        
        #endregion
        
        #region Loot

        /// <summary>
        /// Modifies the loot dropped by this NPC
        /// </summary>
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Inherit Green Slime drops
            var slimeDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.GreenSlime, false);
            foreach (var slimeDropRule in slimeDropRules)
            {
                npcLoot.Add(slimeDropRule);
            }

            // Add unique drop - Mintal Ore (9-23)
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MintalOre>(), 1, 9, 23));
        }
        
        #endregion
        
        #region Spawning

        /// <summary>
        /// Determines the chance of this NPC spawning
        /// </summary>
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Spawns in the Hallow biome with increased chance
            return SpawnCondition.OverworldHallow.Chance * 1.2f;
        }
        
        #endregion
    }
}
