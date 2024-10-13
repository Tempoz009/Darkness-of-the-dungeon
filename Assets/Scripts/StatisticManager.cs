using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticManager : MonoBehaviour
{
    public Text collectedCoins;
    public Text countOfOpenDoors;

    // Для отображения статистики на окне с результатами игры
    void Start()
    {
        collectedCoins.text += " " + Player.globalMoneyAmount;
        countOfOpenDoors.text += " " + Player.countOfOpenDoors;
    }
}
