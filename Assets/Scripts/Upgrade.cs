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
    public BigInteger idleUpgradeValue { get; set; }
    public BigInteger clickUpgradeValue { get; set; }

    private int upradeID;
    public bool isIdleUpgrade { get; set; } = true;
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