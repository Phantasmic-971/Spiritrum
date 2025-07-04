using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Spiritrum.MagnoliaAddon.Global
{
    public class MagnoliaRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.SkyFracture);
            recipe.AddIngredient(ItemID.MagicMissile, 1);
            recipe.AddIngredient(ModContent.ItemType<Items.Other.LunarGem>(), 4);
            recipe.AddIngredient(ModContent.ItemType<Items.Other.SoulOfHeight>(), 16);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            Recipe A162 = Recipe.Create(ItemID.Gel, 50);
            A162.AddIngredient(ItemID.SlimeHook, 1);
            A162.Register();

            
            Recipe A25 = Recipe.Create(ItemID.Flamarang);
            A25.AddIngredient<Items.Other.FireDiamond>(8);
            A25.AddIngredient(ItemID.EnchantedBoomerang, 1);
            A25.AddTile(TileID.Anvils);
            A25.Register();

            Recipe A26 = Recipe.Create(ItemID.PhoenixBlaster);
            A26.AddIngredient<Items.Other.FireDiamond>(8);
            A26.AddIngredient(ItemID.Handgun, 1);
            A26.AddTile(TileID.Anvils);
            A26.Register();

            Recipe A27 = Recipe.Create(ItemID.RainbowRod);
            A27.AddIngredient(ItemID.CrystalShard, 10);
            A27.AddIngredient(ItemID.UnicornHorn, 2);
            A27.AddIngredient(ItemID.PixieDust, 10);
            A27.AddIngredient(ItemID.SoulofLight, 8);
            A27.AddIngredient(ItemID.RainbowBrick, 25);
            A27.AddTile(TileID.Anvils);
            A27.Register();

            Recipe A28 = Recipe.Create(ItemID.Mushroom);
            A28.AddIngredient(ItemID.GlowingMushroom, 1);
            A28.Register();

            Recipe A29 = Recipe.Create(ItemID.Present);
            A29.AddIngredient(ItemID.GoodieBag, 1);
            A29.Register();

            Recipe A30 = Recipe.Create(ItemID.GoodieBag);
            A30.AddIngredient(ItemID.Present, 1);
            A30.Register();

            Recipe A31 = Recipe.Create(ItemID.ImpStaff);
            A31.AddIngredient<Items.Other.FireDiamond>(15);
            A31.AddTile(TileID.Anvils);
            A31.Register();

            Recipe A32 = Recipe.Create(ItemID.FlameWakerBoots);
            A32.AddIngredient(ItemID.SpectreBoots, 1);
            A32.AddIngredient<Items.Other.FireDiamond>(5);
            A32.AddTile(TileID.TinkerersWorkbench);
            A32.Register();

            Recipe A33 = Recipe.Create(ItemID.CloudinaBottle);
            A33.AddIngredient(ItemID.Bottle, 1);
            A33.AddIngredient(ItemID.Cloud, 15);
            A33.AddTile(TileID.Anvils);
            A33.Register();

            Recipe A34 = Recipe.Create(ItemID.SailfishBoots);
            A34.AddIngredient(ItemID.BottledWater, 1);
            A34.AddIngredient(ItemID.HermesBoots, 1);
            A34.AddTile(TileID.Anvils);
            A34.Register();

            Recipe A35 = Recipe.Create(ItemID.FlurryBoots);
            A35.AddIngredient(ItemID.SnowBlock, 15);
            A35.AddIngredient(ItemID.HermesBoots, 1);
            A35.AddTile(TileID.Anvils);
            A35.Register();

            Recipe A36 = Recipe.Create(ItemID.SailfishBoots);
            A36.AddIngredient(ItemID.WaterBucket, 1);
            A36.AddIngredient(ItemID.HermesBoots, 1);
            A36.AddTile(TileID.Anvils);
            A36.Register();

            Recipe A37 = Recipe.Create(ItemID.BandofRegeneration);
            A37.AddIngredient(ItemID.LifeCrystal, 1);
            A37.AddIngredient(ItemID.GoldBar, 4);
            A37.AddTile(TileID.Anvils);
            A37.Register();

            Recipe A38 = Recipe.Create(ItemID.BandofRegeneration);
            A38.AddIngredient(ItemID.LifeCrystal, 1);
            A38.AddIngredient(ItemID.PlatinumBar, 4);
            A38.AddTile(TileID.Anvils);
            A38.Register();

            Recipe A39 = Recipe.Create(ItemID.BandofStarpower);
            A39.AddIngredient(ItemID.ManaCrystal, 1);
            A39.AddIngredient<Items.Other.LunarGem>(3);
            A39.AddIngredient(ItemID.SilverBar, 4);
            A39.AddTile(TileID.Anvils);
            A39.Register();


            Recipe A163 = Recipe.Create(ItemID.Bone, 40);
            A163.AddIngredient(ItemID.SkeletronHand, 1);
            A163.Register();

            Recipe A164 = Recipe.Create(ItemID.CandyCaneBlock, 75);
            A164.AddIngredient(ItemID.CandyCaneHook, 1);
            A164.Register();

            Recipe A165 = Recipe.Create(ItemID.GreenCandyCaneBlock, 75);
            A165.AddIngredient(ItemID.CandyCaneHook, 1);
            A165.Register();

            Recipe A166 = Recipe.Create(ItemID.SoulofLight, 25);
            A166.AddIngredient(ItemID.IlluminantHook, 1);
            A166.Register();

            Recipe A167 = Recipe.Create(ItemID.SoulofNight, 20);
            A167.AddIngredient(ItemID.WormHook, 1);
            A167.Register();

            Recipe A168 = Recipe.Create(ItemID.SoulofNight, 20);
            A168.AddIngredient(ItemID.TendonHook, 1);
            A168.Register();

            Recipe A169 = Recipe.Create(ItemID.GlowingMushroom, 500);
            A169.AddIngredient(ItemID.Hammush, 1);
            A169.Register();

            Recipe A170 = Recipe.Create(ItemID.Shroomerang);
            A170.AddIngredient(ItemID.EnchantedBoomerang, 1);
            A170.AddIngredient(ItemID.GlowingMushroom, 100);
            A170.AddTile(TileID.Anvils);
            A170.Register();

            Recipe A171 = Recipe.Create(ItemID.IceBoomerang);
            A171.AddIngredient(ItemID.EnchantedBoomerang, 1);
            A171.AddIngredient(ItemID.IceBlock, 50);
            A171.AddTile(TileID.Anvils);
            A171.Register();

            Recipe A172 = Recipe.Create(ItemID.LivingFireBlock);
            A172.AddIngredient(ItemID.Torch, 25);
            A172.AddIngredient<Items.Other.FireDiamond>(1);
            A172.Register();

            Recipe A173 = Recipe.Create(ItemID.BoneWand);
            A173.AddIngredient(ItemID.Bone, 25);
            A173.AddTile(TileID.BoneWelder);
            A173.Register();

            Recipe A174 = Recipe.Create(ItemID.Lemon);
            A174.AddRecipeGroup("Fruit");
            A174.Register();

            Recipe A175 = Recipe.Create(ItemID.Mace);
            A175.AddIngredient(ItemID.IronBar, 8);
            A175.AddTile(TileID.Anvils);
            A175.Register();

            Recipe A176 = Recipe.Create(ItemID.Starfury);
            A176.AddIngredient<Items.Other.LunarGem>(4);
            A176.AddIngredient(ItemID.GoldBar, 8);
            A176.AddIngredient(ItemID.FallenStar, 2);
            A176.AddTile(TileID.Anvils);
            A176.Register();

            Recipe A177 = Recipe.Create(ItemID.Starfury);
            A177.AddIngredient<Items.Other.LunarGem>(4);
            A177.AddIngredient(ItemID.PlatinumBar, 8);
            A177.AddIngredient(ItemID.FallenStar, 2);
            A177.AddTile(TileID.Anvils);
            A177.Register();

            Recipe A178 = Recipe.Create(ItemID.EnchantedBoomerang);
            A178.AddIngredient<Items.Other.LunarGem>(2);
            A178.AddIngredient(ItemID.FallenStar, 1);
            A178.AddIngredient(ItemID.WoodenBoomerang, 1);
            A178.Register();

            Recipe A179 = Recipe.Create(ItemID.EnchantedNightcrawler, 2);
            A179.AddIngredient<Items.Other.LunarGem>(3);
            A179.AddIngredient(ItemID.EnchantedNightcrawler, 1);
            A179.Register();

            Recipe A180 = Recipe.Create(ItemID.HellfireArrow, 150);
            A180.AddIngredient<Items.Other.FireDiamond>(3);
            A180.AddIngredient(ItemID.WoodenArrow, 100);
            A180.AddTile(TileID.WorkBenches);
            A180.Register();

            Recipe A181 = Recipe.Create(ItemID.Leather, 1);
            A181.AddIngredient(ItemID.Vertebrae, 5);
            A181.AddTile(TileID.WorkBenches);
            A181.Register();

            Recipe A182 = Recipe.Create(ItemID.GiantHarpyFeather, 1);
            A182.AddIngredient(ItemID.Feather, 150);
            A182.AddTile(TileID.SkyMill);
            A182.Register();

            Recipe A183 = Recipe.Create(ItemID.Book, 1);
            A183.AddIngredient<Items.Other.Paper>(30);
            A183.AddIngredient(ItemID.Leather, 2);
            A183.AddTile(TileID.WorkBenches);
            A183.Register();

            Recipe A184 = Recipe.Create(ItemID.Leather, 7);
            A184.AddIngredient(ItemID.BlandWhip, 1);
            A184.Register();

            Recipe A185 = Recipe.Create(ItemID.PaperAirplaneA, 2);
            A185.AddIngredient<Items.Other.Paper>(1);
            A185.Register();

            Recipe A186 = Recipe.Create(ItemID.PaperAirplaneB, 2);
            A186.AddIngredient<Items.Other.Paper>(1);
            A186.Register();

            Recipe A187 = Recipe.Create(ItemID.LuckyHorseshoe, 1);
            A187.AddIngredient<Items.Other.SoulOfHeight>(3);
            A187.AddIngredient(ItemID.GoldBar, 4);
            A187.AddTile(TileID.Anvils);
            A187.Register();

            Recipe A188 = Recipe.Create(ItemID.LuckyHorseshoe, 1);
            A188.AddIngredient<Items.Other.SoulOfHeight>(3);
            A188.AddIngredient(ItemID.PlatinumBar, 4);
            A188.AddTile(TileID.Anvils);
            A188.Register();
        }




    }
}