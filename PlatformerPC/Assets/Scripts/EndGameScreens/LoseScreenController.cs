using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoseScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _gameInterface;
    [SerializeField] private GameObject _pauseInterface;

    [SerializeField] private TextMeshProUGUI _enemiesKilledNumber;
    [SerializeField] private TextMeshProUGUI _damageDealtNumber;
    [SerializeField] private TextMeshProUGUI _skillPointsEarnedNumber;


    private void UpdateValuesOnScreen()
    {
        _enemiesKilledNumber.text = TotalGameStats.EnemiesKilled.ToString();
        _damageDealtNumber.text = Mathf.Round(TotalGameStats.DamageDealt).ToString();
        _skillPointsEarnedNumber.text = TotalGameStats.SkillPointsFromEnemies.ToString();
    }


    public void PlayerDied()
    {
        DefaultLevelController.IsGameEnd = true;

        AudioController.Instance.PlayLoseScreenSound();
        BackgroundMusicController.Instance.PauseMusic();

        _loseScreen.SetActive(true);
        _gameInterface.SetActive(false);
        _pauseInterface.SetActive(false);

        UpdateValuesOnScreen();

        TotalGameStats.TransferToPlayerUpgrades();
    }


    public void RestartOnClick()
    {
        SoundOnButtonClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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