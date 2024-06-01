using System.Numerics;
using Raylib_cs;

namespace Imperialism2;

public static class Utils {
    public static bool IsVector2In(Vector2 point, Rectangle rectangle) {
        return point.X > rectangle.X && point.Y > rectangle.Y && point.X < rectangle.X + rectangle.Width && point.Y < rectangle.Y + rectangle.Height;
    }
}