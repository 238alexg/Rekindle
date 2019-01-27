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

	public TileBase KeyBlue;
	public TileBase KeyGreen;
	public TileBase KeyRed;
	public TileBase KeyYellow;

	public TileBase ButtonBlue;
	public TileBase ButtonGreen;
	public TileBase ButtonRed;
	public TileBase ButtonStone;
	public TileBase ButtonYellow;

	public TileBase Lever;
	public TileBase Torch;

	public TileBase GetItem(ItemType type)
	{
		switch(type)
		{
			case ButtoBlue:
				return ButtonBlue;
				break;
			case ButtonGreen:
				return ButtonGreen;
				break;
			case ButtonRed:
				return ButtonRed;
				break;
			case ButtonStone:
				return ButtonStone;
				break;
			case ButtonYellow:
				return ButtonYellow;
				break;
			case KeyBlue:
				return KeyBlue;
				break;
			case KeyGreen:
				return KeyGreen;
				break;
			case KeyRed:
				return KeyRed;
				break;
			case KeyYellow:
				return KeyYellow;
				break;
			case Lever:
				return Lever;
				break;
			case Torch:
				return Torch;
				break;
		}
	}
}

class WallOrientations : ScriptableObject
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
		SouthEast
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
			case North:
				return NorthFacingWall;
				break;
			case Northwest:
				return NorthwestFacingWall;
				break;
			case Northeast:
				return NortheastFacingWall;
				break;
			case West:
				return WestFacingWall;
				break;
			case East:
				return EastFacingWall;
				break;
			case South:
				return SouthFacingWall;
				break;
			case Southwest:
				return SouthwestFacingWall;
				break;
			case Southeast:
				return SoutheastFacingWall;
				break;
		}
	}
}