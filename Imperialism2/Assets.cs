using Raylib_cs;

namespace Imperialism2;

public class Assets {
    private static Assets? _instance;

    private static string TexturePath(string path) {
        return $"assets/textures/{path}.png";
    }

    public Font DroidSansM = Raylib.LoadFontEx("assets/fonts/DroidSansMono.ttf", 16, null, 250);
    
    public Texture2D RoadE = Raylib.LoadTexture(TexturePath("roads/E"));
    public Texture2D RoadEw = Raylib.LoadTexture(TexturePath("roads/EW"));
    public Texture2D RoadN = Raylib.LoadTexture(TexturePath("roads/N"));
    public Texture2D RoadS = Raylib.LoadTexture(TexturePath("roads/S"));
    public Texture2D RoadEsw = Raylib.LoadTexture(TexturePath("roads/ESW"));
    public Texture2D RoadNew = Raylib.LoadTexture(TexturePath("roads/NEW"));
    public Texture2D RoadNes = Raylib.LoadTexture(TexturePath("roads/NES"));
    public Texture2D RoadNesw = Raylib.LoadTexture(TexturePath("roads/NESW"));
    public Texture2D RoadNs = Raylib.LoadTexture(TexturePath("roads/NS"));
    public Texture2D RoadNe = Raylib.LoadTexture(TexturePath("roads/NE"));
    public Texture2D RoadEs = Raylib.LoadTexture(TexturePath("roads/ES"));
    public Texture2D RoadW = Raylib.LoadTexture(TexturePath("roads/W"));
    public Texture2D RoadNsw = Raylib.LoadTexture(TexturePath("roads/NSW"));
    public Texture2D RoadNw = Raylib.LoadTexture(TexturePath("roads/NW"));
    public Texture2D RoadSw = Raylib.LoadTexture(TexturePath("roads/SW"));
    public Texture2D Road = Raylib.LoadTexture(TexturePath("roads/road"));

    public Texture2D GetRoad(bool[] neighbours) {
        return neighbours switch {
            [true, false, false, false] => RoadN,
            [false, true, false, false] => RoadE,
            [true, true, false, false] => RoadNe,
            [false, false, true, false] => RoadS,
            [true, false, true, false] => RoadNs,
            [false, true, true, false] => RoadEs,
            [true, true, true, false] => RoadNes,
            [false, false, false, true] => RoadW,
            [true, false, false, true] => RoadNw,
            [false, true, false, true] => RoadEw,
            [true, true, false, true] => RoadNew,
            [false, false, true, true] => RoadSw,
            [true, false, true, true] => RoadNsw,
            [false, true, true, true] => RoadEsw,
            [true, true, true, true] => RoadNesw,
            _ => Road
        };
    }
    private Assets() {}

    public static Assets Instance {
        get {
            if (_instance == null) {
                _instance = new Assets();
            }

            return _instance;
        }
    }
}