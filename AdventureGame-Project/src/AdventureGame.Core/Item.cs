namespace AdventureGame.Core.Items;

public abstract class Item
{
    public string Name { get; }
    public string PickupMessage { get; }

    protected Item(string name, string pickupMessage)
    {
        Name = name;
        PickupMessage = pickupMessage;
    }
}
