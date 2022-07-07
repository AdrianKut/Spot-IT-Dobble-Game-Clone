using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameModeType
{
    Practice = 1,
    Normal = 2,
    OneVSOne = 3,
    Extreme = 4,
}

public enum GameSkins
{
    Devices = 1,
    Toys = 2,
    Animals = 3
}

public class GameMode : MonoBehaviour
{
    private GameMode() { }
    public static GameMode instance;

    [SerializeField]
    private LevelChanger levelChanger;

    [Header("One Vs One Settings")]
    [SerializeField]
    GameObject gameObjectGameModeSettingsView;

    [SerializeField]
    GameObject gameObjectOneVsOneSettingsView;

    [SerializeField]
    Toggle timerToggle;

    public bool isTimerOn = false; 

    [SerializeField]
    Toggle healthToggle;

    [SerializeField]
    TextMeshProUGUI textPointsToGet;

    public int pointsToGet;

    public bool isHealthOn = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private GameModeType gameModeType;
    public GameModeType GetGameModeType() => gameModeType;

    public void Show1vs1SettingsView()
    {
        gameObjectGameModeSettingsView.SetActive(false);
        gameObjectOneVsOneSettingsView.SetActive(true);
    }

    public void BackToGameModeView()
    {
        gameObjectGameModeSettingsView.SetActive(true);
        gameObjectOneVsOneSettingsView.SetActive(false);
    }

    public void StartSelectedGame(int levelToChoose)
    {
        switch (levelToChoose)
        {
            case 1:
                gameModeType = GameModeType.Practice;
                goto default;

            case 2:
                gameModeType = GameModeType.Normal;
                goto default;

            case 3:
                isTimerOn = timerToggle.isOn;
                isHealthOn = healthToggle.isOn;
                pointsToGet = int.Parse(textPointsToGet.text);

                gameModeType = GameModeType.OneVSOne;
                goto default;

            case 4:
                gameModeType = GameModeType.Extreme;
                goto default;

            default:
                levelChanger.FadeToLevel(levelToChoose);
                break;

        }
    }



}
