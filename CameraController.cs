using System.Numerics;
using Raylib_cs;

namespace Imperialism;

public class CameraController {
    public Vector2 Position;
    public float Zoom = 1;
    private float _movementSpeed = 500;
    private const float ZoomStep = 0.125f;
    private const int RegularMovementSpeed = 500;

    public void Update(float dt) {
        if (Raylib.IsKeyDown(KeyboardKey.LeftShift) || Raylib.IsKeyDown(KeyboardKey.RightShift)) {
            _movementSpeed = RegularMovementSpeed * 4;
        } else {
            _movementSpeed = RegularMovementSpeed;
        }
        _movementSpeed /= Zoom;

        if (Raylib.IsKeyDown(KeyboardKey.W)) {
            Position.Y -= _movementSpeed * dt;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S)) {
            Position.Y += _movementSpeed * dt;
        }
        if (Raylib.IsKeyDown(KeyboardKey.A)) {
            Position.X -= _movementSpeed * dt;
        }
        if (Raylib.IsKeyDown(KeyboardKey.D)) {
            Position.X += _movementSpeed * dt;
        }

        Zoom += Raylib.GetMouseWheelMoveV().Y * ZoomStep;
        if (Zoom < ZoomStep) {
            Zoom = ZoomStep;
        }
    }
}