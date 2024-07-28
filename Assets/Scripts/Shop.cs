using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private NumberFormatter numberFormatter = new NumberFormatter();

    [Header("Sound Stuff")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buySuccessSound;

    private UpgradePrefab[] upgradePrefabList = new UpgradePrefab[16];
    private List<Upgrade> upgradeList = new List<Upgrade>( new Upgrade[16] );
    private List<BigInteger> upgradeCosts = new List<BigInteger>{ 15, 40, 1500, 4750, 110000, (BigInteger)oneMillion,
        (BigInteger)tenMillion, (BigInteger)fifteenMillion, (BigInteger) sevenHundredMillion, (BigInteger) threeBillion,
        (BigInteger) fiftyBillion, (BigInteger) oneTrillion, (BigInteger) tenTrillion, (BigInteger) twoHundredFiftyTrillion,
        (BigInteger) twoQuadrillion, (BigInteger)fiveQuadrillion };

    //Prices of upgrades
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

        SetValuesOfUpgrades();
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

            //play purchase sound
            audioSource.PlayOneShot(buySuccessSound);
        }
    }

   

    private Upgrade GetUpgrade(int upgradeId)
    {
        return upgradeList[upgradeId];
    }

    //How much power will upgrade increase
    private void SetValuesOfUpgrades()
    {
        upgradeList[0].idleUpgradeValue = 1;
        upgradeList[1].isIdleUpgrade = false;
        upgradeList[1].clickUpgradeValue = 2; //click
        upgradeList[2].idleUpgradeValue = 25;
        upgradeList[3].isIdleUpgrade = false;
        upgradeList[3].clickUpgradeValue = 50; //click
        upgradeList[4].idleUpgradeValue = 2000;
        upgradeList[5].idleUpgradeValue = 25000;
        upgradeList[6].isIdleUpgrade = false;
        upgradeList[6].clickUpgradeValue = 50_000; //click
        upgradeList[7].idleUpgradeValue = 200_000;
        upgradeList[8].isIdleUpgrade = false;
        upgradeList[8].clickUpgradeValue = 1_000_000; //1M click
        upgradeList[9].idleUpgradeValue = 45_000_000; //45M
        upgradeList[10].idleUpgradeValue = 650_000_000; //650M
        upgradeList[11].isIdleUpgrade = false;
        upgradeList[11].clickUpgradeValue = 1_000_000_000; //1B click
        upgradeList[12].idleUpgradeValue = 10_000_000_000; //10B
        upgradeList[13].idleUpgradeValue = 300_000_000_000; //300B
        upgradeList[14].isIdleUpgrade = false;
        upgradeList[14].clickUpgradeValue = 1_000_000_000_000; //1T click
        upgradeList[15].idleUpgradeValue = 10_000_000_000_000; //10T
    }

}
