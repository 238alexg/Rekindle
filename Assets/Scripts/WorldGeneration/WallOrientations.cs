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

	public TileBase NorthFacingWall;
	public TileBase NorthwestFacingWall;
	public TileBase NortheastFacingWall;
	public TileBase WestFacingWall;
	public TileBase EastFacingWall;
	public TileBase SouthFacingWall;
	public TileBase SouthwestFacingWall;
	public TileBase SoutheastFacingWall;

    public TileBase InnerNorthWestFacingWall;
    public TileBase InnerNorthEastFacingWall;
    public TileBase InnerSouthEastFacingWall;
    public TileBase InnerSouthWestFacingWall;

    public TileBase UpperLeftCorner;
    public TileBase UpperRightCorner;
    public TileBase LowerRightCorner;
    public TileBase LowerLeftCorner;

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