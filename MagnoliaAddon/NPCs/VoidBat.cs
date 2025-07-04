using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using System.Linq;
using Spiritrum.MagnoliaAddon.Items.Other;
using Spiritrum.MagnoliaAddon.Items.Placeables.Banners;

namespace Spiritrum.MagnoliaAddon.NPCs
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class VoidBat : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.CaveBat];

            NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.CaveBat;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 24;
            NPC.damage = 62;
            NPC.defense = 32;
            NPC.lifeMax = 435;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath4;
            NPC.value = 500f;
            NPC.knockBackResist = 0.7f;
            NPC.aiStyle = 14; // bat ai

            AIType = NPCID.CaveBat; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
            AnimationType = NPCID.CaveBat; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
            Banner = Type;
            BannerItem = ModContent.ItemType<VoidBatBanner>();

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Since Party Zombie is essentially just another variation of Zombie, we'd like to mimic the Zombie drops.
            // To do this, we can either (1) copy the drops from the Zombie directly or (2) just recreate the drops in our code.
            // (1) Copying the drops directly means that if Terraria updates and changes the Zombie drops, your ModNPC will also inherit the changes automatically.
            // (2) Recreating the drops can give you more control if desired but requires consulting the wiki, bestiary, or source code and then writing drop code.

            // (1) This example shows copying the drops directly. For consistency and mod compatibility, we suggest using the smallest positive NPCID when dealing with npcs with many variants and shared drop pools.
            var zombieDropRules = Main.ItemDropsDB.GetRulesForNPCID(NPCID.CaveBat, false); // false is important here
            foreach (var zombieDropRule in zombieDropRules)
            {
                // In this foreach loop, we simple add each drop to the PartyZombie drop pool. 
                npcLoot.Add(zombieDropRule);
            }

            // (2) This example shows recreating the drops. This code is commented out because we are using the previous method instead.
            // npcLoot.Add(ItemDropRule.Common(ItemID.Shackle, 50)); // Drop shackles with a 1 out of 50 chance.
            // npcLoot.Add(ItemDropRule.Common(ItemID.ZombieArm, 250)); // Drop zombie arm with a 1 out of 250 chance.

            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentNebula, 4, 2, 5)); // Drop zombie arm with a 1 out of 250 chance.
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentSolar, 4, 2, 5)); // Drop zombie arm with a 1 out of 250 chance.
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentStardust, 4, 2, 5)); // Drop zombie arm with a 1 out of 250 chance.
            npcLoot.Add(ItemDropRule.Common(ItemID.FragmentVortex, 4, 2, 5)); // Drop zombie arm with a 1 out of 250 chance.
        }



        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            //If any player is underground and has an example item in their inventory, the example bone merchant will have a slight chance to spawn.
            if (spawnInfo.Player.inventory.Any(item => item.type == ModContent.ItemType<AlienThornBall>()))
            {
                return 0.69f;
            }

            //Else, the example bone merchant will not spawn if the above conditions are not met.
            return 0f;


        }

    }
}
