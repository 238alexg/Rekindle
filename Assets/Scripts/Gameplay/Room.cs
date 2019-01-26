
using UnityEngine;

public class Room
{
	public readonly int Height;
	public readonly int Width;

	public readonly float HeightToWidthRatio;

	readonly Tile[] Tiles;

	public Tile this[int x, int y]
	{
		get
		{
			return Tiles[x + x * y];
		}
		set
		{
			Tiles[x + x * y] = value;
		}
	}
	
	public Room(int height, int width)
	{
		Height = height;
		Width = width;
		Tiles = new Tile[Height * Width];

		HeightToWidthRatio = (float)Height / Width;
	}
}
