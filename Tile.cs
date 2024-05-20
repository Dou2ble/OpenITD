namespace Imperialism;

public class Tile {
    public TileKind Kind;
    public int Height;

    public Tile(TileKind tileKind, int tileHeight) {
        Kind = tileKind;
        Height = tileHeight;
    }
}
