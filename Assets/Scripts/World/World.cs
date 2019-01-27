using System.Collections.Generic;

public static class World
{
    public static HashSet<Room> Rooms;

    public static void AddRoom(Room room)
    {
        if(!Rooms.Contains(room))
            Rooms.Add(room);
    }
}
