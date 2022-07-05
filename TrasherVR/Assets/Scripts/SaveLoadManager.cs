using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;
    private GameData savedGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public bool CheckSavedGame()
    {
        savedGame = LoadGame();
        if (savedGame != null)
        {
            return true;
        }
        return false;
    }

    public void SaveSettings(SettingsData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.stx";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public SettingsData LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.stx";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            return data;
        }
        else
        {
            return new SettingsData() 
            { 
                soundOn = true, 
                volume = 10f 
            };
        }
    }

    public void SaveGame(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/save.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            return data;
        }
        else
        {
            return null;
        }
    }

    public void RemoveGame()
    {
        string path = Application.persistentDataPath + "/save.sav";
        if(File.Exists(path))
        {
            File.Delete(path);
        }    
    }

    [System.Serializable]
    public class SettingsData
    {
        public bool soundOn;
        public float volume;
    }

    [System.Serializable]
    public class GameData
    {
        public int level;
        public int missed;
        public int caught;
    }
}
