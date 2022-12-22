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
    [SerializeField]
    private LevelChanger levelChanger;

    [Header("One Vs One Settings")]
    [SerializeField]
    private GameObject gameObjectGameModeSettingsView;

    [SerializeField]
    private GameObject gameObjectOneVsOneSettingsView;

    [SerializeField]
    private Toggle timerToggle;

    [SerializeField]
    private Toggle healthToggle;

    [SerializeField]
    private TextMeshProUGUI textPointsToGet;

    public int PointsToGet;

    public bool IsHealthOn = false;
    public bool IsTimerOn = false;

    public static GameMode Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameModeType GameModeType;

    public void Show1vs1SettingsView()
    {
        EnableView(gameObjectGameModeSettingsView, false);
        EnableView(gameObjectOneVsOneSettingsView, true);
    }

    public void BackToGameModeView()
    {
        EnableView(gameObjectGameModeSettingsView, true);
        EnableView(gameObjectOneVsOneSettingsView, false);
    }

    private void EnableView(GameObject gameObject, bool activate)
    {
        gameObject.SetActive(activate);
    }

    public void StartSelectedGame(int levelToChoose)
    {
        switch (levelToChoose)
        {
            case 1:
                GameModeType = GameModeType.Practice;
                goto default;

            case 2:
                GameModeType = GameModeType.Normal;
                goto default;

            case 3:
                IsTimerOn = timerToggle.isOn;
                IsHealthOn = healthToggle.isOn;
                PointsToGet = int.Parse(textPointsToGet.text);

                GameModeType = GameModeType.OneVSOne;
                goto default;

            case 4:
                GameModeType = GameModeType.Extreme;
                goto default;

            default:
                levelChanger.FadeToLevel(levelToChoose);
                break;

        }
    }



}
