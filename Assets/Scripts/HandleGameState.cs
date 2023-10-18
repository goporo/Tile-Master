using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleGameState : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void EnableMenu()
    {
        gameObject.SetActive(true);
    }
    public void EnableWonMenu()
    {
        gameObject.SetActive(true);
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
