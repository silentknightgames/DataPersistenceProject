using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private Text bestScoreText;
    [SerializeField] private TMP_InputField nameField;

    private void Start()
    {
        GameManager.Instance.ShowBestScore(bestScoreText);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ConfirmName()
    {
        GameManager.Instance.PlayerName = nameField.text;
    }

    public void ToLeaderboard()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitApp()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
