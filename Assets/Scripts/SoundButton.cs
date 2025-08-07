using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundButton:MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Sprite[] _soundEnableAndDisableSprites;
    private Button _button;
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] float volume = 0.2f;
    [SerializeField] bool isSound;

    private void Awake()
    {
        //Debug.LogError(this.gameObject.transform.parent.name);

        //if (!isSound)
        {
            _button = GetComponent<Button>();
            AudioManagerOnSoundStateChanged(AudioManager.IsSoundEnable);
        }
    }

    private void OnEnable()
    {
        AudioManagerOnSoundStateChanged(AudioManager.IsSoundEnable);
        AudioManager.SoundStateChanged += AudioManagerOnSoundStateChanged;
    }

    private void OnDisable()
    {
            AudioManager.SoundStateChanged -= AudioManagerOnSoundStateChanged;
    }

    private void AudioManagerOnSoundStateChanged(bool b)
    {
        if (!isSound)
            _button.image.sprite = _soundEnableAndDisableSprites[b ? 0 : 1];
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSound)
        {
            AudioManager.IsSoundEnable = !AudioManager.IsSoundEnable;
            if (AudioManager.IsSoundEnable)
                AudioSource.PlayClipAtPoint(_clickClip, Camera.main.transform.position, volume);
        }
    }
    public void PlaySound()
    {
        if (AudioManager.IsSoundEnable)
            AudioSource.PlayClipAtPoint(_clickClip, Camera.main.transform.position, volume);

    }
}