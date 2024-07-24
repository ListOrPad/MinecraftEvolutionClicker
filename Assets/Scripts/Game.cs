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
    private BigInteger clickPower;
    private BigInteger IncomePerSecond;

    private void Start()
    {
        IncomePerSecond = 0;
        CounterText.text = "0 <sprite=0>";
        IncomeText.text = "1 <sprite=0> per second";
        CounterValue = 0;
        clickPower = 1;
    }
    public void Click()
    {
        CounterValue += clickPower;
    }
    private void Update()
    {
        CounterText.text = $"{CounterValue} <sprite=0>";
    }
}
