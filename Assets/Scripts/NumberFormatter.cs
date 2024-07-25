using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class NumberFormatter
{

    public string FormatNumber(BigInteger number)
    {
        if (number >= 1000000000)
        {
            return ((decimal)number / 1000000000m).ToString("0.##") + "B";
        }
        else if (number >= 1000000)
        {
            return ((decimal)number / 1000000m).ToString("0.##") + "M";
        }
        else if (number >= 1000)
        {
            return ((decimal)number / 1000m).ToString("0.##") + "K";
        }
        else
        {
            return number.ToString();
        }
    }
}
