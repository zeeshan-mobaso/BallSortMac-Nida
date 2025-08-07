using System.Collections;
using System.Collections.Generic;
using dotmob;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PausePanel : ShowHidable
{
    [SerializeField] private Button _removeAdsBtn;
    [SerializeField] private Button _menuBtn;
    [SerializeField] private Button _restorePurchaseBtn;


    private void Awake()
    {
//#if IN_APP
//        _removeAdsBtn.gameObject.SetActive(ResourceManager.EnableAds);
//#else
//        _removeAdsBtn.gameObject.SetActive(false);
//#endif




//        _restorePurchaseBtn.gameObject.SetActive(Application.platform == RuntimePlatform.IPhonePlayer
//#if IN_APP
//                             && ResourceManager.AbleToRestore
//#endif
//        );
    }


    //#if IN_APP
    //    private void OnEnable()
    //    {
    //        ResourceManager.ProductRestored += ResourceManagerOnProductRestored;
    //        ResourceManager.ProductPurchased += ResourceManagerOnProductPurchased;
    //    }
    //    private void OnDisable()
    //    {
    //        ResourceManager.ProductRestored -= ResourceManagerOnProductRestored;
    //        ResourceManager.ProductPurchased -= ResourceManagerOnProductPurchased;
    //    }

    //    private void ResourceManagerOnProductPurchased(string productId)
    //    {
    //        gameObject.SetActive(ResourceManager.EnableAds);
    //    }


    //    private void ResourceManagerOnProductRestored(bool b)
    //    {
    //        gameObject.SetActive(ResourceManager.AbleToRestore);
    //    }

    //#endif
   
    public void OnClickCloseButton()
    {
        SharedUIManager.PausePanel.Hide();
    }
    public void OnClickMenuButton()
    {
        //Debug.Log("Go to menu");
        PlayerPrefs.SetString("OnMainScreenFromGamePlay", "YES");
        SharedUIManager.PausePanel.Hide();
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball b in balls)
            b.gameObject.SetActive(false);
        Holder[] holders = FindObjectsOfType<Holder>();
        foreach (Holder h in holders)
            h.gameObject.SetActive(false);

        GameObject Gp_bg = GameObject.Find("GamePlayPanel");
        if(Gp_bg)
            Gp_bg.SetActive(false);


        GameManager.LoadScene("MainMenu");
        PlayerPrefs.SetInt("BackFromMainScene", 1);
        PlayerPrefs.Save();
        //SceneManager.LoadSceneAsync("MainMenu");
        //SharedUIManager.PausePanel.Hide();
    }

    public void OnClickRemoveAds()
    {
        //Debug.Log("Remove Ads");
//#if IN_APP
//        ResourceManager.PurchaseNoAds(success => { });
//#endif
    }

    public void OnClickRestore()
    {
//#if IN_APP
//        ResourceManager.RestorePurchase();
//#endif
    }
    public void OnClickRestart()
    {
        SharedUIManager.PausePanel.Hide();
        GameManager.LoadScene("Main");

        //GameManager.LoadGame(new LoadGameData
        //{
        //    Level = LevelManager.Instance.Level,
        //    GameMode = LevelManager.Instance.GameMode,
        //}, false);

    }
}
