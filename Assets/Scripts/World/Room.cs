using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public readonly int Width, Height;
	public bool Explored;
	
	public Item Item;
	public List<Entrance> Entrances;
	public Vector2Int RoomIndex;

	public Room(int width, int height, Vector2Int roomIndex)
	{
		Width = width;
		Height = height;
		RoomIndex = roomIndex;
		Explored = false;
		Entrances = new List<Entrance>(4);
	}

	public void AddEntrance(Entrance entrance)
	{
		Entrances.Add(entrance);
	}
}
