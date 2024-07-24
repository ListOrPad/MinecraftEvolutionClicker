using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private UpgradePrefab[] upgradePrefabList = new UpgradePrefab[16];
    private List<Upgrade> upgradeList = new List<Upgrade>( new Upgrade[16] );
    private List<BigInteger> upgradeCosts = new List<BigInteger>{ 15, 100, 1500, 10000, 110000, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
    private void Start()
    {
        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
        upgradePrefabList = GameObject.Find("Upgrades").GetComponentsInChildren<UpgradePrefab>();
        for (int i = 0; i < upgradePrefabList.Length; i++)
        {
            upgradeList[i] = new Upgrade(i);
            upgradeList[i].Cost = upgradeCosts[i];
            upgradePrefabList[i].UpgradeCostText.text = upgradeCosts[i].ToString() + " <sprite=0>";
        }
    }

    public void BuyUpgrade(int upgradeID) //or list of upgrades as param?
    {
        Game.CounterValue -= GetUpgrade(upgradeID).Cost;
        if (Game.CounterValue < 0)
        {
            Game.CounterValue += GetUpgrade(upgradeID).Cost;
        }
    }

   

    private Upgrade GetUpgrade(int upgradeId)
    {
        return upgradeList[upgradeId];
    }
}
