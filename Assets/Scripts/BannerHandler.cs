using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerHandler : MonoBehaviour
{
    public string link = "https://apps.apple.com/us/app/antistress-relaxing-games/";

    public List<GameObject> banners;
    public float bannerRefreshTime;
    int index = 0;
    private void OnEnable()
    {
        if (PlayerPrefs.GetString("SubscriptionPurchased") != "NO")
        {
            //Debug.LogError("SubscriptionPurchased");

            this.gameObject.SetActive(false);

        }
        else
        {
            //Debug.LogError("dasdfdfad");
            index = Random.Range(0, 3);
            StartCoroutine(ChangeBanner());
        }
    }
    IEnumerator ChangeBanner()
    {
        do
        {
            //yield return new WaitForSeconds(0f);
            foreach (GameObject banner in banners)
                banner.SetActive(false);
            banners[index].SetActive(true);
            yield return new WaitForSeconds(bannerRefreshTime);
            if (index < banners.Count - 1)
                index += 1;
            else
                index = 0;

        } while (true);
    }

    public void OnBannerClicked(string appid)
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
        //    //Debug.LogError("balls......");
        //    SubscriptionHandler.instance.gamePlayPanel.EnableBallsAndHolders(false);
        //}
    }
}
