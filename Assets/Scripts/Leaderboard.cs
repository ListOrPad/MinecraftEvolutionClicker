using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using YG;

public class Leaderboard : MonoBehaviour
{
    private BigInteger previousRecord;
    private Coroutine myCoroutine;

    private void Update()
    {
        if (myCoroutine != null)
        {

        }
        else
        {
            myCoroutine = StartCoroutine(WriteNewRecord());
        }
    }

    private IEnumerator WriteNewRecord()
    {
        yield return new WaitForSeconds(3);

        if(previousRecord < Game.CounterValue)
        {
            previousRecord = Game.CounterValue;

            YandexGame.NewLeaderboardScores("diamondCount", (int)Game.CounterValue);
        }
        myCoroutine = null;
    }
}
