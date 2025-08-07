
using System.Collections.Generic;
using System.Linq;
using dotmob;
using UnityEngine;

public class LevelsPanel : ShowHidable
{
    [SerializeField] private LevelTileUI _levelTileUIPrefab;
    [SerializeField] private RectTransform _content;

    public GameMode GameMode
    {
        get => _gameMode;
        set
        {
            _gameMode = value;
            //Debug.LogError("ajsdcghvaskfasf");
            var levels = ResourceManager.GetLevels(value).ToList();

            for (var i = 0; i < levels.Count; i++)
            {
                var level = levels[i];
                if (_tiles.Count<=i)
                {
                    var levelTileUI = Instantiate(_levelTileUIPrefab,_content);
                    levelTileUI.Clicked +=LevelTileUIOnClicked;
                    _tiles.Add(levelTileUI);
                }
                //Debug.LogError($"SubscriptionPurchased...{PlayerPrefs.GetString("SubscriptionPurchased")}");
                if (PlayerPrefs.GetString("SubscriptionPurchased") == "YES" || i < 10)
                {
                    _tiles[i].MViewModel = new LevelTileUI.ViewModel
                    {
                        Level = level,
                        //Locked = ResourceManager.IsLevelLocked(value, level.no),
                        Locked = false,
                        Completed = ResourceManager.GetCompletedLevel(value,level.no)
                        //Completed = ResourceManager.GetCompletedLevel(value) >= level.no

                    };

                }
                else
                {

                    //Uncomment this part to unlock all levels for the purpose of Testing
                    //And comment the lower part

                   /* _tiles[i].MViewModel = new LevelTileUI.ViewModel
                    {
                        Level = level,
                        Locked = false,
                        Completed = ResourceManager.GetCompletedLevel(value, level.no)
                    };*/

                    _tiles[i].MViewModel = new LevelTileUI.ViewModel
                    {
                        Level = level,
                        Locked = ResourceManager.IsLevelLocked(value, level.no),
                        Completed = ResourceManager.GetCompletedLevel(value, level.no)
                    };

                }

            }
            //Debug.LogError("hfhgfcvjbkn");

        }
    }



    public List<LevelTileUI> _tiles = new List<LevelTileUI>();
    private GameMode _gameMode;


    private void LevelTileUIOnClicked(LevelTileUI tileUI)
    {
        if (tileUI.MViewModel.Locked)
        {
            if (MainMenu.UIManager.Instance)
            {
                MainMenu.UIManager.Instance.proscreen.SetActive(true);
                MainMenu.UIManager.Instance.noAdsBtn.SetActive(true);
            }
            return;
        }
        string lastlevelplayed = "";
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
            lastlevelplayed = "EasyLastLevelPlayed";
        else if (PlayerPrefs.GetInt("SelectedMode") == 1)
            lastlevelplayed = "ChallangeLastLevelPlayed";
        else if (PlayerPrefs.GetInt("SelectedMode") == 2)
            lastlevelplayed = "ExpertLastLevelPlayed";

        //GameManager.LoadGame(new LoadGameData
        //{
        //    Level = UIManager.Instance.LevelsPanel._tiles[PlayerPrefs.GetInt(lastlevelplayed, 0)].MViewModel.Level,
        //    GameMode = (GameMode)PlayerPrefs.GetInt("SelectedMode")
        //});

        this.gameObject.SetActive(false);
        GameObject MM_bg = GameObject.Find("MainMenuBg");
        if (MM_bg)
            MM_bg.SetActive(false);

        GameManager.LoadGame(new LoadGameData
        {
            Level = tileUI.MViewModel.Level,
            GameMode = (GameMode)PlayerPrefs.GetInt("SelectedMode")
        });
        //GameManager.LoadGame(new LoadGameData
        //{
        //    Level = tileUI.MViewModel.Level,
        //    GameMode = GameMode

        //});
    }
}