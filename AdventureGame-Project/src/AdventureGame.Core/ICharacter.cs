namespace AdventureGame.Core.Characters;

public interface ICharacter
{
    int Health { get;}
    bool IsAlive { get;}
    int Attack(ICharacter target);
    void TakeDamage(int strength);

}
