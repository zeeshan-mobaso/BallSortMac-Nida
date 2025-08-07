using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Firebase; DANI
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager instance;
    bool isFirebaseReady;

    private bool shouldShowForceUpdate = false;

    //Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.Available; DANI

    private void Awake()
    {
        instance = this;
    }

    //void Start()
    //{
    //    isFirebaseReady = false;

    //    if (Application.internetReachability != NetworkReachability.NotReachable)
    //    {

    //        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
    //        {
    //            dependencyStatus = task.Result;
    //            if (dependencyStatus == Firebase.DependencyStatus.Available)
    //            {
    //                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
    //                InitializeFirebase();
    //            }
    //            else
    //            {

    //            }
    //        });
    //    }
    //    else
    //    {
    //        SceneManager.LoadSceneAsync(1);
    //    }
    //}

    //private float timeCounter = 0f;
    // public IEnumerator LoadMainMenuScene()
    // {
    //     while (!isFirebaseReady && timeCounter < 4f)
    //     {
    //         yield return new WaitForSeconds(1f);
    //         timeCounter += 1f;
    //     }
    //    SceneManager.LoadSceneAsync(1);
    //}
    //void InitializeFirebase()
    //{
    //    System.Collections.Generic.Dictionary<string, object> defaults =
    //        new System.Collections.Generic.Dictionary<string, object>();

    //    defaults.Add("config_test_string", "default local string");
    //    defaults.Add("config_test_int", 1);
    //    defaults.Add("config_test_float", 1.0);
    //    defaults.Add("config_test_bool", false);

    //    Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).SetDefaultsAsync(defaults);
    //    FetchDataAsync();

    //}
    //public void ShowData()
    //{
    //     isFirebaseReady = true;
    //    bool ShouldShowAds = Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).GetValue("ShouldShowAds").BooleanValue;
    //    bool ShouldShowAppOpenAd = Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).GetValue("ShouldShowAppOpenAd").BooleanValue;
    //    bool ShouldShowBanner = Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).GetValue("ShouldShowBanner").BooleanValue;
    //    bool ShouldShowInterstitial = Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).GetValue("ShouldShowInterstitial").BooleanValue;
    //    int interstitialcounter = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).GetValue("InterstitialCounter").LongValue;

    //    ShowAd.Instance.ShouldShowAds = ShouldShowAds;
    //    ShowAd.Instance.ShouldShowAppOpenAd = ShouldShowAppOpenAd;
    //    ShowAd.Instance.ShouldShowBanner = ShouldShowBanner;
    //    ShowAd.Instance.ShouldShowInterstitial = ShouldShowInterstitial;
    //    ShowAd.Instance.interstitialcounter = interstitialcounter;

    //}
    //void SaveOFFNetData()
    // {

    //    ShowAd.Instance.ShouldShowAds = false;
    //    ShowAd.Instance.ShouldShowAppOpenAd = false;
    //    ShowAd.Instance.ShouldShowBanner = false;
    //    ShowAd.Instance.ShouldShowInterstitial = false;
    //    ShowAd.Instance.interstitialcounter = 3;


    //}
    //// Start a fetch request.
    //public Task FetchDataAsync()
    //{
    //    System.Threading.Tasks.Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).FetchAsync(
    //        TimeSpan.Zero);
    //    return fetchTask.ContinueWith(FetchComplete);
    //}

    //void FetchComplete(Task fetchTask)
    //{
    //    var info = Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).Info;
    //    switch (info.LastFetchStatus)
    //    {
    //        case Firebase.RemoteConfig.LastFetchStatus.Success:
    //            Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(Firebase.FirebaseApp.DefaultInstance).ActivateAsync();
    //            ShowData();
    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Failure:
    //            switch (info.LastFetchFailureReason)
    //            {
    //                case Firebase.RemoteConfig.FetchFailureReason.Error:
    //                    SaveOFFNetData();
    //                    break;
    //                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
    //                    break;
    //            }
    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Pending:
    //            Debug.LogError("Latest Fetch call still pending.");
    //            break;
    //    }
    //}

}