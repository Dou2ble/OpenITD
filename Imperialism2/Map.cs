using System.Numerics;
using Raylib_cs;

namespace Imperialism2;

public class Map {
    private static Map? _instance;
    public Tile[,] Tiles = new Tile[128, 128];
    private Map() {
        for (int x = 0; x < Tiles.GetLength(0); x++) {
            for (int y = 0; y < Tiles.GetLength(1); y++) {
                Tiles[x, y] = new Tile();
            }
        }
    }

    public static Map Instance {
        get {
            if (_instance == null) {
                _instance = new Map();
            }

            return _instance;
        }
    }
    private void DrawTiles() {
        for (int x = 0; x < Tiles.GetLength(0); x++) {
            for (int y = 0; y < Tiles.GetLength(1); y++) {
                Raylib.DrawRectangle(x*Constants.TileSize, y*Constants.TileSize, Constants.TileSize, Constants.TileSize, Color.Green); 
                
                if (Tiles[x, y].Building != null) {
                    if (Tiles[x, y].Building is Road) {
                        bool[] neighbours = new bool[4];
                        if (y > 0) {
                            if (Tiles[x, y-1].Building is Road) {
                                neighbours[0] = true;
                            }
                        }
                        if (x < Tiles.GetLength(0)-1) {
                            if (Tiles[x+1, y].Building is Road) {
                                neighbours[1] = true;
                            }
                        }
                        if (y < Tiles.GetLength(1)-1) {
                            if (Tiles[x, y+1].Building is Road) {
                                neighbours[2] = true;
                            }
                        }
                        if (x > 0) {
                            if (Tiles[x-1, y].Building is Road) {
                                neighbours[3] = true;
                            }
                        }
                        
                        Raylib.DrawTexture(Assets.Instance.GetRoad(neighbours), x*Constants.TileSize, y*Constants.TileSize, Color.White);
                    }
                }
                
                Raylib.DrawRectangleLines(x*Constants.TileSize, y*Constants.TileSize, Constants.TileSize, Constants.TileSize, Raylib.ColorAlpha(Color.Black, 0.2f));
            }
        }
    }
    
    public void Draw(Camera2D camera, Tool selectedTool) {
        DrawTiles();
    }
}