using Raylib_cs;

namespace Imperialism2;

public class Player {
    public string Name;
    public List<Vehicle> Vehicles = new List<Vehicle>();
    public List<int> SelectedVehicles = new List<int>();
    private const int HudButtonSize = 48;
    private const int HudButtonMargin = 12;
    private const float HudButtonRoundness = 0.3f;
    public Rectangle HudRectangle;

    public Player(string name) {
        Name = name;
    }

    public void DrawHud(int screenWidth, int sceenHeight) {
        HudRectangle.X = screenWidth - HudButtonSize - 2*HudButtonMargin;
        HudRectangle.Y = sceenHeight - Vehicles.Count*(HudButtonMargin + HudButtonSize) - HudButtonMargin;
        HudRectangle.Width = HudButtonSize + 2 * HudButtonMargin;
        HudRectangle.Height = Vehicles.Count*(HudButtonMargin + HudButtonSize) + HudButtonMargin;
        Raylib.DrawRectangleRec(HudRectangle, Raylib.ColorAlpha(Color.Black, 0.3f));
        
        for (int i = 0; i < Vehicles.Count; i++) {
            Rectangle rectangle = new Rectangle(
                screenWidth - HudButtonSize - HudButtonMargin,
                sceenHeight - (i + 1)*(HudButtonMargin + HudButtonSize), 
                HudButtonSize, HudButtonSize);
            if (GUI.Button(Vehicles[i].GetType().Name, rectangle)) {
                if (Raylib.IsKeyDown(KeyboardKey.LeftShift) || Raylib.IsKeyDown(KeyboardKey.RightShift) ||
                    Raylib.IsKeyDown(KeyboardKey.LeftControl) || Raylib.IsKeyDown(KeyboardKey.RightControl)) {
                    if (SelectedVehicles.Contains(i)) {
                        SelectedVehicles.Remove(i);
                    } else {
                        SelectedVehicles.Add(i);   
                    }
                } else {
                    SelectedVehicles.Clear();
                    SelectedVehicles.Add(i);
                }
            }
        }
        
        Raylib.DrawText($"No Selected: {SelectedVehicles.Count}", 10, 10, 12, Color.Black);
    }
}