using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using YG;

public class MySaver : MonoBehaviour
{
    Evolution evolution;
    Shop shop;
    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    //Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void Start()
    {
        evolution = GameObject.Find("Game").GetComponent<Evolution>();
        shop = GameObject.Find("Game").GetComponent<Shop>();

        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();

            // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    /// <summary>
    /// Get data from plugin and do with it what we want
    /// </summary>
    public void GetLoad()
    {
        //basic
        Game.CounterValue = YandexGame.savesData.CounterValue;
        Game.IncomePerSecond = YandexGame.savesData.IncomePerSecond;
        Game.ClickPower = YandexGame.savesData.ClickPower;

        //evolution
        evolution.level = YandexGame.savesData.level;
        evolution.expBar.value = YandexGame.savesData.expBarValue;
        evolution.expBar.maxValue = YandexGame.savesData.expBarMaxValue;

        //hidden upgrades
        for (int i = 0; i < shop.GetUpgradeListForSave().Count; i++)
        {
            shop.GetUpgradeListForSave()[i] = YandexGame.savesData.upgrades[i];
            shop.GetUpgradeListForSave()[i].wasUpgradeBought = YandexGame.savesData.upgradeBought[i];
        }
    }

    /// <summary>
    /// Our method for saving
    /// </summary>
    public void MySave()
    {
        //basic
        YandexGame.savesData.CounterValue = Game.CounterValue;
        YandexGame.savesData.IncomePerSecond = Game.IncomePerSecond;
        YandexGame.savesData.ClickPower = Game.ClickPower;

        //evolution
        YandexGame.savesData.level = evolution.level;
        YandexGame.savesData.expBarValue = evolution.expBar.value;
        YandexGame.savesData.expBarMaxValue = evolution.expBar.maxValue;

        //hidden upgrades
        for (int i = 0; i < shop.GetUpgradeListForSave().Count; i++)
        {
            YandexGame.savesData.upgrades[i] = shop.GetUpgradeListForSave()[i];
            YandexGame.savesData.upgradeBought[i] = shop.GetUpgradeListForSave()[i].wasUpgradeBought;
        }

        //save
        YandexGame.SaveProgress();
    }

    private void Update()
    {
        MySave();
    }
}
