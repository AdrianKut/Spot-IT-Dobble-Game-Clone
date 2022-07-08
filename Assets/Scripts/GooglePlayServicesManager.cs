using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayServicesManager : MonoBehaviour
{
    public static GooglePlayServicesManager Instance;
    public bool IsConnectedToGooglePlayServices;

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

    private void Start()
    {
        SignInToGooglePlayServices();
    }

    public void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            if (result == SignInStatus.Success)
                IsConnectedToGooglePlayServices = true;
            else
                IsConnectedToGooglePlayServices = false;
        });
    }

    public void ShowAchievementsGoogleServices()
    {
        if (IsConnectedToGooglePlayServices)
            Social.ShowAchievementsUI();
        else
            ShowAndroidToastMessage("Couldn't load google services!");
    }

    public void ShowLeaderboardsGoogleServices()
    {
        if (IsConnectedToGooglePlayServices)
            Social.ShowLeaderboardUI();
        else
            ShowAndroidToastMessage("Couldn't load google services!");
    }

    private void ShowAndroidToastMessage(string message)
    {
        SSTools.ShowMessage(message, SSTools.Position.bottom, SSTools.Time.oneSecond);
    }


    public void SendAchivementProgress(int score, GameModeType gameModeType)
    {
        if (IsConnectedToGooglePlayServices && gameModeType == GameModeType.Normal)
        {
            switch (score)
            {
                case 10:
                    Social.ReportProgress(GPGSIds.achievement_10_score, 100.0f, null);
                    break;

                case 50:
                    Social.ReportProgress(GPGSIds.achievement_50_score, 100.0f, null);
                    break;

                case 100:
                    Social.ReportProgress(GPGSIds.achievement_100_score, 100.0f, null);
                    break;

                default:
                    break;
            }
        }
        else
        {
            Debug.Log("Not signed in .. unable to report score");
        }
    }


    public void SendScoreToLeadership(int score, GameModeType gameModeType)
    {
        if (IsConnectedToGooglePlayServices && gameModeType == GameModeType.Normal)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_top_score, (success) =>
            {
                if (!success)
                {
                    Debug.LogError("Unable to post highscore!");
                }
            });
        }
        else
        {
            Debug.Log("Not signed in .. unable to report score");
        }
    }



}
