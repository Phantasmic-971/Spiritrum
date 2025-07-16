using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using Terraria.Audio;

namespace Spiritrum.Content.Items.Consumables
{
    public class RoyalFrozenGem : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; // This helps sort it in the inventory with other boss spawns
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine useLine = new TooltipLine(Mod, "RoyalFrozenGemUse", "Summons the Frost Emperor and Frost Empress")
            {
                IsModifier = false
            };
            tooltips.Add(useLine);

            TooltipLine loreLine = new TooltipLine(Mod, "RoyalFrozenGemLore", "'A frozen heart that beats with eternal love and sorrow'")
            {
                IsModifier = false
            };
            tooltips.Add(loreLine);
        }

        public override bool CanUseItem(Player player)
        {
            // Can only be used in the Snow biome, after Golem is defeated, and not when the bosses are already alive
            bool golemDefeated = NPC.downedGolemBoss;
            if (!golemDefeated)
            {
                if (Main.myPlayer == player.whoAmI)
                {
                    Main.NewText("The Frost Empire doesn't want to declare war on the 2nd dimension while an ancient construct remains", 100, 200, 255);
                }
                return false;
            }
            return player.ZoneSnow && !NPC.AnyNPCs(NPCType<Spiritrum.Content.NPCS.FrostEmperor>()) && !NPC.AnyNPCs(NPCType<Spiritrum.Content.NPCS.FrostEmpress>()) && !NPC.AnyNPCs(NPCType<Spiritrum.Content.NPCS.FrostEmpire>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                // Spawn both bosses near the player
                SoundEngine.PlaySound(SoundID.Roar, player.position);
                
                int type1 = NPCType<Spiritrum.Content.NPCS.FrostEmperor>();
                int type2 = NPCType<Spiritrum.Content.NPCS.FrostEmpress>();
                
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // Spawn them slightly apart from each other
                    NPC.SpawnOnPlayer(player.whoAmI, type1);
                    NPC.SpawnOnPlayer(player.whoAmI, type2);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type1);
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type2);
                }
                return true;
            }
            return false;
        }
    }
}
