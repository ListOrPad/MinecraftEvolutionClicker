using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class Evolution : MonoBehaviour
{
    [SerializeField] public Slider expBar;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] public Sprite[] creatureSprites;
    [HideInInspector] public int level;
    private GameObject creature;

    private void Start()
    {
        creature = GameObject.Find("Creature");
        creature.GetComponent<Image>().sprite = creatureSprites[level];
        if(YandexGame.lang == "ru")
            levelText.text = "1 уровень";
        else
            levelText.text = "1 level";

    }

    private void Update()
    {
        WriteLvl();
    }

    public void CreatureClick()
    {
        expBar.value += 1;
        if (LvlUp())
        {
            Evolve();
        }
    }

    private void Evolve()
    {
        expBar.value = 0;
        expBar.maxValue += 100;

        level += 1;

        if (level < creatureSprites.Length) // Ensure we have a sprite for the next level
        {
            creature.GetComponent<Image>().sprite = creatureSprites[level];
        }
    }
    private bool LvlUp()
    {
        if (creatureSprites.Length > level)
        {
            return expBar.value >= expBar.maxValue;
        }
        else if (creatureSprites.Length == level)
        {
            expBar.value = expBar.maxValue;
        }
        return false;
    }

    /// <summary>
    /// adds +20% to the exp bar as a reward for watching rewarding ad
    /// </summary>
    public void AdRewardedExperience()
    {
        //+20% exp of max value
        float rewardedExperience = expBar.maxValue / 100 * 20;

        float sum = expBar.value + rewardedExperience;
        float delta = sum - expBar.maxValue;
       
        if (sum >= expBar.maxValue && creatureSprites.Length > level)
        {
            Evolve();
            expBar.value += delta;
        }
        else if (creatureSprites.Length > level)
        {
            expBar.value += rewardedExperience;
        }
        else if (creatureSprites.Length == level)
        {
            expBar.value = expBar.maxValue;
        }
    }

    private void WriteLvl()
    {
        if (YandexGame.lang == "ru" && creatureSprites.Length > level)
        {
            levelText.text = $"{level + 1} уровень";
        }
        else if (YandexGame.lang == "en" && creatureSprites.Length > level)
        {
            levelText.text = $"{level + 1} level";
        }
        else if (creatureSprites.Length == level)
        {
            if (YandexGame.lang == "ru")
                levelText.text = $"максимальный уровень";
            else
                levelText.text = $"max level";
        }
    }
}
