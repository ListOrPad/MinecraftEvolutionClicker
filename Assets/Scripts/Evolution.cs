using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class Evolution : MonoBehaviour
{
    [SerializeField] private AudioClip lvlUpSound;
    [SerializeField] public Slider expBar;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] public Sprite[] creatureSprites;
    public int level { get; set; }
    public int maxCreatureLvl { get; set; }

    private GameObject creature;

    private string currentLang;
    private int currentLevel;

    private void Start()
    {
        maxCreatureLvl = creatureSprites.Length - 1;
        creature = GameObject.Find("Creature");

        currentLang = YandexGame.lang;
        currentLevel = level;
        UpdateCreature();
        WriteLvl();
    }

    private void Update()
    {
        if (currentLang != YandexGame.lang || currentLevel != level)
        {
            // update text and creature only if level or lang have changed
            currentLang = YandexGame.lang;
            currentLevel = level;
            UpdateCreature();

            WriteLvl();
        }
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
            UpdateCreature();
        }

        SoundManager.Instance.PlaySound(lvlUpSound);
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
    /// Adds +20% to the exp bar as a reward for watching a rewarded ad
    /// </summary>
    public void AdRewardedExperience()
    {
        // +20% exp of max value
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
        if (level < maxCreatureLvl)
        {
            // if lvl lesser quantity of available sprites show lvl
            if (YandexGame.lang == "ru")
            {
                levelText.text = $"{level + 1} уровень";
            }
            else if (YandexGame.lang == "en")
            {
                levelText.text = $"{level + 1} level";
            }
        }
        else
        {
            // if max lvl reached, show corresponding message
            if (YandexGame.lang == "ru")
            {
                levelText.text = "максимальный уровень";
            }
            else if (YandexGame.lang == "en")
            {
                levelText.text = "max level";
            }
        }
    }
    private void UpdateCreature()
    {
        if (level > maxCreatureLvl)
        {
            creature.GetComponent<Image>().sprite = creatureSprites[maxCreatureLvl];
        }
        else
        {
            creature.GetComponent<Image>().sprite = creatureSprites[level];
        }
    }
}