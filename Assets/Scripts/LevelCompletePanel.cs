using System.Collections;
using System.Collections.Generic;
using Game;
using dotmob;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletePanel : ShowHidable
{
    [SerializeField] private Text _toastTxt;
    [SerializeField] private Text _txtLevel;
    [SerializeField] private List<string> _toasts = new List<string>();
    [SerializeField] private List<Sprite> emojies;
    [SerializeField] private Image EMOJI;


    private void Start()
    {
        _txtLevel.text = $"Level {LevelManager.Instance.Level.no}";
    }

    protected override void OnShowCompleted()
    {
        base.OnShowCompleted();
        _toastTxt.text = _toasts.GetRandom();
        _toastTxt.gameObject.SetActive(true);
        EMOJI.overrideSprite = emojies.GetRandom();
        StartCoroutine(NextLevel(2f));
    }

    IEnumerator NextLevel(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        OnClickContinue();
    }
    public void OnClickContinue()
    {
        UIManager.Instance.LoadNextLevel();
    }
    public void OnClickMainMenu()
    {
        GameManager.LoadScene("MainMenu");
    }
}