using AdventureGame.Core.Characters;
using AdventureGame.Core.Items;

namespace AdventureGame.Core.Maze;

public class Tile
{
    // using ? because sometimes monster needs to be null etc.
    public TileType Type { get;set;} = TileType.Empty;

    public Monster? Monster {get; set;}
    public Item? Item {get; set;}

    public bool HasMonster => Monster != null;
    public bool HasItem => Item != null;
}