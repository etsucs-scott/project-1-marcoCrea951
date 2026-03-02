using AdventureGame.Core.Characters;
using AdventureGame.Core.Items;

namespace AdventureGame.Core.Maze;

public class GameMaze
{
    public int Width { get; }
    public int Height { get; }
    public Tile[,] Grid { get; }

    public int ExitX { get; private set; }
    public int ExitY { get; private set; }

    private readonly Random _rand = new();

    public GameMaze(int width = 10, int height = 10)
    {
        Width = width;
        Height = height;

        Grid = new Tile[Width, Height];

        for (int x = 0; x < Width; x++)
        for (int y = 0; y < Height; y++)
            Grid[x, y] = new Tile();

        Generate();
    }

    public bool InBounds(int x, int y) =>
        x >= 0 && x < Width && y >= 0 && y < Height;

    public Tile GetTile(int x, int y) => Grid[x, y];

    private void Generate()
    {
        // 1) Set everything to empty
        for (int x = 0; x < Width; x++)
        for (int y = 0; y < Height; y++)
        {
            Grid[x, y].Type = TileType.Empty;
            Grid[x, y].Monster = null;
            Grid[x, y].Item = null;
        }

        // 2) Exit at bottom-right
        ExitX = Width - 1;
        ExitY = Height - 1;
        Grid[ExitX, ExitY].Type = TileType.Exit;

        // 3) Guaranteed path: top row then last column
        for (int x = 0; x < Width; x++)
            Grid[x, 0].Type = TileType.Empty;

        for (int y = 0; y < Height; y++)
            Grid[Width - 1, y].Type = TileType.Empty;

        Grid[ExitX, ExitY].Type = TileType.Exit;

        // 4) Random walls/monsters/items (avoid start, exit, path)
        for (int x = 0; x < Width; x++)
        for (int y = 0; y < Height; y++)
        {
            if (x == 0 && y == 0) continue;
            if (x == ExitX && y == ExitY) continue;

            bool onPath = (y == 0) || (x == Width - 1);
            if (onPath) continue;

            int roll = _rand.Next(100);

            if (roll < 18)
            {
                Grid[x, y].Type = TileType.Wall;
            }
            else if (roll < 28)
            {
                Grid[x, y].Monster = new Monster();
            }
            else if (roll < 38)
            {
                if (_rand.Next(2) == 0)
                    Grid[x, y].Item = new Weapon("Sword", "Picked up a sword!", _rand.Next(1, 11));
                else
                    Grid[x, y].Item = new Potion("Potion", "Drank a potion (+20 HP)!", 20);
            }
        }

        // start empty
        Grid[0, 0].Type = TileType.Empty;
    }
}
