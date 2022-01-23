using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public string PlayerName { get; set; }
    public static int BestScore { get; set; } = 0;
    public static string BestPlayer { get; set; }

    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBest();
        Debug.Log("Data Loaded");
    }
    #endregion

    #region SaveData
    [System.Serializable] 
    class SaveData
    {
        public string BestPlayerS;
        public int BestScoreS;
    }

    public void SaveBest()
    {
        SaveData data = new SaveData();
        data.BestPlayerS = BestPlayer;
        data.BestScoreS = BestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void LoadBest()
    {
        string path = Application.persistentDataPath + "savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPlayer = data.BestPlayerS;
            BestScore = data.BestScoreS;
        }
        else
        {
            Debug.LogWarning("No Saved Data");
        }
    }
    #endregion

    public void ShowBestScore(Text bestText)
    {
        bestText.text = $"Best Score: {BestPlayer} - {BestScore}";

    }

}
