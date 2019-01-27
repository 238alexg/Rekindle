using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class ItemActivation : ScriptableObject
{
	public TileBase ButtonBlue;
	public TileBase ButtonGreen;
	public TileBase ButtonRed;
	public TileBase ButtonStone;
	public TileBase ButtonBluePressed;
	public TileBase ButtonGreenPressed;
	public TileBase ButtonRedPressed; 
	public TileBase ButtonStonePressed;
	public TileBase Lever;
	public TileBase LeverActivated;
	public TileBase Torch;
	public TileBase KeyBlue;
	public TileBase KeyGreen;
	public TileBase KeyRed;
	public TileBase KeyYellow;

	public TileBase GetItem(Item.ItemType type)
	{
		switch(type)
		{
			case Item.ItemType.ButtonBlue:
				return ButtonBlue;
			case Item.ItemType.ButtonGreen:
				return ButtonGreen;
			case Item.ItemType.ButtonRed:
				return ButtonRed;
			case Item.ItemType.ButtonStone:
				return ButtonStone;
			case Item.ItemType.ButtonBluePressed:
				return ButtonBluePressed;
			case Item.ItemType.ButtonGreenPressed:
				return ButtonGreenPressed;
			case Item.ItemType.ButtonRedPressed:
				return ButtonRedPressed;
			case Item.ItemType.ButtonStonePressed:
				return ButtonStonePressed;
			case Item.ItemType.KeyBlue:
				return KeyBlue;
			case Item.ItemType.KeyGreen:
				return KeyGreen;
			case Item.ItemType.KeyRed:
				return KeyRed;
			case Item.ItemType.KeyYellow:
				return KeyYellow;
			case Item.ItemType.Lever:
				return Lever;
			case Item.ItemType.LeverActivated:
				return LeverActivated;
			case Item.ItemType.Torch:
				return Torch;
			default:
				return null;
		}
	}
}
