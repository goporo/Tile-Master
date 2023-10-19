using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleGameState : MonoBehaviour
{
    [SerializeField] TMP_Text levelStage;
    private void Start()
    {
        Application.targetFrameRate = 60;
        if (levelStage != null)
        {
            int levelNumber = PlayerPrefs.GetInt("CurrentLevelIndex", 0) + 1;
            levelStage.text = levelNumber.ToString();
        }
    }
    public void EnableMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void DisableMenu()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void EnableWonMenu()
    {
        gameObject.SetActive(true);

        // limit max level to 3
        int nextLevel = PlayerPrefs.GetInt("CurrentLevelIndex", 0) + 1;
        if (nextLevel > 2) nextLevel = 0;
        PlayerPrefs.SetInt("CurrentLevelIndex", nextLevel);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

}
