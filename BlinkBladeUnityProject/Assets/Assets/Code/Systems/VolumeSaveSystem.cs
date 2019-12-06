using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class VolumeSaveSystem
{
    public static void SaveVolume(VolumeSettings volumeManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/levelsvolume.blink";
        FileStream stream = new FileStream(path, FileMode.Create);

        VolumeData data = new VolumeData(volumeManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static VolumeData LoadVolume()
    {
        string path = Application.persistentDataPath + "/levelsvolume.blink";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            VolumeData data = formatter.Deserialize(stream) as VolumeData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in" + path);
            return null;
        }
    }

    public static void DeleteSave()
    {
        string path = Application.persistentDataPath + "/levelsvolume.blink";
        File.Delete(path);
    }

    public static bool DataExists()
    {
        string path = Application.persistentDataPath + "/levelsvolume.blink";
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
