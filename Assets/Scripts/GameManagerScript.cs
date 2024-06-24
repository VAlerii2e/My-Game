using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject victoryMenu;

    public void ShowVictoryMenu()
    {
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(true);
            Time.timeScale = 0f; // Зупиняємо гру
        }
        else
        {
            Debug.LogWarning("Victory menu is not assigned in the inspector.");
        }
    }

    public void HideVictoryMenu()
    {
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(false);
            Time.timeScale = 1f; // Продовжуємо гру
        }
        else
        {
            Debug.LogWarning("Victory menu is not assigned in the inspector.");
        }
    }

    public void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Game over UI is not assigned in the inspector.");
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Продовжуємо гру
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        //Time.timeScale = 1f; // Продовжуємо гру
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
