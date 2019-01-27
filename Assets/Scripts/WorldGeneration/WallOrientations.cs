using UnityEngine;
using UnityEngine.Tilemaps;

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
		Southeast,
        InnerNorthWest,
        InnerNorthEast,
        InnerSouthEast,
        InnerSouthWest,
        UpperLeftCorner,
        UpperRightCorner,
        LowerRightCorner,
        LowerLeftCorner
	}

	public Tilebase  NorthFacingWall;
	public Tilebase  NorthwestFacingWall;
	public Tilebase  NortheastFacingWall;
	public Tilebase  WestFacingWall;
	public Tilebase  EastFacingWall;
	public Tilebase  SouthFacingWall;
	public Tilebase  SouthwestFacingWall;
	public Tilebase  SoutheastFacingWall;

    public Tilebase InnerNorthWestFacingWall;
    public Tilebase InnerNorthEastFacingWall;
    public Tilebase InnerSouthEastFacingWall;
    public Tilebase InnerSouthWestFacingWall;

    public Tilebase UpperLeftCorner;
    public Tilebase UpperRightCorner;
    public Tilebase LowerRightCorner;
    public Tilebase LowerLeftCorner;

	public Tilebase  GetWall(WallDirection direction)
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
            case WallDirection.InnerNorthWest:
                return InnerNorthWestFacingWall;
            case WallDirection.InnerNorthEast:
                return InnerNorthEastFacingWall;
            case WallDirection.InnerSouthEast:
                return InnerSouthEastFacingWall;
            case WallDirection.InnerSouthWest:
                return InnerSouthWestFacingWall;
            case WallDirection.UpperLeftCorner:
                return UpperLeftCorner;
            case WallDirection.UpperRightCorner:
                return UpperRightCorner;
            case WallDirection.LowerRightCorner:
                return LowerRightCorner;
            case WallDirection.LowerLeftCorner:
                return LowerLeftCorner;
            default:
				return null;
		}
	}
}