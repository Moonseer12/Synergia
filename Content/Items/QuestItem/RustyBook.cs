using Avalon.Tiles.Furniture.Crafting;
using Bismuth.Content.Items.Materials;
using NewHorizons.Content.Items.Materials;
using Terraria.ID;
using ValhallaMod.Items.Material;

namespace Synergia.Content.Items.QuestItem {
    public class RustyBook : ModItem {
        public override void SetDefaults() {
            Item.questItem = true;
            Item.rare = ItemRarityID.Quest;
            Item.width = 40;
            Item.height = 25;
        }
        public override void AddRecipes()
        {
            var recipe = CreateRecipe()
                .AddIngredient(ItemType<TatteredBook>(), 1)
                .AddIngredient(ItemType<PeatPowder>(), 10)
                .AddIngredient(ItemType<AncientScrap>(), 8)
                .AddTile(TileType<TomeForge>())
                .Register();
        }
    }
}