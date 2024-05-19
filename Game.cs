using System.Numerics;
using Raylib_cs;

namespace Imperialism;

class Game
{
    private Map _map;
    private CameraController _cameraController;
    private Camera2D _camera;
    
    public Game() {
        Raylib.InitWindow(1280, 720, "Imperialism");
        _map = new();
        _cameraController = new();
        _camera = new(new Vector2(Settings.TotalWidth/2,Settings.TotalHeight/2), _cameraController.Position, 0, _cameraController.Zoom);

        _map.RedrawMap();
        EventLoop();

        Raylib.CloseWindow();
    }

    private void EventLoop() {
        while (!Raylib.WindowShouldClose()) {
            float dt = Raylib.GetFrameTime();
            _cameraController.Update(dt);
            _camera.Target = _cameraController.Position;
            _camera.Zoom = _cameraController.Zoom;
            
            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                Raylib.BeginMode2D(_camera);
                    _map.Draw();
                    Raylib.DrawRectangle(-10, -10, 20, 20, Color.Orange);
                    
                    Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
                Raylib.EndMode2D();
                


            Raylib.EndDrawing();
        }
    }
}
