using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    // Метод для показа всплывающего текста
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    // Метод для прекращения показа всплывающего текста
    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    // Метод для реализации перемещения всплывающего текста во время его отображения
    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }

        // Время в игре - Время отображения > Продолжительность отображения
        // 10 - 7 > 2
        if(Time.time - lastShown > duration)
        {
            Hide();
        }

        go.transform.position += motion * Time.deltaTime;
    }

}
