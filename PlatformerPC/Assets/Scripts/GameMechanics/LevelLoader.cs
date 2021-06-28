using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _crossFade;

    private static Animator _transition;

    public static LevelLoader Instance { get; private set; }

    private const float CROSSFADE_ANIM_DELAY = 1f;


    void Awake()
    {
        Instance = this;
        _crossFade.SetActive(true);
        Time.timeScale = 1f;
        _transition = GetComponentInChildren<Animator>();
    }


    public IEnumerator AnimationDelayBeetwenLevels(int levelNumber)
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(CROSSFADE_ANIM_DELAY);
        SceneManager.LoadScene($"DefaultModeLevel_{levelNumber}");
    }


    public IEnumerator AnimationDelayPlayerUpgrades()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(CROSSFADE_ANIM_DELAY);
        SceneManager.LoadScene("PlayerUpgrades");
    }

    
    public IEnumerator AnimationDelayToMenu()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(CROSSFADE_ANIM_DELAY);
        SceneManager.LoadScene("Menu");
    }


    public IEnumerator AnimationDelayToGamePassed()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(CROSSFADE_ANIM_DELAY);
        SceneManager.LoadScene("GamePassed");
    }


    public IEnumerator AnimationDelayToRestart()
    {
        _transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(CROSSFADE_ANIM_DELAY);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}