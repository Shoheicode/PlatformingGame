using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public GameObject endScreen;
    public TextMeshProUGUI endScreenHeader;
    public TextMeshProUGUI endScreenText;
    public static GameUI instance;

    public GameObject pauseScreen;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameManager.instance.score;
    }

    public void setEndScreen(bool hasWon)
    {
        endScreen.SetActive(true);

        endScreenText.text = "<b> Score: </b>\n" + GameManager.instance.score;
        
        if(hasWon)
        {
            endScreenHeader.text = "You Win";
            endScreenHeader.color = Color.green;
        }
        else
        {
            endScreenHeader.text = "Game Over";
            endScreenHeader.color = Color.red;
        }

    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(1);
        GameManager.instance.score = 0;
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnResumeButton()
    {
        GameManager.instance.togglePauseGame();
    }

    public void TogglePauseScreen(bool pause)
    {
        pauseScreen.SetActive(pause);
    }
}
