using UnityEngine;
public class Item : Entity
{
    public enum ItemType
    {
        Lever,
        Key
    }

    public readonly ItemType Type;
    public readonly int Id;

    public Item(ItemType type, int x, int y, int id)
    {
        Position.x = x;
        Position.y = y;
        Type = type;
        Id = id;
    }
}
