using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Spiritrum.Content.NPCS;
using Spiritrum.Content.Items;

namespace Spiritrum.Content.Items.Consumables
{
    public class VoidCatalyst : ModItem
    {
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Void Catalyst");
        // Tooltip.SetDefault("Summons the Void Harbinger");
        
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; // This helps sort inventory, similar to other boss summon items
    }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(0, 1, 0, 0); // 1 gold
            Item.rare = ItemRarityID.LightPurple; // Tier 6
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp; // Similar to other boss summon items
            Item.consumable = true;
            Item.UseSound = SoundID.Item44; // Celestial sound effect
        }

    public override bool CanUseItem(Player player)
    {
        // Only in Corruption or Crimson
        var tileType = Main.tile[(int)(player.Center.X / 16), (int)(player.Center.Y / 16)].WallType;
        bool inCorruption = player.ZoneCorrupt;
        bool inCrimson = player.ZoneCrimson;
        if (!inCorruption && !inCrimson)
            return false;

        // Cannot be used if the boss is already present
        return !NPC.AnyNPCs(ModContent.NPCType<VoidHarbinger>());
    }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                // Create an effect to show the summoning
                for (int i = 0; i < 50; i++)
                {
                    Vector2 position = player.Center + Main.rand.NextVector2Circular(50f, 50f);
                    Vector2 velocity = (position - player.Center) * 0.03f;
                    Dust.NewDustPerfect(position, DustID.PurpleTorch, velocity, 0, default, 1.5f);
                }
                
                // Play a sound effect for the summoning
                SoundEngine.PlaySound(SoundID.Roar, player.position);
                
                // Spawn the boss above the player
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<VoidHarbinger>());
                }                else
                {
                    // If in multiplayer, request to spawn the boss via the server
                    // Using SendData for SyncNPC, which is what we need for boss spawning
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, player.whoAmI, ModContent.NPCType<VoidHarbinger>());
                }
            }
            
            return true;
        }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.SoulofNight, 5) // Requires 5 Souls of Night
            .AddIngredient(ItemID.SoulofLight, 5) // Requires 5 Souls of Light
            .AddIngredient(ItemID.CrystalShard, 15) // Requires 15 Crystal Shards
            .AddIngredient(ItemID.BeetleHusk, 5)
            .AddIngredient(ItemID.FragmentSolar, 3) // Lunar Fragment (Solar)
            .AddIngredient(ItemID.FragmentVortex, 3) // Lunar Fragment (Vortex)
            .AddIngredient(ItemID.FragmentNebula, 3) // Lunar Fragment (Nebula)
            .AddIngredient(ItemID.FragmentStardust, 3) // Lunar Fragment (Stardust)
            .AddTile(TileID.LunarCraftingStation) // Crafted at Ancient Manipulator
            .Register();
    }
    }
}
