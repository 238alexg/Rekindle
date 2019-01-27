using UnityEngine;
public class Item : Entity
{
    public enum ItemType
    {
        ButtonBlue,
        ButtonGreen,
        ButtonRed,
        ButtonStone,
        ButtonYellow,
        Lever,
        KeyBlue,
        KeyGreen,
        KeyRed,
        KeyYellow,
        Torch
    }

    public readonly ItemType Type;
    public readonly int Id;

    public Item(ItemType type, int id)
    {
        Type = type;
        Id = id;
    }
}
