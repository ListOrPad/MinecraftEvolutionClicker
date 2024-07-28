using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Game : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private TMP_Text CounterText;
    [SerializeField] private TMP_Text IncomeText;

    public static BigInteger CounterValue { get; set; }
    public static BigInteger ClickPower;
    public static BigInteger IncomePerSecond;
    private float accumulatedTime = 0f;

    private Evolution evolution;
    private NumberFormatter numberFormatter = new NumberFormatter();

    [Header("Sound Stuff")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip tapSound;
    [SerializeField] Button soundButton;
    [SerializeField] Sprite soundImageOn;
    [SerializeField] Sprite soundImageOff;
    private bool soundOn = true;

    [Header("Localization")]
    [SerializeField] private Button ruLanguageButton;
    [SerializeField] private Button enLanguageButton;

    private void Start()
    {
        IncomePerSecond = 0;
        CounterText.text = "0 <sprite=0>";
        IncomeText.text = "1 <sprite=0> в сек.";
        CounterValue = 0;
        ClickPower = 1;

        evolution = GetComponent<Evolution>();
    }
    public void Click()
    {
        CounterValue += ClickPower;
        evolution.Evolve();
        audioSource.PlayOneShot(tapSound);
        //DropDiamondsEffect()
        //ShowClickNumbers()
    }
    private void Update()
    {
        // Accumulate the time
        accumulatedTime += (float)IncomePerSecond * Time.deltaTime;

        if (accumulatedTime >= 1f)
        {
            // Calculate how many whole numbers we have accumulated
            BigInteger wholeNumbers = (BigInteger)accumulatedTime;

            // Increment the CounterValue by the whole numbers
            CounterValue += wholeNumbers;

            // Subtract the whole numbers from the accumulated time
            accumulatedTime -= (float)wholeNumbers;
        }

        CounterText.text = $"{numberFormatter.FormatCounterNumber(CounterValue)} <sprite=0>";
        if (YandexGame.lang == "ru")
        {
            IncomeText.text = $"{numberFormatter.FormatNumber(IncomePerSecond)} <sprite=0> в сек.";
        }
        else
        {
            IncomeText.text = $"{numberFormatter.FormatNumber(IncomePerSecond)} <sprite=0> per sec.";
        }
        //if Language Button is pressed
        ChangeLanguageButton();
    }

    public void ToggleSound()
    {
        if (soundOn)
        {
            audioSource.gameObject.SetActive(false);
        }
        else
        {
            audioSource.gameObject.SetActive(true);
        }
        soundOn = !soundOn;
        soundButton.image.sprite = soundOn ? soundImageOn : soundImageOff;
    }

    private void ChangeLanguageButton()
    {
        if(YandexGame.lang == "en")
        {
            enLanguageButton.gameObject.SetActive(true);
            ruLanguageButton.gameObject.SetActive(false);
        }
        else
        {
            ruLanguageButton.gameObject.SetActive(true);
            enLanguageButton.gameObject.SetActive(false);
        }
    }
}
