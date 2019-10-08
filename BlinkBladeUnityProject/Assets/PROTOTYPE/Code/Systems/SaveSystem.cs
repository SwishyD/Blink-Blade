using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(LevelManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levels.blink";
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadData()
    {
        string path = Application.persistentDataPath + "/levels.blink";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/levels.blink";
        File.Delete(path);
    }

    public static bool DataExists()
    {
        string path = Application.persistentDataPath + "/levels.blink";
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
