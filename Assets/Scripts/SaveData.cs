using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private SaveData() { }
    public static SaveData instance;

    public int vibration;
    public int bestScore;
    public string skinType;

    private void Awake()
    {
        Load();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    class SaveManager
    {
        public int vibration;
        public int bestScore;
        public string skinType;
    }


    public void Save()
    {
        SaveManager data = new SaveManager();

        data.vibration = vibration;
        data.bestScore = bestScore;
        data.skinType = skinType;

        string json = JsonUtility.ToJson(data);
        BinaryFormatter bFormatter = new BinaryFormatter();

        using (Stream output = File.Create(Application.persistentDataPath + "/savefile.dat"))
        {
            bFormatter.Serialize(output, json);
        }
    }

    public void Load()
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.dat";

        if (File.Exists(path))
        {
            using (Stream input = File.OpenRead(path))
            {
                string json = (string)bFormatter.Deserialize(input);
                SaveManager data = JsonUtility.FromJson<SaveManager>(json);
                vibration = data.vibration;
                bestScore = data.bestScore;
                skinType = data.skinType;
            }
        }
    }
}
