using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private LevelChanger levelChanger;

    public void PauseOnClick()
    {
        Time.timeScale = 0.0f;
        GameManager.instance.ShowPauseUI();
    }

    public void ResumeOnClick()
    {
        Time.timeScale = 1.0f;
        GameManager.instance.HidePauseUI();
    }

    public void RetryOnClick()
    {
        Time.timeScale = 1.0f;
        levelChanger.FadeToNextLevel((int)GameMode.instance.GetGameModeType());
    }

    public void MenuOnClick()
    {
        Time.timeScale = 1.0f;
        levelChanger.FadeToNextLevel(0);
    }


}
