using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

    public int Vibration;
    public int BestScore;
    public string SkinType;

    private void Awake()
    {
        Load();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    class SaveManager
    {
        public int Vibration;
        public int BestScore;
        public string SkinType;
    }


    public void Save()
    {
        SaveManager data = new SaveManager();

        data.Vibration = Vibration;
        data.BestScore = BestScore;
        data.SkinType = SkinType;

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
                Vibration = data.Vibration;
                BestScore = data.BestScore;
                SkinType = data.SkinType;
            }
        }
    }
}
