using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainAudioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _effectsVolumeSlider;
    [SerializeField] private Slider _UIVolumeSlider;

    [Header("Mute toggler")]
    [SerializeField] private Toggle _toggleSound;

    [Header("Only in the game menu")]
    [SerializeField] private Image _buttonImage;

    [SerializeField] private Sprite _secondSprite;

    private Sprite _currentSprite;

    #region VolumeLevels

    public static float MasterVolumeLevel;
    public static float MusicVolumeLevel;
    public static float EffectsVolumeLevel;
    public static float UIVolumeLevel;

    #endregion

    public static bool IsMuted;
    private static bool s_isNotFirstStart;
    private bool _isCanChangeSprite = true;


    void Start()
    {
        if (!s_isNotFirstStart)
        {
            FirstStart();
        }

        UpdateComponentsOnScreen();
        s_isNotFirstStart = true;
    }


    private void ChangeSprite()
    {
        _currentSprite = _buttonImage.sprite;
        _buttonImage.sprite = _secondSprite;
        _secondSprite = _currentSprite;
    }


    private void FirstStart()
    {
        SaveDataController.LoadVolumeValue();
    }


    private void UpdateComponentsOnScreen()
    {
        _masterVolumeSlider.value = MasterVolumeLevel;
        _musicVolumeSlider.value = MusicVolumeLevel;
        _effectsVolumeSlider.value = EffectsVolumeLevel;
        _UIVolumeSlider.value = UIVolumeLevel;

        _toggleSound.isOn = IsMuted;
    }


    public void MuteOnToggleClick(bool flag)
    {
        _toggleSound.isOn = flag;

        if (flag)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }

        IsMuted = flag;

        SaveDataController.SaveMuteState(IsMuted);

        SoundOnButtonClick();

        if (_buttonImage != null && _secondSprite != null && _isCanChangeSprite)
        {
            _isCanChangeSprite = false;
            ChangeSprite();
            StartCoroutine(CanChangeDelay());
        }
    }


    public void SetMasterVolumeLevel(float sliderValue)
    {
        _mainAudioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
        MasterVolumeLevel = sliderValue;
        SaveDataController.SaveVolumeValue("Master", sliderValue);
    }


    public void SetMusicLevel(float sliderValue)
    {
        _mainAudioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
        MusicVolumeLevel = sliderValue;
        SaveDataController.SaveVolumeValue("Music", sliderValue);
    }


    public void SetEffectsLevel(float sliderValue)
    {
        _mainAudioMixer.SetFloat("Effects", Mathf.Log10(sliderValue) * 20);
        EffectsVolumeLevel = sliderValue;
        SaveDataController.SaveVolumeValue("Effects", sliderValue);
    }


    public void SetUILevel(float sliderValue)
    {
        _mainAudioMixer.SetFloat("UI", Mathf.Log10(sliderValue) * 20);
        UIVolumeLevel = sliderValue;
        SaveDataController.SaveVolumeValue("UI", sliderValue);
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }


    private IEnumerator CanChangeDelay()
    {
        yield return new WaitForEndOfFrame();
        _isCanChangeSprite = true;
    }
}
