using UnityEngine;
using UnityEngine.UI;

public class MiniMenuController : MonoBehaviour
{
    [SerializeField] private Transform _menu;
    [SerializeField] private Transform _settingsPanel;

    [SerializeField] private Button _pauseButton;

    [SerializeField] private CanvasGroup _background;

    private float _openOrCloseDelay;

    private bool _isOpened;

    public static bool PauseEnabled;


    void Start()
    {
        PauseEnabled = false;
        _settingsPanel.gameObject.SetActive(true);
        _menu.localScale = Vector2.zero;
        _settingsPanel.localScale = Vector2.zero;
    }


    void Update()
    {
        if (_openOrCloseDelay <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !PauseEnabled)
            {
                OpenPauseMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && PauseEnabled)
            {
                ClosePauseMenu();
                CloseSettingsOnUnpause();
            }
        }
        _openOrCloseDelay -= Time.unscaledDeltaTime;
    }


    private void ToggleTime()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }


    public void ClosePauseMenuOnClick()
    {
        SoundOnButtonClick();
        ClosePauseMenu();
    }



    public void RestartOnClick()
    {
        StartCoroutine(LevelLoader.Instance.AnimationDelayToRestart());

        SoundOnButtonClick();
    }


    public void OpenSettingsOnClick()
    {
        if (_isOpened)
        {
            _settingsPanel.position = new Vector2(Screen.width / 2, Screen.height / 2); // Начать анимацию с центра экрана
            _settingsPanel.LeanScale(Vector2.one, 0.15f).setIgnoreTimeScale(true);

            SoundOnButtonClick();
        }
    }


    public void ExitToMenuOnClick()
    {
        if (_isOpened)
        {
            StartCoroutine(LevelLoader.Instance.AnimationDelayToMenu());
            PauseEnabled = false;
            SoundOnButtonClick();
        }
    }


    public void CloseSettingsOnClick()
    {
        _settingsPanel.LeanScale(Vector2.zero, 0.15f).setIgnoreTimeScale(true);
        _settingsPanel.LeanMoveLocalY(-1000, 0.15f).setIgnoreTimeScale(true);

        SoundOnButtonClick();
    }


    private void CloseSettingsOnUnpause()
    {
        _settingsPanel.LeanMove(new Vector2(Screen.width, Screen.height), 0.15f).setIgnoreTimeScale(true);
        _settingsPanel.LeanScale(Vector2.zero, 0.135f).setIgnoreTimeScale(true);
    }


    public void OpenPauseMenuOnClick()
    {
        SoundOnButtonClick();
        OpenPauseMenu();
    }


    private void OpenPauseMenu()
    {
        _isOpened = true;
        PauseEnabled = true;

        _openOrCloseDelay = 0.3f;

        _menu.localScale = Vector2.zero;
        _background.alpha = 0;
        _pauseButton.interactable = false;

        _menu.position = new Vector2(Screen.width, Screen.height); // Начать анимацию с правого верхнего угла экрана
        _menu.LeanMoveLocalY(0.5f, 0.15f).setIgnoreTimeScale(true);
        _menu.LeanMoveLocalX(0.5f, 0.15f).setIgnoreTimeScale(true);
        _menu.LeanScale(Vector2.one, 0.135f).setDelay(0.015f).setIgnoreTimeScale(true);

        _background.LeanAlpha(0.5f, 0.15f).setDelay(0.15f).setIgnoreTimeScale(true);
        ToggleTime();


        AudioController.Instance.PauseEffectsSound();
    }


    private void ClosePauseMenu()
    {
        _isOpened = false;
        PauseEnabled = false;

        _openOrCloseDelay = 0.2f;

        LeanTween.cancel(_background.gameObject);
        _background.LeanAlpha(0, 0).setIgnoreTimeScale(true);

        _menu.LeanMove(new Vector2(Screen.width, Screen.height), 0.15f).setIgnoreTimeScale(true); // Начать анимацию правого верхнего угла экрана
        _menu.LeanScale(Vector2.zero, 0.135f).setIgnoreTimeScale(true);

        PauseEnabled = false;
        _pauseButton.interactable = true;

        ToggleTime();

        AudioController.Instance.PlayEffectsSound();
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }
}
