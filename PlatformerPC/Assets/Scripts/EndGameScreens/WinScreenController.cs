using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _gameInterface;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _skillPointsInfoPanel;
    [SerializeField] private GameObject _pauseInterface;

    [SerializeField] private TextMeshProUGUI _enemiesKilledNumber;
    [SerializeField] private TextMeshProUGUI _damageDealtNumber;
    [SerializeField] private TextMeshProUGUI _skillPointsEarnedNumber;

    [SerializeField] private TextMeshProUGUI _skillPointsEarnedFromLevel;
    [SerializeField] private TextMeshProUGUI _skillPointsEarnedFromEnemies;


    void Start()
    {
        _skillPointsInfoPanel.transform.localScale = Vector3.zero;
    }


    private void UpdateValuesOnScreen()
    {
        _enemiesKilledNumber.text = TotalGameStats.EnemiesKilled.ToString();
        _damageDealtNumber.text = Mathf.Round(TotalGameStats.DamageDealt).ToString();
        _skillPointsEarnedNumber.text = (TotalGameStats.SkillPointsFromCompletedLevel + TotalGameStats.SkillPointsFromEnemies).ToString();

        _skillPointsEarnedFromEnemies.text = "From killed enemies: " + TotalGameStats.SkillPointsFromEnemies.ToString();
        _skillPointsEarnedFromLevel.text = "From completed level: " + TotalGameStats.SkillPointsFromCompletedLevel.ToString();
    }


    public void OpenSkillPointsInfoOnClick()
    {
        _skillPointsInfoPanel.SetActive(!_skillPointsInfoPanel.activeInHierarchy);

        if (_skillPointsInfoPanel.activeInHierarchy)
        {
            _skillPointsInfoPanel.LeanScale(Vector3.one, 0.1f);
        }
        else
        {
            _skillPointsInfoPanel.transform.localScale = Vector3.zero;
        }
    }


    public void PlayerFinished()
    {
        DefaultLevelController.IsGameEnd = true;

        AudioController.Instance.PlayWinScreenSound();
        BackgroundMusicController.Instance.PauseMusic();

        _winScreen.SetActive(true);
        _gameInterface.SetActive(false);
        _pauseInterface.SetActive(false);

        if (SaveDataController.CurrentLevelSaved < DefaultLevelController.CurrentLevel)
        {
            TotalGameStats.EarnSkillPointsFromCompletedLevel(DefaultLevelController.SkillPointsForWin);
        }

        UpdateValuesOnScreen();

        TotalGameStats.TransferToPlayerUpgrades();
    }


    public void NextLevelOnClick()
    {
        SoundOnButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UnPauseOnClick();
    }


    public void UpgradesOnClick()
    {
        SoundOnButtonClick();
        SceneManager.LoadScene("PlayerUpgrades");
        UnPauseOnClick();
    }


    public void ExitToMenuOnClick()
    {
        SoundOnButtonClick();
        SceneManager.LoadScene("Menu");
        UnPauseOnClick();
    }


    private void UnPauseOnClick()
    {
        BackgroundMusicController.Instance.UnPauseMusic();
    }


    private void SoundOnButtonClick()
    {
        AudioController.Instance.PlayButtonSound();
    }
}