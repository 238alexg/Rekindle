using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World
{
	readonly TileFinder TileFinder;

	readonly Tilemap Walls;
	readonly Tilemap Floors;
	public HashSet<Room> Rooms = new HashSet<Room>();

	public World(TileFinder tileFinder, Tilemap wallTileMap, Tilemap floorTileMap)
	{
		TileFinder = tileFinder;
		Walls = wallTileMap;
		Floors = floorTileMap;
	}

	public void CreateRoom(Vector2Int position, Vector2Int size, Vector2Int[] Entrances)
	{
		int endTileX = position.x + size.x;
		int endTileY = position.y + size.y;

		for (int x = position.x; x < endTileX; x++)
		{
			for (int y = position.y; y < endTileY; y++)
			{
				Vector3Int currentTile = new Vector3Int(x, y, 0);

				bool onEdge = x == position.x || x == endTileX - 1 || y == position.y || y == endTileY - 1;
				bool isWall = onEdge && !Entrances.Contains((Vector2Int)currentTile);

				if (isWall)
				{
					Walls.SetTile(currentTile, TileFinder.Wall);
				}
				else
				{
					Floors.SetTile(currentTile, TileFinder.Floor);
				}
			}
		}
	}

	public void CreateKey(Vector2Int position, int id)
	{
		int keyXPosition = position.x;
		int keyYPosition = position.y;
		Vector3Int keyPlacement = new Vector3Int(keyXPosition, keyYPosition, 0);
		// Key.SetTile(KeyPlacement, TileFinder.Key);
		// TODO: SET TILE
	}


	public void CreateLever(Vector2Int position, int id)
	{
		int LeverXPosition = position.x;
		int LeverYPosition = position.y;
		Vector3Int LeverPlacement = new Vector3Int(LeverXPosition, LeverYPosition, 0);
		// Lever.SetTile(LeverPlacement, TileFinder.Lever);
		// TODO: SET TILE
	}

	public void CreateDoor(Vector2Int position, int id)
	{
		int keyXPosition = position.x;
		int keyYPosition = position.y;
		Vector3Int doorPlacement = new Vector3Int(keyXPosition, keyYPosition, 0);
		// Door.SetTile(doorPlacement, TileFinder.Door);
		// TODO: SET TILE
	}
	public void AddRoom(Room room)
	{
		if(!Rooms.Contains(room))
			Rooms.Add(room);
	}

	public void LoadFromString(string loadString)
	{
		// TODO: Populate rooms, hallways, items, etc. with string
	}

	public void CreateBetaMap()
	{
		// Create a map of rooms with boxes
		for (int i = 0; i < 3; i ++) {
			for (int j = 0; j < 8; j++) {
				// CreateRoom(new Vector2Int(4*-j, 4*-i), Vector2Int.one * 4, new Vector2Int[] {new Vector2Int(-2, 2),new Vector2Int(-10, 0),new Vector2Int(2, 5), new Vector2Int(-4, -2)});
			}
		}
		CreateDoor((new Vector2Int (0,0)), 1);
		CreateKey((new Vector2Int (2,0)), 1);
		CreateLever((new Vector2Int (2,0)), 1);

	}
}
