using UnityEngine;
using UnityEngine.UI;

public class SelectDefaultLevelController : MonoBehaviour
{
    [SerializeField] private GameObject _selectDefaultLevelPanel;
    [SerializeField] private GameObject _selectModePanel;
    [SerializeField] private GameObject _menuPanel;

    [SerializeField] private Button[] _levelButtons;

    public static SelectDefaultLevelController Instance { get; private set; }


    void Awake()
    {
        Instance = this;
        UpdateUnlockedLevels();
    }


    public void UpdateUnlockedLevels()
    {
        for (int i = 0; i < _levelButtons.Length; ++i)
        {
            if (i <= SaveDataController.CurrentLevelSaved)
            {
                _levelButtons[i].interactable = true;
            }
            else
            {
                _levelButtons[i].interactable = false;

            }
        }
    }


    public void BackToMenuOnClick()
    {
        SoundOnButtonClick();

        _selectDefaultLevelPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }


    public void BackToSelectModeOnClick()
    {
        SoundOnButtonClick();

        _selectDefaultLevelPanel.SetActive(false);
        _selectModePanel.SetActive(true);
    }


    public void StartLevelOnClick(int levelNumber)
    {
        SoundOnButtonClick();
        StartCoroutine(LevelLoader.Instance.AnimationDelayBeetwenLevels(levelNumber));
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }
}
