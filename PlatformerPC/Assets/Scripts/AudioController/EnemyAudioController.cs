using System.Collections;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _enemyStepsSource;

    [SerializeField] private AudioClip[] _step;

    public bool IsRunning;

    private float _enemyStepTime;


    void Start()
    {
        _enemyStepsSource.volume = 0;
        StartCoroutine(TurnEnemyStepsSoundOnDelay());
    }


    private IEnumerator TurnEnemyStepsSoundOnDelay()
    {
        yield return new WaitForSeconds(CutsceneController.CutSceneDuration);
        TurnOnStepAudioSource();
    }


    void Update()
    {
        _enemyStepTime -= Time.deltaTime;
        PlayEnemyStepSound();
    }


    public void TurnOnStepAudioSource()
    {
        if (_enemyStepsSource.volume != 1)
        {
            _enemyStepsSource.volume = 1;
        }
    }


    public void PlayEnemyStepSound()
    {
        if (_enemyStepTime <= 0 && IsRunning)
        {
            _enemyStepsSource.pitch = Random.Range(0.97f, 1.03f);
            _enemyStepsSource.PlayOneShot(_step[Random.Range(0, 2)]);
            _enemyStepTime = 0.35f;
        }
        else if (!IsRunning)
        {
            _enemyStepTime = 0.05f;
        }
    }
}
