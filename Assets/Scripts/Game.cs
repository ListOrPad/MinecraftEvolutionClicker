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

    [SerializeField] private GameObject fadingNumbersObject;
    [SerializeField] private GameObject spawnClickObject;
    private Transform fadeAwayText;

    public static BigInteger CounterValue { get; set; }
    public static BigInteger ClickPower;
    public static BigInteger IncomePerSecond;
    private float accumulatedTime = 0f;

    private Evolution evolution;
    private NumberFormatter numberFormatter = new NumberFormatter();

    [Header("Sound Stuff")]
    [SerializeField] AudioClip tapSound;
    [SerializeField] AudioClip rewAdSound;
    

    [Header("Localization")]
    [SerializeField] private Button ruLanguageButton;
    [SerializeField] private Button enLanguageButton;

    private void Start()
    {
        //those two lines probably should be deleted, this text is handled in update
        CounterText.text = "0 <sprite=0>";
        IncomeText.text = "1 <sprite=0> в сек.";

        evolution = GetComponent<Evolution>();
        fadeAwayText = GameObject.Find("FadeAwayText").GetComponent<Transform>();
    }
    public void Click()
    {
        CounterValue += ClickPower;
        evolution.CreatureClick();
        SoundManager.Instance.PlaySound(tapSound);
        DropDiamondsEffect();
        ShowClickNumbers();
    }

    private void ShowClickNumbers()
    {
        Instantiate(fadingNumbersObject, fadeAwayText);
    }
    private void DropDiamondsEffect()
    {
        UnityEngine.Vector3 pos = Input.mousePosition;
        UnityEngine.Vector3 offset = new UnityEngine.Vector3(0, 0, 10);
        GameObject spawnedObjects = Instantiate(spawnClickObject, pos + offset, UnityEngine.Quaternion.identity);
        spawnedObjects.transform.SetParent(GameObject.Find("Spawned Click Objects").transform);
        Destroy(spawnedObjects, 1);
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

        //text for counter
        CounterText.text = $"{numberFormatter.FormatCounterNumber(CounterValue)} <sprite=0>";
        //text for income per sec
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

    #region rewarded ad
    private void DoubleDiamondsForAd()
    {
        CounterValue *= 2;
    }

    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    // Подписанный метод получения награды
    private void Rewarded(int id)
    {
        if (id == 1)
            DoubleDiamondsForAd();
        else if (id == 2)
            evolution.AdRewardedExperience();

        SoundManager.Instance.PlaySound(rewAdSound);
    }

    // Метод для вызова видео рекламы
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }
    #endregion
}
