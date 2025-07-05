using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System.Configuration;
using Microsoft.Xna.Framework.Graphics;

namespace Spiritrum.Content.Items.Modes
{
    public class RiverMode : ModItem
    {


        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 26; // Hitbox width of the item.
            Item.height = 26; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.value = 1;
            Item.maxStack = 1;
            Item.accessory = true;
            Item.defense = -2140000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxFallSpeed = player.maxFallSpeed * 16f;
            player.luck -= 0.5f;
            player.extraAccessorySlots = 1;
            player.killGuide = true;
            player.statLifeMax2 = 220;
            player.statManaMax = 40;
            player.breathMax = 2;
            player.aggro = 2140000000;
            player.autoPaint = true;
            player.AddBuff(BuffID.Slow, 2);
            player.AddBuff(BuffID.Darkness, 2);
            player.AddBuff(BuffID.Bleeding, 2);
            player.AddBuff(BuffID.WaterCandle, 2);
            player.AddBuff(BuffID.ShadowCandle, 2);

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Here we add a tooltipline that will later be removed, showcasing how to remove tooltips from an item
            var line = new TooltipLine(Mod, "Face", "Activates River Mode when equipped");
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "I bet you would")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);

            line = new TooltipLine(Mod, "Face", "Changes? Wouldn't you like to know...")
            {
                OverrideColor = new Color(255, 255, 255)
            };
            tooltips.Add(line);



            // Here we will hide all tooltips whose title end with ':RemoveMe'
            // One like that is added at the start of this method
            foreach (var l in tooltips)
            {
                if (l.Name.EndsWith(":RemoveMe"))
                {
                    l.Hide();
                }
            }

            // Another method of hiding can be done if you want to hide just one line.
            // tooltips.FirstOrDefault(x => x.Mod == "ExampleMod" && x.Name == "Verbose:RemoveMe")?.Hide();
        }

        // Some movement effects are not suitable to be modified in ModItem.UpdateAccessory due to how the math is done.
        // ModPlayer.PostUpdateRunSpeeds is suitable for these modifications.
        public class ExampleStatBonusAccessoryPlayer : ModPlayer
        {
            public bool exampleStatBonusAccessory = false;

            public override void ResetEffects()
            {
                exampleStatBonusAccessory = false;
            }

            public override void PostUpdateRunSpeeds()
            {
                // We only want our additional changes to apply if ExampleStatBonusAccessory is equipped and not on a mount.
                if (Player.mount.Active || !exampleStatBonusAccessory)
                {
                    return;
                }

                // The following modifications are similar to Shadow Armor set bonus
                Player.runAcceleration *= 0.25f; // Modifies player run acceleration
                Player.maxRunSpeed *= 0.25f;
                Player.accRunSpeed *= 0.25f;
                Player.runSlowdown *= 0.25f;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.Register();
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            scale = 0.05f;

            Texture2D texture = Terraria.GameContent.TextureAssets.Item[Item.type].Value;
            Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height - texture.Height * 0.05f);

            spriteBatch.Draw(texture, position, null, lightColor, rotation, texture.Size() * 0.05f, scale, SpriteEffects.None, 0f);

            return false;
        }
    }
}
