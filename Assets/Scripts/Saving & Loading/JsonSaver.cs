using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaver 
{
    // public List<Vector2> characterData;
    // public List<Room> mapData;
    // string dataPath;

    void Start ()
    {
        string dataPath = Path.Combine(UnityEngine.Application.persistentDataPath, "save.txt");
    }

    static void Save (string path, List<Vector2> characterData = null, List<Room> mapData = null)
    {
        string characterDataStringified = characterData.ToString();
        string mapDataStringified = mapData.ToString();
        string data = mapDataStringified + "+" + mapDataStringified; 
        string jsonString = JsonUtility.ToJson (data);

        using (StreamWriter streamWriter = File.CreateText (path))
        {
            streamWriter.Write (jsonString);
        }
    }

    static string Load (string path)
    {
        using (StreamReader streamReader = File.OpenText (path))
        {
            string jsonString = streamReader.ReadToEnd ();
            int dataSeparator = jsonString.IndexOf("+", 0);
            string mapData = jsonString.Substring (0, dataSeparator);
            string characterData = jsonString.Substring (dataSeparator, jsonString.Length - dataSeparator);

            return mapData;
        }
    }
}

