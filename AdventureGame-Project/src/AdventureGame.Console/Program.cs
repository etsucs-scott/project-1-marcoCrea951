using AdventureGame.Core;
using AdventureGame.Core.Items;

GameEngine game = new GameEngine();

string lastMessage = "Move with W A S D. Press Q to quit.";

while (!game.GameOver)
{
    Console.Clear();

    Console.WriteLine("Welcome to the Adventure Game!!");
    Console.WriteLine("Controls: W A S D to move | Q to quit");
    Console.WriteLine($"Health: {game.Player.Health}   Position: ({game.PlayerX}, {game.PlayerY})");
    Console.WriteLine();
    Console.WriteLine(lastMessage);
    Console.WriteLine();

    DrawMaze(game);

    ConsoleKey key = Console.ReadKey(intercept: true).Key;
    if (key == ConsoleKey.Q) break;

    char input = key switch
    {
        ConsoleKey.W => 'W',
        ConsoleKey.A => 'A',
        ConsoleKey.S => 'S',
        ConsoleKey.D => 'D',
        _ => '\0'
    };

    if (input == '\0')
    {
        lastMessage = "Use W A S D to move. Press Q to quit.";
        continue;
    }

    lastMessage = game.Move(input);
}

Console.Clear();
Console.WriteLine(game.Won ? "YOU WIN!!" : "GAME OVER.");
Console.WriteLine("Press any key to exit...");
Console.ReadKey(true);

static void DrawMaze(GameEngine game)
{
    for (int y = 0; y < game.Maze.Height; y++)
    {
        for (int x = 0; x < game.Maze.Width; x++)
        {
            char symbol = GetSymbol(game, x, y);
            Console.Write(symbol);
            Console.Write(' ');
        }
        Console.WriteLine();
    }

    Console.WriteLine();
    Console.WriteLine("Legend: # wall  . empty  @ player  M monster  W weapon  P potion  E exit");
}

static char GetSymbol(GameEngine game, int x, int y)
{
    if (x == game.PlayerX && y == game.PlayerY)
        return '@';

    // Don't reference Tile/TileType types directly — just use the object.
    var tile = game.Maze.GetTile(x, y);

    // If your TileType is an enum, ToString() will give "Wall", "Exit", etc.
    string typeName = tile.Type.ToString();

    if (typeName == "Wall") return '#';
    if (typeName == "Exit") return 'E';

    if (tile.Monster != null) return 'M';

    if (tile.Item != null)
    {
        if (tile.Item is Weapon) return 'W';
        if (tile.Item is Potion) return 'P';
    }

    return '.';
}
