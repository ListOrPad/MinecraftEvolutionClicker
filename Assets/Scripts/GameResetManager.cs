using UnityEngine;
using YG;

public class GameResetManager : MonoBehaviour
{
    public void OnResetButtonClicked()
    {
        GameReset();

        ReloadZeroScene();
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
}