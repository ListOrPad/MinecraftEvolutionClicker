using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private NumberFormatter numberFormatter = new NumberFormatter();
    private YandexGame YG;

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

    //hiding
    private List<string> hiddenNames = new List<string>();
    private List<string> hiddenCosts = new List<string>();
    private List<string> hiddenUpgradeValues = new List<string>();
    private List<string> hiddenNamesEn = new List<string>();
    private List<string> hiddenCostsEn = new List<string>();
    private List<string> hiddenUpgradeValuesEn = new List<string>();

    private void Start()
    {
        YG = GameObject.Find("YandexGame").GetComponent<YandexGame>();

        InitializeUpgrades();
        SaveHiddenData();
    }
    private void Update()
    {
        ConcealUpgrades();
        DimExpensiveUpgrades();
    }

    private void InitializeUpgrades()
    {
        //set costs of upgrades and initialize upgrade list
        upgradePrefabList = GameObject.Find("Upgrades").GetComponentsInChildren<UpgradePrefab>();
        for (int i = 0; i < upgradePrefabList.Length; i++)
        {
            upgradeList[i] = new Upgrade(i);
            upgradeList[i].Cost = upgradeCosts[i];
            upgradePrefabList[i].upgradeCostText.text = numberFormatter.FormatNumber(upgradeCosts[i]) + " <sprite=0>";
        }

        upgradeList[0].wasUpgradeBought = true;
        SetValuesOfUpgrades();
    }

    /// <summary>
    /// Conceals upgrade until previous one is bought
    /// </summary>
    private void ConcealUpgrades()
    {
        for (int i = 1; i < upgradePrefabList.Length; i++)
        {
            //if a buy of specific upgrade wasn't purchased, then...
            if (!upgradeList[i - 1].wasUpgradeBought)
            {
                //change data to hidden
                upgradePrefabList[i].UpgradeButton.interactable = false;

                upgradePrefabList[i].UpgradeBackgroundImage.color = new Color32(80, 130, 125, 255);

                upgradePrefabList[i].upgradeCostText.text = "???";
                upgradePrefabList[i].upgradeCostText.color = new Color(0, 0, 0, 1);

                upgradePrefabList[i].upgradeNameText.text = "??????????";
                upgradePrefabList[i].upgradeNameText.color = new Color(0, 0, 0, 1);

                upgradePrefabList[i].upgradeValueText.text = "????????????????????";
                upgradePrefabList[i].upgradeValueText.color = new Color(0, 0, 0, 1);

                upgradePrefabList[i].upgradePicture.color = new Color(0, 0, 0, 1);
            }
            else if (YandexGame.lang == "ru") //get data back to normal
            {
                upgradePrefabList[i].UpgradeButton.interactable = true;

                upgradePrefabList[i].UpgradeBackgroundImage.color = new Color32(255, 255, 255, 255);

                upgradePrefabList[i].upgradeCostText.text = hiddenCosts[i];
                upgradePrefabList[i].upgradeCostText.color = new Color32(73, 207, 255, 255);

                upgradePrefabList[i].upgradeNameText.text = hiddenNames[i];
                upgradePrefabList[i].upgradeNameText.color = new Color32(186, 255, 0, 255);

                upgradePrefabList[i].upgradeValueText.text = hiddenUpgradeValues[i];
                upgradePrefabList[i].upgradeValueText.color = new Color32(229, 174, 152, 255);

                upgradePrefabList[i].upgradePicture.color = new Color(1, 1, 1, 1);
            }
            else //get data back to normal Eng
            {
                upgradePrefabList[i].UpgradeButton.interactable = true;

                upgradePrefabList[i].UpgradeBackgroundImage.color = new Color32(255, 255, 255, 255);

                upgradePrefabList[i].upgradeCostText.text = hiddenCostsEn[i];
                upgradePrefabList[i].upgradeCostText.color = new Color32(73, 207, 255, 255);

                upgradePrefabList[i].upgradeNameText.text = hiddenNamesEn[i];
                upgradePrefabList[i].upgradeNameText.color = new Color32(186, 255, 0, 255);

                upgradePrefabList[i].upgradeValueText.text = hiddenUpgradeValuesEn[i];
                upgradePrefabList[i].upgradeValueText.color = new Color32(229, 174, 152, 255);

                upgradePrefabList[i].upgradePicture.color = new Color(1, 1, 1, 1);
            }
        }
    }

    private void SaveHiddenData()
    {
        //save hidden text before changing it to '???'
        for (int i = 0; i < upgradePrefabList.Length; i++)
        {
            hiddenCosts.Add(upgradePrefabList[i].upgradeCostText.text);
            hiddenNames.Add(upgradePrefabList[i].upgradeNameText.text);
            hiddenUpgradeValues.Add(upgradePrefabList[i].upgradeValueText.text);
            
            YG._SwitchLanguage("en");
            hiddenCostsEn.Add(upgradePrefabList[i].upgradeCostText.text);
            hiddenNamesEn.Add(upgradePrefabList[i].upgradeNameText.text);
            hiddenUpgradeValuesEn.Add(upgradePrefabList[i].upgradeValueText.text);

            YG._SwitchLanguage("ru");
        }
    }

    private void DimExpensiveUpgrades()
    {
        for (int i = 0; i < upgradePrefabList.Length; i++)
        {
            if (!upgradeList[i].isUpgradeExpensive())
            {
                upgradePrefabList[i].UpgradeBackgroundImage.color = new Color32(195, 195, 195, 255);
                upgradePrefabList[i].upgradeCostText.color = new Color32(255, 103, 74, 255);
                upgradePrefabList[i].upgradeNameText.color = new Color32(132,181,0, 255);
                upgradePrefabList[i].upgradeValueText.color = new Color32(176, 118, 95, 255);
            }
            else
            {
                upgradePrefabList[i].UpgradeBackgroundImage.color = new Color(1, 1, 1, 1);
                upgradePrefabList[i].upgradeCostText.color = new Color32(73, 207, 255, 255);
                upgradePrefabList[i].upgradeNameText.color = new Color32(187, 255, 0, 255);
                upgradePrefabList[i].upgradeValueText.color = new Color32(228, 174, 152, 255);
            }
        }
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

            GetUpgrade(upgradeID).wasUpgradeBought = true;
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
