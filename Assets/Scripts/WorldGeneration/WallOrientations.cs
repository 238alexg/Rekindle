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