using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class Upgrade
{
    
    public Upgrade(int upgradeID)
    {
        this.upradeID = upgradeID;
    }
    public BigInteger Cost { get; set; }

    private int upradeID;
    private bool idle;
    private bool isUpgradeShown;


  

    private bool isUpgradeAvailable()
    {
        if (Cost > Game.CounterValue)
        {
            return false;
        }
        return true;
    }

    private void Update()
    {
        if (!isUpgradeAvailable())
        {

        }
    }
}
