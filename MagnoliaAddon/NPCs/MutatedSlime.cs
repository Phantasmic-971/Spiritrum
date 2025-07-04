using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Spiritrum.MagnoliaAddon.Items;
using Spiritrum.MagnoliaAddon.Items.Placeables.Banners;

namespace Spiritrum.MagnoliaAddon.NPCs
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class MutatedSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BloodZombie];

            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.KingSlime;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 46;
            NPC.damage = 62;
            NPC.defense = 10;
            NPC.lifeMax = 274;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 500f;
            NPC.knockBackResist = 0.7f;
            NPC.aiStyle = 3; // slime ai

            AIType = NPCID.Mummy; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
            AnimationType = NPCID.BloodZombie; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
            Banner = Type;
            BannerItem = ModContent.ItemType<MutatedSlimeBanner>();

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Since Party Zombie is essentially just another variation of Zombie, we'd like to mimic the Zombie drops.
            // To do this, we can either (1) copy the drops from the Zombie directly or (2) just recreate the drops in our code.
            // (1) Copying the drops directly means that if Terraria updates and changes the Zombie drops, your ModNPC will also inherit the changes automatically.
            // (2) Recreating the drops can give you more control if desired but requires consulting the wiki, bestiary, or source code and then writing drop code.

            // (1) This example shows copying the drops directly. For consistency and mod compatibility, we suggest using the smallest positive NPCID when dealing with npcs with many variants and shared drop pools.
            var zombieDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.GreenSlime, false); // false is important here
            foreach (var zombieDropRule in zombieDropRules)
            {
                // In this foreach loop, we simple add each drop to the PartyZombie drop pool. 
                npcLoot.Add(zombieDropRule);
            }

            // (2) This example shows recreating the drops. This code is commented out because we are using the previous method instead.
            // npcLoot.Add(ItemDropRule.Common(ItemID.Shackle, 50)); // Drop shackles with a 1 out of 50 chance.
            // npcLoot.Add(ItemDropRule.Common(ItemID.ZombieArm, 250)); // Drop zombie arm with a 1 out of 250 chance.

            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1));
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode) return SpawnCondition.OverworldDaySlime.Chance * 0.4f;
            return 0f;
        }
    }
}
