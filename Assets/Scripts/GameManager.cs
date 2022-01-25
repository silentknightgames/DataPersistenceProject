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

    public static List<int> BestScores = new List<int>();
    public static List<string> BestPlayers = new List<string>();

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
    }
    #endregion

    #region SaveData
    [System.Serializable] 
    class SaveData
    {
        public string BestPlayerS;
        public int BestScoreS;
        public List<int> LeadScoresS = new List<int>() {0, 0, 0, 0, 0 };
        public List<string> LeadPlayersS = new List<string>() { "", "", "", "", "" };
    }

    public void SaveBest()
    {
        SaveData data = new SaveData();
        data.BestPlayerS = BestPlayer;
        data.BestScoreS = BestScore;

        for (int i = 0; i < BestScores.Count; i++)
        {
            data.LeadScoresS[i] = BestScores[i];
            data.LeadPlayersS[i] = BestPlayers[i];
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
        Debug.Log(json);
        Debug.Log("Saved");
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
            
            for (int i = 0; i < data.LeadScoresS.Count; i++)
            {
                BestPlayers.Add(data.LeadPlayersS[i]);
                BestScores.Add(data.LeadScoresS[i]);
            }

            Debug.Log("Data Loaded");
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

    public void UpdateLeaderboard(int score, string name)
    {
        if (BestScores.Count != 0)
        {
            for (int i = 0; i < BestScores.Count; i++)
            {
                int lastIndex = BestScores.Count - 1;

                if (score >= BestScores[i])
                {
                    BestScores.Insert(i, score);
                    BestPlayers.Insert(i, name);

                    if (BestScores.Count == 6)
                    {
                        BestScores.Remove(BestScores[lastIndex + 1]);
                        BestPlayers.Remove(BestPlayers[lastIndex + 1]);
                    }

                    break;
                }
                else if (score < BestScores[lastIndex] && BestScores.Count < 5)
                {
                    BestScores.Insert(lastIndex + 1, score);
                    BestPlayers.Insert(lastIndex + 1, name);
                    break;
                }
            }
        }
        else
        {
            BestScores.Add(score);
            BestPlayers.Add(name);
        }
    }

}
