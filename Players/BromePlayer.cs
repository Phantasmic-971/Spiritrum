using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System; // Needed for Random
using Spiritrum.Systems;
using Spiritrum.Content.Items.Modes;

namespace Spiritrum.Players
{
	public class BromeModePlayer : ModPlayer
	{
		// Timer for the chat messages, stored in frames
		public int bromemodeChatTimer = 0;

		// Array of possible chat messages
		private readonly string[] bromemodeMessages = new string[]
		{
			"LIVE BROME REACTION!!!",
			"CHAT, CLIP THAT!!!",
			"NO WAY HE JUST DID THAT!!!",
			"BEREFT SOULS IS SO PEAK!!!",
			"THIS IS THE BEST GAME EVER!!!",
			"I'M GOING BROME MODE!!!"
		};

		// Check if the player is wearing the BromeMode accessory in either a regular slot or the ModeSlot
		public bool HasBromeModeAccessory()
		{
			// Check regular accessory slots
			for (int i = 3; i < 8 + Player.extraAccessorySlots; i++)
			{
				if (Player.armor[i].type == ItemType<Content.Items.Modes.BromeMode>())
				{
					return true;
				}
			}
			// Check custom ModeSlot
			var modeSlotPlayer = Player.GetModPlayer<ModeSlotPlayer>();
			if (modeSlotPlayer != null && modeSlotPlayer.modeSlotItem.type == ItemType<Content.Items.Modes.BromeMode>())
			{
				return true;
			}
			return false;
		}

		// This hook runs every frame after the player's update methods
		public override void PostUpdateEquips()
		{
			// Only run the timer logic if the player is wearing the BromeMode accessory
			if (HasBromeModeAccessory())
			{
				bromemodeChatTimer++;

				// 5 seconds = 300 frames
				if (bromemodeChatTimer >= 300)
				{
					bromemodeChatTimer = 0; // Reset the timer

					// Select a random message
					Random rand = new Random();
					string message = bromemodeMessages[rand.Next(bromemodeMessages.Length)];

					// Display the message in the chat
					// Ensure this only happens on the local client in multiplayer to avoid spam
					if (Main.netMode != NetmodeID.Server || Player.whoAmI == Main.myPlayer)
					{
						Main.NewText(message, Color.White); // Use a color, e.g., White
					}
				}
			}
			else
			{
				// Reset the timer if the accessory is unequipped
				bromemodeChatTimer = 0;
			}
		}

		// This hook runs when the player takes damage
		public override void PostHurt(Player.HurtInfo info)
		{
			// Only apply the effect if the player is wearing the Brome accessory
			if (HasBromeModeAccessory())
			{
				// Apply Potion Sickness debuff for 5 seconds (5 * 60 frames)
				Player.AddBuff(BuffID.PotionSickness, 300);
			}
		}

		// Reset timer when the player dies or leaves the world
		public override void ResetEffects()
		{
			// This method is called when player effects are reset.
			// The logic in PostUpdateEquips already handles unequip, but this is a safe place
			// to ensure timer is reset if needed on other effect changes.
		}

		// This hook runs when the player spawns or loads
		// CORRECTED SIGNATURE for tModLoader 1.4.4+
		public override void OnRespawn()
		{
			// Reset timer on respawn
			bromemodeChatTimer = 0;
		}

		// This hook runs when the player leaves the world
		// CORRECTED SIGNATURE for tModLoader 1.4.4+
		public override void PlayerDisconnect()
		{
			// Reset timer on disconnect
			bromemodeChatTimer = 0;
		}
	}
}