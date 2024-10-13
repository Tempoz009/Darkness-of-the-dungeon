using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public Text text;
    public Button button;

    //[SerializeField] Color basicColor;
    public static Color basicColor = new Color(0.8f, 0.1f, 0.2f);
    public static Color hoverColor = Color.white;

    // При попадании на окно цвет текста изначально заданный
    private void Start()
    {
        //basicColor = GetComponent<Color>();
        text.color = basicColor;
    }

    // При наведении курсора на кнопку в виде текста текст меняет свой цвет на белый
    public void OnPointerEnter(PointerEventData button)
    {
        text.color = hoverColor;
    }

    // При отведении курсора от кнопки текст меняет свой цвет на изначальный
    public void OnPointerExit(PointerEventData button)
    {
        //basicColor = GetComponent<Color>();
        text.color = basicColor;
    }
}
