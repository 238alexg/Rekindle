    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    public static class JsonSaver
    {
    static void Save (string path = null, List<Vector2> characterData = null, List<Room> mapData = null)
    {
        string characterDataStringified = characterData.ToString();
        string mapDataStringified = mapData.ToString();
        string data = mapDataStringified + "+" + mapDataStringified; 
        string jsonString = JsonUtility.ToJson (data);
        if (path == null) {
            path = Path.Combine(UnityEngine.Application.persistentDataPath, "save.txt");
        }
        using (StreamWriter streamWriter = File.CreateText (path))
        {
            streamWriter.Write (jsonString);
        }
    }

    public static string Load (string path)
    {
        using (StreamReader streamReader = File.OpenText (path))
        {
            return streamReader.ReadToEnd ();
        }
    }
}
