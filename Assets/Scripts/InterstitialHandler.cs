using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialHandler : MonoBehaviour
{
    public string link = "https://apps.apple.com/us/app/antistress-relaxing-games/";

    public List<GameObject> interstitials;
    int index = 0;
    private void OnEnable()
    {
        foreach (GameObject banner in interstitials)
            banner.SetActive(false);

        index = Random.Range(0, 3);
        interstitials[index].SetActive(true);
    }

    public void OnInterstitialClicked(string appid)
    {
        Application.OpenURL(appid);
    }
    public void ShowSubscriptionScreen()
    {
        //if (MainMenu.UIManager.Instance)
        //    MainMenu.UIManager.Instance.subscriptionscreen.SetActive(true);
        //else if (Game.UIManager.Instance)
        //    Game.UIManager.Instance.subscriptionscreen.SetActive(true);
        //if (SubscriptionHandler.instance.gamePlayPanel)
        //{
        //    SubscriptionHandler.instance.gamePlayPanel.EnableBallsAndHolders(false);
        //}
    }
}
