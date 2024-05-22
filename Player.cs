using System.Numerics;
using Raylib_cs;

namespace Imperialism;

public class Player {
    private List<Vehicle> _vehicles;
    public string Name;
    
    public Player(string playerName) {
        _vehicles = new();
        _vehicles.Add(new Car());
        Name = playerName;
    }

    public void Update(float dt, Camera2D camera) {
        Vector2 mousePosition = Raylib.GetMousePosition();
        Vector2 mouseTilePosition = Raylib.GetScreenToWorld2D(mousePosition, camera) / Constants.TileSize;

        if (Raylib.IsMouseButtonDown(MouseButton.Left)) {
            for (int i = 0; i < _vehicles.Count; i++) {
                Vehicle vehicle = _vehicles[i];
                if (vehicle.Position == mouseTilePosition) {
                    vehicle.Selected = true;
                } else {
                    vehicle.Selected = false;
                }
            }
        }
    }

    public void Draw() {
        foreach (Vehicle vehicle in _vehicles) {
            vehicle.Draw(); 
        } 
    }

    public void DrawHUD() {
        Raylib.DrawText(Name, 10, 10, 20, Color.Black);
        
        int selectedVehicles = _vehicles.Count(vehicle => vehicle.Selected);
        Raylib.DrawText($"Selected vehicles: {selectedVehicles}", 10, 30, 20, Color.Black);
    }
}