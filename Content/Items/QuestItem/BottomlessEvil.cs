using Avalon.Items.Material.Shards;
using Avalon.Tiles.Furniture.Crafting;
using Terraria.ID;

namespace Synergia.Content.Items.QuestItem {
    public class BottomlessEvil : ModItem {
        public override void SetDefaults() {
            Item.questItem = true;
            Item.rare = ItemRarityID.Quest;
            Item.width = 40;
            Item.height = 25;
        }
        public override void AddRecipes()
        {
            var recipe = CreateRecipe()
                .AddIngredient(ItemType<WickedShard>(), 10)
                .AddIngredient(ItemID.SoulofMight, 3)
                .AddIngredient(ItemID.SoulofFright, 3)
                .AddIngredient(ItemID.SoulofSight, 3)
                .AddTile(TileType<TomeForge>());

            if (ModLoader.TryGetMod("PrimeRework", out Mod primeReworkMod))
            {
                if (primeReworkMod.TryFind<ModItem>("SoulofFreight", out var freight) &&
                    primeReworkMod.TryFind<ModItem>("SoulofPlight", out var plight) &&
                    primeReworkMod.TryFind<ModItem>("SoulofDight", out var dight))
                {
                    recipe
                        .AddIngredient(freight.Type, 3)
                        .AddIngredient(dight.Type, 3);
                }
            }

            recipe.Register();
        }
    }
}