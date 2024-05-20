using Raylib_cs;

namespace Imperialism;

using System.Reflection;

public class Textures {
    private static Textures instance;
    private static readonly object lockObject = new object();

    private static string TexturePath(string name) {
        return $"resources/textures/{name}.png";
    }

    public Texture2D Grass = Raylib.LoadTexture(TexturePath("grass"));

    public static Textures Instance {
        get {
            if (instance == null) {
                lock (lockObject) {
                    if (instance == null) {
                        instance = new Textures();
                    }
                }
            }
            return instance;
        }
    }
}