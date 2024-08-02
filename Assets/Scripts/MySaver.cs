using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MySaver : MonoBehaviour
{
    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    // Отписываемся от события GetDataEvent в OnDisable
    //private void OnDisable()
    //{
    //    YandexGame.GetDataEvent -= GetLoad;
    //}

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();

            // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    public void GetLoad()
    {
        // Получаем данные из плагина и делаем с ними что хотим
        // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
        //textMoney.text = YandexGame.savesData.money.ToString();
        Game.CounterValue = YandexGame.savesData.CounterValue;
    }

    // Допустим, это Ваш метод для сохранения
    public void MySave()
    {
        // Записываем данные в плагин
        // Например, мы хотил сохранить количество монет игрока:
        //YandexGame.savesData.money = money;
        YandexGame.savesData.CounterValue = Game.CounterValue;

        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }

    private void Update()
    {
        MySave();
    }
}
