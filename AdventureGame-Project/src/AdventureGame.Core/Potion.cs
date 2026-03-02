namespace AdventureGame.Core.Items;

using System.Security;
using AdventureGame.Core.Characters;

public class Potion : Item
{
    public int HealAmount {get;}

    public Potion(string name, string pickupMessage, int healAmount)
        : base(name, pickupMessage)
    {
        HealAmount = healAmount;

    }
    
    public void Use(Player player)
    {
        player.Heal(HealAmount);
    }

}
