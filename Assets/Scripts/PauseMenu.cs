using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    // Использую встроенный метод Update для реализации открытия Меню паузы в любой момент игры
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // Открытие меню паузы происходит на клавишу Ecsape
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Метод для возвращения в игру из меню паузы
    public void Resume() 
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    // Метод для реализации паузы
    void Pause() 
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    // Метод для реализации выхода в главное меню
    public void LoadMainMenu() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    // Метод для реализации выхода из игры
    public void ExitGame() 
    { 
        Application.Quit();
    }
}
