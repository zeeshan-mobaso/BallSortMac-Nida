using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IAP;
using UnityEngine.SceneManagement;
public class SubscriptionHandler : MonoBehaviour
{

    public static SubscriptionHandler instance;

    public List<GameObject> allSelectedPlans;
    public List<GameObject> allUnselectedPlans;
    public List<GameObject> allPurchaseButtons;
    public GameObject noAdsBtn;
    public GameObject loader;

    public List<Text> weeklyOFF, monthlyOFF, yearlyOFF;
    public List<Text> weekly_Price, monthly_Price, yearly_Price;
    public List<Text> weekly_month, weekly_year, weekly;

    public Text freeTrialBtnDescription;
    //public GameObject banner;
    public GameModesHandler gameModesHandler;
    public GamePlayPanel gamePlayPanel;

    public Text price_Sign;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        //ShowAd.Instance.HideBanner(); DANI
        SelectPlans();
        ActivePurchaseBtn();
        allSelectedPlans[2].SetActive(true);
        allUnselectedPlans[2].SetActive(false);
        allPurchaseButtons[2].SetActive(true);
        UpdateSubscriptionPrices();
        UpdateOFFRate();//V1.5Checks
    }

    public void SelectPlans()
    {
        foreach (GameObject plan in allSelectedPlans)
            plan.SetActive(false);
        foreach (GameObject plan in allUnselectedPlans)
            plan.SetActive(true);

    }
    public void ActivePurchaseBtn()
    {
        foreach (GameObject purchaseBtn in allPurchaseButtons)
            purchaseBtn.SetActive(false);

    }
    public void BuySubscription(string productID)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("SubscriptionPurchased") != "YES")
            {
                SubscriptionPurchased(true);
                Purchaser.instance.BuyProductID(productID);
            }
            else
            {
                //Toast.instance.ShowMessage("You are already subscribed");
                Toast.instance.ShowMessage("Alert !", "You are already subscribed");

            }
        }
        else
        {
            //Toast.instance.ShowMessage("Please check your internet connection.");
            Toast.instance.ShowMessage("No internet !", "Please check your internet connection.");
        }
    }
    private void OnDisable()
    {
        if (PlayerPrefs.GetString("SubscriptionPurchased") != "YES")
        {
            if (noAdsBtn)
                noAdsBtn.SetActive(true);
        }
        else
        {
            if (noAdsBtn)
                noAdsBtn.SetActive(false);
        }

    }
    public void SubscriptionPurchased(bool status)
    {
        loader.SetActive(status);
    }
    public void UpdateSubscriptionPrices()
    {
        foreach (Text price in weekly_Price)
            price.text = PlayerPrefs.GetString("WeeklyPrice");
        foreach (Text price in monthly_Price)
            price.text = PlayerPrefs.GetString("MonthlyPrice");
        foreach (Text price in yearly_Price)
            price.text = PlayerPrefs.GetString("YearlyPrice");
    }
    public void UpdateOFFRate()
    {
        
        foreach (Text price in weeklyOFF)
            price.text = "3 Days Free Trial";

        float w_price, m_price, y_price;

        w_price = PlayerPrefs.GetFloat("WeeklyOFF");
        m_price = PlayerPrefs.GetFloat("MonthlyOFF");
        y_price = PlayerPrefs.GetFloat("YearlyOFF");


        string[] removeChars = new string[] {".",",", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string priceSign = PlayerPrefs.GetString("MonthlyPrice");
        foreach (string ch in removeChars)
        {
            priceSign = priceSign.Replace(ch, "");
        }
        foreach (Text price in weekly)
            price.text = priceSign + (w_price/1).ToString("#.00") + " /Week";
        freeTrialBtnDescription.text = $"Try Free for 3 Days, then {priceSign}{m_price / 4:F0}/Week";
        foreach (Text price in weekly_month)
            price.text = priceSign + (m_price/4).ToString("#.00") + " /Week";
        foreach (Text price in weekly_year)
            price.text = priceSign + (y_price/52).ToString("#.00") + " /Week";

        m_price = 100f - Mathf.Ceil((m_price / (w_price * 4)) * 100);
        y_price = 100f - Mathf.Ceil((y_price / (w_price * 52)) * 100);


        foreach (Text price in monthlyOFF)
            price.text = m_price.ToString() + "%";
        foreach (Text price in yearlyOFF)
            price.text = y_price.ToString() + "%";

    }
    private void Update()
    {
       // ShowAd.Instance.HideBanner(); DANI

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSubscriptionScreen();
        }
    }
    public void CloseSubscriptionScreen()
    {
        this.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //GameObject obj = GameObject.Find("LevelCompletePanel");
            //if (obj == null && gamePlayPanel)
            //    gamePlayPanel.EnableBallsAndHolders(true);
        }

    }
    public void ActiveModes()
    {
        if(gameModesHandler)
            gameModesHandler.ActiveModes();
    }
    public void RestorePurchases()
    {
        //Purchaser.instance.RestorePurchases();
    }
    public void OpenTermsOfService()
    {
        Application.OpenURL("https://vitalappstudio.blogspot.com/2024/04/terms%20of%20use.html");
    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://vitalappstudio.blogspot.com/2023/08/ball-sort-privacy-policy.html");
    }
}
