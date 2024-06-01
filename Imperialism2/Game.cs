using System.Numerics;
using Raylib_cs;

namespace Imperialism2;

public class Game {
    private const float CameraZoomSpeed = 0.128f;
    private const float CameraTargetMoveSpeed = 1000;
    private const int Width = 1280;
    private const int Height = 720;
    private const int HudBlurSize = 2;
    private float _dt = 0.0f;

    private bool _menuFocused = false;
    private Tool _selectedTool = Tool.Road;
    private int _screenWidth;
    private int _screenHeight;

    private bool MenuFocused {
        get => _menuFocused;
        set {
            if (value) {
                Image image = Raylib.LoadImageFromScreen();
                unsafe {Raylib.ImageBlurGaussian(&image, HudBlurSize);}
                _menuBackground = Raylib.LoadTextureFromImage(image);
            } else {
                Raylib.UnloadTexture(_menuBackground);
            }
            
            _menuFocused = value;
        }
    }
    private Texture2D _menuBackground;
    
    private Camera2D _camera = new Camera2D(new Vector2(Width/2, Height/2), Vector2.Zero, 0, 1);
    private Player _player = new Player("Player One");
    
    public Game() {
        Raylib.InitWindow(Width, Height, "Hello World");
        
        _player.Vehicles.Add(new Car());
        
        MainLoop();

        Raylib.CloseWindow();
    }

    private void MainLoop() {
        while (!Raylib.WindowShouldClose())
        {
            _dt = Raylib.GetFrameTime();
            _screenWidth = Raylib.GetScreenWidth();
            _screenHeight = Raylib.GetScreenHeight();
            
            
            if (Raylib.IsKeyPressed(KeyboardKey.Tab)) {
                MenuFocused = !MenuFocused;
            }
            
            Raylib.BeginDrawing();

            if (_menuFocused) {
                DrawMenu();
            } else {
                updateCamera();
                Draw();
                Update();
            }
            
            Raylib.EndDrawing();
        }
    }

    private void updateCamera() {
        _camera.Zoom += _camera.Zoom*CameraZoomSpeed*Raylib.GetMouseWheelMoveV().Y;
        _camera.Offset.X = (float)_screenWidth / 2;
        _camera.Offset.Y = (float)_screenHeight / 2;
        
        if (Raylib.IsKeyDown(KeyboardKey.A)) {
            _camera.Target.X -= CameraTargetMoveSpeed/_camera.Zoom*_dt;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D))
        {
            _camera.Target.X += CameraTargetMoveSpeed/_camera.Zoom*_dt;
        }
        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            _camera.Target.Y -= CameraTargetMoveSpeed/_camera.Zoom*_dt;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S))
        {
            _camera.Target.Y += CameraTargetMoveSpeed/_camera.Zoom*_dt;
        }
    }

    private void DrawMenu() {
        // Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), Raylib.ColorAlpha(Color.Black, 0.4f));
        Raylib.DrawTexture(_menuBackground, 0, 0, Color.White);
        if (GUI.Button("Road tool", new Rectangle(10, 10, 100, 100))) {
            _selectedTool = Tool.Road;
        }
        if (GUI.Button("Bulldozer", new Rectangle(10, 120, 100, 100))) {
            _selectedTool = Tool.Bulldozer;
        }
        if (GUI.Button("Select tool", new Rectangle(120, 10, 100, 100))) {
            _selectedTool = Tool.Select;
        }
    }

    private void Draw() {
        Raylib.ClearBackground(Color.White); 
        
        Raylib.BeginMode2D(_camera);
        
        Map.Instance.Draw(_camera, _selectedTool);
        foreach (Vehicle vehicle in _player.Vehicles) {
            vehicle.Draw();    
        }
        
        Raylib.EndMode2D();

        _player.DrawHud(_screenWidth, _screenHeight);
    }

    private void Update() {
        if (_player.SelectedVehicles.Count != 0) {
            _selectedTool = Tool.Select;
        }
        
        Vector2 mousePosition = Raylib.GetMousePosition();
        Vector2 mouseTilePosition = Raylib.GetScreenToWorld2D(mousePosition, _camera)/Constants.TileSize;
        mouseTilePosition.X = float.Floor(mouseTilePosition.X);
        mouseTilePosition.Y = float.Floor(mouseTilePosition.Y);

        // if the mouse tile position is inside the map and outside the HUD
        if (mouseTilePosition.X >= 0 && mouseTilePosition.Y >= 0 &&
            mouseTilePosition.X < Map.Instance.Tiles.GetLength(0) &&
            mouseTilePosition.Y < Map.Instance.Tiles.GetLength(1) &&
            !Utils.IsVector2In(mousePosition, _player.HudRectangle)) {
            int mouseTileX = (int)mouseTilePosition.X;
            int mouseTileY = (int)mouseTilePosition.Y;
            
            if (Raylib.IsMouseButtonDown(MouseButton.Left)) {
                switch (_selectedTool) {
                    case Tool.Bulldozer:
                        Map.Instance.Tiles[mouseTileX, mouseTileY].Building = null;
                        break;
                    case Tool.Road:
                        Map.Instance.Tiles[mouseTileX, mouseTileY].Building = new Road();
                        break;
                }
            }
        }
    }
}