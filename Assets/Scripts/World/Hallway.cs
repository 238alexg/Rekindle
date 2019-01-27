using System.Collections.Generic;

public class Hallway : Space
{
    public HashSet<Room> Rooms;
    public Room StartRoom;
	public int Length;

    public void entryWay(Room startRoom, int length)
    {
        StartRoom = startRoom;
        Length = length;
    }
}
