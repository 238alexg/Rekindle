using UnityEngine;
public class Item
{
    public enum ItemType
    {
        ButtonBlue,
        ButtonGreen,
        ButtonRed,
        ButtonStone,
        ButtonBluePressed,
        ButtonGreenPressed,
        ButtonRedPressed,
        ButtonStonePressed,
        Lever,
        LeverActivated,
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
