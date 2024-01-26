using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int score;

    public bool paused;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            //If the gamemanager object exists and does not equal the current instance, then delete the one that exists.
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Won't destroy the GameManager which will help keep track of score. 
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            togglePauseGame();
        }
    }

    public void togglePauseGame()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        GameUI.instance.TogglePauseScreen(paused);
    }
    public void AddScore(int scoreAdd)
    {
        score += scoreAdd;
    }

    public void LevelEnd()
    {
        Debug.Log("BUILD INDEX" + SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Scene COUNT BUILD SETTING" + SceneManager.sceneCountInBuildSettings);
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex+1)
        {
            //
            WinGame();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public void WinGame()
    {
        GameUI.instance.setEndScreen(true);
        Time.timeScale = 0.0f;
    }

    public void LoseGame()
    {
        GameUI.instance.setEndScreen(false);
        Time.timeScale = 0.0f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
