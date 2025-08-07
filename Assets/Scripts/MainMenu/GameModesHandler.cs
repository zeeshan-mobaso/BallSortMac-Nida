using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using dotmob;
using MainMenu;
public class GameModesHandler : MonoBehaviour
{

    [SerializeField] Toggle[] modes;
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] float volume = 0.2f;
    [SerializeField] private List<GameObject> lockedModes;
    private void OnEnable()
    {
        //Debug.LogError(PlayerPrefs.GetInt("SelectedMode"));
        modes[PlayerPrefs.GetInt("SelectedMode")].isOn = true;
        //if (PlayerPrefs.GetString("SubscriptionPurchased") == "YES")
        //    ActiveModes();
        ActiveModes();


        //ChangeMode(PlayerPrefs.GetInt("SelectedMode"));
    }
    public void ActiveModes()
    {
        foreach (GameObject lm in lockedModes)
            lm.SetActive(false);
    }
    public void ChangeMode(int mode)
    {
        PlayerPrefs.SetInt("SelectedMode", mode);
        if (AudioManager.IsSoundEnable)
            AudioSource.PlayClipAtPoint(_clickClip, Camera.main.transform.position, volume);


        string lastlevelplayed = "";
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
            lastlevelplayed = "EasyLastLevelPlayed";
        else if (PlayerPrefs.GetInt("SelectedMode") == 1)
            lastlevelplayed = "ChallangeLastLevelPlayed";
        else if (PlayerPrefs.GetInt("SelectedMode") == 2)
            lastlevelplayed = "ExpertLastLevelPlayed";
        //Debug.LogError(PlayerPrefs.GetInt(lastlevelplayed));

        //if (PlayerPrefs.GetInt(lastlevelplayed) > 99)
        //    PlayerPrefs.SetInt(lastlevelplayed, 99);


        GameManager.SetMode(new LoadGameData
        {
            Level = UIManager.Instance.LevelsPanel._tiles[PlayerPrefs.GetInt(lastlevelplayed, 0)].MViewModel.Level,
            GameMode = (GameMode)PlayerPrefs.GetInt("SelectedMode")
        });

        OnClickButton(5);

    }
    public void OnClickButton(int mode)
    {
        var levelsPanel = UIManager.Instance.LevelsPanel;
        levelsPanel.GameMode = (GameMode)(PlayerPrefs.GetInt("SelectedMode", 0));
        levelsPanel.Show();

    }
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //}

}
