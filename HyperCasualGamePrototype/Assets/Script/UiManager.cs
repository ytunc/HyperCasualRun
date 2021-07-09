using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;


    public enum panels
    {
        game, gameOver
    }

    public void ChangePanel(panels panelName)
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        switch (panelName)
        {
            case panels.game:
                Time.timeScale = 1;
                gamePanel.SetActive(true);
                break;
            case panels.gameOver:
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        ChangePanel(panels.game);
    }
}
