using System.Collections.Generic;

public class Room : Space
{
    public HashSet<Hallway> Hallways;
    public int Width, Height;
}
