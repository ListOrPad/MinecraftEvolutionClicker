using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;

public class Evolution : MonoBehaviour
{
    [SerializeField] private Slider expBar;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Sprite[] creatureSprites;
    private int level;
    private GameObject creature;

    private void Start()
    {
        level = 0;
        creature = GameObject.Find("Creature");
        creature.GetComponent<Image>().sprite = creatureSprites[level];
        expBar.value = 0;
        if(YandexGame.lang == "ru")
            levelText.text = "1 уровень";
        else
            levelText.text = "1 level";

    }

    private void Update()
    {
        if (YandexGame.lang == "ru")
            levelText.text = $"{level + 1} уровень";
        else
            levelText.text = $"{level + 1} level";
    }

    public void Evolve()
    {
        expBar.value += 100;

        if (LvlUp())
        {
            expBar.value = 0;
            expBar.maxValue += 100;

            level += 1;

            if (level < creatureSprites.Length) // Ensure we have a sprite for the next level
            {
                creature.GetComponent<Image>().sprite = creatureSprites[level];
            }
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
            if(YandexGame.lang == "ru")
                levelText.text = $"максимальный уровень";
            else
                levelText.text = $"max level";
        }
        return false;
    }
}
