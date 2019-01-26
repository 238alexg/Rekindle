using UnityEngine;

public static class World
{
    public static int Width, Height;
    public static object[,] WorldTiles = new object[Width, Height];

    public static void PlaceObjectAtLocation(object objectToPlace, int x, int y)
    {
        WorldTiles[x, y] = objectToPlace;
    }

    public static void PlaceObjectAtLocation(object objectToPlace, Vector2Int location)
    {
        PlaceObjectAtLocation(objectToPlace, location.x, location.y);
    }

    public static void PlaceObjectAtLocation(object objectToPlace, Vector2 location)
    {
        var intLocation = Vector2Int.FloorToInt(location);
        PlaceObjectAtLocation(objectToPlace, intLocation);
    }

    public static object GetObjectAtLocation(int x, int y)
    {
        return WorldTiles[x, y];
    }

    public static object GetObjectAtLocation(Vector2Int location)
    {
        return GetObjectAtLocation(location.x, location.y);
    }

    public static object GetObjectAtLocation(Vector2 location)
    {
        var intLocation = Vector2Int.FloorToInt(location);
        return GetObjectAtLocation(intLocation);
    }
}
