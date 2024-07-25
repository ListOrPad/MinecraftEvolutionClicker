using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        levelText.text = "1 уровень";
    }
    public void Evolve()
    {
        expBar.value += 1;

        if (LvlUp())
        {
            expBar.value = 0;
            expBar.maxValue += 100;

            level += 1;

            if (level < creatureSprites.Length) // Ensure we have a sprite for the next level
            {
                creature.GetComponent<Image>().sprite = creatureSprites[level];
            }
            levelText.text = $"{level + 1} уровень";
        }
    }
    private bool LvlUp()
    {
        return expBar.value >= expBar.maxValue;
    }
}
