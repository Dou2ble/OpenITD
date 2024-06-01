using System.Numerics;
using Raylib_cs;

namespace Imperialism2;

public abstract class Vehicle {
    public Vector2 Position;
    public const float Radius = Constants.TileSize/2;

    public virtual void Draw() {
        Raylib.DrawCircleV(Position, Radius, Color.Pink);
    }
}