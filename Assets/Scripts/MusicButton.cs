using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MusicButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite[] _musicEnableAndDisableSprites;
    private Button _button;
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] float volume = 0.2f;
    [SerializeField] bool isMusic;
    [SerializeField] AudioSource audio_source;


    private void Awake()
    {
        //{
        _button = GetComponent<Button>();
        AudioManagerOnMusicStateChanged(AudioManager.IsMusicEnable);
        //}
    }

    private void OnEnable()
    {
        AudioManagerOnMusicStateChanged(AudioManager.IsMusicEnable);
        AudioManager.MusicStateChanged += AudioManagerOnMusicStateChanged;
    }

    private void OnDisable()
    {
        AudioManager.MusicStateChanged -= AudioManagerOnMusicStateChanged;
    }

    private void AudioManagerOnMusicStateChanged(bool b)
    {
        //if (!isMusic)
        _button.image.sprite = _musicEnableAndDisableSprites[b ? 0 : 1];
        audio_source.mute = !AudioManager.IsMusicEnable;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        //if (!isMusic)
        {
            AudioManager.IsMusicEnable = !AudioManager.IsMusicEnable;
        }
    }
    //public void PlayMusic()
    //{
    //    if (AudioManager.IsMusicEnable)
    //        AudioSource.PlayClipAtPoint(_clickClip, Camera.main.transform.position, volume);

    //}
}