using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System.Collections.Generic;
using Spiritrum.Content.Projectiles;

namespace Spiritrum.Content.Items.Weapons
{
    public class TheGrimWraith : ModItem
    {
        // Combo system variables
        private int comboCounter = 0;
        private const int MaxCombo = 3;
        private int comboResetTimer = 0;
        private const int ComboResetTime = 60; // 1 second reset time
          // Animation variables
        private const int TotalFrames = 3; // Number of different attack animations
        private int currentFrame = 0;
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Grim Wraith");
            // Tooltip.SetDefault("'A blade forged from the very essence of darkness'\nGrows stronger with each consecutive strike\nEmits void energy on the third strike of a combo");
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            
            // Register item for use in Journey Mode's Research menu
            ItemID.Sets.ItemNoGravity[Type] = true; // Makes the item float slightly when dropped
        }

        public override void SetDefaults()
        {
            // Basic properties
            Item.width = 48;
            Item.height = 48;
            Item.rare = ItemRarityID.Yellow; // Post-Plantera rarity
            Item.value = Item.sellPrice(gold: 15);
            
            // Combat properties
            Item.DamageType = DamageClass.Melee;
            Item.damage = 150;
            Item.knockBack = 7f;
            Item.crit = 14;
            
            // Use properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 22; // Slightly slower for the custom animation
            Item.useTime = 22;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item71; // More ethereal sound that fits a void weapon
            
            // Appearance
            Item.scale = 2.5f; // Adjust scale to make the sprite more centered
            Item.noUseGraphic = false; // We'll use the item's texture, not a projectile
            Item.noMelee = false; // This weapon will hit enemies using its own hitbox
        }

        public override void UseItemFrame(Player player)
        {
            // Custom attack animation based on combo counter
            if (player.itemAnimation > 0)
            {
                // Calculate animation progress (0.0 to 1.0)
                float animationProgress = 1f - (float)player.itemAnimation / player.itemAnimationMax;
                
                // Determine which frame of attack animation to use based on combo counter
                int attackFrame = comboCounter % TotalFrames;
                
                // Different attack animations based on combo counter
                switch (attackFrame)
                {
                    case 0: // Basic horizontal slash
                        DefaultSwingAnimation(player, animationProgress);
                        break;
                    case 1: // Overhead slash
                        OverheadSwingAnimation(player, animationProgress);
                        break;
                    case 2: // Thrust attack
                        ThrustAnimation(player, animationProgress);
                        break;
                }
                
                // Add void effects that intensify with combo
                CreateVoidEffects(player, comboCounter);
            }
        }
        
        private void DefaultSwingAnimation(Player player, float progress)
        {
            // Standard horizontal swing with some custom positioning
            float rotation = MathHelper.ToRadians(-90f + 180f * progress); // -90° to 90° rotation
            
            if (player.direction == -1)
                rotation = MathHelper.ToRadians(90f - 180f * progress); // 90° to -90° for left facing
            
            // Adjust item position based on swing progress
            player.itemRotation = rotation;
            
            // The hand position adjustment
            float handOffset = MathHelper.Lerp(0.2f, -0.2f, progress);
            player.itemLocation = player.MountedCenter + new Vector2(player.direction * 10, handOffset * 20);
        }
        
        private void OverheadSwingAnimation(Player player, float progress)
        {
            // Overhead vertical slash (from top to bottom)
            float rotation;
            
            if (player.direction == 1)
                rotation = MathHelper.ToRadians(-135f + 180f * progress); // From above head to below
            else
                rotation = MathHelper.ToRadians(135f - 180f * progress); // Mirror for left facing
            
            player.itemRotation = rotation;
            
            // Move the item to create a more overhead swing feel
            float heightOffset = MathHelper.Lerp(-0.3f, 0.4f, progress);
            player.itemLocation = player.MountedCenter + new Vector2(player.direction * 6, heightOffset * 24);
        }
        
        private void ThrustAnimation(Player player, float progress)
        {
            // A thrusting attack (stab forward)
            float rotation;
            
            if (player.direction == 1)
                rotation = MathHelper.ToRadians(0); // Horizontal point forward
            else
                rotation = MathHelper.ToRadians(180); // Horizontal point forward (left)
                
            player.itemRotation = rotation;
            
            // Move the item forward during thrust
            float forwardOffset = MathHelper.SmoothStep(0f, 1f, progress < 0.5f ? progress * 2 : 2 - progress * 2);
            player.itemLocation = player.MountedCenter + new Vector2(player.direction * 10 * (1 + forwardOffset), 0);
        }
        
