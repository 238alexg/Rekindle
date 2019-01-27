public class Item : Entity
{
    public enum ItemType
    {
        Lever,
        Key
    }

    public readonly ItemType Type;

    public Item(ItemType type, int x, int y)
    {
        Position.x = x;
        Position.y = y;
        Type = type;
    }
}
