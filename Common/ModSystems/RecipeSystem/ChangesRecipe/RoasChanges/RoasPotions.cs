using Avalon.Items.Material;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Synergia.Common.ModSystems.RecipeSystem.ChangesRecipe.AvalonsChanges {
        public class RoasPotion : BaseRecipe {

            public override void Ingredient(Recipe recipe) {
                AddIngredient(recipe, RoAItem("BloodlustPotion"), 0, new Item(ItemType<BottledLava>(), 1));
                AddIngredient(recipe, RoAItem("DeathWardPotion"), 0, new Item(ItemType<BottledLava>(), 1));
                AddIngredient(recipe, RoAItem("DeathWardPotion"), 3, new Item(ItemType<LifeDew>(), 15));
                AddIngredient(recipe, RoAItem("PrismaticPotion"), 2, new Item(ItemType<ChaosDust>(), 1));
            }
        public override void PostRecipe()
        {
            CreateWeakPotion();


        }
        static void CreateWeakPotion()
        {
            Recipe recipe = Recipe.Create(RoAItem("WeaknessPotion"), 3);
            recipe.AddIngredient(ItemID.BottledWater, 3);
            recipe.AddIngredient(RoAItem("Bonerose"), 1);
            recipe.AddIngredient(RoAItem("MiracleMint"), 1);
            recipe.AddIngredient(ItemType<YuckyBit>(), 1);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }

    }
    
}