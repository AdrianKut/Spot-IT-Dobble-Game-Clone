using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    public static string gameId = "4802541";
    public static string intersititalAd = "Interstitial_Android";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Advertisement.Initialize(gameId);
        Advertisement.Load(intersititalAd);
    }

    public static void ShowIntersitialAd()
    {
        if (Advertisement.IsReady())
            Advertisement.Show(intersititalAd);
    }
}
