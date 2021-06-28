using UnityEngine;

public class SelectModeController : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _selectModePanel;
    [SerializeField] private GameObject _defaultModeLevelsPanel;


    public void OpenAttackModeOnClick()
    {
        SoundOnButtonClick();
        _defaultModeLevelsPanel.SetActive(true);
        _selectModePanel.SetActive(false);
    }


    public void OpenAttackModeUpgradesOnClick()
    {
        SoundOnButtonClick();
        StartCoroutine(LevelLoader.Instance.AnimationDelayPlayerUpgrades());
    }

   
    public void BackToMenuOnClick()
    {
        SoundOnButtonClick();
        _menuPanel.SetActive(true);
        _selectModePanel.SetActive(false);
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }
}
