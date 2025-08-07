using System;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelTileUI : MonoBehaviour,IPointerClickHandler
{
    public event Action<LevelTileUI> Clicked;

    [SerializeField] private Text _txt;
    [SerializeField] private GameObject _completeMark;
    [SerializeField] private GameObject _lockMark;
    [SerializeField] private Image _fillImg;

    private ViewModel _mViewModel;


    public ViewModel MViewModel
    {
        get => _mViewModel;
        set
        {
            _txt.text = value.Level.no.ToString();
            //_fillImg.color = _fillImg.color.WithAlpha(value.Locked ? 0 : 1);
            SetImageColor(value.Locked ? 0 : 1);
            _completeMark.SetActive(value.Completed);
            _lockMark.SetActive(value.Locked);
            //_lockMark.SetActive(false);
            _mViewModel = value;
            _txt.gameObject.SetActive(value.Locked ? false : true);

            //_txt.gameObject.SetActive(true);
            //if (!value.Locked)
            //    _txt.color = new Color(1f, 1f, 1f, 1f);

            //0.131052
            //0.2008878
            //0.2924528
        }
    }
    public void SetImageColor(float alphaVal)
    {
        _fillImg.color = _fillImg.color.WithAlpha(alphaVal);
    }
    public void ShowLevelText(bool status)
    {
        _txt.gameObject.SetActive(status);

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(this);
    }


    public struct ViewModel
    {
        public Level Level { get; set; }
        public bool Locked { get; set; }
        public bool Completed { get; set; }
    }
}