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
        //set costs of upgrades and initialize upgrade list
        upgradePrefabList = GameObject.Find("Upgrades").GetComponentsInChildren<UpgradePrefab>();
        for (int i = 0; i < upgradePrefabList.Length; i++)
        {
            upgradeList[i] = new Upgrade(i);
            upgradeList[i].Cost = upgradeCosts[i];
            upgradePrefabList[i].UpgradeCostText.text = upgradeCosts[i].ToString() + " <sprite=0>";
        }
        //set values of upgrades
        upgradeList[0].idleUpgradeValue = 1;
        upgradeList[1].isIdleUpgrade = false;
        upgradeList[1].clickUpgradeValue = 1;
    }

    /// <summary>
    /// set to onClick event of the upgrade button
    /// </summary>
    public void BuyUpgrade(int upgradeID)
    {
        if (Game.CounterValue >= GetUpgrade(upgradeID).Cost)
        {
            //logic of substracting cost of upgrade from counter
            Game.CounterValue -= GetUpgrade(upgradeID).Cost;
            if (Game.CounterValue < 0)
            {
                Game.CounterValue += GetUpgrade(upgradeID).Cost;
            }

            //logic of upgrading dps and click power
            if (GetUpgrade(upgradeID).isIdleUpgrade)
            {
                Game.IncomePerSecond += GetUpgrade(upgradeID).idleUpgradeValue;
            }
            else
            {
                Game.ClickPower += GetUpgrade(upgradeID).clickUpgradeValue;
            }
        }
    }

   

    private Upgrade GetUpgrade(int upgradeId)
    {
        return upgradeList[upgradeId];
    }
}
