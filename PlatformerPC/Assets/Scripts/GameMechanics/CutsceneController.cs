using System.Collections;
using UnityEngine;
using TMPro;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private GameObject _cutsceneCamera;
    [SerializeField] private GameObject _gameInterface;
    [SerializeField] private GameObject _pauseInterface;
    [SerializeField] public GameObject _skipButton;

    [Header("Level info values")]
    [SerializeField] private int _numberOfEnemies;
    [SerializeField] private float _averageDamage;
    [SerializeField] private float _averageAttackSpeed;

    [Header("Level info text")]
    [SerializeField] private TextMeshProUGUI _numberOfEnemiesText;
    [SerializeField] private TextMeshProUGUI _averageDamageText;
    [SerializeField] private TextMeshProUGUI _averageAttackSpeedText;

    [Header("")]
    [SerializeField] private TextMeshProUGUI _goText;

    [Header("")]
    [SerializeField] private Animator _cutsceneAnimator;

    [Header("")]
    [SerializeField] private float _cutSceneDuration;

    private EnemyAudioController[] _enemyAudioController;

    public static bool IsCutSceneEnabled;
    public static float CutSceneDuration;


    void Awake()
    {
        _enemyAudioController = FindObjectsOfType<EnemyAudioController>();

        CutSceneDuration = _cutSceneDuration;
    }


    void Start()
    {
        TurnOffUI();
        UpdateLevelInfoOnScreen();

        _cutsceneAnimator.SetTrigger(DefaultLevelController.CurrentLevel.ToString());
        _cutsceneCamera.SetActive(true);
        IsCutSceneEnabled = true;
        _skipButton.SetActive(true);
        StartCoroutine(CutSceneDelay());
    }


    private void UpdateLevelInfoOnScreen()
    {
        _numberOfEnemiesText.text = "Number of enemies: " + _numberOfEnemies.ToString();
        _averageDamageText.text = "Average damage: " + _averageDamage.ToString();
        _averageAttackSpeedText.text = "Average attack speed: " + _averageAttackSpeed.ToString();
    }


    private void TurnOffText()
    {
        _numberOfEnemiesText.gameObject.SetActive(false);
        _averageDamageText.gameObject.SetActive(false);
        _averageAttackSpeedText.gameObject.SetActive(false);
    }


    private void TurnOffUI()
    {
        _gameInterface.SetActive(false);
        _pauseInterface.SetActive(false);
        _goText.alpha = 0;
    }


    private void TurnOnUI()
    {
        _gameInterface.SetActive(true);
        _pauseInterface.SetActive(true);
        _skipButton.SetActive(false);
        _goText.alpha = 1;

        LeanTween.value(_goText.gameObject, a => _goText.alpha = a, 1f, 0f, 1.5f).setDelay(2f); // smoothly fade text. (start fade, end fade, time)
    }


    private void CutSceneEnd()
    {
        IsCutSceneEnabled = false;
        _cutsceneCamera.SetActive(false);
    }


    public void SkipCutsceneOnClick()
    {
        CutSceneEnd();
        TurnOnUI();
        TurnOffText();

        foreach (var item in _enemyAudioController)
        {
            item.TurnOnStepAudioSource();
        }
    }


    private IEnumerator CutSceneDelay()
    {
        yield return new WaitForSeconds(_cutSceneDuration);
        if (IsCutSceneEnabled)
        {
            CutSceneEnd();
            TurnOffText();
            TurnOnUI();
        }
    }
}
