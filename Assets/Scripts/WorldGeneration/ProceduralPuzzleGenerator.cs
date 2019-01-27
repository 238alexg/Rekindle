using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralPuzzleGenerator
{
	List<Room> P1VisitedRooms = new List<Room>();
	List<Room> P2VisitedRooms = new List<Room>();

	List<Room> UnvisitedRooms = new List<Room>();

	Vector2Int P1StartingPos;
	Vector2Int P2StartingPos;
	Vector2Int FinalRoom;


	Room DestinationRoom;

	World World;

	public void GeneratePuzzle(World world)
	{
		World = world;

		P1StartingPos = new Vector2Int(0, World.RoomCount.y / 2);
		P2StartingPos = new Vector2Int(World.RoomCount.x - 1, World.RoomCount.y / 2);
		FinalRoom = new Vector2Int(World.RoomCount.x / 2, World.RoomCount.y / 2);

		foreach (var worldRoom in World.Rooms)
		{
			UnvisitedRooms.Add(worldRoom);
		}

		Room p1Start = World.Rooms[P1StartingPos.x, P1StartingPos.y];
		Room p2Start = World.Rooms[P2StartingPos.x, P2StartingPos.y];

		Application.Inst.PlayerOne.transform.position = new Vector3((P1StartingPos.x + 0.5f) * World.RoomSize.x * World.TileSize, P1StartingPos.y * World.RoomSize.y * World.TileSize, -20);
		Application.Inst.PlayerTwo.transform.position = new Vector3((P2StartingPos.x - 0.5f) * World.RoomSize.x * World.TileSize, P2StartingPos.y * World.RoomSize.y * World.TileSize, -20);

		DestinationRoom = World.Rooms[FinalRoom.x, FinalRoom.y];

		UnvisitedRooms.Remove(p1Start);
		UnvisitedRooms.Remove(p2Start);
		P1VisitedRooms.Add(p1Start);
		P2VisitedRooms.Add(p2Start);

		Debug.LogWarning("Start: " + p1Start.RoomIndex);

		bool successP1Walk = false;
		int tries = 1000;
		while (!successP1Walk)
		{
			successP1Walk = WalkPlayer(p1Start, P1VisitedRooms);
			if (tries-- == 0)
			{
				Debug.LogError("Failed to make a valid puzzle");
				return;
			}
		}

		bool successP2Walk = false;
		while (!successP2Walk)
		{
			successP2Walk = WalkPlayer(p2Start, P2VisitedRooms);
			if (tries-- == 0)
			{
				Debug.LogError("Failed to make a valid puzzle");
				return;
			}
		}

		Debug.LogWarning("Successful walks!");
	}

	bool WalkPlayer(Room startingRoom, List<Room> VisitedRooms)
	{
		Room currentRoom = startingRoom;
		VisitedRooms.RemoveRange(1, VisitedRooms.Count - 1);
		while (currentRoom != DestinationRoom)
		{
			Room neighborRoom = GetRandomUntraveledNeighborRoom(startingRoom);

			if (neighborRoom == null)
			{
				// Just fail and try again
				return false;
			}

			Debug.LogWarning(neighborRoom.RoomIndex);
			
			VisitedRooms.Add(neighborRoom);
			currentRoom = neighborRoom;
		}

		return true;
	}

	List<int> TriedNeighbors = new List<int>(4);
	Room GetRandomUntraveledNeighborRoom(Room room)
	{
		TriedNeighbors.Clear();
		int tries = room.Entrances.Count;
		int indexTries = 100;

		while (tries-- > 0)
		{
			int index = Random.Range(0, room.Entrances.Count);
			if (TriedNeighbors.Contains(index))
			{
				if (indexTries-- == 0)
				{
					return null;
				}
			}

			Room otherRoom = room.Entrances[index].RoomTwo == room
				? room.Entrances[index].RoomOne
				: room.Entrances[index].RoomTwo;
			
			if (otherRoom != DestinationRoom && UnvisitedRooms.Contains(otherRoom))
			{
				UnvisitedRooms.Remove(otherRoom);
				return otherRoom;
			}
		}

		return null;
	}
}
