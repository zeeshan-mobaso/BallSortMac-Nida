using System;
using Game;
using dotmob;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel : ShowHidable
{
    [SerializeField] private Button _undoBtn;
    [SerializeField] private Text _lvlTxt;
    [SerializeField] private Text _lvlTutorial;
    [SerializeField] private GameObject _rewardedicon;
    public GameObject undoRewardedBtn, undoBtn;
    public Text _undomovestxt;
    bool giverewarde = false;
   
    private void Start()
    {
       // _lvlTxt.text = $"{LevelManager.Instance.Level.no}";
        _lvlTxt.text = "LEVEL " + LevelManager.Instance.Level.no;


        if (LevelManager.Instance.Level.no == 1)
        {
          //  _lvlTutorial.gameObject.SetActive(true);
        }
        else
        {
           // _lvlTutorial.gameObject.SetActive(false);
        }
        _undomovestxt.text = PlayerPrefs.GetInt("UndoMovesLeft").ToString();
    }
    public void WatchVideoAndAddHolder()
    {
        LevelManager.Instance.OnClickHolderAdd();

        //if (ShowAd.Instance.isRewardedAdLoaded())
        //{
        //    _rewardedicon.SetActive(false);
        //    ShowAd.Instance._rewardedAdType = ShowAd.RewardedAdType.AddHolder;
        //    ShowAd.Instance.ShowRewardedVideAdGeneric();
        //}

    }
    public void WatchVideoAndUnlockUndoMoves()
    {
        //DANI
        //if (ShowAd.Instance.isRewardedAdLoaded())
        //{
        //    ShowAd.Instance._rewardedAdType = ShowAd.RewardedAdType.UndoMove;
        //    undoRewardedBtn.SetActive(false);
        //    undoBtn.SetActive(true);
        //    PlayerPrefs.SetInt("UndoMovesLeft", 3);
        //    _undomovestxt.text = PlayerPrefs.GetInt("UndoMovesLeft").ToString();
        //    ShowAd.Instance.ShowRewardedVideAdGeneric();
        //}
        //DANI
    }
    public void OnClickHolderAdd()
    {
        LevelManager.Instance.OnClickHolderAdd();
    }
    public void OnClickUndo()
    {
        
        LevelManager.Instance.OnClickUndo();

    }

    public void OnClickRestart()
    {
        GameManager.LoadGame(new LoadGameData
        {
            Level = LevelManager.Instance.Level,
            GameMode = LevelManager.Instance.GameMode,
        },false);
    }

    public void OnClickSkip()
    {
        //StandaloneCABallsort
        //Debug.LogError("IsVideoAvailable....." + ShowAd.Instance.IsVideoAvailable());
        //Debug.LogError("SubscriptionPurchased....." + PlayerPrefs.GetString("SubscriptionPurchased"));

        //if (!ShowAd.Instance.IsVideoAvailable())
        //{
        //    SharedUIManager.PopUpPanel.ShowAsInfo("Notice", "Sorry no video ads available.Check your internet connection!");
        //    return;
        //}

        if (PlayerPrefs.GetString("SubscriptionPurchased") != "YES")
        {
            SharedUIManager.PopUpPanel.ShowAsConfirmation("Skip Level", "Do you want watch Video ads to skip this level", success =>
            {
                if (!success)
                    return;

               // DANI
                //ShowAd.ShowVideoAds(true, s =>
                //{
                //    if (!s)
                //        return;
                //});
               // DANI
            });
        }
        else
        {
            string lastlevelplayed = "";
            if (PlayerPrefs.GetInt("SelectedMode") == 0)
                lastlevelplayed = "EasyLastLevelPlayed";
            else if (PlayerPrefs.GetInt("SelectedMode") == 1)
                lastlevelplayed = "ChallangeLastLevelPlayed";
            else if (PlayerPrefs.GetInt("SelectedMode") == 2)
                lastlevelplayed = "ExpertLastLevelPlayed";

            PlayerPrefs.SetInt(lastlevelplayed, LevelManager.Instance.Level.no);
            //SharedUIManager.PopUpPanel.Hide();
            ResourceManager.CompleteLevel((GameMode)PlayerPrefs.GetInt("SelectedMode"), LevelManager.Instance.Level.no);
            UIManager.Instance.LoadNextLevel();
        }
            //Debug.LogError("dsaffadsfdfad");

    }

    public void OnClickMenu()
    {
        //StandaloneCABallsort
        //SharedUIManager.PopUpPanel.ShowAsConfirmation("Exit?", "Are you sure want to exit the game?", success =>
        // {
        //     if (!success)
        //         return;

        //     GameManager.LoadScene("MainMenu");
        // });
        SharedUIManager.PausePanel.Show();
    }
    public void OnClickBackButton()
    {
        GameManager.LoadScene("MainMenu");
        PlayerPrefs.SetInt("BackFromMainSceneToLevel", 1);
        PlayerPrefs.Save();
    }
    private void Update()
    {
        _undoBtn.interactable = LevelManager.Instance.HaveUndo;
    }
}