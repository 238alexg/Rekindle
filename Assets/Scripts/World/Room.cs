using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public readonly int Width, Height;
	public bool Explored;
	
	public Item Item;
	List<Entrance> Entrances;

	public Room(int width, int height)
	{
		Width = width;
		Height = height;
		Explored = false;
		Entrances = new List<Entrance>(4);
	}

	public void AddEntrance(Entrance entrance)
	{
		Entrances.Add(entrance);
	}
}
