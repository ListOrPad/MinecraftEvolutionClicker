using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour //rename to 'Clicker'?
{
    [SerializeField] private TMP_Text diamondsText;
    [SerializeField] private TMP_Text dpsText;

    private BigInteger diamondCount;
    private BigInteger clickPower;
    private BigInteger dps;

    private void Start()
    {
        diamondsText.text = "0 <sprite=0>";
        dpsText.text = "1 <sprite=0> per second";
        diamondCount = 0;
        clickPower = 1;
    }
    public void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            diamondCount += clickPower;
            diamondsText.text = $"{diamondCount} <sprite=0>";
        }
    }
    private void Update()
    {
        Click();
    }
}
