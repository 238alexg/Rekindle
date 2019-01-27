using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.LowLevel;
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
	public readonly Room[,] Rooms;

	Vector2Int ScreenSize = new Vector2Int(Screen.width, Screen.height);
	public Vector2Int TileDimensions;
	public Vector2Int RoomCount;
	public Vector2Int RoomSize = new Vector2Int(9, 9);
	public Vector2Int RoomSizeSharedWalls;
	
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
		CreateHardcodedPuzzle();
	}

	void CenterRoomsInCamera()
	{
		var camera = Camera.main;
		var cameraPos = camera.transform.position;
		cameraPos.x += ScreenSize.x / 2;
		cameraPos.y += ScreenSize.y / 2;

		if ((RoomCount.x * RoomSizeSharedWalls.x + 1) * TileSize < Screen.width)
		{
			cameraPos.x += (Screen.width - RoomCount.x * RoomSize.x * TileSize);
		}
		if ((RoomCount.y * RoomSizeSharedWalls.y + 1) * TileSize < Screen.height)
		{
			cameraPos.y += (Screen.height - RoomCount.y * RoomSize.y * TileSize);
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

					Floors.SetTile(entrancePosition, TileFinder.Floor);
					entranceCount++;
				}
				if (y != 0)
				{
					Vector3Int entrancePosition = new Vector3Int(x * RoomSizeSharedWalls.x + RoomSize.x / 2, y * RoomSizeSharedWalls.y, 0);
					Entrance newEntrance = new Entrance(Rooms[x, y], Rooms[x, y - 1], entrancePosition);
					Walls.SetTile(entrancePosition, null);

					Floors.SetTile(entrancePosition, TileFinder.Floor);
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
		
		Rooms[roomIndex.x, roomIndex.y] = new Room(RoomSize.x, RoomSize.y, roomIndex);
	}

	void CreateHardcodedPuzzle()
	{
		Vector2Int P1StartingPos = new Vector2Int(0, RoomCount.y / 2);
		Vector2Int P2StartingPos = new Vector2Int(RoomCount.x - 1, RoomCount.y / 2);

		Application.Inst.PlayerOne.transform.position = new Vector3((P1StartingPos.x + 0.5f) * RoomSize.x * World.TileSize, P1StartingPos.y * RoomSize.y * TileSize, -20);
		Application.Inst.PlayerTwo.transform.position = new Vector3((P2StartingPos.x - 0.5f) * RoomSize.x * World.TileSize, P2StartingPos.y * RoomSize.y * TileSize, -20);

		Item key1 = new Item(Item.ItemType.KeyRed, 0);

		Application.Inst.PlayerOne.Inventory.Add(key1);

		AddDoor(Rooms[0, 1].Entrances[0].TilePosition, key1);
	}

	void AddDoor(Vector3Int position, Item key)
	{
		Obstacles.SetTile(Rooms[0, 1].Entrances[0].TilePosition, TileFinder.Door);
		var door = Obstacles.GetTile<Obstacle>(position);
		door.Position = position;
		door.Solution = key;
		door.ObstacleLockChangedEvent += OnObstacleLockChanged;
		ObstacleList.Add(door);
	}

	void AddWall(Vector3Int position)
	{

	}

	void OnObstacleLockChanged(Obstacle obstacle)
	{
		if (obstacle.Unlocked)
		{
			Obstacles.SetTile(obstacle.Position, null);
			Floors.SetTile(obstacle.Position, TileFinder.Open.Open);
		}
		else
		{
			Obstacles.SetTile(obstacle.Position, TileFinder.Door);
			Floors.SetTile(obstacle.Position, null);
		}
	}

	public void CreateKey(Vector3Int position, int id)
	{

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
