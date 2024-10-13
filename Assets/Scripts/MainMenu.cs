using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static bool gameIsStart = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("FadeOutMainMenu");

    }
    private IEnumerator aboba()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }

    // Метод, позволяющий перенестись на игровой уровень
    public void StartGame()
    {
        animator.Play("FadeInMainMenu");
        StartCoroutine("aboba");
        Time.timeScale = 1f;
        gameIsStart = true;
    }

    /*
    // Метод, позволяющий отобразить Информацию об игре
    public void AboutGame()
    {
        SceneManager.LoadScene("AboutGame");
    }
    */
    // Метод, позволяющий открыть настройки игры
    public void OpenOptions()
    {
        SceneManager.LoadScene("Options");
    }

    // Метод для реализации выхода из игры
    public void ExitGame() 
    {
        Application.Quit();
    }
}
