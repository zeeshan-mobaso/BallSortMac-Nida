using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreHandler : MonoBehaviour
{
    [SerializeField] List<Image> storeOptions;
    [SerializeField] List<GameObject> selectedStoreOptionPanel;
    [SerializeField] List<string> titles;
    [SerializeField] Text title;

    public void SelectStoreOption(int index)
    {
        foreach(Image img in storeOptions)
            img.color = new Color(1f, 1f, 1f, 0f);
        foreach (GameObject storeoption in selectedStoreOptionPanel)
            storeoption.SetActive(false);

        storeOptions[index].color = new Color(1f, 1f, 1f, 1f);
        selectedStoreOptionPanel[index].SetActive(true);
        title.text=titles[index];
    }
    public void ShowRewardedVideo(GameObject objectToUnlock)
    {
        ShowAd.Instance.objectToUnlock = objectToUnlock.name;
        ShowAd.Instance.rewardedLock = objectToUnlock;
        ShowAd.Instance._rewardedAdType = ShowAd.RewardedAdType.UnlockBallType;


        //if (PlayerPrefs.GetString("SubscriptionPurchased") != "YES")
      //  ShowAd.Instance.ShowRewardedVideAdGeneric(); DANI
    }
}
