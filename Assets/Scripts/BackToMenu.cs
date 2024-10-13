using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator Abiba()
    {
        animator.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Menu");
    }

    // Метод для возврта из меню Об игре в Главное меню
    public void BackToMainMenu()
    {     
        StartCoroutine("Abiba");
    }
}
