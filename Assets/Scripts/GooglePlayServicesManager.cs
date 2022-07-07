using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooglePlayServicesManager : MonoBehaviour
{
    public static GooglePlayServicesManager instance;

    [HideInInspector]
    public bool isConnectedToGooglePlayServies;
    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        if (instance == null)
        {
            instance = this;
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
            isConnectedToGooglePlayServies = true;
        }
        else
        {
            isConnectedToGooglePlayServies = false;
        }
    }
 
    public void ShowAchievementsGoogleServices()
    {
        if (isConnectedToGooglePlayServies)
            Social.ShowAchievementsUI();
        else
            ShowAndroidToastMessage("Couldn't load google services!");
    }

    public void ShowLeaderboardsGoogleServices()
    {
        if (isConnectedToGooglePlayServies)
            Social.ShowLeaderboardUI();
        else
            ShowAndroidToastMessage("Couldn't load google services!");
    }

    private void ShowAndroidToastMessage(string message)
    {
        SSTools.ShowMessage(message, SSTools.Position.bottom, SSTools.Time.oneSecond);
    }

}
