using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.Items.Other;
using Spiritrum.Content.Items.Placeables;
using Spiritrum.Content.Items.Weapons;
using Spiritrum.Content.Items.Materials;

namespace Spiritrum.Content.Global
{


    public class ExampleNPCLoot : GlobalNPC
    {
        // ModifyNPCLoot uses a unique system called the ItemDropDatabase, which has many different rules for many different drop use cases.
        // Here we go through all of them, and how they can be used.
        // There are tons of other examples in vanilla! In a decompiled vanilla build, GameContent/ItemDropRules/ItemDropDatabase adds item drops to every single vanilla NPC, which can be a good resource.

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Demon)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireDiamond>(), 4, 2, 7));

            }
            if (npc.type == NPCID.FireImp)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireDiamond>(), 4, 2, 7));

            }
            if (npc.type == NPCID.Hellbat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireDiamond>(), 4, 2, 7));

            }
            if (npc.type == NPCID.LavaSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireDiamond>(), 4, 2, 7));

            }
            if (npc.type == NPCID.Lavabat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireDiamond>(), 4, 2, 7));

            }
            if (npc.type == NPCID.BoneSerpentHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireDiamond>(), 4, 2, 7));

            }
            if (npc.type == NPCID.RedDevil)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireDiamond>(), 4, 2, 7));

            }
            if (npc.type == NPCID.VoodooDemon)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FireDiamond>(), chanceDenominator: 4, 2, 7));

            }
            if (npc.type == NPCID.Harpy)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarGem>(), chanceDenominator: 3, 1, 4));

            }
            if (npc.type == NPCID.WyvernBody)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarGem>(), chanceDenominator: 2, 5, 12));

            }
            if (npc.type == NPCID.Zombie)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarGem>(), chanceDenominator: 10, 3, 5));

            }
            if (npc.type == NPCID.DemonEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LunarGem>(), chanceDenominator: 10, 3, 5));

            }
            if (npc.type == NPCID.UndeadMiner)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.AleThrowingGlove, 5));
            }

            if (npc.type == NPCID.PossessedArmor)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SteelBar>(), chanceDenominator: 5, 1, 3));

            }
            if (npc.type == NPCID.Corruptor)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.FloatyGross)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.GiantBat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.GiantFlyingFox)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.Gastropod)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.IlluminantBat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.IceElemental)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.Lavabat)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.MossHornet)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.Pixie)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.RedDevil)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.Wraith)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));


            }
            if (npc.type == NPCID.WanderingEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfHeight>(), chanceDenominator: 3));

            }
            if (npc.type == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AlienThornBall>(), chanceDenominator: 1));

            }

            if (npc.type == NPCID.DarkCaster)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.WaterBolt, 50));
            }

            {

            }
            if (npc.type == NPCID.BlackSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CoalLump>(), 1, 2, 7));

            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Lens, 1, 1, 5));
            }

            if (npc.type == NPCID.SandSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.SandBlock, 1, 1, 2));
            }

            if (npc.type == NPCID.AngryNimbus)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.RainCloud, 3, 20, 35));
            }
            if (npc.type == NPCID.IceMimic)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.SnowGlobe, 3));
            }

            if (npc.type == NPCID.IceSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.SnowBlock, 1, 1, 2));
            }

            if (npc.type == NPCID.MeteorHead)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 10, 2, 5));
            }

            if (npc.type == NPCID.SpikedIceSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.IceBlock, 1, 1, 2));
            }

            if (npc.type == NPCID.CrimsonAxe)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.BloodLustCluster, 6));
            }

            if (npc.type == NPCID.IceSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
            }

            if (npc.type == NPCID.ArmedZombieEskimo)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
            }

            if (npc.type == NPCID.ZombieEskimo)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
            }

            if (npc.type == NPCID.IceElemental)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
            }

            if (npc.type == NPCID.Wolf)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
            }

            if (npc.type == NPCID.IceGolem)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.HandWarmer, 100));
            }

            if (npc.type == NPCID.TwiggyZombie)
            {

            }

            if (npc.type == NPCID.FlyingFish)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.SharkFin, 66));
            }

            if (npc.type == NPCID.Angler)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.MagicConch, 1));
            }

            if (npc.type == NPCID.HoppinJack)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Pumpkin, 4, 12, 28));
            }

            if (npc.type == NPCID.HoppinJack)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 3, 7));
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.FragmentNebula, 4, 6, 30));
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.FragmentSolar, 4, 6, 30));
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.FragmentStardust, 4, 6, 30));
            }

            if (npc.type == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.FragmentVortex, 4, 6, 30));
            }

            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.BlackLens, 5));
            }
            {
            }
            if (npc.type == NPCID.RuneWizard)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RunicCodex>(), 1));
            }
            if (npc.type == NPCID.BlueArmoredBones)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorbiumBar>(), 2, 1, 4));
            }
            if (npc.type == NPCID.BlueArmoredBonesMace)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorbiumBar>(), 2, 1, 4));
            }
            if (npc.type == NPCID.BlueArmoredBonesNoPants)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorbiumBar>(), 2, 1, 4));
            }
            if (npc.type == NPCID.BlueArmoredBonesSword)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorbiumBar>(), 2, 1, 4));
            }
            if (npc.type == NPCID.SkeletonSniper)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorbiumBar>(), 2, 1, 4));
            }
            if (npc.type == NPCID.TacticalSkeleton)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MorbiumBar>(), 2, 1, 4));
            }

        }
    }
}