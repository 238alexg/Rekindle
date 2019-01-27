using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaver 
{
    public List<Vector2> characterData;
    public List<Room> mapData;
    string dataPath;

    void Start ()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "save.txt");
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

    static CharacterData Load (string path)
    {
        using (StreamReader streamReader = File.OpenText (path))
        {
            string jsonString = streamReader.ReadToEnd ();
            int dataSeparator = jsonString.IndexOf("+", 0);
            // TODO: split string into different data type
            // turn string back into List
            // return the list
            // return JsonUtility.FromJson<CharacterData> (jsonString);
        }
    }
}

