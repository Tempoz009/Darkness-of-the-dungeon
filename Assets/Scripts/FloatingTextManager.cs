using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    // Метод Show использующийся для реализации настройки и вызова метода показа всплывающего текста
    public void Show(string msg, int fontSize, FontStyle fontstyle, Color color, Vector3 position, Vector3 motion, float duration, Font font)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.txt.GetComponent < RectTransform >().sizeDelta = new Vector2(700, 200);
        floatingText.txt.font = font;
        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.fontStyle = fontstyle;
        floatingText.txt.color = color;

        // Перенос мирового пространства в пространство экрана, чтобы можно было использовать его в пользовательском интерфейсе.
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);

        floatingText.motion = motion;
        floatingText.duration = duration; // Передача данных из менеджера к самому объекту плавающего текста

        floatingText.Show();
    } 

    private FloatingText GetFloatingText()
    {
        // Поиск неактивных элементов в массиве
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if(txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform); 
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
