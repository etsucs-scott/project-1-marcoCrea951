using AdventureGame.Core.Characters;
namespace AdventureGame.Core.Characters;

public class Monster : ICharacter
{
    public int Health { get; private set; }
    public int CurrentHealth { get; set;}
    public int BaseDamage { get;} = 10;
    private static readonly Random _random = new();

    Random random = new Random();
    public Monster()
    {
        Health = _random.Next(30,51);
        CurrentHealth = Health;
    }
    public bool IsAlive {get
        {
            return Health > 0;
        }
    }
    public int Attack(ICharacter target)
    {
        target.TakeDamage(BaseDamage);
        return BaseDamage;
    }
    public void TakeDamage(int strength)
    {
        Health -= strength;
        if (Health < 0)
            Health = 0;
    }
    

}
