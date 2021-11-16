using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;


public class GameStatus : MonoBehaviour
{


    public static GameStatus status;

    public string currentLevel;

    public float volume;

    public AudioMixer mixer;

    public int resWidth;
    public int resHeight;
    public int resValue;




    // Start is called before the first frame update
    void Awake()
    {
        if (status == null)
        {
            DontDestroyOnLoad(gameObject);
            status = this;
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        //data.currentLevel = currentLevel;
        data.volume = volume;
        data.resWidth = resWidth;
        data.resHeight = resHeight;
        data.resValue = resValue;
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            volume = data.volume;
            resWidth = data.resWidth;
            resHeight = data.resHeight;
            resValue = data.resValue;
            Screen.SetResolution(resWidth, resHeight, true);
            mixer.SetFloat("MusicVol", volume);
            //currentLevel = data.currentLevel;
            
        }
    }
}
[Serializable]
class PlayerData
{
    public float volume;
    public string currentLevel;
    public int resWidth;
    public int resHeight;
    public int resValue;
}
