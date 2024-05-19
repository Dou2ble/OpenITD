using System.Numerics;
using Raylib_cs;

namespace Imperialism;

class CameraController
{
    private Map _map;
    private Player _player;
    private Camera2D _camera;
    
    public CameraController() {
        Raylib.InitWindow(1280, 720, "Imperialism");
        _map = new();
        _player = new();
        _camera = new(new Vector2(Settings.TotalWidth/2,Settings.TotalHeight/2), _player.Position, 0, _player.Zoom);

        _map.RedrawMap();
        EventLoop();

        Raylib.CloseWindow();
    }

    private void EventLoop() {
        while (!Raylib.WindowShouldClose()) {
            float dt = Raylib.GetFrameTime();
            _player.Update(dt);
            _camera.Target = _player.Position;
            _camera.Zoom = _player.Zoom;
            
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
