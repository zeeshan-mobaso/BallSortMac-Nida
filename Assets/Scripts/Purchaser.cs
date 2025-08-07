using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace IAP
{
    public class Purchaser : MonoBehaviour, IStoreListener
    {
        public string licensekey;
        private static IStoreController m_StoreController;
        private static IExtensionProvider m_StoreExtensionProvider;

        public Text BuyButtonText, PriceTextWeekly, PriceTextMonthly, PriceTextYearly, DescTextHotOffer, DescTextMonthly, DescTextYearly;
        public GameObject RadioButtonHotOffer, RadioButtonMonthlySub, RadioButtonYearlySub;
        bool offer1, offer2, offer3;
        public string subscriptionIDWeekly = "com.colorgame.weekly.pro";
        public string subscriptionIDMonthly = "com.colorgame.monthly.pro";
        public string subscriptionIDYearly = "com.colorgame.yearly.pro";

        public static Purchaser instance;
        public Text restore;
        public GameObject restore_Btn;
        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            if (m_StoreController == null)
            {
                InitializePurchasing();
            }

            //btnSelectHotOffer();
        }

        public void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(subscriptionIDWeekly, ProductType.Subscription);
            builder.AddProduct(subscriptionIDMonthly, ProductType.Subscription);
            builder.AddProduct(subscriptionIDYearly, ProductType.Subscription);

            UnityPurchasing.Initialize(this, builder);
        }

        private bool IsInitialized()
        {
#if UNITY_IOS
            return m_StoreController != null && m_StoreExtensionProvider != null;
#else
            return m_StoreController != null;
#endif

        }



        public void BuyProductID(string productId)
        {
            try
            {

                if (IsInitialized())
                {
                    Product product = m_StoreController.products.WithID(productId);
                    if (product != null && product.availableToPurchase)
                    {
                        Debug.LogError(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                        m_StoreController.InitiatePurchase(product);
                    }
                    else
                    {
                        if (SubscriptionHandler.instance)
                            SubscriptionHandler.instance.SubscriptionPurchased(false);
                    }
                }
                else
                {
                    if (SubscriptionHandler.instance)
                        SubscriptionHandler.instance.SubscriptionPurchased(false);
                }
            }
            catch (Exception e)
            {
                if (SubscriptionHandler.instance)
                    SubscriptionHandler.instance.SubscriptionPurchased(false);
            }
        }

        public void btnSelectHotOffer()
        {
            RadioButtonHotOffer.SetActive(true);
            RadioButtonMonthlySub.SetActive(false);
            RadioButtonYearlySub.SetActive(false);

            offer1 = true;
            offer2 = false;
            offer3 = false;

            BuyButtonText.text = "START FREE TRIAL";
        }

        public void btnSelectMonthlyOffer()
        {
            RadioButtonHotOffer.SetActive(false);
            RadioButtonMonthlySub.SetActive(true);
            RadioButtonYearlySub.SetActive(false);

            offer1 = false;
            offer2 = true;
            offer3 = false;

            BuyButtonText.text = "CONTINUE";
        }

        public void btnSelectYearlyOffer()
        {
            RadioButtonHotOffer.SetActive(false);
            RadioButtonMonthlySub.SetActive(false);
            RadioButtonYearlySub.SetActive(true);

            offer1 = false;
            offer2 = false;
            offer3 = true;

            BuyButtonText.text = "CONTINUE";
        }


        public void btnBuySubscription()
        {
            if (offer1)
            {
            }
            else if (offer2)
            {
            }
            else if (offer3)
            {
            }
        }

        public void RestorePurchases()
        {
            if (!IsInitialized())
            {
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                //SubscriptionHandler.instance.SubscriptionPurchased(true);
                bool status = false;
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result) =>
                {
                    Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                    //if (SubscriptionHandler.instance)
                    //    SubscriptionHandler.instance.SubscriptionPurchased(false);
                    status = result;

                });
                if (!status)
                {
                    PlayerPrefs.SetString("SubscriptionPurchased", "NO");
                    //ShowAd.Instance.ShowBanner();
                    if (SubscriptionHandler.instance)
                        SubscriptionHandler.instance.SubscriptionPurchased(false);
                    if (MainMenu.UIManager.Instance)
                       MainMenu.UIManager.Instance.noAdsBtn.SetActive(true);
                }
                else
                {

                }
                //restore.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                if (SubscriptionHandler.instance)
                    SubscriptionHandler.instance.SubscriptionPurchased(false);
                Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
            }
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.LogError("OnInitialized: Completed!");

            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;


            //for(int i=0;i< m_StoreController.products.all.Length;i++)

            //foreach (Product pro in m_StoreController.products.all)
            //    Debug.LogError(pro.metadata.localizedTitle);

            //#if UNITY_EDITOR
            //            if (MainMenu.UIManager.Instance)
            //                MainMenu.UIManager.Instance.proscreen.SetActive(true);
            //#endif
            RestorePurchases();//V1.5Checks
            checkSubscriptionEnded();
            UpdateSubscriptionPrices();

        }
        void UpdateSubscriptionPrices()
        {

            PlayerPrefs.SetString("WeeklyPrice", GetWeeklyPrice());
            PlayerPrefs.SetString("MonthlyPrice", GetMonthlyPrice());
            PlayerPrefs.SetString("YearlyPrice", GetYearlyPrice());


            //PriceTextWeekly.text = PlayerPrefs.GetString("WeeklyPrice");

            float w_price = float.Parse(m_StoreController.products.WithID(subscriptionIDWeekly).metadata.localizedPrice.ToString());
            float m_price = float.Parse(m_StoreController.products.WithID(subscriptionIDMonthly).metadata.localizedPrice.ToString());
            float y_price = float.Parse(m_StoreController.products.WithID(subscriptionIDYearly).metadata.localizedPrice.ToString());


            PlayerPrefs.SetFloat("WeeklyOFF", w_price);
            PlayerPrefs.SetFloat("MonthlyOFF", m_price);
            PlayerPrefs.SetFloat("YearlyOFF", y_price);

            //V1.5Checks
            //if (SubscriptionHandler.instance)
            //{
            //    SubscriptionHandler.instance.UpdateSubscriptionPrices();
            //    SubscriptionHandler.instance.UpdateOFFRate();

            //}

            //PlayerPrefs.SetFloat("WeeklyPrice", GetWeeklyPrice());

        }
        public string GetWeeklyPrice()
        {
            if (m_StoreController != null)
                return m_StoreController.products.WithID(subscriptionIDWeekly).metadata.localizedPriceString;
            else
                return "$2.99";
        }
        public string GetMonthlyPrice()
        {
            if (m_StoreController != null)
                return m_StoreController.products.WithID(subscriptionIDMonthly).metadata.localizedPriceString;
            else
                return "$6.99";
        }
        public string GetYearlyPrice()
        {
            if (m_StoreController != null)
                return m_StoreController.products.WithID(subscriptionIDYearly).metadata.localizedPriceString;
            else
                return "$16.99";
        }


        void checkSubscriptionEnded()
        {
#if !UNITY_EDITOR
            if (!m_StoreController.products.WithID(subscriptionIDWeekly).hasReceipt &&
                !m_StoreController.products.WithID(subscriptionIDMonthly).hasReceipt &&
                !m_StoreController.products.WithID(subscriptionIDYearly).hasReceipt)
            {
                ShowAd.inappinitialized = true;
                PlayerPrefs.SetString("SubscriptionPurchased", "NO");
                restore_Btn.SetActive(true);
                if(MainMenu.UIManager.Instance)
                   MainMenu.UIManager.Instance.noAdsBtn.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetString("SubscriptionPurchased", "YES");
                restore_Btn.SetActive(false);
                if (MainMenu.UIManager.Instance)
                   MainMenu.UIManager.Instance.noAdsBtn.SetActive(false);
                //SubscriptionHandler.instance.banner.SetActive(false);
                //  ShowAd.Instance.HideBanner(); DANI
            }
#endif


        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError("OnInitializeFailed InitializationFailureReason:" + error);
            ShowAd.inappinitialized = false;
            //ShowAd.Instance.ShowBanner();
            if (SubscriptionHandler.instance)
            {
                SubscriptionHandler.instance.SubscriptionPurchased(false);
            }
            if (MainMenu.UIManager.Instance)
                   MainMenu.UIManager.Instance.noAdsBtn.SetActive(true);
            }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            Debug.LogError(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            if (String.Equals(args.purchasedProduct.definition.id, subscriptionIDWeekly, StringComparison.Ordinal) ||
                String.Equals(args.purchasedProduct.definition.id, subscriptionIDMonthly, StringComparison.Ordinal) ||
                String.Equals(args.purchasedProduct.definition.id, subscriptionIDYearly, StringComparison.Ordinal))
            {
                PlayerPrefs.SetString("SubscriptionPurchased", "YES");
                restore_Btn.SetActive(false);
                if (SubscriptionHandler.instance)
                {
                    SubscriptionHandler.instance.SubscriptionPurchased(false);
                    SubscriptionHandler.instance.ActiveModes();
                    SubscriptionHandler.instance.CloseSubscriptionScreen();
                    SubscriptionHandler.instance.noAdsBtn.SetActive(false);
                    //if (SubscriptionHandler.instance.gamePlayPanel)
                    //SubscriptionHandler.instance.gamePlayPanel.EnableBallsAndHolders(true);
                }
                ObjectsHandler store = GameObject.FindObjectOfType<ObjectsHandler>();
                if (store)
                {
                    store.EnableLocks(false);
                }
                GameObject[] levellocks = FindGameObjectsWithName("LevelLockMark");
                foreach (GameObject lvllock in levellocks)
                    lvllock.SetActive(false);

                LevelTileUI[] lvltilesUI = GameObject.FindObjectsOfType<LevelTileUI>();
                foreach (LevelTileUI lvltile in lvltilesUI)
                {
                    lvltile.SetImageColor(1f);
                    lvltile.ShowLevelText(true);
                }

                //ShowAd.Instance.HideBanner(); DANI
                if (MainMenu.UIManager.Instance)
                   MainMenu.UIManager.Instance.noAdsBtn.SetActive(false);

            }
            return PurchaseProcessingResult.Complete;
        }
        GameObject[] FindGameObjectsWithName(string name)
        {
            int a = GameObject.FindObjectsOfType<GameObject>().Length;
            GameObject[] arr = new GameObject[a];
            int FluentNumber = 0;
            for (int i = 0; i < a; i++)
            {
                if (GameObject.FindObjectsOfType<GameObject>()[i].name == name)
                {
                    arr[FluentNumber] = GameObject.FindObjectsOfType<GameObject>()[i];
                    FluentNumber++;
                }
            }
            Array.Resize(ref arr, FluentNumber);
            return arr;
        }
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
            //ShowAd.Instance.ShowBanner();
            if (SubscriptionHandler.instance)
                SubscriptionHandler.instance.SubscriptionPurchased(false);
            if (MainMenu.UIManager.Instance)
               MainMenu.UIManager.Instance.noAdsBtn.SetActive(true);
            //if (LocalizationManager.instance)
            //{
            //    GameManager.Instance.ShowMessage(LocalizationManager.instance.ChangeLanguageText("Purchase failed. Please try again"));
            //}
            //else
            //{
            //    GameManager.Instance.ShowMessage("Purchase failed. Please try again");
            //}
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            throw new NotImplementedException();
        }

    }
}