using UnityEngine;

public class Tile : MonoBehaviour
{
	public enum TileType
	{
		Wall,
		Door,
		Floor
	}

	public readonly int X;
	public readonly int Y;

	public readonly TileType Type;

	public Tile(TileType type, int x, int y)
	{
		X = x;
		Y = y;
	}
}
