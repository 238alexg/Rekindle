using UnityEngine;

public class Entrance
{
    public Room RoomOne;
    public Room RoomTwo;
	public Vector3Int TilePosition;

	public Entrance(Room roomOne, Room roomTwo, Vector3Int tilePosition)
	{
		RoomOne = roomOne;
		RoomTwo = roomTwo;
		TilePosition = tilePosition;

		roomOne.AddEntrance(this);

		if (roomTwo == null)
		{
			int i = 0;
		}

		roomTwo.AddEntrance(this);
	}
}
