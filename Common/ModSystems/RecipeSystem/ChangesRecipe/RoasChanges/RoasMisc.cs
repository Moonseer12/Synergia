using Avalon.Items.Placeable.Tile;
using Terraria;
using Terraria.ID;
using ValhallaMod.Items.Garden;

namespace Synergia.Common.ModSystems.RecipeSystem.ChangesRecipe.AvalonsChanges {
        public class RoaMisc : BaseRecipe {
            public override void DisableRecipe(Recipe recipe)
            {
                DisableRecipe(recipe, RoAItem("Tapper"));
            }
            public override void Ingredient(Recipe recipe) {
            AddIngredient(recipe, RoAItem("SlipperyGrenade"), 1, new Item(ItemType<Sap>(), 1));
            AddIngredient(recipe, RoAItem("SlipperyBomb"), 1, new Item(ItemType<Sap>(), 1));
            AddIngredient(recipe, RoAItem("SlipperyDynamite"), 1, new Item(ItemType<Sap>(), 1));
            AddIngredient(recipe, RoAItem("SlipperyGlowstick"), 1, new Item(ItemType<Sap>   (), 1));

        }
            public override void PostRecipe()   
            {
                CreateGalipot();
                CreateMercuriumNugget();
            }
            static void CreateGalipot()
            {
                Recipe recipe = Recipe.Create(RoAItem("Galipot"));
                recipe.AddIngredient(ItemID.Bottle);
                recipe.AddIngredient(ItemType<Sap>(), 2);
                recipe.AddTile(TileID.Bottles);
                recipe.Register();
            }
            static void CreateMercuriumNugget()
            {
                Recipe recipe = Recipe.Create(RoAItem("MercuriumNugget"));
                recipe.AddIngredient(RoAItem("MercuriumOre"), 4);
                recipe.AddIngredient(ItemType<ChunkstoneBlock>(), 2);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }

    }
    
}