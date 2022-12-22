using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private LevelChanger levelChanger;

    [SerializeField]
    private GameObject pauseGameObjectUI;

    public void PauseOnClick()
    {
        SetTimeScaleAndPauseActivate(0.0f, true);
    }

    public void ResumeOnClick()
    {
        SetTimeScaleAndPauseActivate(1.0f, false);
    }

    public void RetryOnClick()
    {
        SetDefaultTimeScaleAndChangeLevel((int)GameMode.Instance.GameModeType);
    }

    public void MenuOnClick()
    {
        SetDefaultTimeScaleAndChangeLevel(0);
    }

    private void SetTimeScaleAndPauseActivate(float timeScaleValue, bool activate)
    {
        Time.timeScale = timeScaleValue;
        pauseGameObjectUI.SetActive(activate);
    }

    private void SetDefaultTimeScaleAndChangeLevel(int levelToLoad)
    {
        Time.timeScale = 1.0f;
        levelChanger.FadeToNextLevel(levelToLoad);
    }

}
