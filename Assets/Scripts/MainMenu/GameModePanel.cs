using UnityEngine;
using dotmob;
namespace MainMenu
{
    public class GameModePanel : ShowHidable
    {

        public void OnClickButton(int mode)
        {
            var levelsPanel = UIManager.Instance.LevelsPanel;
            levelsPanel.GameMode = (GameMode)(PlayerPrefs.GetInt("SelectedMode", 0));
            levelsPanel.Show();

            //string lastlevelplayed = "";
            //if (PlayerPrefs.GetInt("SelectedMode") == 0)
            //    lastlevelplayed = "EasyLastLevelPlayed";
            //else if (PlayerPrefs.GetInt("SelectedMode") == 1)
            //    lastlevelplayed = "ChallangeLastLevelPlayed";
            //else if (PlayerPrefs.GetInt("SelectedMode") == 2)
            //    lastlevelplayed = "ExpertLastLevelPlayed";

            //GameManager.LoadGame(new LoadGameData
            //{
            //    Level = UIManager.Instance.LevelsPanel._tiles[PlayerPrefs.GetInt(lastlevelplayed, 0)].MViewModel.Level,
            //    GameMode = (GameMode)PlayerPrefs.GetInt("SelectedMode")
            //});

        }
    }
}