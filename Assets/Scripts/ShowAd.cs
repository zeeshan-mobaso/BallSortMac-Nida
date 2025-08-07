using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Game;
using UnityEngine.SceneManagement;
//using GoogleMobileAds.Api; DANI

public class ShowAd : MonoBehaviour
{
    public enum RewardedAdType
    {
        AddHolder,
        UnlockBallType,
        UndoMove,
        None
    }
	public static ShowAd Instance;
	public bool isTestMode;
	public string admobAppID;
	bool isAppOpenAdLoaded = false;
	public bool shouldShowProscreen;
	public int GamePlayAdDisplayCounter;
	public static bool inappinitialized = false;


    public bool ShouldShowAds, ShouldShowAppOpenAd, ShouldShowBanner, ShouldShowInterstitial;
    public string admob_inter, admob_banner, admob_rewarded, admob_appopen, inapp_licensekey;

    public string objectToUnlock = "";
    public GameObject rewardedLock;
    #region Variables
    [Header("REMOVE ADS")] public bool removeAllAds;
    [Header("PRIORITY CHECK")] public bool AdmobPriorityInter;
    public bool UnityPriorityInter;
    public bool AdmobPriorityRewarded;
    public bool UnityPriorityRewarded;
    private const string removeAds = "NOADS";

    #endregion
    public RewardedAdType _rewardedAdType;
    public static int showRateUsCounter = -1;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        rewardedLock = new GameObject();
    }

    private void OnEnable()
	{
        PlayerPrefs.SetString("8Balls", "NO");
        PlayerPrefs.SetString("Fruits", "NO");
        PlayerPrefs.SetString("Social", "NO");
        PlayerPrefs.SetString("Candy", "NO");
        PlayerPrefs.SetString("Emojis", "NO");
        PlayerPrefs.SetString("Animals", "NO");
        PlayerPrefs.SetInt("UndoMovesLeft", 3);

    }
}
