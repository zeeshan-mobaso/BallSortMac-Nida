using System;
using System.Collections;
using dotmob;
using UnityEngine;

public class Splash : MonoBehaviour
{
    private IEnumerator Start()
    {
        if (!PlayerPrefs.HasKey("SelectedPlayingBall"))
            PlayerPrefs.SetInt("SelectedPlayingBall", 0);
        if (!PlayerPrefs.HasKey("SelectedHolder"))
            PlayerPrefs.SetInt("SelectedHolder", 0);
        if (!PlayerPrefs.HasKey("SelectedTheme"))
            PlayerPrefs.SetInt("SelectedTheme", 0);

        if (!PlayerPrefs.HasKey("SubscriptionPurchased"))
            PlayerPrefs.SetString("SubscriptionPurchased", "NO");
        if (!PlayerPrefs.HasKey("WeeklyPrice"))
            PlayerPrefs.SetString("WeeklyPrice", "$2.99");
        if (!PlayerPrefs.HasKey("MonthlyPrice"))
            PlayerPrefs.SetString("MonthlyPrice", "$6.99");
        if (!PlayerPrefs.HasKey("YearlyPrice"))
            PlayerPrefs.SetString("YearlyPrice", "$16.99");

        if (!PlayerPrefs.HasKey("WeeklyOFF"))
            PlayerPrefs.SetFloat("WeeklyOFF", 2.99f);
        if (!PlayerPrefs.HasKey("MonthlyOFF"))
            PlayerPrefs.SetFloat("MonthlyOFF", 6.99f);
        if (!PlayerPrefs.HasKey("YearlyOFF"))
            PlayerPrefs.SetFloat("YearlyOFF", 16.99f);

        if (!PlayerPrefs.HasKey("AdsCanBeDisplayed"))
            PlayerPrefs.SetString("AdsCanBeDisplayed", "YES");
        

        if (!PlayerPrefs.HasKey("GamePlayCurrentAdsCounter"))
            PlayerPrefs.SetInt("GamePlayCurrentAdsCounter", 1);

        if (!PlayerPrefs.HasKey("OnMainScreenFromGamePlay"))
            PlayerPrefs.SetString("OnMainScreenFromGamePlay", "NO");
        PlayerPrefs.SetString("OnMainScreenFromGamePlay", "NO");

        //StandaloneCABallsort

        //if (!AdsManager.HaveSetupConsent)
        //{
        //    SharedUIManager.ConsentPanel.Show();
        //    yield return new WaitUntil(() => !SharedUIManager.ConsentPanel.Showing);
        //}
        yield return new WaitForSeconds(1f);
        StartCoroutine(GameManager.LoadSceneDelay("MainMenu"));
        //GameManager.LoadScene("MainMenu");
    }
}