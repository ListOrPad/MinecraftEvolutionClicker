using TMPro;
using UnityEngine;
using YG;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI prestigeText;
    [SerializeField] private Button prestigeLbButton;
    [SerializeField] private GameObject prestigeLeaderboard;
    private bool eventSubscribed;

    private LeaderboardYG leaderboardYG;

    public int previousRecord { get; set; }

    private void Start()
    {
        leaderboardYG = prestigeLeaderboard.GetComponent<LeaderboardYG>();
        leaderboardYG.UpdateLB();

        prestigeLbButton.onClick.AddListener(() => prestigeLeaderboard.SetActive(true));
        eventSubscribed = true;
    }
    private void Update()
    {
        HideLeaderboard();
        WritePrestigeText();
        WriteNewRecord();
    }


    private void WriteNewRecord()
    {
        if(previousRecord < Game.Prestige)
        {
            previousRecord = Game.Prestige;

            YandexGame.NewLeaderboardScores("prestigeLB", Game.Prestige);
            leaderboardYG.NewScore(Game.Prestige);
            leaderboardYG.UpdateLB();
        }
    }

    private void WritePrestigeText()
    {
        if (YandexGame.lang == "ru")
        {
            prestigeText.text = $"Уровень Престижа: {Game.Prestige}";
        }
        else
        {
            prestigeText.text = $"Prestige Level: {Game.Prestige}";
        }
    }

    private void HideLeaderboard()
    {
        //if prestige leaderboard is active we subscribe deactivate event on the button
        if (prestigeLeaderboard.activeSelf == true && eventSubscribed)
        {
            prestigeLbButton.onClick.RemoveAllListeners();
            prestigeLbButton.onClick.AddListener(() => prestigeLeaderboard.SetActive(false));
            eventSubscribed = false;

        }
        if (prestigeLeaderboard.activeSelf == false && !eventSubscribed)
        {
            prestigeLbButton.onClick.RemoveAllListeners();
            prestigeLbButton.onClick.AddListener(() => prestigeLeaderboard.SetActive(true));
            eventSubscribed = true;
        }
    }
}
