using System.Collections;
using dotmob;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private LevelCompletePanel _levelCompletePanel;
        [SerializeField] private TutorialPanel _tutorialPanel;
        [SerializeField] private GameObject _winEffect;
        public static int currentCounter = -1;
        public int interstitialcounter = 3 ;
        public GameObject MacInterstitialAds;
        public GameObject proscreen;
        public void EnableSubscriptionScreen(bool status)
        {
            proscreen.SetActive(status);
        }


        public static bool IsFirstTime
        {
            get => PrefManager.GetBool(nameof(IsFirstTime), true);
            set => PrefManager.SetBool(nameof(IsFirstTime), value);
        }
        private void Awake()
        {
            Instance = this;
            if (IsFirstTime)
            {
                //_tutorialPanel.Show();
                IsFirstTime = false;
            }
        }

        private void OnEnable()
        {
            //  Debug.Log(" Rating Available "+ RatingPopUp.Available);
            if (ShowAd.showRateUsCounter % 2 == 0 && RatingPopUp.Available)
            {
                SharedUIManager.RatingPopUp.Show();
            }
            ShowAd.showRateUsCounter++;
            LevelManager.LevelCompleted += LevelManagerOnLevelCompleted;
            if (currentCounter >= 0)
                CallAds();
            else
                currentCounter++;
        }
        public void HideRateUsPopUp()
        {
            SharedUIManager.RatingPopUp.Hide();
        }

        private void OnDisable()
        {
            LevelManager.LevelCompleted -= LevelManagerOnLevelCompleted;
        }
        private void LevelManagerOnLevelCompleted()
        {
            StartCoroutine(LevelCompletedEnumerator());
        }

        private IEnumerator LevelCompletedEnumerator()
        {


            yield return new WaitForSeconds(0.5f);
            _levelCompletePanel.Show();

            Ball[] balls =FindObjectsOfType<Ball>();
            foreach (Ball b in balls)
                b.gameObject.SetActive(false);
            Holder[] holders = FindObjectsOfType<Holder>();
            foreach (Holder h in holders)
                h.gameObject.SetActive(false);

            yield return new WaitForSeconds(0.3f);
            var point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f)).WithZ(0);
            Instantiate(_winEffect, point, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        public void CallAds()
        {
            if (PlayerPrefs.GetString("SubscriptionPurchased") != "YES")
            {
                currentCounter++;
                if (currentCounter % interstitialcounter == 0)
                {
                    currentCounter = 0;
                    MacInterstitialAds.SetActive(true);
                }
            }
        }



        public void LoadNextLevel()
        {
            var gameMode = LevelManager.Instance.GameMode;
            var levelNo = LevelManager.Instance.Level.no;
            if (!ResourceManager.HasLevel(gameMode, levelNo + 1))
            {
                _levelCompletePanel.Hide();
                SharedUIManager.PopUpPanel.ShowAsInfo("Congratulations!", "You have successfully Completed this Game Mode",
                    () =>
                    {
                        GameManager.LoadScene("MainMenu");
                    });
                return;
            }
            _levelCompletePanel.Hide();
            GameManager.LoadGame(new LoadGameData
            {
                Level = ResourceManager.GetLevel(gameMode, levelNo + 1),
                GameMode = gameMode
            });
        }
    }
}