using AdventureGame.Core.Characters;
using AdventureGame.Core.Items;
using AdventureGame.Core.Maze;

namespace AdventureGame.Core;

public class GameEngine
{
    public Player Player { get; } = new Player();
    public GameMaze Maze { get; } = new GameMaze(10, 10);



    public int PlayerX { get; private set; } = 0;
    public int PlayerY { get; private set; } = 0;

    public bool GameOver { get; private set; } = false;
    public bool Won { get; private set; } = false;

    public string Move(char input)
    {
        int dx = 0;
        int dy = 0;

        input = char.ToUpper(input);

        if (input == 'W') dy = -1;
        else if (input == 'S') dy = 1;
        else if (input == 'A') dx = -1;
        else if (input == 'D') dx = 1;
        else return "Use W A S D.";

        int newX = PlayerX + dx;
        int newY = PlayerY + dy;

        // off map
        if (!Maze.InBounds(newX, newY))
            return "Can't go there (off the map).";

        Tile tile = Maze.GetTile(newX, newY);

        // wall
        if (tile.Type == TileType.Wall)
            return "Wall is blocking you.";

        // ✅ update position
        PlayerX = newX;
        PlayerY = newY;

        // exit
        if (tile.Type == TileType.Exit)
        {
            Won = true;
            GameOver = true;
            return "You Win!!";
        }

        // monster
        if (tile.Monster != null)
        {
            string msg = Battle(tile.Monster);

            if (!tile.Monster.IsAlive)
                tile.Monster = null; // tile becomes empty after defeat

            return msg;
        }

        // item
        if (tile.Item != null)
        {
            string msg = Pickup(tile.Item);
            tile.Item = null; // tile becomes empty after pickup
            return msg;
        }

        return "Moved.";
    }

    private string Pickup(Item item)
    {
        if (item is Weapon weapon)
        {
            Player.AddItem(weapon);
            return "Weapon picked up!";
        }

        if (item is Potion potion)
        {
            potion.Use(Player); // heals +20
            return $"Potion used! Health is now {Player.Health}.";
        }

        return "Picked something up.";
    }

    private string Battle(Monster monster)
    {
        while (Player.IsAlive && monster.IsAlive)
        {
            int playerDamage = Player.Attack(monster);

            if (!monster.IsAlive)
                return $"You hit for {playerDamage} and defeated the monster!";

            int monsterDamage = monster.Attack(Player);

            if (!Player.IsAlive)
            {
                GameOver = true;
                Won = false;
                return $"Monster hit for {monsterDamage}. You died. GAME OVER.";
            }
        }

        return "Battle ended.";
    }
}
