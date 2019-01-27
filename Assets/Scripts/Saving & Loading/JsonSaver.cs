using System.Collections.Generic;
using System.IO;
using UnityEngine;
public static class JsonSaver
{
    static void Save (string path = null, List<Vector2> characterData = null, HashSet<Space> mapData = null)
    {
        string characterString = JsonUtility.ToJson(characterData);
        string mapdataString = GetMapData(mapData);

        if (path == null) 
        {
            path = Path.Combine(UnityEngine.Application.persistentDataPath, "save.txt");
        }

        using (StreamWriter streamWriter = File.CreateText (path))
        {
            streamWriter.Write (mapdataString + "+" + characterString);
        }
    }

    static string GetMapData(HashSet<Space> spaces)
    {
        string mapString = "[";

        foreach(Space space in spaces)
        {
            mapString += JsonUtility.ToJson(space);
        }
        return  mapString + "]";
    }

    public static string Load (string path)
    {
        using (StreamReader streamReader = File.OpenText (path))
        {
            return streamReader.ReadToEnd ();
        }
    }
}

