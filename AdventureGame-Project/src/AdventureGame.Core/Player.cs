using AdventureGame.Core.Items;
namespace AdventureGame.Core.Characters;


public class Player : ICharacter
{
    public int Health { get; private set;}
    public int MaxHealth {get;} = 150;
    public int BaseDamage { get;} = 10;
    // looked up how to write the inventory, was not sure how to do it
    public List<Item> Inventory { get;} = new();
    // checks if health is above 0
    public bool IsAlive {get
        {
            return Health > 0;
        }
    }
    public Player()
    {
        //starting health
        Health = 100; 
    }

    
    public int Attack(ICharacter target)
    {
        int damage = BaseDamage + GetBestWeaponModifier();
        target.TakeDamage(damage);
        return damage;

    }
    public void TakeDamage(int strength)
    {
        Health -= strength;
        if (Health < 0)
            Health = 0;
    }
    public void AddItem(Item item)
    {
        if (item is Weapon)
        {
            Inventory.Add(item);
        }
    }

    private int GetBestWeaponModifier()
    {
        int bestModifier = 0;

        foreach (var item in Inventory)
        {
            if (item is Weapon Weapon)
            {
                if (Weapon.AttackModifier > bestModifier)
                {
                    bestModifier = Weapon.AttackModifier;
                }
            }
        }

        return bestModifier;
    }
    
    public void Heal(int amount)
{
    if (amount < 0) return;
    Health += amount;

    
}

}
    


