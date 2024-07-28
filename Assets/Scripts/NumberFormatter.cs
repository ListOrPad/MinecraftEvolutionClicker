using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class NumberFormatter
{

    public string FormatNumber(BigInteger number)
    {
        if ((double)number >= Mathf.Pow(10, 18)) //Quintillion
        {
            return ((double)number / Mathf.Pow(10, 18)).ToString("0.##") + "Q";
        }
        else if ((double)number >= Mathf.Pow(10, 15)) //Quadrillion
        {
            return ((double)number / Mathf.Pow(10, 15)).ToString("0.##") + "q";
        }
        else if ((double)number >= Mathf.Pow(10, 12)) //Trillion
        {
            return ((double)number / Mathf.Pow(10, 12)).ToString("0.##") + "T";
        }
        else if (number >= 1_000_000_000)
        {
            return ((decimal)number / 1_000_000_000m).ToString("0.##") + "B";
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
    /// <summary>
    /// formatting specifically for counter
    /// </summary>
    public string FormatCounterNumber(BigInteger number)
    {
        if ((double)number >= Mathf.Pow(10, 18)) //Quintillion
        {
            return ((double)number / Mathf.Pow(10, 18)).ToString("0.00") + "Q";
        }
        else if ((double)number >= Mathf.Pow(10, 15)) //Quadrillion
        {
            return ((double)number / Mathf.Pow(10, 15)).ToString("0.00") + "q";
        }
        else if ((double)number >= Mathf.Pow(10, 12)) //Trillion
        {
            return ((double)number / Mathf.Pow(10, 12)).ToString("0.00") + "T";
        }
        else if (number >= 1_000_000_000)
        {
            return ((decimal)number / 1_000_000_000m).ToString("0.00") + "B";
        }
        else if (number >= 1000000)
        {
            return ((decimal)number / 1000000m).ToString("0.00") + "M";
        }
        else if (number >= 1000)
        {
            return ((decimal)number / 1000m).ToString("0.00") + "K";
        }
        else
        {
            return number.ToString();
        }
    }
}
