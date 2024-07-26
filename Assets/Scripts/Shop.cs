using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private NumberFormatter numberFormatter = new NumberFormatter();

    private UpgradePrefab[] upgradePrefabList = new UpgradePrefab[16];
    private List<Upgrade> upgradeList = new List<Upgrade>( new Upgrade[16] );
    private List<BigInteger> upgradeCosts = new List<BigInteger>{ 15, 40, 1500, 4750, 110000, (BigInteger)oneMillion,
        (BigInteger)tenMillion, (BigInteger)fifteenMillion, (BigInteger) sevenHundredMillion, (BigInteger) threeBillion,
        (BigInteger) fiftyBillion, (BigInteger) oneTrillion, (BigInteger) tenTrillion, (BigInteger) twoHundredFiftyTrillion,
        (BigInteger) twoQuadrillion, (BigInteger)fiveQuadrillion };

    private const double oneMillion = 1_000_000;
    private const double tenMillion = 10_000_000;
    private const double fifteenMillion = 15_000_000;
    private const double sevenHundredMillion = 700_000_000;
    private const double threeBillion = 3_000_000_000;
    private const double fiftyBillion = 50_000_000_000;
    private const double oneTrillion = 1_000_000_000_000;
    private const double tenTrillion = 10_000_000_000_000;
    private const double twoHundredFiftyTrillion = 250_000_000_000_000;
    private const double twoQuadrillion = 2_000_000_000_000_000;
    private const double fiveQuadrillion = 5_000_000_000_000_000;

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
            upgradePrefabList[i].UpgradeCostText.text = numberFormatter.FormatNumber(upgradeCosts[i]) + " <sprite=0>";
        }
        //set values of upgrades
        upgradeList[0].idleUpgradeValue = 1;
        upgradeList[1].isIdleUpgrade = false;
        upgradeList[1].clickUpgradeValue = 2;
        upgradeList[2].idleUpgradeValue = 25;
        upgradeList[3].clickUpgradeValue = 50;


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
