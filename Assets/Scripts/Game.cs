using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private TMP_Text CounterText;
    [SerializeField] private TMP_Text IncomeText;

    public static BigInteger CounterValue { get; set; }
    public static BigInteger ClickPower;
    public static BigInteger IncomePerSecond;
    private float accumulatedTime = 0f;

    private Evolution evolution;
    private NumberFormatter numberFormatter = new NumberFormatter();

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
        evolution.expBar.value += 1;
    }
    private void Update()
    {
        // Accumulate the time
        accumulatedTime += (float)IncomePerSecond * Time.deltaTime;

        // Check if the accumulated time is greater than or equal to 1
        if (accumulatedTime >= 1f)
        {
            // Calculate how many whole numbers we have accumulated
            BigInteger wholeNumbers = (BigInteger)accumulatedTime;

            // Increment the CounterValue by the whole numbers
            CounterValue += wholeNumbers;

            // Subtract the whole numbers from the accumulated time
            accumulatedTime -= (float)wholeNumbers;
        }

        CounterText.text = $"{numberFormatter.FormatNumber(CounterValue)} <sprite=0>";
        IncomeText.text = $"{numberFormatter.FormatNumber(IncomePerSecond)} <sprite=0> в сек.";
    }
}
