using System.Numerics;

namespace Imperialism;

using Raylib_cs;

public class Map {
    private const int TotalWidth = 128;
    private const int TotalHeight = 128;
    private const float heightIntensity = 0.25f;
    private const int waterLevel = 20;
    private const int TileSize = 32;
    private RenderTexture2D _texture;

    private Tile[,] _tiles;
    public Tile[,] Tiles {
        get => _tiles;
        set {
            _tiles = value;
            RedrawMap();
        }
    }

    public void RedrawMap() {
        Raylib.BeginTextureMode(_texture);

        for (int x = 0; x < TotalWidth; x++) {
            for (int y = 0; y < TotalHeight; y++) {
                Color color = _tiles[x, y].Kind switch {
                    TileKind.DeepWater => Color.DarkBlue,
                    TileKind.Water => Color.Blue,
                    TileKind.Beach => Color.Yellow,
                    TileKind.Grass => Color.Green,
                    TileKind.Forest => Color.DarkGreen,
                    TileKind.Mountain => Color.LightGray,
                    TileKind.Peak => Color.White,
                    _ => Color.Pink
                };

                Raylib.DrawRectangle(x * TileSize, y * TileSize, TileSize, TileSize, color);

                // if (_tiles[x, y].Kind == TileKind.Grass) {
                // Raylib.DrawTexture(Textures.Instance.Grass, x * TileSize, y * TileSize, Raylib.ColorFromHSV(0, 0, _tiles[x, y].Heightk));
                //     float rotation = (float)Random.Shared.Next(0, 3)*90;
                //     
                //     Vector2 position = new Vector2(x * TileSize, y * TileSize);
                //     switch (rotation) {
                //         case 90f:
                //             position.X += TileSize;
                //             break;
                //         case 180f:
                //             position.X += TileSize;
                //             position.Y += TileSize;
                //             break;
                //         case 270f:
                //             position.Y += TileSize;
                //             break;
                //     }
                //     
                // Raylib.DrawTextureEx(Textures.Instance.Grass, position, rotation, 1f, Color.White);
                // }
            }
        }

        Raylib.EndTextureMode();
    }

    public Map() {
        _tiles = new Tile[TotalWidth, TotalHeight];
        _texture = Raylib.LoadRenderTexture(TotalWidth * TileSize, TotalHeight * TileSize);

        int offsetX = Random.Shared.Next(int.MinValue / 512, int.MaxValue / 512);
        int offsetY = Random.Shared.Next(int.MinValue / 512, int.MaxValue / 512);
        Image heightMap = Raylib.GenImagePerlinNoise(TotalWidth, TotalHeight, offsetX, offsetY, 4);
        Image forestMap = Raylib.GenImagePerlinNoise(TotalWidth, TotalHeight, offsetX, offsetY, 5);
        Image beachMap = Raylib.GenImagePerlinNoise(TotalWidth, TotalHeight, offsetX, offsetY, 8);
        unsafe {
            // Height map
            Color* heightMapColors = Raylib.LoadImageColors(heightMap);

            for (int x = 0; x < TotalWidth; x++) {
                for (int y = 0; y < TotalHeight; y++) {
                    byte brightness = heightMapColors[y * TotalWidth + x].R;

                    sbyte height = (sbyte)(brightness * heightIntensity - waterLevel);
                    TileKind kind = height switch {
                        < -30 => TileKind.DeepWater,
                        < 0 => TileKind.Water,
                        < 25 => TileKind.Grass,
                        < 32 => TileKind.Mountain,
                        _ => TileKind.Peak
                    };

                    _tiles[x, y] = new(kind, height);
                }
            }


            // Forest map
            Color* forestMapColors = Raylib.LoadImageColors(forestMap);

            for (int x = 0; x < TotalWidth; x++) {
                for (int y = 0; y < TotalHeight; y++) {
                    if (forestMapColors[y * TotalWidth + x].R > 150 && heightMapColors[y * TotalWidth + x].R < 150 && _tiles[x, y].Kind == TileKind.Grass) {
                        _tiles[x, y].Kind = TileKind.Forest;
                    }
                }
            }
        }

        unsafe {
            Color* beachMapColors = Raylib.LoadImageColors(beachMap);

            for (int x = 1; x < TotalWidth - 1; x++) {
                for (int y = 1; y < TotalHeight - 1; y++) {
                    if (_tiles[x, y].Kind == TileKind.Grass) {
                        if ((_tiles[x - 1, y].Kind == TileKind.Water ||
                             _tiles[x + 1, y].Kind == TileKind.Water ||
                             _tiles[x, y - 1].Kind == TileKind.Water ||
                             _tiles[x, y + 1].Kind == TileKind.Water) &&
                            beachMapColors[y * TotalWidth + x].R > 160
                            ) {
                            _tiles[x, y].Kind = TileKind.Beach;
                        }
                    }
                }
            }
        }



        RedrawMap();
    }

    public void Draw() {
        // Raylib.DrawRectangle(100, 100, 100, 100, Color.Red);
        Raylib.DrawTexture(_texture.Texture, 0, 0, Color.White);
    }
}
