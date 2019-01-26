using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaver : MonoBehaviour
{
    public CharacterData characterData;
    public MapData       mapData;
    public InventoryData inventoryData;
    string dataPath;

    void Start ()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "CharacterData.txt");
    }

    void Update ()
    {
        if(Input.GetKeyDown (KeyCode.S))
            Save (characterData, mapData, inventoryData, dataPath);

        if (Input.GetKeyDown (KeyCode.L))
            characterData = Load (characterData, mapData, inventoryData, dataPath);
    }

    static void Save (CharacterData data, string path)
    {
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
            return JsonUtility.FromJson<CharacterData> (jsonString);
        }
    }
}

