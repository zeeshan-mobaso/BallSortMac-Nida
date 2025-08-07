﻿using System.Collections.Generic;
using UnityEngine;

public partial class GameSettings : ScriptableObject
{
    public const string DEFAULT_NAME = nameof(GameSettings);


 
    [SerializeField] private string _iosAppId;
    [SerializeField] private InAppSetting _inAppSetting;
//    [SerializeField] private DailyRewardSetting _dailyRewardSetting;
    [SerializeField] private AdsSettings _adsSettings;
    [SerializeField] private PrivatePolicySetting _privatePolicySetting;

    public PrivatePolicySetting PrivatePolicySetting => _privatePolicySetting;
    public AdsSettings AdsSettings => _adsSettings;
    public string IosAppId => _iosAppId;
    public AdmobSetting IosAdmobSetting => _adsSettings.iosAdmobSetting;
    public AdmobSetting AndroidAdmobSetting => _adsSettings.androidAdmobSetting;
   
    public InAppSetting InAppSetting => _inAppSetting;
}

public partial class GameSettings
{
    public static GameSettings Default => Resources.Load<GameSettings>(nameof(GameSettings));

#if UNITY_EDITOR
    [UnityEditor.MenuItem("BallSort/GameSettings")]
    public static void OpenGameSettings()
    {
        GamePlayEditorManager.OpenScriptableAtDefault<GameSettings>();
    }
#endif
}


public partial class GameSettings
{

    //    public const string ANDROID_LEADERSBOARD_SETTINGS = nameof(_androidLeadersboardSetting);
    //    public const string IOS_LEADERSBOARD_SETTINGS = nameof(_iosLeadersboardSetting);
    public const string IOS_APP_ID = nameof(_iosAppId);
    public const string IN_APP_SETTING = nameof(_inAppSetting);
//    public const string DAILY_REWARD_SETTING = nameof(_dailyRewardSetting);

    public const string PRIVATE_POLICY_SETTING = nameof(_privatePolicySetting);
    //    public const string ANDROID_AD_COLONY_SETTINGS = nameof(_androidAdColonySetting);
    //    public const string IOS_AD_COLONY_SETTINGS = nameof(_iosAdColonySetting);
    public const string ADS_SETTINGS = nameof(_adsSettings);

}


[System.Serializable]
public struct AdsSettings
{
    public string iosUnityAppId;
    public string androidUnityAppId;
    //    public AdColonySettings iosAdColonySettings;
    //    public AdColonySettings androidAdColonySettings;
    public AdmobSetting iosAdmobSetting;
    public AdmobSetting androidAdmobSetting;
    public Vector2Int minAndMaxGameOversBetweenInterstitialAds;
    public Vector2Int minAndMaxGameOversBetweenVideoAds;
    [Range(0, 1f)]
    public float videoAdsFrequency;
}

[System.Serializable]
public struct DailyRewardSetting
{
    public bool enable;
    public List<int> rewards;
}

[System.Serializable]
public struct PrivatePolicySetting
{
    public bool enable;
    public string url;
}

//[System.Serializable]
//public struct NotificationSetting
//{
//    public bool enable;
//    public List<string> comebackMessages;
//    public List<int> comebackDelayTimes;
//}

[System.Serializable]
public struct InAppSetting
{
    public bool enable;
    public string removeAdsId;
}

//[System.Serializable]
//public struct AdColonySettings
//{
//    public bool enable;
//    public string appId;
//    public string interstitialZoneId;
//    public string currencyZoneId;
//}

[System.Serializable]
public struct AdmobSetting
{
    public bool enable;
    public string bannerId;
    public string interstitialId;
    public string admobRewardedId;
}

[System.Serializable]
public struct LeadersboardSetting
{
    public bool enable;
    public string leadersboardId;
}
