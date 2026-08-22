using Synergia.Helpers;
using Terraria;
using Terraria.ID;
using ValhallaMod.Tiles.Blocks;

namespace Synergia.Common.ModSystems.WorldGens {
    public class SnowCaven {
        static readonly byte[,] SnowCavenTiles = {
            {4, 4, 4, 4, 4, 4, 4, 4, 4}, // 1
            {1, 0, 2, 2, 3, 2, 3, 0, 4}, // 2
            {0, 0, 0, 3, 3, 2, 3, 0, 0}, // 3
            {0, 0, 0, 3, 5, 3, 0, 0, 0}, // 4
            {5, 0, 0, 2, 5, 3, 0, 0, 5}, // 5
            {5, 0, 3, 2, 5, 3, 3, 0, 5}, // 6
            {6, 0, 3, 2, 5, 2, 3, 0, 6}, // 7
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 8
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 9
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 10
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 11
            {0, 0, 0, 3, 3, 3, 0, 0, 0}, // 12
        };
        // 0 - empty, 1 - ElderwoodFence, 2 - Gray Brick, 3 - Stoun Slab
        static readonly byte[,] SnowCavenWales = {
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 1
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 2
            {1, 0, 0, 0, 0, 0, 0, 0, 1}, // 3
            {1, 0, 0, 0, 0, 0, 0, 0, 1}, // 4
            {1, 0, 0, 0, 0, 0, 0, 0, 1}, // 5
            {1, 0, 0, 0, 0, 0, 0, 0, 1}, // 6
            {0, 0, 0, 0, 2, 0, 0, 0, 0}, // 7
            {0, 0, 0, 0, 2, 0, 0, 0, 0}, // 8
            {0, 0, 0, 0, 2, 0, 0, 0, 0}, // 9
            {0, 0, 0, 0, 3, 0, 0, 0, 0}, // 10
            {0, 0, 0, 0, 2, 0, 0, 0, 0}, // 11
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 12
        };
        // 0 - empty, 1 - hamer, 2 - /|, 3 - |/, 4 - \|, 5 - |\
        static readonly byte[,] SnowCavenSlope = {
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 1
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 2
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 3
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 4
            {4, 0, 0, 0, 0, 0, 0, 0, 3}, // 5
            {0, 0, 4, 0, 0, 0, 3, 0, 0}, // 6
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 7
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 8
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 9
            {0, 0, 0, 0, 0, 0, 0, 0, 0}, // 10
            {3, 0, 0, 0, 0, 0, 0, 0, 0}, // 11
            {0, 0, 0, 2, 0, 5, 0, 4, 0}, // 12
        };

        static readonly byte[,] SnowCavenTiles2 = {
            {1, 1, 1, 1, 0, 2, 2, 3, 2, 3, 0, 4, 4, 4, 4}, // 1
            {1, 1, 1, 0, 0, 0, 3, 3, 2, 3, 0, 0, 4, 4, 4}, // 2
            {1, 1, 0, 0, 0, 0, 3, 5, 3, 0, 0, 0, 0, 4, 4}, // 3
            {1, 0, 0, 5, 0, 0, 2, 5, 3, 0, 0, 5, 0, 4, 4}, // 4
            {1, 0, 0, 5, 0, 3, 2, 5, 3, 3, 0, 5, 0, 4, 4}, // 5
            {1, 0, 0, 6, 0, 3, 2, 5, 2, 3, 0, 6, 0, 4, 4}, // 6
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 7
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 8
            {2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 9
            {0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 10
            {1, 0, 2, 2, 0, 0, 3, 3, 3, 0, 1, 1, 1, 1, 4}, // 11
            {1, 0, 0, 2, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 4}, // 12
            {1, 4, 0, 2, 0, 0, 0, 1, 1, 0, 0, 0, 0, 4, 1}, // 13
            {1, 4, 1, 1, 1, 0, 1, 1, 0, 0, 4, 4, 4, 4, 4}, // 14
        };
        // 0 - empty, 1 - ElderwoodFence, 2 - Gray Brick, 3 - Stoun Slab
        static readonly byte[,] SnowCavenWales2 = {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 1
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // 2
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // 3
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // 4
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // 5
            {0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0}, // 6
            {0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0}, // 7
            {0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0}, // 8
            {0, 0, 0, 1, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0}, // 9
            {0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0}, // 10
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // 11
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // 12
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // 13
            {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0}, // 14
        };
        // 0 - empty, 1 - hamer, 2 - /|, 3 - |/, 4 - \|, 5 - |\
        static readonly byte[,] SnowCavenSlope2 = {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 1
            {0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0}, // 2
            {0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0}, // 3
            {0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0}, // 4
            {0, 0, 0, 0, 0, 4, 0, 0, 0, 3, 0, 0, 0, 0, 0}, // 5
            {5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2}, // 6
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 7
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 8
            {0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 9
            {0, 2, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // 10
            {4, 0, 2, 0, 0, 0, 2, 0, 5, 0, 4, 0, 0, 0, 0}, // 11
            {0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0}, // 12
            {0, 3, 0, 0, 0, 0, 0, 4, 5, 0, 0, 0, 0, 4, 0}, // 13
            {0, 0, 0, 0, 3, 0, 4, 5, 0, 0, 4, 0, 0, 0, 0}, // 14
        };

        public static bool Gen(int x, int y) {
            //WorldHelper.CleaningAll(x -7, y - 16, x + 8, y);
            int width = SnowCavenTiles.GetLength(1);
            int height = SnowCavenTiles.GetLength(0);


            for (int X = 0; X < width; X++) {
                for (int Y = 0; Y < height; Y++) {
                    int worldX = x + X;
                    int worldY = y - Y;

                    if (!WorldGen.InWorld(worldX, worldY, 10)) { continue; }

                    Tile tile = Framing.GetTileSafely(worldX, worldY);
                    tile.ClearEverything();

                    switch (SnowCavenTiles[Y, X]) {
                        case 0: break;
                        case 1: tile.TileType = TileID.IceBlock; tile.HasTile = true; break;
                        case 2: tile.TileType = (ushort)TileType<ValhalliteOre>(); tile.HasTile = true; break;
                        case 3: tile.TileType = (ushort)TileType<ValhalliteBrick>(); tile.HasTile = true; break;
                        case 4: tile.TileType = TileID.SnowBlock; tile.HasTile = true; break;
                        case 5: tile.TileType = TileID.StoneSlab; tile.HasTile = true; break;
                        case 6: WorldGen.PlaceTile(worldX, worldY, TileID.Torches, false, false, -1, 9); break;
                    }
                    switch (SnowCavenWales[Y, X]) {
                        case 0: break;
                        case 1: tile.WallType = ModList.Roa.Find<ModWall>("ElderwoodFence").Type; break;
                        case 2: tile.WallType = WallID.GrayBrick; break;
                        case 3: tile.WallType = WallID.StoneSlab; break;
                    }
                    switch (SnowCavenSlope[Y, X]) {
                        case 0: break;
                        case 1: tile.IsHalfBlock = true; break;
                        case 2: tile.Slope = SlopeType.SlopeDownRight; break;
                        case 3: tile.Slope = SlopeType.SlopeUpLeft; break;
                        case 4: tile.Slope = SlopeType.SlopeUpRight; break;
                        case 5: tile.Slope = SlopeType.SlopeDownLeft; break;
                    }
                }
            }

            WorldGen.PlaceObject(x + 4, y - 7, ModList.Valhalla.Find<ModTile>("DwarvenAnvil").Type, false, 1);

            return true;
        }
    }
}