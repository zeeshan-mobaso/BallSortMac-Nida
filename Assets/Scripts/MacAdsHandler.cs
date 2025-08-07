using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MacAdsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public static int index_adstoshow = 0;
    public List<GameObject> interstitail_gameAds;

    void OnEnable()
    {
        EnableAds(false);
        interstitail_gameAds[index_adstoshow % 2].SetActive(true);
        index_adstoshow += 1;
    }
    void OnDisable()
    {
        EnableAds(false);
    }
    void EnableAds(bool status)
    {
        foreach (GameObject ads in interstitail_gameAds)
            ads.gameObject.SetActive(status);
    }
    public void VisitGamePage(string GameSoreLink)
    {
        Application.OpenURL(GameSoreLink);
    }

}
