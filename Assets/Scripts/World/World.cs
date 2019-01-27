using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR.WSA.Input;

public class World
{
	public const int TileSize = 32;

	readonly TileFinder TileFinder;

	readonly Tilemap Walls;
	readonly Tilemap Floors;
	readonly Tilemap Items;
	readonly Tilemap Obstacles;

	public readonly List<Obstacle> ObstacleList = new List<Obstacle>();
	public readonly Room[,] Rooms;

	Vector2Int ScreenSize = new Vector2Int(Screen.width, Screen.height);
	Vector2Int TileDimensions;
	Vector2Int RoomCount;
	Vector2Int RoomSize = new Vector2Int(11, 11);
	Vector2Int RoomSizeSharedWalls;
	
	public World(TileFinder tileFinder, Tilemap wallTileMap, Tilemap floorTileMap, Tilemap itemTilemap, Tilemap obstacleTilemap)
	{
		TileFinder = tileFinder;
		Walls = wallTileMap;
		Floors = floorTileMap;
		Items = itemTilemap;
		Obstacles = obstacleTilemap;

		RoomSizeSharedWalls = new Vector2Int(RoomSize.x - 1, RoomSize.y - 1);

		var availableTiles = new Vector2Int(Screen.width / TileSize, Screen.height / TileSize);
		availableTiles -= Vector2Int.one; // Account for top and right walls
		RoomCount = new Vector2Int(availableTiles.x / RoomSizeSharedWalls.x, availableTiles.y / RoomSizeSharedWalls.y);
		TileDimensions.x = RoomCount.x * RoomSizeSharedWalls.x + 1;
		TileDimensions.y = RoomCount.y * RoomSizeSharedWalls.y + 1;

		Rooms = new Room[RoomCount.x, RoomCount.y];

		CenterRoomsInCamera();
		CreateRoomGrid();

		CreateRoomEntrances();
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
		for (int x = 0; x < RoomCount.x; x++)
		{
			for (int y = 0; y < RoomCount.y; y++)
			{
				CreateRoom(new Vector2Int(x, y), new Vector2Int(x * RoomSizeSharedWalls.x, y * RoomSizeSharedWalls.y), RoomSizeSharedWalls);
			}
		}
	}

	void CreateRoomEntrances()
	{
		int entranceCount = 0;
		for (int x = 0; x < RoomCount.x; x++)
		{
			for (int y = 0; y < RoomCount.y; y++)
			{
				if (x != 0)
				{
					Vector3Int entrancePosition = new Vector3Int(x * RoomSizeSharedWalls.x, y * RoomSizeSharedWalls.y + RoomSize.y / 2, 0);
					Entrance newEntrance = new Entrance(Rooms[x, y], Rooms[x - 1, y], entrancePosition);
					Walls.SetTile(entrancePosition, null);
					Obstacles.SetTile(entrancePosition, TileFinder.Door);
					entranceCount++;
				}
				if (y != 0)
				{
					Vector3Int entrancePosition = new Vector3Int(x * RoomSizeSharedWalls.x + RoomSize.x / 2, y * RoomSizeSharedWalls.y, 0);
					Entrance newEntrance = new Entrance(Rooms[x, y], Rooms[x, y - 1], entrancePosition);
					Walls.SetTile(entrancePosition, null);
					Obstacles.SetTile(entrancePosition, TileFinder.Door);
					entranceCount++;
				}
			}
		}
	}
	
	public void CreateRoom(Vector2Int roomIndex, Vector2Int position, Vector2Int size)
	{
		int endTileX = position.x + size.x;
		int endTileY = position.y + size.y;

		for (int x = position.x; x < endTileX; x++)
		{
			for (int y = position.y; y < endTileY; y++)
			{
				Vector3Int currentTile = new Vector3Int(x, y, 0);
				
				bool onTopOrRightEdge = x == endTileX - 1 && roomIndex.x == RoomCount.x - 1 || y == endTileY - 1 && roomIndex.y == RoomCount.y - 1;
				bool onEdge = x == position.x || y == position.y;
				bool isSharedWall = onEdge;

				if (isSharedWall || onTopOrRightEdge)
				{
					Walls.SetTile(currentTile, TileFinder.Wall);
				}
				else
				{
					Floors.SetTile(currentTile, TileFinder.Floor);
				}
			}
		}
		
		Rooms[roomIndex.x, roomIndex.y] = new Room(RoomSize.x, RoomSize.y);
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
