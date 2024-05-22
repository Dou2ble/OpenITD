using System.Numerics;
using Raylib_cs;

namespace Imperialism;

public class Car : Vehicle {
    public override void Draw() {
        // Raylib.DrawRectangleV(Position*Constants.TileSize, new Vector2(Constants.TileSize, Constants.TileSize), Color.Red);
        Raylib.DrawTextureV(Textures.Instance.Car, Position*Constants.TileSize, Color.White);
    }

    public Car() {
        Position = new Vector2(0, 0);
    }
}