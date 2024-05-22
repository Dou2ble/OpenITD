using System.Numerics;
using Raylib_cs;

namespace Imperialism;

class Game {
    private Map _map;
    private CameraController _cameraController;
    private Camera2D _camera;
    private Player _player;

    public Game() {
        Raylib.InitWindow(1280, 720, "Imperialism");
        _map = new();
        _cameraController = new();
        _camera = new(new Vector2(Settings.TotalWidth / 2, Settings.TotalHeight / 2), _cameraController.Position, 0, _cameraController.Zoom);
        _player = new("Player 1");

        _map.RedrawMap();
        EventLoop();

        Raylib.CloseWindow();
    }

    private void EventLoop() {
        while (!Raylib.WindowShouldClose()) {
            float dt = Raylib.GetFrameTime();
            _cameraController.Update(dt);
            _player.Update(dt, _camera);
            _camera.Target = _cameraController.Position;
            _camera.Zoom = _cameraController.Zoom;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.BeginMode2D(_camera);
            
            _map.Draw();
            _player.Draw(); 
            
            Raylib.EndMode2D();
            
            _player.DrawHUD();



            Raylib.EndDrawing();
        }
    }
}