        private void CreateVoidEffects(Player player, int comboLevel)
        {
            // Create void-themed dust effects based on the combo level
            int dustCount = 1 + comboLevel * 2;
            
            for (int i = 0; i < dustCount; i++)
            {
                Vector2 itemPos = player.itemLocation + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10));
                
                // Purple dust for void effects
                Dust dust = Dust.NewDustDirect(
                    itemPos,
                    Item.width, Item.height,
                    DustID.PurpleTorch, 
                    0f, 0f, 
                    100, default, 
                    1f + comboLevel * 0.2f);
                
                dust.noGravity = true;
                dust.velocity = Vector2.Zero;
                dust.fadeIn = 1.2f;
            }
            
            // Enhanced light effect with combo
            float lightIntensity = 0.3f + comboLevel * 0.2f;
            Lighting.AddLight(player.Center, 0.1f * lightIntensity, 0.0f, 0.5f * lightIntensity);
        }

        public override void HoldItem(Player player)
        {
            // Emit void particles when held
            if (Main.rand.NextBool(20)) // 5% chance per frame
            {
                Dust dust = Dust.NewDustDirect(
                    player.position,
                    player.width,
                    player.height,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, 1.2f);
                dust.noGravity = true;
                dust.velocity *= 0.3f;
            }
            
            // Handle combo reset timer
            if (comboResetTimer > 0)
            {
                comboResetTimer--;
                if (comboResetTimer == 0)
                    comboCounter = 0;
            }
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            // Create void dust along the swing arc
            Vector2 position = new Vector2(
                hitbox.X + Main.rand.Next(hitbox.Width),
                hitbox.Y + Main.rand.Next(hitbox.Height));
                
            // More intense dust based on combo counter
            int dustCount = 1 + comboCounter;
            float dustScale = 1.0f + (comboCounter * 0.2f);
            
            for (int i = 0; i < dustCount; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    position,
                    2, 2,
                    DustID.PurpleTorch,
                    0f, 0f, 0, default, dustScale);
                dust.noGravity = true;
                dust.velocity = player.DirectionTo(position) * 2f;
            }
            
            // Add light based on combo counter
            float lightIntensity = 0.3f + (comboCounter * 0.2f);
            Lighting.AddLight(position, 0.3f * lightIntensity, 0.0f, 0.5f * lightIntensity);
        }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Increment combo counter
            comboCounter = Math.Min(comboCounter + 1, MaxCombo);
            comboResetTimer = ComboResetTime;
            
            // Special effect on third hit
            // Create void explosion (visual)
            for (int i = 0; i < 40; i++)
            {
                Vector2 velocity = Main.rand.NextVector2Circular(10f, 10f);
                Dust dust = Dust.NewDustDirect(
                    target.position,
                    target.width,
                    target.height,
                    DustID.PurpleTorch,
                    velocity.X, velocity.Y,
                    0, default, 2.0f);
                dust.noGravity = true;
            }
            // Play special sound
            SoundEngine.PlaySound(SoundID.Item103, target.position);
            // Spawn void explosion projectiles with independent i-frames
            if (Main.myPlayer == player.whoAmI)
            {
                int numProjectiles = 8;
                for (int i = 0; i < numProjectiles; i++)
                {
                    float rotation = MathHelper.TwoPi * i / numProjectiles;
                    Vector2 velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 8f;
                    int projectileType = ModContent.ProjectileType<VoidExplosionProjectile>();
                    int damage = (int)(Item.damage * 0.75f);
                    Projectile.NewProjectile(
                        player.GetSource_ItemUse(Item),
                        target.Center,
                        velocity,
                        projectileType,
                        damage,
                        Item.knockBack,
                        player.whoAmI
                    );
                }
            }
            // Reset combo after special attack
            comboCounter = 0;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Add special lore tooltip
            TooltipLine lore = new TooltipLine(Mod, "TheGrimWraithLore", "A scythe forged from the void essence of a powerful entity. Its blade seems to absorb light rather than reflect it.")
            {
                OverrideColor = new Color(180, 100, 255)
            };
            tooltips.Add(lore);
        }
    }
}
