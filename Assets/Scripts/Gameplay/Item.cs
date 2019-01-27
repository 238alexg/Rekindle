using UnityEngine;

public class Item : WorldEnity
{
    public enum ItemType
    {
        Lever,
        Key
    }
    public readonly int X;
    public readonly int Y;

    public readonly ItemType Type;

    public Item(ItemType type, int x, int y)
    {
        X = x;
        Y = y;
        Type = type;
    }
}
