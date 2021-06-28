using UnityEngine;
using UnityEngine.UI;

public class MainSettingsController : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _audioPanel;
    [SerializeField] private GameObject _dataPanel;
    [SerializeField] private GameObject _blockClickPanel;
    [SerializeField] private GameObject _blockClickResetPanel;

    [SerializeField] private GameObject _sureResetDataPanel;

    [SerializeField] private Button _audioPanelButton;
    [SerializeField] private Button _dataPanelButton;

    private int _resetID;


    void Start()
    {
        DefaultValues();
    }


    private void DefaultValues()
    {
        _settingsPanel.LeanScale(Vector2.zero, 0f);
        _settingsPanel.LeanMoveLocalY(-1000, 0f);

        _blockClickPanel.SetActive(false);
        _blockClickResetPanel.SetActive(false);
        _sureResetDataPanel.SetActive(false);
    }


    public void DefaultState()
    {
        _audioPanel.LeanMoveLocalX(0, 0.02f).setDelay(0.18f);
        _dataPanel.LeanMoveLocalX(1000, 0.02f).setDelay(0.18f);

        _blockClickPanel.SetActive(false);
    }


    public void ResetAttackModeData()
    {
        SaveDataController.ResetAttackModeData();
    }


    public void ResetDefenseModeData()
    {
        // nothing yet
    }


    public void OpenSureResetPanelOnClick(int ID)
    {
        _blockClickResetPanel.SetActive(true);
        _sureResetDataPanel.SetActive(true);
        _resetID = ID;
        SoundOnButtonClick();
    }


    public void CancelResetOnClick()
    {
        _resetID = -1;
        _blockClickResetPanel.SetActive(false);
        _sureResetDataPanel.SetActive(false);
        SoundOnButtonClick();
    }


    public void ResetOnClick()
    {
        if (_resetID == 0)
        {
            ResetAttackModeData();
        }
        else if (_resetID == 1)
        {
            ResetDefenseModeData();
        }
        _blockClickResetPanel.SetActive(false);
        _sureResetDataPanel.SetActive(false);
        SoundOnButtonClick();
    }


    public void OpenSettingsOnClick()
    {
        SoundOnButtonClick();
        _dataPanelButton.interactable = true;
        _audioPanelButton.interactable = false;

        _blockClickPanel.SetActive(true);
        _settingsPanel.LeanScale(Vector2.one, 0.2f);
        _settingsPanel.LeanMoveLocalY(0, 0.2f);
        _audioPanel.SetActive(true);
    }


    public void CloseSettingsOnClick()
    {
        SoundOnButtonClick();
        _settingsPanel.LeanScale(Vector2.zero, 0.2f);
        _settingsPanel.LeanMoveLocalY(-1000, 0.2f);

        DefaultState();
    }


    public void OpenDataPanelOnClick()
    {
        SoundOnButtonClick();
        _dataPanel.LeanMoveLocalX(800, 0);
        _audioPanel.LeanMoveLocalX(-1000, 0.2f);
        _dataPanel.SetActive(true);
        _dataPanel.LeanMoveLocalX(0, 0.2f);
    }


    public void OpenAudioPanelOnClick()
    {
        SoundOnButtonClick();
        _audioPanel.LeanMoveLocalX(0, 0.2f);
        _dataPanel.LeanMoveLocalX(1000, 0.2f);
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }
}
