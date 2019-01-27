using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World
{
	readonly Tilemap Walls;
	readonly Tilemap Floors;
	public HashSet<Room> Rooms = new HashSet<Room>();

	public World(Tilemap wallTileMap, Tilemap floorTileMap)
	{
		Walls = wallTileMap;
		Floors = floorTileMap;
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
}
