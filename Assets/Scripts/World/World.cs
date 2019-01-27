using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World
{
	public const int TileSize = 32;

	readonly TileFinder TileFinder;

	readonly Tilemap Walls;
	readonly Tilemap Floors;
	readonly Tilemap Items;
	readonly Tilemap Obstacles;

	public readonly List<Obstacle> ObstacleList = new List<Obstacle>();
	public readonly HashSet<Room> Rooms = new HashSet<Room>();

	Vector2Int ScreenSize = new Vector2Int(Screen.width, Screen.height);
	Vector2Int TileDimensions;
	Vector2Int RoomCount;
	Vector2Int RoomSize = new Vector2Int(7, 7);
	
	public World(TileFinder tileFinder, Tilemap wallTileMap, Tilemap floorTileMap, Tilemap itemTilemap, Tilemap obstacleTilemap)
	{
		TileFinder = tileFinder;
		Walls = wallTileMap;
		Floors = floorTileMap;
		Items = itemTilemap;
		Obstacles = obstacleTilemap;

		var availableTiles = new Vector2Int(Screen.width / TileSize, Screen.height / TileSize);
		RoomCount = new Vector2Int(availableTiles.x / RoomSize.x, availableTiles.y / RoomSize.y);
		TileDimensions.x = RoomCount.x * RoomSize.x;
		TileDimensions.y = RoomCount.y * RoomSize.y;

		CenterRoomsInCamera();
		CreateRoomGrid();
	}

	void CenterRoomsInCamera()
	{
		var camera = Camera.main;
		var cameraPos = camera.transform.position;
		cameraPos.x += ScreenSize.x / 2;
		cameraPos.y += ScreenSize.y / 2;

		if (RoomCount.x * RoomSize.x * TileSize < Screen.width)
		{
			cameraPos.x -= (Screen.width - RoomCount.x * RoomSize.x * TileSize) / 2;
		}
		if (RoomCount.y * RoomSize.y * TileSize < Screen.height)
		{
			cameraPos.y -= (Screen.height - RoomCount.y * RoomSize.y * TileSize) / 2;
		}

		camera.transform.position = cameraPos;
	}

	void CreateRoomGrid()
	{
		List<Vector2Int> entrances = new List<Vector2Int>(4);
		for (int x = 0; x < RoomCount.x; x++)
		{
			for (int y = 0; y < RoomCount.y; y++)
			{
				entrances.Clear();
				if (x != 0)
				{
					entrances.Add(new Vector2Int(x * RoomSize.x, y * RoomSize.y + RoomSize.y / 2));
				}
				if (x != RoomCount.x - 1)
				{
					entrances.Add(new Vector2Int((x + 1) * RoomSize.x - 1, y * RoomSize.y + RoomSize.y / 2));
				}
				if (y != 0)
				{
					entrances.Add(new Vector2Int(x * RoomSize.x + RoomSize.x / 2, y * RoomSize.y));
				}
				if (y != RoomCount.y - 1)
				{
					entrances.Add(new Vector2Int(x * RoomSize.x + RoomSize.x / 2, (y + 1) * RoomSize.y - 1));
				}
				CreateRoom(new Vector2Int(x * RoomSize.x, y * RoomSize.y), RoomSize, entrances);
			}
		}
	}
	
	public void CreateRoom(Vector2Int position, Vector2Int size, List<Vector2Int> Entrances)
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
					//Obstacles.SetTile(currentTile, TileFinder.Door);
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
