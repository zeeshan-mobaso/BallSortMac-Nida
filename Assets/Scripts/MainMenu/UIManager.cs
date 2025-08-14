using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MainMenu
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private LevelsPanel _levelsPanel;
        [SerializeField] private GameModePanel _gameModePanel;
        public GameModePanel GameModePanel => _gameModePanel;
        public LevelsPanel LevelsPanel => _levelsPanel;
        public GameObject quitpanel,rateuspanel;
       public GameObject noAdsBtn;
        public GameObject proscreen;

        private void Awake()
        {
            Instance = this;
            if (!PlayerPrefs.HasKey("SelectedMode"))
                PlayerPrefs.SetInt("SelectedMode", 0);
            if (!PlayerPrefs.HasKey("EasyLastLevelPlayed"))
                PlayerPrefs.SetInt("EasyLastLevelPlayed", 0);
            if (!PlayerPrefs.HasKey("ChallangeLastLevelPlayed"))
                PlayerPrefs.SetInt("ChallangeLastLevelPlayed", 0);
            if (!PlayerPrefs.HasKey("ExpertLastLevelPlayed"))
                PlayerPrefs.SetInt("ExpertLastLevelPlayed", 0);

            var levelsPanel = LevelsPanel;
            levelsPanel.GameMode = (GameMode)(PlayerPrefs.GetInt("SelectedMode", 0));
       

        }

    public void hideLevelPanel()
        { 
            _levelsPanel.gameObject.SetActive(false);
        }
        private void Start()
        {
            if (PlayerPrefs.GetInt("BackFromMainScene", 0) == 1)
            {
                Debug.Log("User came back from Main Scene, skipping Pro Screen.");
                PlayerPrefs.SetInt("BackFromMainScene", 0); // Reset for next time
                return;
            }
            if (PlayerPrefs.GetInt("BackFromMainSceneToLevel", 0) == 1)
            {
                Debug.Log("User came back from Main Scene, skipping Pro Screen.");
                PlayerPrefs.SetInt("BackFromMainSceneToLevel", 0); // Reset for next time
                _levelsPanel.gameObject.SetActive(true);
                return;
            }
            Debug.LogError("Now I am on home...");
            ShowAd.showRateUsCounter = -1;
            if (PlayerPrefs.GetString("SubscriptionPurchased") == "NO")
            { //here I need to set logic of not showing pro screen
                proscreen.SetActive(true);
                noAdsBtn.SetActive(true);
            }
            StartCoroutine(DisplayAds());
        }
        IEnumerator DisplayAds()
        {
            yield return new WaitForSeconds(0.5f);
            if (PlayerPrefs.GetString("OnMainScreenFromGamePlay") == "YES")
            {

            }
        }
        public void ShareGame()
        {
        }
        public void QuitGame()
        {
            Application.Quit();
        }
        public void RateUS()
        {
            //Application.OpenURL("https://play.google.com/store/apps/details?id=httpcom.ballsort.colorballs.puzzle.game");
            Application.OpenURL("https://apps.apple.com/us/app/color-match-sorting-games/id6479373476");

        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject sub = GameObject.Find("SubscriptionScreenCanvas");
                if (sub == null)
                {
                  //  rateuspanel.SetActive(true); //DANI
                    quitpanel.SetActive(true);
                }
            }
        }
        private void OnEnable()
        {
            string lastlevelplayed = "";
            if (PlayerPrefs.GetInt("SelectedMode") == 0)
                lastlevelplayed = "EasyLastLevelPlayed";
            else if (PlayerPrefs.GetInt("SelectedMode") == 1)
                lastlevelplayed = "ChallangeLastLevelPlayed";
            else if (PlayerPrefs.GetInt("SelectedMode") == 2)
                lastlevelplayed = "ExpertLastLevelPlayed";
            if (PlayerPrefs.GetInt(lastlevelplayed) > 99)
                PlayerPrefs.SetInt(lastlevelplayed, 99);
            if (PlayerPrefs.GetString("SubscriptionPurchased") == "NO")
            { 
                noAdsBtn.SetActive(true);
            }

            //ShowAd.Instance.ShowBanner(); //DANI
            //ShowAd.Instance.ShowAppOpenAd();

        }
        public void ShowProScreen()
        {
        }
    }
}