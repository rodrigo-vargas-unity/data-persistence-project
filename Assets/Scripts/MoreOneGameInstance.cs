using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MoreOneGameInstance : MonoBehaviour
{

    public static MoreOneGameInstance Instance;

    public List<Player> Players;
    public Player Player = new Player();

    private void Awake()
    {
        Debug.Log("Awakening application!");

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighscores();

        var HighScoreText = GameObject.Find("MenuCanvas").GetComponent<MenuUIHandler>().HighScoreText.GetComponent<Text>();

        var highScorePlayer = MoreOneGameInstance.Instance.Players.OrderByDescending(x => x.Score).FirstOrDefault();

        if (highScorePlayer == null)
            return;

        HighScoreText.text = $"Best Score : {highScorePlayer.Name} : {highScorePlayer.Score}";
    }

    public void SaveHighscores()
    {
        SaveData data = new SaveData();
        data.Players = Players;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighscores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Players = data.Players;
        }
    }
}

[Serializable]
public class SaveData
{
    public List<Player> Players;
}

[Serializable]
public class Player
{
    public string Name;
    public int Score;
}