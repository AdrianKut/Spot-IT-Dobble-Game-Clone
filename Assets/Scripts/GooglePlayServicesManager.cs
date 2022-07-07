using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayServicesManager : MonoBehaviour
{
    public static GooglePlayServicesManager Instance;

    [HideInInspector]
    public bool IsConnectedToGooglePlayServies;
    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            IsConnectedToGooglePlayServies = true;
        }
        else
        {
            IsConnectedToGooglePlayServies = false;
        }
    }
 
    public void ShowAchievementsGoogleServices()
    {
        if (IsConnectedToGooglePlayServies)
            Social.ShowAchievementsUI();
        else
            ShowAndroidToastMessage("Couldn't load google services!");
    }

    public void ShowLeaderboardsGoogleServices()
    {
        if (IsConnectedToGooglePlayServies)
            Social.ShowLeaderboardUI();
        else
            ShowAndroidToastMessage("Couldn't load google services!");
    }

    private void ShowAndroidToastMessage(string message)
    {
        SSTools.ShowMessage(message, SSTools.Position.bottom, SSTools.Time.oneSecond);
    }

}
