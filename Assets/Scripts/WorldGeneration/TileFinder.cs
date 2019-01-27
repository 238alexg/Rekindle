using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileFinder : ScriptableObject
{
	public TileBase Wall;
	public TileBase Floor;
	public Obstacle Door;

	public WallOrientations HappyStoneWalls;
	public WallOrientations SadStoneWalls;

	public ItemActivation Activated;
	public ItemActivation NotActivatedYet;

	public GateKeeping Open;
	public GateKeeping Close;
}

[CreateAssetMenu]
public class GateKeeping : ScriptableObject
{
	public enum DoorColor
	{
		Blue,
		Green,
		Red,
		Yellow
	}

	public TileBase BlueDoor;
	public TileBase GreenDoor;
	public TileBase RedDoor;
	public TileBase YellowDoor;
	public TileBase Open; 

	public TileBase GetDoor(DoorColor color)
	{
		switch(color)
		{
			case DoorColor.Blue:
				return BlueDoor;
			case DoorColor.Green:
				return GreenDoor;
			case DoorColor.Red:
				return RedDoor;
			case DoorColor.Yellow:
				return YellowDoor;
			default:
				return null;
		}
	}


}
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

[CreateAssetMenu]
public class WallOrientations : ScriptableObject
{
	public enum WallDirection
	{
		North,
		Northwest,
		Northeast,
		West,
		East,
		South,
		Southwest,
		Southeast
	}

	public TileBase NorthFacingWall;
	public TileBase NorthwestFacingWall;
	public TileBase NortheastFacingWall;
	public TileBase WestFacingWall;
	public TileBase EastFacingWall;
	public TileBase SouthFacingWall;
	public TileBase SouthwestFacingWall;
	public TileBase SoutheastFacingWall;

	public TileBase GetWall(WallDirection direction)
	{
		switch (direction)
		{
			case WallDirection.North:
				return NorthFacingWall;
			case WallDirection.Northwest:
				return NorthwestFacingWall;
			case WallDirection.Northeast:
				return NortheastFacingWall;
			case WallDirection.West:
				return WestFacingWall;
			case WallDirection.East:
				return EastFacingWall;
			case WallDirection.South:
				return SouthFacingWall;
			case WallDirection.Southwest:
				return SouthwestFacingWall;
			case WallDirection.Southeast:
				return SoutheastFacingWall;
			default:
				return null;
		}
	}
}