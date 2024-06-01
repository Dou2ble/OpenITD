using System.Numerics;
using Raylib_cs;

namespace Imperialism2;

public static class GUI {
    private const int FontSize = 16;
    // private static readonly Font Font = Raylib.LoadFontEx("assets/fonts/DroidSansMono.ttf", FontSize, [0], 250);
    public static bool Button(string label, Rectangle rectangle) {
        bool result = false;
        Vector2 mousePosition = Raylib.GetMousePosition();
        if (Utils.IsVector2In(mousePosition, rectangle)) {
            if (Raylib.IsMouseButtonDown(MouseButton.Left)) {
                Raylib.DrawRectangleRec(rectangle, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
                    result = true;
                }
            } else {
                Raylib.DrawRectangleRec(rectangle, Color.LightGray);
            }
        } else {
            Raylib.DrawRectangleRec(rectangle, Color.Gray);
        }
        Raylib.DrawRectangleLinesEx(rectangle, 2, Color.DarkGray);
        Vector2 textWidth = Raylib.MeasureTextEx(Assets.Instance.DroidSansM, label, FontSize, 0f);
        Raylib.DrawTextEx(Assets.Instance.DroidSansM, label, new Vector2(rectangle.X + rectangle.Width/2 - textWidth.X/2, rectangle.Y + rectangle.Height/2 - textWidth.Y/2), FontSize, 0, Color.Black);

        return result;
    }
}