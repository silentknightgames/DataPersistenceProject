using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private Text[] scoreSlots;
    [SerializeField] private Text[] nameSlots;

    private void Start()
    {
        UpdateScores();
    }

    private void UpdateScores()
    {
        for (int i = 0; i < GameManager.BestScores.Count; i++)
        {
            scoreSlots[i].text = "" + GameManager.BestScores[i];
            nameSlots[i].text = GameManager.BestPlayers[i];
        }
    }

    private void ClearSlots()
    {
        for (int i = 0; i < GameManager.BestScores.Count; i++)
        {
            scoreSlots[i].text = "0";
            nameSlots[i].text = "";
        }
    }

    public void ToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetBestScores()
    {
        ClearSlots();

        GameManager.BestPlayer = "";
        GameManager.BestScore = 0;
        GameManager.BestPlayers.Clear();
        GameManager.BestScores.Clear();

        GameManager.Instance.SaveBest();
    }
}
