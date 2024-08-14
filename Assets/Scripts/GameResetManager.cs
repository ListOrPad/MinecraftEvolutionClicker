using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameResetManager : MonoBehaviour
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button okButton;
    [SerializeField] private TextMeshProUGUI resetText;
    private Evolution evolution;
    private bool evolutionError = false;

    private void Update()
    {
        if (!evolutionError)
        {
            if (YandexGame.lang == "ru")
            {
                resetText.text = "Вы уверены, что хотите сбросить прогресс?";
            }
            else
            {
                resetText.text = "Are you sure you want to reset your progress?";
            }
        }
    }
    private void Start()
    {
        evolution = GameObject.Find("Game").GetComponent<Evolution>();
    }
    public void OnPrestigeButtonClicked()
    {
        if (evolution.level == evolution.maxCreatureLvl)
        {
            Game.Prestige += 1;
            PlayerPrefs.SetInt("PrestigeLvl", Game.Prestige);

            GameReset();
            ReloadZeroScene();
        }
        else
        {
            evolutionError = true;

            if (YandexGame.lang == "ru")
            {
                resetText.text = "Вы не достигли максимальной эволюции";
            }
            else
            {
                resetText.text = "You haven't reached max evolution";
            }
            yesButton.gameObject.SetActive(false);
            noButton.gameObject.SetActive(false);
            okButton.gameObject.SetActive(true);

        }
    }

    private void GameReset()
    {
       YandexGame.ResetSaveProgress();

        YandexGame.savesData = new SavesYG();
        YandexGame.SaveProgress();
    }

    private void ReloadZeroScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ResetEvolutionError()
    {
        evolutionError = false;
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);
    }
}